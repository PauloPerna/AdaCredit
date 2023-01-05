using System;
using AdaCredit.Data;
using AdaCredit.UseCases;
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
			string pass = string.Empty;
			do{
				System.Console.WriteLine("--- ÁREA DE LOGIN ---");
				System.Console.WriteLine("Digite seu login (cpf)");
				login = Console.ReadLine();
				System.Console.WriteLine("Digite sua senha");
                ConsoleKey key;
                do
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    key = keyInfo.Key;

                    if (key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        Console.Write("\b \b");
                        pass = pass[0..^1];
                    }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                        Console.Write("*");
                        pass += keyInfo.KeyChar;
                    }
                } while (key != ConsoleKey.Enter);
				loginSuccess = Login.Execute(login, pass);
				if(!loginSuccess)
				{
					System.Console.WriteLine("\nUsuário ou senha errado. Por favor, tente novamente.\n");
				}
			} while (!loginSuccess);
			System.Console.WriteLine("\nLogin realizado com sucesso!\n");
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