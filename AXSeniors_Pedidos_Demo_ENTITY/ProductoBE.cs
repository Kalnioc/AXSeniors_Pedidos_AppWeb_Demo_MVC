using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AXSeniors_Pedidos_Demo_ENTITY
{
    public class ProductoBE
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}