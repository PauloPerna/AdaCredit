using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Data;
using AdaCredit.Domain.Entities;
using AdaCredit.UseCases;

namespace AdaCredit.UI
{
    public class UserMenu
    {
        public static void View()
        {
            bool exit = false;
            int input;
            while(!exit){
                Console.WriteLine("Menu de Funcionários - Selecione uma opção");
                Console.WriteLine("1 - Cadastrar novo funcionário");
                Console.WriteLine("2 - Alterar senha de um funcionário");
                Console.WriteLine("3 - Desativar Cadastro de funcionário");
                Console.WriteLine("4 - Retornar ao Menu Principal");
                int.TryParse(Console.ReadLine(), out input);
                switch(input)
                {
                    case 1:
                        RegisterNewUser();
                        break;
                    case 2:
                        ChangePassword();
                        break;
                    case 3:
                        DeactivateUser();
                        break;
                    case 4:
                        Console.WriteLine("Retornando ao menu principal");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opção não disponível. Tente novamente.");					
                        break;
                }
            }
        }
        public static void RegisterNewUser()
        {
            Console.WriteLine("Digite o nome do funcionário");
            string name = Console.ReadLine();
            Console.WriteLine("Digite o cpf do funcionário (apenas números)");
            long cpf;
            while(!long.TryParse(Console.ReadLine(), out cpf))
            {
                Console.WriteLine("Por favor, digite apenas números");
            }
            Console.WriteLine("Digite a senha do funcionário");
            string pass = string.Empty;
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
            var newUser = new User(cpf, name, pass);
            UsersRepository.Add(newUser);
            Console.WriteLine("Novo usuário registrado!\n");
        }
        public static void ChangePassword()
        {
            Console.WriteLine("Digite o CPF do funcionário que vamos editar a senha");
            long cpf;
            while(!long.TryParse(Console.ReadLine(), out cpf))
            {
                Console.WriteLine("Por favor, digite apenas números");
            }
            if(!UsersRepository.Exists(cpf)){
                Console.WriteLine("Funcionário não encontrado.");
                return;
            }
            Console.WriteLine("Digite a senha antiga:");
			string pass = string.Empty;
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
            if(!Login.Execute(cpf,pass))
            {
                Console.WriteLine("\nSenha incorreta. Retornando ao menu de Funcionários");
                return;
            }
            Console.WriteLine("\nDigite a nova senha:");
            string newPass = string.Empty;
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
            if(UsersRepository.RedefinePass(cpf,newPass))
            {
                Console.WriteLine("\nSenha redefinida com sucesso!");
            }
        }
        public static void DeactivateUser()
        {
            Console.WriteLine("Digite o CPF do funcionário que vamos desativar");
            long cpf;
            while(!long.TryParse(Console.ReadLine(), out cpf))
            {
                Console.WriteLine("Por favor, digite apenas números");
            }
            if(!UsersRepository.Exists(cpf)){
                Console.WriteLine("Funcionário não encontrado.");
                return;
            }
            if(!UsersRepository.Deactivate(cpf))
            {
                System.Console.WriteLine("Não foi possível desativar o funcionário.");
            }
            System.Console.WriteLine($"Usuário {UsersRepository.Get(cpf).name} desativado.");
        }
    }
}