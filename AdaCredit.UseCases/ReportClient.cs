using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetterConsoleTables;
using AdaCredit.Domain.Entities;

namespace AdaCredit.UseCases
{
    public static class ReportClient
    {
        public static string Execute(Client client)
        {
            string message = $"Cliente: {client.name}\n";
            message = message + $"CPF: {client.cpf}\n";
            if(client.active)
            {
                message = message + $"Situação: ativo\n";
            } else {
                message = message + $"Situação: inativo\n\n";
            }
            message = message + "---- Contas Vinculadas ao Cliente ----\n";
            Table tableAccounts = new Table("Conta", "Agência", "Banco","Saldo");
            foreach(Account account in client.accounts)
            {
                tableAccounts.AddRow(account.accountNumber,account.agencyNumber,account.bankCode,account.balance);
            }
            tableAccounts.Config = TableConfiguration.Unicode();
            message = message + tableAccounts.ToString();
            return message;
        }
    }
}