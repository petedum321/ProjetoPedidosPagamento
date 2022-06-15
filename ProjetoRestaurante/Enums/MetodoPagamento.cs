using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoRestaurante.Enums
{
    enum MetodoPagamento : int
    {
        APagar = 0,
        AVista = 1,
        Parcelado = 2,      
        CancelarPagamento = 6
    }
}
