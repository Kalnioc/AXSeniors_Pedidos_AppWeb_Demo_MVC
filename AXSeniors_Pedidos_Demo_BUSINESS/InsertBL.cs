using AXSeniors_Pedidos_Demo_DATA;
using AXSeniors_Pedidos_Demo_ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSeniors_Pedidos_Demo_BUSINESS
{
    public class InsertBL
    {
        InsertDL insertDL = new InsertDL();

        public int InsertarProducto(ProductoBE pProductoBE)
        {
            return insertDL.InsertarProducto(pProductoBE);
        }

        public int InsertarPedidoCabecera(PedidoCabeceraBE pPedidoCabeceraBE)
        {
            return insertDL.InsertarPedidoCabecera(pPedidoCabeceraBE);
        }

        //public void InsertarPedidoDetalle(List<PedidoDetalleBE> pPedidoDetalleListBE)
        //{
        //    foreach (PedidoDetalleBE wPedidoDetalleBE in pPedidoDetalleListBE)
        //    {
        //        insertDL.InsertarPedidoDetalle(wPedidoDetalleBE);
        //    }
        //}
        public void InsertarPedidoDetalle(List<PedidoDetalleBE> detalles)
        {
            insertDL.InsertarPedidoDetalle(detalles);
        }

        public int InsertarCliente(ClienteBE pClienteBE)
        {
            return insertDL.InsertarCliente(pClienteBE);
        }
        public int InsertarTipoComprobante(TipoComprobanteBE pTipoBE)
        {
            return insertDL.InsertarTipoComprobante(pTipoBE);
        }
    }
}
