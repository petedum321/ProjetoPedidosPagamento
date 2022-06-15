using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoRestaurante.Entities
{
    class Recebimento
    {
        private Pedido _pedido;
        public decimal ValorParcela { get; set; }
        public int Parcela { get; set; }
        public DateTime DataVencimento { get; set; }

        public Recebimento()
        {
        }

        public Recebimento(Pedido pedido, int parcela)
        {
            Parcela = parcela;
            _pedido = pedido;
            ValorParcela = pedido.CustoPedido() / Parcela;
        }

        public override string ToString()
        {
            string espaco = "";
            StringBuilder datasVencimento = new StringBuilder();

            datasVencimento.AppendLine($"Valor da parcela{espaco.PadRight(10)}parcela{espaco.PadRight(10)}data de vencimento");
            for (int i = 1; i <= Parcela; i++)
            {
                DataVencimento = _pedido.DataRecebimento.AddDays(30 * i);
                datasVencimento.AppendLine($"{ValorParcela:F2}{espaco.PadRight(15)}{i}{espaco.PadRight(9)}{DataVencimento:dd/MM/yyyy}");
            }
            return datasVencimento.ToString();
        }

    }
}
