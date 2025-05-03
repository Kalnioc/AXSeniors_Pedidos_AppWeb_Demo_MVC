using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AXSeniors_Pedidos_Demo_ENTITY
{
    public class PedidoDetalleBE
    {
        public int PedidoDetalleId { get; set; }
        public int PedidoCabeceraId { get; set; }
        public int ProductoId { get; set; }
        public ProductoBE Producto { get; set; }
        public int CantidadItem { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int DescuentoItem { get; set; }
        public decimal TotalItem { get; set; }
    }
}