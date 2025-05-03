using AXSeniors_Pedidos_Demo_BUSINESS.Interfaces;
using AXSeniors_Pedidos_Demo_DATA;
using AXSeniors_Pedidos_Demo_ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSeniors_Pedidos_Demo_BUSINESS
{
    public class ConsultaBL : IConsultaBL
    {
        private readonly ConsultaDL _consultaDL = new ConsultaDL();
        public List<ProductoBE> ConsultaProducto()
        {          
            return _consultaDL.ConsultaProducto();
        }

        public List<PedidoCabeceraBE> ConsultaPedidoCabecera()
        {
            return _consultaDL.ConsultaPedidoCabecera();
        }

        public List<ClienteBE> ConsultaCliente()
        {
            return _consultaDL.ConsultaCliente();
        }

        public List<TipoComprobanteBE> ConsultaTipoComprobante()
        {
            return _consultaDL.ConsultaTipoComprobante();
        }

        public PedidoCabeceraBE ConsultaPedidoDetalle(int pPedidoCabeceraId)
        {
            return _consultaDL.ConsultaPedidoDetalle(pPedidoCabeceraId);
        }

        public List<EstadoBE> ConsultaEstado()
        {
            return _consultaDL.ConsultaEstado();
        }
    }
}
