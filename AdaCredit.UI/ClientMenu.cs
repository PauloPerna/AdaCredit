using AdaCredit.Domain.Entities;
using AdaCredit.UseCases;
using AdaCredit.Data;

namespace AdaCredit.UI
{
    public static class ClientMenu
    {
        public static void View()
        {

            bool exit = false;
            int input;
            while(!exit){
                Console.WriteLine("Menu de Clientes - Selecione uma opção");
                Console.WriteLine("1 - Cadastrar novo cliente");
                Console.WriteLine("2 - Consultar os dados de um Cliente");
                Console.WriteLine("3 - Alterar o Cadastro de um Cliente");
                Console.WriteLine("4 - Desativar Cadastro de um Cliente");
                Console.WriteLine("5 - Cadastrar nova conta bancária a um cliente existente");
                Console.WriteLine("6 - Retornar ao Menu Principal");
                int.TryParse(Console.ReadLine(), out input);
                switch(input)
                {
                    case 1:
                        RegisterNewClient();
                        break;
                    case 2:
                        GetClient();
                        break;
                    case 3:
                        EditClient();
                        break;
                    case 4:
                        DeactivateClient();
                        break;
                    case 5:
                        NewAccount();
                        break;
                    case 6:
                        Console.WriteLine("Retornando ao menu principal");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opção não disponível. Tente novamente.");					
                        break;
                }
            }
        }
        public static void RegisterNewClient()
        {
            Console.WriteLine("Digite o cpf do novo cliente");
            long cpf;
            while(!long.TryParse(Console.ReadLine(), out cpf))
            {
                Console.WriteLine("Por favor, digite apenas números");
            }
            Console.WriteLine("Digite o nome do cliente");
            string name = Console.ReadLine();
            Tuple<Client, bool> result = AddNewClient.Execute(cpf, name);
            Client client = result.Item1;
            bool success = result.Item2;
            if(!success)
            {
                Console.WriteLine("O cliente não pode ser criado. Já existe alguém cadastrado com esse CPF?");
                return;
            }
            bool exit = false;
            while(!exit)
            {
                Console.WriteLine("Deseja criar uma conta para o cliente? <s/n>");
                string input = Console.ReadLine().ToLower();
                switch(input)
                {
                    case "s":
                        NewAccount(client);
                        exit = true;
                        break;
                    case "n":
                        Console.WriteLine("Retornando ao menu de clientes.");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Por favor, digite apenas <s/n>");
                        break;
                }
            }
        }
        public static void GetClient()
        {
            Console.WriteLine("Digite o CPF do cliente que deseja consultar os dados");
            long cpf;
            while(!long.TryParse(Console.ReadLine(), out cpf))
            {
                Console.WriteLine("Por favor, digite apenas números");
            }
            Client client = ClientsRepository.Get(cpf);
            ReportClient.Execute(client);
        }
        public static void EditClient()
        {
            Console.WriteLine("Digite o CPF do cliente que deseja alterar os dados");
            long cpf;
            while(!long.TryParse(Console.ReadLine(), out cpf))
            {
                Console.WriteLine("Por favor, digite apenas números");
            }
            if(!ClientsRepository.Exists(cpf))
            {
                Console.WriteLine("Cliente não encontrado. Saindo da função.");
                return;
            }
            Client client = ClientsRepository.Get(cpf);
            bool exit = false;
            while(!exit)
            {    
                Console.WriteLine("O que deseja alterar?");
                Console.WriteLine("1 - Alterar Nome");
                Console.WriteLine("2 - Ativar cliente");
                Console.WriteLine("3 - Sair");
                int input;
                while(!int.TryParse(Console.ReadLine(),out input))
                {
                    Console.WriteLine("Por favor, digite apenas núemros");
                }
                switch(input)
                {
                    case 1:
                        Console.WriteLine("Digite o novo nome do cliente");
                        string newName = Console.ReadLine();
                        client.name = newName;
                        ClientsRepository.Update(client);
                        Console.WriteLine("Cliente Atualizado com sucesso.");
                        exit = true;
                        break;
                    case 2:
                        ClientsRepository.Activate(client.cpf);
                        Console.WriteLine($"Cliente {client.name} ativado");
                        exit = true;
                        break;
                    case 3:
                        Console.WriteLine("Retornando ao menu de clientes");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Por favor, digite uma das opções");
                        break;
                }
            }
        }
        public static void DeactivateClient()
        {
            Console.WriteLine("Digite o CPF do cliente que deseja desativar");
            long cpf;
            while(!long.TryParse(Console.ReadLine(), out cpf))
            {
                Console.WriteLine("Por favor, digite apenas números");
            }
            if(!ClientsRepository.Exists(cpf))
            {
                Console.WriteLine("Cliente não encontrado. Saindo da função.");
                return;
            }
            ClientsRepository.Deactivate(cpf);
            Console.WriteLine("Client desativado com sucesso!");
        }
        public static void NewAccount(Client client)
        {
            Tuple<bool,Account> result = GenerateNewAccount.Execute(client);
            Account account = result.Item2; 
            Console.WriteLine($"Conta de número {account.accountNumber} atribuída ao cliente {client.name}");
        }
        public static void NewAccount()
        {
            Console.WriteLine("Digite o CPF do cliente que deseja atribuir uma nova conta bancária");
            long cpf;
            while(!long.TryParse(Console.ReadLine(), out cpf))
            {
                Console.WriteLine("Por favor, digite apenas números");
            }
            if(!ClientsRepository.Exists(cpf))
            {
                Console.WriteLine("Cliente não encontrado. Saindo da função.");
                return;
            }
            Client client = ClientsRepository.Get(cpf);
            NewAccount(client);
        }
    }
}