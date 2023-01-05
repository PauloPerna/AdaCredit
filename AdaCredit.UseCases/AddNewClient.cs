using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Domain.Entities;
using AdaCredit.Data;

namespace AdaCredit.UseCases
{
    public static class AddNewClient
    {
        public static Tuple<Client,bool> Execute(long cpf, string name)
        {
            Client client = new Client(cpf, name);
            bool success = ClientsRepository.Add(client);
            return Tuple.Create<Client,bool>(client,success);
        }
    }
}

































