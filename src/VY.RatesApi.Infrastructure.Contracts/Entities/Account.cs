using System;
using System.Collections.Generic;

#nullable disable

namespace VY.RatesApi.Infrastructure.Contracts.Entities
{
    public partial class Account
    {
        public Account()
        {
            AccounteeAccounts = new HashSet<AccounteeAccount>();
        }

        public string AccountId { get; set; }
        public double Amount { get; set; }

        public virtual ICollection<AccounteeAccount> AccounteeAccounts { get; set; }
    }
}
