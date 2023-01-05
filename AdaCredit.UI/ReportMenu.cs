using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdaCredit.UI
{
    public class ReportMenu
    {
        public static int input { get; set; }
        public static void View()
        {
            Console.WriteLine("Selecione uma opção");
            Console.WriteLine("1 - Exibir todos os clientes ativos com seus respectivos saldos");
            Console.WriteLine("2 - Exibir todos os clientes inativos");
            Console.WriteLine("3 - Exibir todos os funcionários ativos e sua última data e hora de login");
            Console.WriteLine("4 - Exibir transações com erro");
            Console.WriteLine("5 - Sair");
            Console.ReadLine();
        }
    }
}