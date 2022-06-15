using ProjetoRestaurante.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoRestaurante.Entities
{
    class Pedido
    {
        
        public int CodigoPedido { get; set; }
        public DateTime DataPedido { get; set; }
        public DateTime DataRecebimento { get; set; }
        public DateTime DataCancelamento { get; set; }
        public Cliente Cliente { get; set; }
        public Recebimento Recebimento { get; set; }
        public StatusPedido StatusPedido { get; set; }
        public MetodoPagamento Pagamento { get; set; }

        private List<ItemPedido> _itensPedido;
        public Pedido(int codigoPedido, Cliente cliente)
        {
            Cliente = cliente;
            CodigoPedido = codigoPedido;
            _itensPedido = new List<ItemPedido>();
            //DataPedido = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
            DataPedido = DateTime.Now;
            StatusPedido = StatusPedido.Novo;
            Pagamento = MetodoPagamento.APagar;
        }

        public void AdicionarItemPedido(ItemPedido pedido)
        {
            _itensPedido.Add(pedido);
        }

        public decimal CustoPedido()
        {
            decimal custo = 0;
            foreach (ItemPedido ip in _itensPedido)
                custo += ip.ValorUnitario * ip.Quantidade;

            return custo;
        }



        public void PagamentoAVista()
        {
            StatusPedido = StatusPedido.Pago;
            Pagamento = MetodoPagamento.AVista;
        }

        public void PagamentoParcelado(int parcela)
        {
            StatusPedido = StatusPedido.Pago;
            Pagamento = MetodoPagamento.Parcelado;
            //DataRecebimento = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Local);
            DataRecebimento = DateTime.Now;
            Recebimento = new Recebimento(this, parcela);
        }
        public override string ToString()
        {
            StringBuilder notaFiscal = new StringBuilder();

            notaFiscal.AppendLine($"Pedido {CodigoPedido} Cliente {Cliente.Nome} Valor Total: {CustoPedido():F2}, Data do Pedido: {DataPedido.ToString("dd/MM/yyyy HH:mm:ss")}");
            notaFiscal.AppendLine($"Status: {StatusPedido}");

            if (StatusPedido == StatusPedido.Pago)
            {
                notaFiscal.AppendLine($"Forma de Pagamento: {Pagamento}");
                if (Pagamento == MetodoPagamento.Parcelado)
                    notaFiscal.AppendLine($"{Recebimento}");
            }

            notaFiscal.AppendLine("Itens");
            foreach (var ip in _itensPedido)
            {
                notaFiscal.AppendLine($"{ip}");
            }

            return notaFiscal.ToString();
        }
    }
}
