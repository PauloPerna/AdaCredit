using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
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
            //TODO: linkar accounts repository
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
        public static bool Deactivate(Client client)
        {
            var index = _clients.FindIndex(c => c == client);
            return _clients[index].Deactivate();
        }
    }
}