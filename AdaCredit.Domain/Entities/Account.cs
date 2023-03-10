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
        public int bankCode = 777;
        public decimal balance { get; set; }
        public Client client {get; init; }
        private static Random rnd = new Random();
        public Account(Client client, int accountNumber){
            this.accountNumber = accountNumber;
            this.agencyNumber = 1;
            this.balance = 0;
            this.client = client;
        }
        public bool Debit(decimal value)
        {
            if(balance < value)
            {
                return false;
            }
            balance = balance - value;
            return true;
        }
        public bool Credit(decimal value)
        {
            balance = balance + value;
            return true;
        }
    }
}