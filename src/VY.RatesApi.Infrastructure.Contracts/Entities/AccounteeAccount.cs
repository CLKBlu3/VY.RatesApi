using System;
using System.Collections.Generic;

#nullable disable

namespace VY.RatesApi.Infrastructure.Contracts.Entities
{
    public partial class AccounteeAccount
    {
        public string AccounteeId { get; set; }
        public string AccountId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Accountee Accountee { get; set; }
    }
}
