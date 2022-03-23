using AutoMapper;
using VY.RatesApi.Dtos;
using VY.RatesApi.Infrastructure.Contracts.Entities;

namespace VY.RatesApi.Business.Implementation.Mapping_Profiles
{
    public class AccounteeProfile : Profile
    {
        public AccounteeProfile()
        {
            CreateMap<Accountee, AccounteeDto>()
                .ForMember(x => x.DNI, opt => opt.MapFrom(y => y.Dni))
                .ForMember(x => x.Name, opt => opt.MapFrom(y => y.Name))
                .ForMember(x => x.Surname, opt => opt.MapFrom(y => y.Surname))
                .ForMember(x => x.Age, opt => opt.MapFrom(y => y.Age))
                .ReverseMap();
            //Will set the accounts in a different profile because conversion is needed!
        }
    }
}
