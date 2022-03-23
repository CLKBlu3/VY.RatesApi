using AutoMapper;
using VY.RatesApi.Dtos;
using VY.RatesApi.Infrastructure.Contracts.Entities;

namespace VY.RatesApi.Business.Implementation.Mapping_Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(y => y.AccountId))
                .ForMember(x => x.Amount, opt => opt.MapFrom(y => y.Amount))
                .ReverseMap();
        }
    }
}
