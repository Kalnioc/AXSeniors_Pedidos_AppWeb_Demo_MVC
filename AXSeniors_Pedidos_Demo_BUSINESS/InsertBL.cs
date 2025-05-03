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
    public class InsertBL : IInsertBL
    {
        private readonly InsertDL _insertDL = new InsertDL();

        public int InsertarProducto(ProductoBE pProductoBE)
        {
            return _insertDL.InsertarProducto(pProductoBE);
        }

        public int InsertarPedidoCabecera(PedidoCabeceraBE pPedidoCabeceraBE)
        {
            return _insertDL.InsertarPedidoCabecera(pPedidoCabeceraBE);
        }

        public void InsertarPedidoDetalle(List<PedidoDetalleBE> detalles)
        {
            _insertDL.InsertarPedidoDetalle(detalles);
        }

        public int InsertarCliente(ClienteBE pClienteBE)
        {
            return _insertDL.InsertarCliente(pClienteBE);
        }

        public int InsertarTipoComprobante(TipoComprobanteBE pTipoBE)
        {
            return _insertDL.InsertarTipoComprobante(pTipoBE);
        }
    }

}
