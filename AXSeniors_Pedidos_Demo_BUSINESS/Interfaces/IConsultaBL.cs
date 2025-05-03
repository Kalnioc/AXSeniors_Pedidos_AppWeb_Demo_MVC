using AXSeniors_Pedidos_Demo_ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSeniors_Pedidos_Demo_BUSINESS.Interfaces
{
    public interface IConsultaBL
    {
        List<ProductoBE> ConsultaProducto();
        List<PedidoCabeceraBE> ConsultaPedidoCabecera();
        List<ClienteBE> ConsultaCliente();
        List<TipoComprobanteBE> ConsultaTipoComprobante();
        PedidoCabeceraBE ConsultaPedidoDetalle(int pPedidoCabeceraId);
        List<EstadoBE> ConsultaEstado();
    }
}
