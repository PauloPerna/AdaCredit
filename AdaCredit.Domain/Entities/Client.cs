using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetterConsoleTables;

namespace AdaCredit.Domain.Entities
{
    public class Client
    {
        public long cpf { get; init; }
        // Although the cpf is just init, the name can change over the years (social name cases for example)
        public string name { get; set; }
        public List<Account> accounts { get; set; }
        public bool active { get; private set; }
        // Criar um DTO onde tudo é público
        // então salvamos e lemos o DTO.
        // clientDTO: tem todos os atributos públicos, n tem metodos

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
        public override string ToString()
        {
            string message = $"Cliente: {this.name}\n";
            message = message + $"CPF: {this.cpf}\n";
            if(this.active)
            {
                message = message + $"Situação: ativo\n";
            } else {
                message = message + $"Situação: inativo\n\n";
            }
            message = message + "---- Contas Vinculadas ao Cliente ----\n";
            Table tableAccounts = new Table("Conta", "Agência", "Banco","Saldo");
            foreach(Account account in this.accounts)
            {
                tableAccounts.AddRow(account.accountNumber,account.agencyNumber,account.bankCode,account.balance);
            }
            tableAccounts.Config = TableConfiguration.Unicode();
            message = message + tableAccounts.ToString();
            return message;
        }
    }
}