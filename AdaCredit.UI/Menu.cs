using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdaCredit.UI
{
    public class Menu
    {
        public static void View()
        {
            int input;
			bool exit = false;
			while(!exit){
                Console.WriteLine("Selecione uma opção do menu");
                Console.WriteLine("1 - Clientes");
                Console.WriteLine("2 - Funcionários");
                Console.WriteLine("3 - Transações");
                Console.WriteLine("4 - Relatórios");
                Console.WriteLine("5 - Sair");
                int.TryParse(Console.ReadLine(),out input);
				switch(input)
				{
					case 1:
						ClientMenu.View();
						break;
					case 2:
						UserMenu.View();
						break;
					case 3:
						TransactionMenu.View();
						break;
					case 4:
						ReportMenu.View();
						break;
					case 5:
                        System.Console.WriteLine("Até breve!");
                        System.Console.WriteLine("Seção encerrada");
						exit = true;
						break;
					default:
						System.Console.WriteLine("Opção não disponível. Tente novamente.");
						break;
				}
			}
        }
    }
}