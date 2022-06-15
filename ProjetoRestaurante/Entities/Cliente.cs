using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoRestaurante.Entities
{
    class Cliente
    {
        public int CodigoCliente { get; set; }
        public string Nome { get; set; }

        public Cliente()
        {
        }

        public Cliente(int codigoCliente, string nome)
        {
            CodigoCliente = codigoCliente;
            Nome = nome;
        }

    }
}
