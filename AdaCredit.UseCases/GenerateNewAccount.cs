using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Domain.Entities;
using AdaCredit.Data;

namespace AdaCredit.UseCases
{
    public static class GenerateNewAccount
    {
        public static Tuple<bool,Account> Execute(Client client)
        {
            Account account = AccountsRepository.GenerateNewUnique(client);
            client.AddAccount(account);
            ClientsRepository.Update(client);
            return Tuple.Create<bool,Account>(true, account);
        }
    }
}