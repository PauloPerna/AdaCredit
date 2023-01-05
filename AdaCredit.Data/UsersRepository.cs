using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using CsvHelper;
using BetterConsoleTables;
using AdaCredit.Domain.Entities;

namespace AdaCredit.Data
{
    public static class UsersRepository
    {
        private static List<User> _users = new List<User>();
        public static int loggedUserIndex;
        public static bool firstLogin { get; private set; }
        static UsersRepository()
        {
            string path = Environment.GetFolderPath (Environment.SpecialFolder.Desktop);
            string fileName = "users.csv";
            string fileFullName = Path.Combine(path,fileName);
            if(!File.Exists(fileFullName))
            {
                firstLogin = true;
                return;
            }
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
        public static bool GetActiveState(int index)
        {
            return _users[index].active;
        }
        public static bool Login(int index, string pass)
        {
            return _users[index].Login(pass);
        }
        public static bool UpdateLastLoginTime(int index)
        {
            _users[index].lastLogin = DateTime.Now;
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
            UsersRepository.WriteUsersToFile();
            return true;
        }
        public static int GetIndex(long cpf)
        {
            // Return user index by cpf
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
            bool success = _users[index].RedefinePass(pass) &&
                            UsersRepository.WriteUsersToFile();
            return success;
        }
        public static bool Deactivate(long cpf)
        {
            var index = GetIndex(cpf);
            if(_users.Count(c => c.active) == 1)
            {
                Console.WriteLine("Único funcionário ativo");
                return false;
            }
            bool success = _users[index].Deactivate() &&
                            UsersRepository.WriteUsersToFile();
            return success;
        }
        public static Table ShowActiveUsers()
        {
            Table activeUsers = new Table("Funcionário","CPF","Último login");
            foreach(User user in _users.Where(c => c.active))
            {
                activeUsers.AddRow(user.name,user.cpf,user.lastLogin);
            }
            activeUsers.Config = TableConfiguration.Unicode();
            return activeUsers;
        }
    }
}