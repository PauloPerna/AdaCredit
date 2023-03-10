using System;

namespace AdaCredit.Domain.Entities
{
    public class Client
    {
        public long cpf { get; init; }
        // Although the cpf is just init, the name can change over the years (social name cases for example)
        public string name { get; set; }
        public List<Account> accounts { get; set; }
        public bool active { get; private set; }
        public Client(long cpf, string name)
        {
            this.cpf = cpf;
            this.name = name;
            this.active = true;
            this.accounts = new List<Account>();
        }
        public Client(long cpf, string name, bool active)
        {
            this.cpf = cpf;
            this.name = name;
            this.active = active;
            this.accounts = new List<Account>();
        }
        public bool AddAccount(Account account)
        {
            this.accounts.Add(account);
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
        public bool Activate()
        {
            if(this.active)
            {
                return false;
            }
            this.active = true;
            return true;
        }
    }
}