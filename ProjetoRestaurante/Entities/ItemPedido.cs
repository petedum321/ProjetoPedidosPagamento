using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoRestaurante.Entities
{
    class ItemPedido
    {
        public string Descricao { get; set; }
        public decimal ValorUnitario { get; set; }
        public int Quantidade { get; set; }

        public ItemPedido()
        {
        }

        public ItemPedido(string descricao, decimal valorUnitario, int quantidade)
        {
            Descricao = descricao;
            ValorUnitario = valorUnitario;
            Quantidade = quantidade;
        }

        public override string ToString()
        {
            return $"Descrição: {Descricao}, Valor unitário: {ValorUnitario:F2} R$, Quantidade: {Quantidade} unidades";
        }
    }
}
