using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdaCredit.Domain.Entities
{
    public class Transaction
    {
        public int originBankCode { get; init; }
        public int originAgencyCode { get; init; }
        public int originAccountCode { get; init; }
        public int destinationBankCode { get; init; }
        public int destinationAgencyCode { get; init; }
        public int destinationAccountCode { get; init; }
        public string typeTransaction { get; init; }
        public int Credit { get; init; }
        public double value { get; init; }
        public DateTime date { get; set; }
        public string errorDescription { get; set; }
    }
}