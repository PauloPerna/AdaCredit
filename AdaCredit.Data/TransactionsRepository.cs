using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using AdaCredit.Domain.Entities;
using System.Text.RegularExpressions;

namespace AdaCredit.Data
{
    public class TransactionsRepository
    {
        private List<Transaction> _trasactions = new List<Transaction>();
        private List<Transaction> _failedTransactions = new List<Transaction>();
        private List<Transaction> _completedTransactions = new List<Transaction>();
        private string fileFullName;
        private bool fileReaded;
        public TransactionsRepository(string fileName)
        {
            string path = Environment.GetFolderPath (Environment.SpecialFolder.Desktop);
            string folder = "Transactions";
            string fileFullName = Path.Combine(path,folder,fileName);
            this.fileFullName = fileFullName;
            if(!File.Exists(fileFullName))
            {
                this.fileReaded = false;
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
            this.fileReaded = true;
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
        public bool WriteTransactionsToFile()
        {
            // Failed Transactions
            string failedFileName = Regex.Replace(this.fileFullName,"\\.csv$","-failed.csv");
            using (var writer = new StreamWriter(failedFileName))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(_failedTransactions);
            }
            // Completed Transactions
            string completedFileName = Regex.Replace(this.fileFullName,"\\.csv$","-completed.csv");
            using (var writer = new StreamWriter(completedFileName))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(_completedTransactions);
            }
            return true;
        }
        public List<Transaction> GetAllTransactions()
        {
            return _trasactions;
        }
        public void AddFailedTransaction(Transaction transaction)
        {
            _failedTransactions.Add(transaction);
        }
        public void AddCompletedTransaction(Transaction transaction)
        {
            _completedTransactions.Add(transaction);
        }
    }
}