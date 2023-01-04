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
            // testar name
            return false;
        }
        public static User Get(int cpf)
        {
            //TODO: retornar usuário por cpf
            return null;
        }
        public static User Get(string name)
        {
            //TODO: retornar usuário por name
            return null;
        }
        public static bool EditPassword(User user)
        {
            //TODO: editar senha do usuário
            return false;
        }
    }
}