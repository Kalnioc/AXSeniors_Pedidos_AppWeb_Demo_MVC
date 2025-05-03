using AXSeniors_Pedidos_Demo_ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSeniors_Pedidos_Demo_BUSINESS.Interfaces
{
    public interface IUpdateBL
    {
        int ActualizarProducto(ProductoBE pProductoBE);
        int ActualizarCliente(ClienteBE pClienteBE);
        int ActualizarPedidoCabecera(PedidoCabeceraBE pPedidoBE);
        int ActualizarTipoComprobante(TipoComprobanteBE pTipoBE);
        int ActualizarEstadoPedido(int pPedidoCabeceraId, int pEstadoId);
        void ActualizarStock(int pProductoId, int pCantidad, string pOperacion);
        int ActualizarPedidoDetalle(PedidoDetalleBE detalle);
        int EliminarProducto(int pProductoId);
        int EliminarCliente(int pClienteId);
        int EliminarPedidoDetalle(int detalleId);
        int EliminarTipoComprobante(int pTipoComprobanteId);
    }
}
