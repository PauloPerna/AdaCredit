using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Domain.Entities;
using CsvHelper.Configuration;

namespace AdaCredit.Data
{
    public sealed class TransactionMap : ClassMap<Transaction>
    {
        public TransactionMap()
        {
            Map(m => m.originBankCode).Index(0);
            Map(m => m.originAgencyCode).Index(1);
            Map(m => m.originAccountCode).Index(2);
            Map(m => m.destinationBankCode).Index(3);
            Map(m => m.destinationAgencyCode).Index(4);
            Map(m => m.destinationAccountCode).Index(5);
            Map(m => m.typeTransaction).Index(6);
            Map(m => m.Credit).Index(7);
            Map(m => m.value).Index(8);
        }
    }
}