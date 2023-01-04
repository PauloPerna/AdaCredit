using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdaCredit.Domain.Entities
{
    public class Transaction
    {
        private int originBankCode { get; init; }
        private int originAgencyCode { get; init; }
        private int originAccountCode { get; init; }
        private int destinationBankCode { get; init; }
        private int destinationAgencyCode { get; init; }
        private int destinationAccountCode { get; init; }
    }
}