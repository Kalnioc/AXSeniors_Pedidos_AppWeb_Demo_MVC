using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AXSeniors_Pedidos_Demo_ENTITY
{
    public class PedidoCabeceraBE
    {
        public int PedidoCabeceraId { get; set; }
        public DateTime FechaPedido { get; set; }
        public int ClienteId { get; set; }
        public ClienteBE Cliente { get; set; }
        public int TipoComprobanteId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Total { get; set; }
        public int EstadoId { get; set; }
        public string EstadoNombre { get; set; }
        public List<PedidoDetalleBE> PedidoDetalleLista { get; set; } = new List<PedidoDetalleBE>();
    }
}