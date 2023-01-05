using AdaCredit.UseCases;
using AdaCredit.Data;

namespace AdaCredit.UI
{
    public class TransactionMenu
    {
        public static void View()
        {
            bool exit = false;
            int input;
            while(!exit){
                Console.WriteLine("Menu de Transações - Selecione uma opção");
                Console.WriteLine("1 - Processar transações");
                Console.WriteLine("2 - Retornar ao Menu Principal");
                int.TryParse(Console.ReadLine(), out input);
                switch(input)
                {
                    case 1:
                        Process();
                        break;
                    case 2:
                        Console.WriteLine("Retornando ao menu principal");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opção não disponível. Tente novamente.");					
                        break;
                }
            }
        }
        public static void Process()
        {
            List<string> files = FindTransactionsFileNames.Execute();
            Console.WriteLine("Qual arquivo deseja processar?");
            for(int i = 0; i < files.Count; i++ )
            {
                Console.WriteLine($"{i} - {files[i]}");
            }
            int input;
            while(!int.TryParse(Console.ReadLine(), out input))
            {
                Console.WriteLine("Por favor, digite apenas o número referente ao arquivo");
            }
            string fileName = files[input];
            TransactionsRepository.Load(fileName);
            ProcessTransactions.ProcessAllTransactions();
            Console.WriteLine($"Arquivo {fileName} processado com sucesso!");
        }
    }
}