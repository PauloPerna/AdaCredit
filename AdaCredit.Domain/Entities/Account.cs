using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdaCredit.Domain.Entities
{
    public class Account
    {
        private int accountNumber { get; init; }
        private int agencyNumber { get; init; }
        private double balance { get; set; }
        private Client client {get; set; }
    }
}