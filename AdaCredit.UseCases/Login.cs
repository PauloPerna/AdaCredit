using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Data;

namespace AdaCredit.UseCases
{
    public static class Login
    {
        public static bool Execute(string login, string pass)
        {
            if(long.TryParse(login, out long cpf))
            {
                return Execute(cpf, pass);
            }
            if(UsersRepository.firstLogin == true)
            {
                return login == "admin" && pass == "pass";
            }
            return false;
        }
        public static bool Execute(long cpf, string pass)
        {
            int index = UsersRepository.GetIndex(cpf);
            var cpfExists = index != -1;
            if(!cpfExists)
            {
                return false;
            }
            var userActive = UsersRepository.GetActiveState(index);
            if(!userActive)
            {
                return false;
            }
            var loginSuccess = UsersRepository.Login(index, pass);
            if(!loginSuccess)
            {
                return false;
            }
            UsersRepository.loggedUserIndex = index;
            UsersRepository.UpdateLastLoginTime(index);
            return true;
        }
    }
}