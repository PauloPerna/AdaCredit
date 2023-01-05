using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using Bogus;

namespace AdaCredit.Domain.Entities
{
    public class User
    {
        public long cpf { get; init; }
        public string name { get; set; }
        public string pass { get; private set; }
        private string salt { get; set; }
        public bool active { get; private set; }
        public DateTime lastLogin { get; set; }
        public User(long cpf, string name, string pass, bool active, DateTime lastLogin)
        {
            this.cpf = cpf;
            this.name = name;
            this.active = active;
            this.pass = pass;
            this.active = active;
            this.lastLogin = lastLogin;
        }
        public User(long cpf, string name, string cleanPass)
        {
            this.cpf = cpf;
            this.name = name;
            this.active = true;
            this.salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            this.pass = BCrypt.Net.BCrypt.HashPassword(cleanPass, this.salt);
        }
        public bool Login(string pass)
        {
            return BCrypt.Net.BCrypt.Verify(pass, this.pass);
        }
        public bool RedefinePass(string cleanPass)
        {
            this.salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            this.pass = BCrypt.Net.BCrypt.HashPassword(cleanPass, this.salt);
            return true;
        }
        public bool Deactivate()
        {
            if(!this.active)
            {
                return false;
            }
            this.active = false;
            return true;
        }
        public override string ToString()
        {
            return $"{this.cpf},{this.name},{this.pass},{this.active},{this.lastLogin}";
        }
    }
}