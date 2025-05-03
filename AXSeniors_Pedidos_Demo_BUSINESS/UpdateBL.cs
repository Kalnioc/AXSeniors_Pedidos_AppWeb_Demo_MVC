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
    public class UpdateBL: IUpdateBL
    {
        private readonly UpdateDL updateDL = new UpdateDL();

        public int ActualizarProducto(ProductoBE pProductoBE)
        {
            return updateDL.ActualizarProducto(pProductoBE);
        }

        public int ActualizarCliente(ClienteBE pClienteBE)
        {
            return updateDL.ActualizarCliente(pClienteBE);
        }

        public int ActualizarPedidoCabecera(PedidoCabeceraBE pPedidoBE)
        {
            return updateDL.ActualizarPedidoCabecera(pPedidoBE);
        }
        public int ActualizarTipoComprobante(TipoComprobanteBE pTipoBE)
        {
            return updateDL.ActualizarTipoComprobante(pTipoBE);
        }

        public int ActualizarEstadoPedido(int pPedidoCabeceraId, int pEstadoId)
        {
            return updateDL.ActualizarEstadoPedido(pPedidoCabeceraId, pEstadoId);
        }
        public void ActualizarStock(int pProductoId, int pCantidad, string pOperacion)
        {
            updateDL.ActualizarStock(pProductoId, pCantidad, pOperacion);
        }

        public int ActualizarPedidoDetalle(PedidoDetalleBE detalle)
        {
            return updateDL.ActualizarPedidoDetalle(detalle);
        }
        public int EliminarProducto(int pProductoId)
        {
            return updateDL.EliminarProducto(pProductoId);
        }
        public int EliminarCliente(int pClienteId)
        {
            return updateDL.EliminarCliente(pClienteId);
        }

        public int EliminarPedidoDetalle(int detalleId)
        {
            return updateDL.EliminarPedidoDetalle(detalleId);
        }
        public int EliminarTipoComprobante(int pTipoComprobanteId)
        {
            return updateDL.EliminarTipoComprobante(pTipoComprobanteId);
        }
    }
}
