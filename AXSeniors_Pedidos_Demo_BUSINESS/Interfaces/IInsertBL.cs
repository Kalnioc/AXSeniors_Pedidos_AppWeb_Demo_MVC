using AXSeniors_Pedidos_Demo_ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSeniors_Pedidos_Demo_BUSINESS.Interfaces
{
    public interface IInsertBL
    {
        int InsertarProducto(ProductoBE pProductoBE);
        int InsertarPedidoCabecera(PedidoCabeceraBE pPedidoCabeceraBE);
        int InsertarCliente(ClienteBE pClienteBE);
        int InsertarTipoComprobante(TipoComprobanteBE pTipoBE);
        void InsertarPedidoDetalle(List<PedidoDetalleBE> detalles);
    }
}
