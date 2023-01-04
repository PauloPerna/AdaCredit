using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdaCredit.Domain.Entities
{
    public class User
    {
        private int cpf { get; set; }
        private string name { get; set; }
        private string pass { get; set; }
        public User(int cpf, string name)
        {
            this.cpf = cpf;
            this.name = name;
            // TODO:
            // gerar senha
            this.pass = "senha";
        }
    }
}