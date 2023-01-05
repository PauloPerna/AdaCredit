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
            string path = Environment.GetFolderPath (Environment.SpecialFolder.Desktop);
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
            string path = Environment.GetFolderPath (Environment.SpecialFolder.Desktop);
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
            return true;
        }
        public static Account GetNewUnique(Client client)
        {
            int accountNumber;
            do{
                accountNumber = rnd.Next(1,999999);
            }while(_accounts.Any(c => c.accountNumber == accountNumber));

            return new Account(client, accountNumber);
        }
    }
}