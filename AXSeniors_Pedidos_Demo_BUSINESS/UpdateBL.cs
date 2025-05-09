﻿using AXSeniors_Pedidos_Demo_BUSINESS.Interfaces;
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
        private readonly UpdateDL _updateDL = new UpdateDL();

        public int ActualizarProducto(ProductoBE pProductoBE)
        {
            return _updateDL.ActualizarProducto(pProductoBE);
        }

        public int ActualizarCliente(ClienteBE pClienteBE)
        {
            return _updateDL.ActualizarCliente(pClienteBE);
        }

        public int ActualizarPedidoCabecera(PedidoCabeceraBE pPedidoBE)
        {
            return _updateDL.ActualizarPedidoCabecera(pPedidoBE);
        }
        public int ActualizarTipoComprobante(TipoComprobanteBE pTipoBE)
        {
            return _updateDL.ActualizarTipoComprobante(pTipoBE);
        }

        public int ActualizarEstadoPedido(int pPedidoCabeceraId, int pEstadoId)
        {
            return _updateDL.ActualizarEstadoPedido(pPedidoCabeceraId, pEstadoId);
        }
        public void ActualizarStock(int pProductoId, int pCantidad, string pOperacion)
        {
            _updateDL.ActualizarStock(pProductoId, pCantidad, pOperacion);
        }

        public int ActualizarPedidoDetalle(PedidoDetalleBE detalle)
        {
            return _updateDL.ActualizarPedidoDetalle(detalle);
        }
        public int EliminarProducto(int pProductoId)
        {
            return _updateDL.EliminarProducto(pProductoId);
        }
        public int EliminarCliente(int pClienteId)
        {
            return _updateDL.EliminarCliente(pClienteId);
        }

        public int EliminarPedidoDetalle(int detalleId)
        {
            return _updateDL.EliminarPedidoDetalle(detalleId);
        }
        public int EliminarTipoComprobante(int pTipoComprobanteId)
        {
            return _updateDL.EliminarTipoComprobante(pTipoComprobanteId);
        }
    }
}
