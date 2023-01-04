using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Domain.Entities;

namespace AdaCredit.Data
{
    public static class UsersRepository
    {
        private static List<User> _users { get; set; }
        public static bool Add(User user)
        {
            //TODO: lógica de adicionar user
            // testar se já existe
            // testar CPF
            return false;
        }
    }
}