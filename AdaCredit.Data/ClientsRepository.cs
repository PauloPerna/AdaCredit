using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using BetterConsoleTables;
using CsvHelper;
using AdaCredit.Domain.Entities;

namespace AdaCredit.Data
{
    public static class ClientsRepository
    {
        private static List<Client> _clients = new List<Client>();
        static ClientsRepository()
        {
            string path = Environment.GetFolderPath (Environment.SpecialFolder.Desktop);
            string fileName = "clients.csv";
            string fileFullName = Path.Combine(path,fileName);
            if(!File.Exists(fileFullName))
            {
                return;
            }
            using (var reader = new StreamReader(fileFullName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                _clients = csv.GetRecords<Client>().ToList();
            }
            LinkAccountsRepository();
        }
        public static bool LinkAccountsRepository()
        {
            for(int i = 0; i < _clients.Count; i++)
            {
                List<Account> accounts = AccountsRepository.GetAccounts(_clients[i]);
                _clients[i].accounts = accounts;
            }
            return true;
        }
        public static bool WriteClientsToFile()
        {
            string path = Environment.GetFolderPath (Environment.SpecialFolder.Desktop);
            string fileName = "clients.csv";
            string fileFullName = Path.Combine(path,fileName);
            using (var writer = new StreamWriter(fileFullName))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(_clients);
            }
            return true;
        }
        public static bool Add(Client client)
        {
            if(_clients.Any(c => c.cpf == client.cpf))
            {
                return false;
            }
            _clients.Add(client);
            WriteClientsToFile();
            return true;
        }
        public static bool Update(Client client)
        {
            var index = GetIndex(client);
            if(index == -1)
            {
                return false;
            }
            _clients[index] = client;
            WriteClientsToFile();
            return true;
        }
        public static Client Get(long cpf)
        {
            return _clients.FirstOrDefault(c => c.cpf == cpf, null);;
        }
        public static int GetIndex(Client client)
        {
            return _clients.FindIndex(c => c == client);;
        }
        public static int GetIndex(long cpf)
        {
            return _clients.FindIndex(c => c.cpf == cpf);;
        }
        public static bool Deactivate(long cpf)
        {
            var index = GetIndex(cpf);
            return _clients[index].Deactivate() &&
                    WriteClientsToFile();
        }
        public static bool Activate(long cpf)
        {
            var index = GetIndex(cpf);
            return _clients[index].Activate() &&
                    WriteClientsToFile();
        }
        public static Table ShowActiveClientWithBalance()
        {
            Table activeClients = new Table("Cliente","CPF","Ativo","Conta","Agência","Banco","Saldo");
            foreach(Client client in _clients)
            {
                foreach(Account account in client.accounts)
                {
                    activeClients.AddRow(client.name,client.cpf,client.active,account.accountNumber,account.agencyNumber,account.bankCode,account.balance);
                }
            }
            activeClients.Config = TableConfiguration.Unicode();
            return activeClients;
        }
        public static Table ShowInactiveClient()
        {
            Table inactiveClients = new Table("Cliente","CPF");
            foreach(Client client in _clients.Where(c => !c.active))
            {
                inactiveClients.AddRow(client.name,client.cpf);
            }
            inactiveClients.Config = TableConfiguration.Unicode();
            return inactiveClients;
        }
    }
}