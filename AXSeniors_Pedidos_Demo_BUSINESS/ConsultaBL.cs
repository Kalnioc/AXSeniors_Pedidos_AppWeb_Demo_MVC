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
        private readonly ConsultaDL consultaDL = new ConsultaDL();
        public List<ProductoBE> ConsultaProducto()
        {          
            return consultaDL.ConsultaProducto();
        }

        public List<PedidoCabeceraBE> ConsultaPedidoCabecera()
        {
            return consultaDL.ConsultaPedidoCabecera();
        }

        public List<ClienteBE> ConsultaCliente()
        {
            return consultaDL.ConsultaCliente();
        }

        public List<TipoComprobanteBE> ConsultaTipoComprobante()
        {
            return consultaDL.ConsultaTipoComprobante();
        }

        public PedidoCabeceraBE ConsultaPedidoDetalle(int pPedidoCabeceraId)
        {
            return consultaDL.ConsultaPedidoDetalle(pPedidoCabeceraId);
        }

        public List<EstadoBE> ConsultaEstado()
        {
            return consultaDL.ConsultaEstado();
        }
    }
}
