using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VY.RatesApi.Business.Contracts.Services;
using VY.RatesApi.Dtos;
using VY.RatesApi.Infrastructure.Contracts.Entities;
using VY.RatesApi.Infrastructure.Contracts.Repostiories;
using VY.RatesApi.XCutting.Domain.OperationResult;

namespace VY.RatesApi.Business.Implementation.Services
{
    public class AccountService : IAccountService
    {
        private readonly IRatesService _ratesService;
        private readonly ILogger<AccountService> _logger;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AccountService(IRatesService ratesService,
                              ILogger<AccountService> logger,
                              IRepository repository,
                              IMapper mapper)
        {
            _ratesService = ratesService;
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OperationResult> Create(AccounteeDto accounteeDto)
        {
            OperationResult result = new OperationResult();
            _logger.LogInformation("Mapping accountees");
            Accountee accounteeEntity = _mapper.Map<Accountee>(accounteeDto);
            _logger.LogInformation("Converting currencies to EUR");
            var accountsInEur = await _ratesService.ConvertToEurAsync(accounteeDto.Accounts);
            if (accountsInEur.HasErrors())
            {
                _logger.LogError("Currencies couldn't be converted.");
                result.AddError(accountsInEur.Errors);
                return result;
            }
            _logger.LogInformation("Mapping accounts");
            List<Account> accountEntities = _mapper.Map<List<Account>>(accountsInEur.Result);
            List<AccounteeAccount> accounteeAccounts = GenerateRelations(accounteeEntity, accountEntities);
            try
            {
                _logger.LogInformation("Persisting data in database");
                await Persist(accounteeEntity, accountEntities, accounteeAccounts);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error persisting data in database.");
                result.AddException(ex);
            }

            return result;
        }

        private async Task Persist(Accountee accounteeEntity, List<Account> accountEntities, List<AccounteeAccount> accounteeAccounts)
        {
            await _repository.AddAsync(new List<Accountee>() { accounteeEntity });
            await _repository.AddAsync(accounteeAccounts);
            await _repository.AddAsync(accountEntities);
            await _repository.SaveChangesAsync();
        }

        private static List<AccounteeAccount> GenerateRelations(Accountee accounteeEntity, List<Account> accountEntities)
        {
            var result = new List<AccounteeAccount>();
            foreach (var account in accountEntities)
            {
                AccounteeAccount accAcc = new AccounteeAccount();
                accAcc.Account = account;
                accAcc.AccountId = account.AccountId;
                accAcc.Accountee = accounteeEntity;
                accAcc.AccounteeId = accounteeEntity.Dni;
                result.Add(accAcc);
            }

            return result;
        }
    }
}
