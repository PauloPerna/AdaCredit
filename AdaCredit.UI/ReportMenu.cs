using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BetterConsoleTables;
using AdaCredit.Data;

namespace AdaCredit.UI
{
    public class ReportMenu
    {
        public static void View()
        {
            bool exit = false;
            int input;
            while(!exit){
                Console.WriteLine("Selecione uma opção");
                Console.WriteLine("1 - Exibir todos os clientes ativos com seus respectivos saldos");
                Console.WriteLine("2 - Exibir todos os clientes inativos");
                Console.WriteLine("3 - Exibir todos os funcionários ativos e sua última data e hora de login");
                Console.WriteLine("4 - Exibir transações desta seção com erro");
                Console.WriteLine("5 - Retornar ao Menu Principal");
                int.TryParse(Console.ReadLine(), out input);
                switch(input)
                {
                    case 1:
                        ShowActiveClientWithBalance();
                        break;
                    case 2:
                        ShowInactiveClient();
                        break;
                    case 3:
                        ShowActiveUsers();
                        break;
                    case 4:
                        ShowErrorTransactions();
                        break;
                    case 5:
                        Console.WriteLine("Retornando ao menu principal");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opção não disponível. Tente novamente.");					
                        break;
                }
            }
        }
        public static void ShowActiveClientWithBalance(){
            Table report = ClientsRepository.ShowActiveClientWithBalance();
            Console.WriteLine(report);
        }
        public static void ShowInactiveClient(){
            Table report = ClientsRepository.ShowInactiveClient();
            Console.WriteLine(report);
        }
        public static void ShowActiveUsers(){
            Table report = UsersRepository.ShowActiveUsers();
            Console.WriteLine(report);
        }
        public static void ShowErrorTransactions(){
            Table report = TransactionsRepository.ShowErrorTransactions();
            Console.WriteLine(report);
        }
    }
}