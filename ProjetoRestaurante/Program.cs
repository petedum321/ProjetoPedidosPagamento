using ProjetoRestaurante.Entities;
using ProjetoRestaurante.Enums;
using System;
using System.Collections.Generic;

namespace ProjetoRestaurante
{
    class Program
    {
        static void Main(string[] args)
        {
            int opcaoMenuMain;
            List<Pedido> pedidos = new List<Pedido>();
            List<Cliente> clientes = new List<Cliente>();
            do
            {
                Console.Clear();
                Console.WriteLine("RESTAURANTE DO JEFTE");
                Console.WriteLine("Menu - Cliente, qual opção satisfaz sua necessidade? ");
                Console.WriteLine("1 - Cadastrar novo cliente: ");
                Console.WriteLine("2 - Cadastrar novo pedido: ");
                Console.WriteLine("3 - Receber novo pagamento: ");
                Console.WriteLine("4 - Gerar Relatório dos Pedidos: ");
                Console.WriteLine("5 - Cancelar um pedido: ");
                Console.WriteLine("6 - Sair ");

                opcaoMenuMain = int.Parse(Console.ReadLine());
                Menu(opcaoMenuMain, clientes, pedidos);

            } while (opcaoMenuMain != 6);
        }

        static void NovoCliente(List<Cliente> clientes)
        {
            Console.WriteLine("Registrando Clientes...");
            Console.Write("Nome do cliente: ");
            string nome = Console.ReadLine();
            clientes.Add(new Cliente(clientes.Count + 1, nome));
        }

        static Pedido BuscaPedido(List<Pedido> pedidos)
        {
            Console.WriteLine("Qual o código do pedido a ser buscado: ");
            int codigo = int.Parse(Console.ReadLine());
            Console.WriteLine("Buscando pedido...");
            var pedido = pedidos.Find(x => x.CodigoPedido == codigo);
            return pedido;
        }

        static void AdicionarItensPedido(Pedido pedido)
        {
            char continuar;
            int quantidade;
            string descricao;
            decimal precoUnitario;
            do
            {
                Console.Clear();
                Console.WriteLine("Adicionando itens...");

                Console.Write("Digite a descrição do item: ");
                descricao = Console.ReadLine();

                Console.Write("Digite o preco unitário: R$");
                precoUnitario = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Digite a quantidade desejada: ");
                quantidade = int.Parse(Console.ReadLine());

                pedido.AdicionarItemPedido(new ItemPedido(descricao, precoUnitario, quantidade));

                Console.WriteLine("Digite S para sair, pressione qualquer tecla para continuar...");
                continuar = char.Parse(Console.ReadLine().ToUpper());
            } while (continuar != 'S');
        }

        static void NovoPedido(List<Cliente> clientes, List<Pedido> pedidos)
        {
            Console.Clear();
            Console.WriteLine("Cadastrando novo pedido...");

            Console.Write("Digite o código do cliente para criar um pedido: ");
            int codigo = int.Parse(Console.ReadLine());
            var cliente = clientes.Find(x => x.CodigoCliente == codigo);
            if (cliente != null)
            {
                Pedido pedido = new Pedido(pedidos.Count + 1, cliente);
                pedidos.Add(pedido);
                AdicionarItensPedido(pedido);
            }
            else
                Console.WriteLine("Este código não está atribuido a nenhum cliente.");
        }

        static bool IdentificaStatusPedido(Pedido pedido)
        {
            if (pedido != null && pedido.StatusPedido == StatusPedido.Novo)
                return true;

            else
            {
                if (pedido != null && pedido.StatusPedido == StatusPedido.Pago)
                    Console.WriteLine("O pedido está pago!");

                else if (pedido != null && pedido.StatusPedido == StatusPedido.Cancelado)
                    Console.WriteLine("O pedido foi cancelado");

                else
                    Console.WriteLine("Esse pedido não existe");

                return false;
            }

        }

        static void PagamentoAvista(Pedido pedido)
        {
            pedido.PagamentoAVista();
        }

        static void PagamentoParcelado(Pedido pedido)
        {
            Console.WriteLine("Digite a quantidade de Parcelas: ");
            int parcelas = int.Parse(Console.ReadLine());

            if (parcelas > 0)
                pedido.PagamentoParcelado(parcelas);

            else
                Console.WriteLine("Não é possível ter um número negativo de parcelas :(");
        }

        static void ReceberPagamento(List<Pedido> pedidos)
        {

            MetodoPagamento metodoPagamento;

            Console.Clear();
            Console.WriteLine("Recebendo pagamento...");

            var pedido = BuscaPedido(pedidos);
            bool statusPedido = IdentificaStatusPedido(pedido);

            if (statusPedido)
            {

                do
                {
                    Console.Clear();
                    Console.WriteLine("3.1) 1 - Pagamento a vista: ");
                    Console.WriteLine("3.2) 2 - Pagamento a prazo: ");
                    Console.WriteLine("6 - Sair: ");
                    metodoPagamento = Enum.Parse<MetodoPagamento>(Console.ReadLine());
                    switch (metodoPagamento)
                    {
                        case MetodoPagamento.AVista:
                            PagamentoAvista(pedido);
                            break;
                        case MetodoPagamento.Parcelado:
                            PagamentoParcelado(pedido);
                            break;
                        case MetodoPagamento.CancelarPagamento:
                            break;
                        default:
                            metodoPagamento = MetodoPagamento.APagar;
                            Console.WriteLine("Não é possível receber este valor.");
                            Console.ReadKey();
                            break;
                    }
                } while (metodoPagamento == MetodoPagamento.APagar);

            }
        }

        static void GerarRelatorio(List<Pedido> pedidos)
        {
            Console.Clear();
            Console.WriteLine("Gerando Relatório dos pedidos...");
            foreach (var pedido in pedidos)
            {
                Console.WriteLine(pedido);
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        static bool Cancelamento(Pedido pedido)
        {
            if (pedido != null && pedido.StatusPedido == StatusPedido.Novo)
                return true;

            else
            {
                if (pedido != null && pedido.StatusPedido == StatusPedido.Pago)
                    Console.WriteLine("O pedido está pago :)");

                else if (pedido != null && pedido.StatusPedido == StatusPedido.Cancelado)
                    Console.WriteLine("O pedido está cancelado");

                else
                    Console.WriteLine("Esse pedido não existe");

                Console.ReadKey();
                return false;
            }

        }

        static void CancelaPedido(List<Pedido> pedidos)
        {

            Console.Clear();
            Console.WriteLine("5) Cancelar Pedido");
            var pedido = BuscaPedido(pedidos);
            bool statusPedido = Cancelamento(pedido);
            if (statusPedido)
            {
                pedido.StatusPedido = StatusPedido.Cancelado;
                pedido.DataCancelamento = DateTime.Now;
            }

        }

        static void Menu(int opcaoMenuPrincipal, List<Cliente> clientes, List<Pedido> pedidos)
        {
            switch (opcaoMenuPrincipal)
            {
                case 1:
                    NovoCliente(clientes);
                    break;
                case 2:
                    NovoPedido(clientes, pedidos);
                    break;
                case 3:
                    ReceberPagamento(pedidos);
                    break;
                case 4:
                    GerarRelatorio(pedidos);
                    break;
                case 5:
                    CancelaPedido(pedidos);
                    break;
                case 6:
                    Console.WriteLine("Saindo...");
                    break;

                default:
                    Console.WriteLine("Essa opção não existe");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
