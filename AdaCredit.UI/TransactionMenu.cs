using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdaCredit.UI
{
    public class TransactionMenu
    {
        public static int input { get; set; }
        public static void View()
        {
            Console.WriteLine("Selecione uma opção");
            Console.WriteLine("1 - Processar transações");
            Console.WriteLine("2 - Sair");
            Console.ReadLine();
        }
    }
}