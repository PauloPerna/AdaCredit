using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using CsvHelper;
using AdaCredit.Domain.Entities;

namespace AdaCredit.Data
{
    public static class UsersRepository
    {
        private static List<User> _users = new List<User>();
        public static int loggedUserIndex;
        public static bool firstLogin;
        static UsersRepository()
        {
            //TODO: leitura arquivo de usuários
            string path = Environment.GetFolderPath (Environment.SpecialFolder.Desktop);
            string fileName = "users.csv";
            string fileFullName = Path.Combine(path,fileName);
            if(!File.Exists(fileFullName))
            {
                // Se o arquivo não existe, sinalizamos como primeiro login
                firstLogin = true;
                return;
            }
            // se o arquivo existe, então carregamos a lista de objetos
            firstLogin = false;
            using (var reader = new StreamReader(fileFullName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                _users = csv.GetRecords<User>().ToList();
            }
        }
        public static bool WriteUsersToFile()
        {
            string path = Environment.GetFolderPath (Environment.SpecialFolder.Desktop);
            string fileName = "users.csv";
            string fileFullName = Path.Combine(path,fileName);
            using (var writer = new StreamWriter(fileFullName))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(_users);
            }
            return true;
        }
        public static bool Login(long cpf, string pass)
        {
            int index = GetIndex(cpf);
            var loginSuccess = index != -1 &&
                             _users[index].active == true && 
                             _users[index].Login(pass);
            if(!loginSuccess)
            {
                return false;
            }
            loggedUserIndex = index;
            _users[loggedUserIndex].lastLogin = DateTime.Now;
            return true;
        }
        public static bool Add(User user)
        {
            // Users shold have unique cpf
            if(_users.Any(c => c.cpf == user.cpf))
            {
                return false;
            }
            _users.Add(user);
            return true;
        }
        public static int GetIndex(long cpf)
        {
            // Return user by cpf
            return _users.FindIndex(c => c.cpf == cpf);
        }
        public static User Get(long cpf)
        {
            // Return user by cpf
            return _users.FirstOrDefault(c => c.cpf == cpf);
        }
        public static IEnumerable<User> Get(string name)
        {
            // Return all users that match name
            return _users.Where(c => c.name == name);
        }
        public static bool RedefinePass(long cpf, string pass)
        {
            var index = GetIndex(cpf);
            return _users[index].RedefinePass(pass);
        }
        public static bool Deactivate(long cpf)
        {
            var index = GetIndex(cpf);
            return _users[index].Deactivate();
        }
    }
}