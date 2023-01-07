using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdaCredit.UseCases
{
    public static class CreateFiles
    {
        public static void Execute()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string pathSource = System.IO.Directory.GetCurrentDirectory();
            // Clients
            string clientFileSource = Path.Combine(pathSource,"TestFiles","clients.csv");
            string clientFileTarget = Path.Combine(desktopPath,"clients.csv");
            System.IO.File.Copy(clientFileSource, clientFileTarget, true);

            // Accounts
            string accountFileSource = Path.Combine(pathSource,"TestFiles","accounts.csv");
            string accountFileTarget = Path.Combine(desktopPath,"accounts.csv");
            System.IO.File.Copy(accountFileSource, accountFileTarget, true);

            // Transactions
            string transactionsFileSource = Path.Combine(pathSource,"TestFiles","caixa-20230101.csv");
            string targetPath = Path.Combine(desktopPath,"Transactions","Pending");
            System.IO.Directory.CreateDirectory(targetPath);
            string transactionsFileTarget = Path.Combine(targetPath,"caixa-20230101.csv");
            System.IO.File.Copy(transactionsFileSource, transactionsFileTarget, true);
        }
    }
}