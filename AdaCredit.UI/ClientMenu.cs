using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdaCredit.UI
{
    public static class ClientMenu
    {
        public static int input { get; set; }
        public static void View()
        {
            Console.WriteLine("Selecione uma opção");
            Console.WriteLine("1 - Cadastrar novo cliente");
            Console.WriteLine("2 - Consultar os dados de um Cliente");
            Console.WriteLine("3 - Alterar o Cadastro de um Cliente");
            Console.WriteLine("4 - Desativar Cadastro de um Cliente");
            Console.WriteLine("5 - Sair");
            Console.ReadLine();
        }
        public static void ViewOption1()
        {
            
        }
    }
}