using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using AdaCredit.Domain.Entities;
using System.Text.RegularExpressions;
using BetterConsoleTables;

namespace AdaCredit.Data
{
    public static class TransactionsRepository
    {
        private static List<Transaction> _trasactions = new List<Transaction>();
        private static List<Transaction> _failedTransactions = new List<Transaction>();
        private static List<Transaction> _completedTransactions = new List<Transaction>();
        private static string fileFullName;
        private static bool fileReaded;
        public static void Load(string fileName)
        {
            string path = Environment.GetFolderPath (Environment.SpecialFolder.Desktop);
            string folder = "Transactions";
            string folder2 = "Pending";
            string fileFullName = Path.Combine(path,folder,folder2,fileName);
            fileFullName = fileFullName;
            if(!File.Exists(fileFullName))
            {
                fileReaded = false;
            }
            var config = new CsvConfiguration(CultureInfo.InvariantCulture) 
            {
                HasHeaderRecord = false 
            };
            using (var reader = new StreamReader(fileFullName))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<TransactionMap>();
                _trasactions = csv.GetRecords<Transaction>().ToList();
            }
            fileReaded = true;
            string dateInFileName = Regex.Match(fileName,"[0-9]{8}.csv$").ToString(); 
            int year, month, day;
            DateTime dateFile = DateTime.Today;
            if(int.TryParse(dateInFileName.Substring(0,4), out year) &&
               int.TryParse(dateInFileName.Substring(4,2), out month) &&
               int.TryParse(dateInFileName.Substring(6,2), out day))
            {
                dateFile = new DateTime(year,month,day);
            }
            foreach(Transaction t in _trasactions)
            {
                t.date = dateFile;
            }
        }
        public static bool WriteTransactionsToFile()
        {
            // Failed Transactions
            string failedFileName = Regex.Replace(fileFullName,"\\.csv$","-failed.csv");
            using (var writer = new StreamWriter(failedFileName))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(_failedTransactions);
            }
            // Completed Transactions
            string completedFileName = Regex.Replace(fileFullName,"\\.csv$","-completed.csv");
            using (var writer = new StreamWriter(completedFileName))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(_completedTransactions);
            }
            return true;
        }
        public static List<Transaction> GetAllTransactions()
        {
            return _trasactions;
        }
        public static void AddFailedTransaction(Transaction transaction, string errorDescription)
        {
            transaction.errorDescription = errorDescription;
            _failedTransactions.Add(transaction);
        }
        public static void AddCompletedTransaction(Transaction transaction)
        {
            _completedTransactions.Add(transaction);
        }
        public static Table ShowErrorTransactions()
        {
            Table errorTransactions = new Table("Data","Conta de Origem","Ag??ncia de Origem","Banco de Origem","Conta de Destino","Ag??ncia de Destino","Banco de Destino","D??bito/Cr??dito (0/1)","Valor","Tipo","Descri????o do Erro");
            foreach(Transaction transaction in _failedTransactions)
            {
                errorTransactions.AddRow(transaction.date,transaction.originAccountCode,transaction.originAgencyCode,transaction.originBankCode,transaction.destinationAccountCode,transaction.destinationAgencyCode,transaction.destinationBankCode,transaction.Credit,transaction.value.ToString("C"),transaction.typeTransaction,transaction.errorDescription);
            }
            errorTransactions.Config = TableConfiguration.Unicode();
            return errorTransactions;
        }
    }
}