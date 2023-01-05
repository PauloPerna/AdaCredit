using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;

namespace AdaCredit.Domain.Entities
{
    public class Account
    {
        public int accountNumber { get; init; }
        public int agencyNumber { get; init; }
        public double balance { get; set; }
        private Client client {get; set; }
        private static Random rnd = new Random();
        public Account(Client client, int accountNumber){
            this.accountNumber = accountNumber;
            this.agencyNumber = 1;
            this.balance = 0;
            this.client = client;
        }
    }
}