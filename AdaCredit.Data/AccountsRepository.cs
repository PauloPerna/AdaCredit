using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Domain.Entities;

namespace AdaCredit.Data
{
    public static class AccountsRepository
    {
        private static List<Account> _accounts { get; set; }
    }
}