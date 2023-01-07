using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using CsvHelper;
using AdaCredit.Domain.Entities;

namespace AdaCredit.Data
{
    public static class AccountsRepository
    {
        private static List<Account> _accounts = new List<Account>();
        private static Random rnd = new Random();
        static AccountsRepository()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = "accounts.csv";
            string fileFullName = Path.Combine(path,fileName);
            if(!File.Exists(fileFullName))
            {
                return;
            }
            using (var reader = new StreamReader(fileFullName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                _accounts = csv.GetRecords<Account>().ToList();
            }
        }
        public static bool WriteAccountsToFile()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string fileName = "accounts.csv";
            string fileFullName = Path.Combine(path,fileName);
            using (var writer = new StreamWriter(fileFullName))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(_accounts);
            }
            return true;
        }
        public static bool Add(Account account)
        {
            if(_accounts.Any(c => c.accountNumber == account.accountNumber))
            {
                return false;
            }
            _accounts.Add(account);
            WriteAccountsToFile();
            return true;
        }
        public static Account GenerateNewUnique(Client client)
        {
            int accountNumber;
            do{
                accountNumber = rnd.Next(1,999999);
            }while(_accounts.Any(c => c.accountNumber == accountNumber));
            Account account = new Account(client, accountNumber);
            Add(account);
            return account;
        }
        public static List<Account> GetAccounts(Client client)
        {
            return _accounts.Where(a => a.client.cpf == client.cpf).ToList();
        }
        public static int GetIndexAccountNumber(int accountNumber)
        {
            return _accounts.FindIndex(a => a.accountNumber == accountNumber);
        }
        public static decimal GetBalanceByAccountCode(int accountNumber)
        {
            var index = GetIndexAccountNumber(accountNumber);
            return _accounts[index].balance;
        }
        public static bool Credit(int accountNumber, decimal value)
        {
            var index = GetIndexAccountNumber(accountNumber);
            return _accounts[index].Credit(value) &&
                    WriteAccountsToFile();
        }
        public static bool Debit(int accountNumber, decimal value)
        {
            var index = GetIndexAccountNumber(accountNumber);
            return _accounts[index].Debit(value) &&
                    WriteAccountsToFile();
        }
        public static string DisplayString()
        {
            string message = "";
            string tString;
            foreach(Account t in _accounts)
            {
                tString = $"Número: {t.accountNumber} Saldo: {t.balance}\n";
                message = message + tString;
            }
            return message;
        }
    }
}