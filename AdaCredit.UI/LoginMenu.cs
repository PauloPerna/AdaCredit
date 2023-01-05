using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Data;
using AdaCredit.Domain.Entities;

namespace AdaCredit.UI
{
    public class LoginMenu
    {
        public static int input { get; set; }
        public static void View()
        {
            string login;
			bool loginSuccess;
			string pass;
			do{
				System.Console.WriteLine("Digite seu login (cpf)");
				login = Console.ReadLine();
				System.Console.WriteLine("Digite sua senha");
				pass = Console.ReadLine();
				loginSuccess = UsersRepository.Login(login, pass);
				if(!loginSuccess)
				{
					System.Console.WriteLine("Usuário ou senha errado. Por favor, tente novamente.");
				}
			} while (!loginSuccess);
			System.Console.WriteLine("Login realizado com sucesso!");
            if(UsersRepository.firstLogin)
            {
                FirstLogin();
            }
        }
        public static void FirstLogin()
        {
            System.Console.WriteLine("Vamos realizar agora o cadastro do primeiro funcionário");
            UserMenu.RegisterNewUser();
        }
    }
}