using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSeniors_Pedidos_Demo_ENTITY
{
    public class EstadoBE
    {
        public int EstadoId { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
