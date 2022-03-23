using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VY.RatesApi.Business.Contracts.Domain;
using VY.RatesApi.Business.Contracts.Services;
using VY.RatesApi.Dtos;
using VY.RatesApi.Infrastructure.Contracts.Entities;
using VY.RatesApi.XCutting.Contracts.Services;
using VY.RatesApi.XCutting.Domain.OperationResult;

namespace VY.RatesApi.Business.Implementation.Services
{
    public class RatesService : IRatesService
    {
        private readonly IProxy<Rate> _proxy;

        public RatesService(IProxy<Rate> proxy)
        {
            _proxy = proxy;
        }

        public async Task<OperationResult<IEnumerable<AccountDto>>> ConvertToEurAsync(IEnumerable<AccountDto> accounts)
        {
            OperationResult<IEnumerable<AccountDto>> result = new OperationResult<IEnumerable<AccountDto>>();
            IEnumerable<Rate> rates = await _proxy.GetAsync();
            if (!rates.Any())
            {
                result.AddError(10, "Unable to get conversion rates.");
                return result;
            }
            foreach(var account in accounts)
            {
                rates = rates.Select(c => { c.IsVisited = false; return c; }).ToList();
                account.Amount = Convert(account.Amount, account.Currency, "EUR", rates);
            }
            result.SetResult(accounts);
            return result;
        }

        private double Convert(double amount, string from, string to, IEnumerable<Rate> rates)
        {
            if (from == null || to == null)
            {
                throw new ArgumentNullException();
            }
            Rate rate = rates.Where(x => x.From == from && x.To == to).FirstOrDefault();
            //Base
            if (rate == null)
            {
                rate = rates.Where(x => x.To == from && x.From == to).FirstOrDefault();
                if (rate != null) return 1 / Double.Parse(rate.Ratio, System.Globalization.CultureInfo.InvariantCulture) * amount;
                //Recursive
                var aux = rates.Where(x => x.From == to || x.To == to && !x.IsVisited).FirstOrDefault();
                //Throws exception if all nodes have been visited / is not possible to reach the result.
                if (aux == null) throw new NotSupportedException();
                aux.IsVisited = true;
                if (aux.From == to)
                {
                    amount = Convert(amount, from, aux.To, rates);
                    return Convert(amount, aux.To, to, rates);
                }
                else
                {
                    amount = Convert(amount, from, aux.From, rates);
                    return Convert(amount, aux.From, to, rates);
                }
            }
            return Double.Parse(rate.Ratio, System.Globalization.CultureInfo.InvariantCulture) * amount;
        }
    }
}
