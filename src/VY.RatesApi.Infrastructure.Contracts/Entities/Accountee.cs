using System;
using System.Collections.Generic;

#nullable disable

namespace VY.RatesApi.Infrastructure.Contracts.Entities
{
    public partial class Accountee
    {
        public Accountee()
        {
            AccounteeAccounts = new HashSet<AccounteeAccount>();
        }

        public string Dni { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public virtual ICollection<AccounteeAccount> AccounteeAccounts { get; set; }
    }
}
