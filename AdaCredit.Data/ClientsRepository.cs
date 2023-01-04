using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Domain.Entities;

namespace AdaCredit.Data
{
    public static class ClientsRepository
    {
        private static List<Client> _client { get; set; }
        
        public static bool Add(Client client)
        {
            //TODO: lógica de adicionar cliente
            // testar se já existe
            // testar CPF
            return false;
        }

        public static Client Get(int cpf)
        {
            //TODO: retornar um cliente da lista
            return null;
        }

        public static Client Get(string name)
        {
            //TODO: retornar um cliente da lista
            return null;
        }
        public static bool Edit(Client client)
        {
            //TODO: modificar um cliente da lista
            return false;
        }
        public static bool Deactivate(Client client)
        {
            //TODO: desativar um cliente da lista
            return false;
        }
    }
}