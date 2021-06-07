using DomainDonation;
using Infra;
using System;
using System.Globalization;

namespace ConsoleApp
{
    class Program
    {
        static string pressButtons = "Pressione qualquer tecla para exibir o menu principal ...";
        static DonationRepository repository = new DonationRepository();

        static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");

            string menu;
            do
            {

                OptionMenu();

                menu = Console.ReadLine();
                try
                {

                    if (menu == "1")
                    {
                        AddDonation();
                    }
                    else if (menu == "2")
                    {
                        SearchDonation();
                    }
                    else if (menu == "3")
                    {
                        Console.Write("Saindo do programa... ");
                    }
                    else if (menu != "3")
                    {
                        Console.Write($"Opcao inválida! Escolha uma opção válida. {pressButtons}");
                        Console.ReadKey();
                    }
                }
                catch
                {
                    continue;
                }

            } while (menu != "3");
        }

        static void OptionMenu()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("==== Gerenciador de Doações Good Hands ==== ");
            Console.ResetColor();
            Console.WriteLine("1 - Adicionar doação");
            Console.WriteLine("2 - Pesquisar doação");
            Console.WriteLine("3 - Sair");
            Console.WriteLine("\nEscolha uma das opções acima: ");
        }

        static void SearchDonation()

        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("==== Pesquisar doação ====");
            Console.ResetColor();
            Console.WriteLine("Verifique abaixo se a doação requerida está disponível.");
            Console.WriteLine("\nQual item deseja pesquisar: (necessário termo completo para busca)");
            var search = Console.ReadLine();
            var foundedDonation = repository.Search(search);

            if (foundedDonation.Count > 0)
            {
                Console.WriteLine("==== Selecione uma das opções abaixo para visualizar os dados da doação ====");
                for (var index = 0; index < foundedDonation.Count; index++)
                    Console.WriteLine($"{index} - {foundedDonation[index].GetTypeOfDonation()}");

                if (!ushort.TryParse(Console.ReadLine(), out var showIndex) || showIndex >= foundedDonation.Count)
                {
                    Console.WriteLine($"Opção inválida! {pressButtons}");
                    Console.ReadKey();

                    throw new ArgumentNullException();
                }

                if (showIndex < foundedDonation.Count)
                {
                    var donation = foundedDonation[showIndex];
                    var state = donation.GetState() ? "Novo" : "Usado";

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("==== Dados da doação ====");
                    Console.ResetColor();
                    Console.WriteLine($"\nTipo de doação: {donation.GetTypeOfDonation()}");
                    Console.WriteLine($"Quatidade de itens doados: {donation.GetQuantityItems()}");
                    Console.WriteLine($"Breve descrição: {donation.GetDescription()}");
                    Console.WriteLine($"Data de cadastro: {donation.GetDateOfRegister():dd/MM/yyyy}");
                    Console.WriteLine($"Tempo de cadastro: {donation.GetTimeOfRegister()} dias");
                    Console.WriteLine($"Valor do frete: {double.Parse(Math.Round(donation.GetCourrierTax(), 2).ToString())}");
                    Console.WriteLine($"Estado da doação: {state}");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("=========================");
                    Console.ResetColor();

                    Console.Write(pressButtons);
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine($"Não foi encontrada nenhuma doação nos termos pesquisados. Tente novamente mais tarde! \n{pressButtons}");
                Console.ReadKey();
            }
        }

        static void AddDonation()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("==== Adicionar doação ====");
            Console.ResetColor();

            Console.WriteLine("\nTipo de doação:");
            var typeOfDonation = Console.ReadLine();

            Console.WriteLine("Quantidade de itens:");
            var quantityItems = int.Parse(Console.ReadLine());

            Console.WriteLine("Breve descrição:");
            var description = Console.ReadLine();

            Console.WriteLine("Data de cadastro (formato dd/MM/yyyy):");
            if (!DateTime.TryParse(Console.ReadLine(), out var dateOfRegister))
            {
                Console.WriteLine($"Data inválida! Dados descartados! \n{pressButtons}");
                Console.ReadKey();

                throw new ArgumentNullException();
            }

            Console.WriteLine("A doação é nova ou usada ( digite 'nova' ou 'usada'):");
            var state = Console.ReadLine();
            var boolState = state == "nova" ? true : false;

            Console.WriteLine("Valor do frete para envio ao interessado:");
            var courrierTax = double.Parse(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("==== Os dados estão corretos? ====");
            Console.ResetColor();
            Console.WriteLine("==================================");
            Console.WriteLine($"Tipo de doação: {typeOfDonation}");
            Console.WriteLine($"Quantidade de itens: {quantityItems}");
            Console.WriteLine($"Breve descrição: {description}");
            Console.WriteLine($"Data de registro: {dateOfRegister:dd/MM/yyyy}");
            Console.WriteLine($"Estado: {state}");
            Console.WriteLine($"Valor do frete: R$ {double.Parse(Math.Round(courrierTax, 2).ToString())}");
            Console.WriteLine("==================================");
            Console.WriteLine("1 - Sim \n2 - Não");

            var opcaoParaAdicionar = Console.ReadLine();
            if (opcaoParaAdicionar == "1")
            {
                repository.Add(new Donation(typeOfDonation, description, dateOfRegister, boolState, courrierTax, quantityItems));

                Console.WriteLine($"Dados adicionados com sucesso! {pressButtons}");
                Console.ReadKey();
            }
            else if (opcaoParaAdicionar == "2")
            {
                Console.WriteLine($"Dados descartados! {pressButtons}");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Opção inválida! {pressButtons}");
                Console.ReadKey();
            }
        }
    }
}