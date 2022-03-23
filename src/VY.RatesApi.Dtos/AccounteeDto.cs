using System.Collections.Generic;

namespace VY.RatesApi.Dtos
{
    public class AccounteeDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DNI { get; set; }
        public int Age { get; set; }
        public List<AccountDto> Accounts { get; set; }
    }
}
