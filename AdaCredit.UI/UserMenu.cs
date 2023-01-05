using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaCredit.Data;
using AdaCredit.Domain.Entities;

namespace AdaCredit.UI
{
    public class UserMenu
    {
        public static void View()
        {
            bool exit = false;
            int input;
            while(!exit){
                Console.WriteLine("Selecione uma opção");
                Console.WriteLine("1 - Cadastrar novo funcionário");
                Console.WriteLine("2 - Alterar senha de um funcionário");
                Console.WriteLine("3 - Desativar Cadastro de funcionário");
                Console.WriteLine("4 - Retornar");
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
            string pass = Console.ReadLine();
            var newUser = new User(cpf, name, pass);
            UsersRepository.Add(newUser);
        }
        public static void ChangePassword()
        {
            Console.WriteLine("Digite o CPF do funcionário que vamos editar a senha");
            long cpf;
            while(!long.TryParse(Console.ReadLine(), out cpf))
            {
                Console.WriteLine("Por favor, digite apenas números");
            }
            Console.WriteLine("Digite a senha antiga:");
            string pass = Console.ReadLine();
            if(!UsersRepository.Login(cpf,pass))
            {
                Console.WriteLine("Senha incorreta. Retornando ao menu de Funcionários");
                return;
            }
            Console.WriteLine("Digite a nova senha:");
            string newPass = Console.ReadLine();
            if(UsersRepository.RedefinePass(cpf,newPass))
            {
                Console.WriteLine("Senha redefinida com sucesso!");
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
            if(!UsersRepository.Deactivate(cpf))
            {
                System.Console.WriteLine("Não foi possível desativar o funcionário.");
            }
            System.Console.WriteLine($"Usuário {UsersRepository.Get(cpf).name} desativado.");
        }
    }
}