using AXSeniors_Pedidos_Demo_DATA.Connection;
using AXSeniors_Pedidos_Demo_ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AXSeniors_Pedidos_Demo_DATA
{
    public class UpdateDL
    {
        public int ActualizarProducto(ProductoBE pProductoBE)
        {
            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_tblproducto_update", cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@pProductoId", SqlDbType.Int)).Value = pProductoBE.ProductoId;
                        cmd.Parameters.Add(new SqlParameter("@pNombre", SqlDbType.NVarChar)).Value = pProductoBE.Nombre;
                        cmd.Parameters.Add(new SqlParameter("@pDescripcion", SqlDbType.NVarChar)).Value = pProductoBE.Descripcion;
                        cmd.Parameters.Add(new SqlParameter("@pPrecio", SqlDbType.Decimal)).Value = pProductoBE.Precio;
                        cmd.Parameters.Add(new SqlParameter("@pStock", SqlDbType.Int)).Value = pProductoBE.Stock;

                        cnx.Open();
                        return cmd.ExecuteNonQuery(); // Devuelve 1 si tuvo éxito
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int ActualizarCliente(ClienteBE pClienteBE)
        {
            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_tblcliente_update", cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.AddWithValue("@pClienteId", pClienteBE.ClienteId);
                        cmd.Parameters.AddWithValue("@pNombre", pClienteBE.Nombre);
                        cmd.Parameters.AddWithValue("@pApellido", pClienteBE.Apellido);
                        cmd.Parameters.AddWithValue("@pNumeroDocumento", pClienteBE.NumeroDocumento);
                        cmd.Parameters.AddWithValue("@pDireccion", pClienteBE.Direccion);
                        cmd.Parameters.AddWithValue("@pEmail", pClienteBE.Email);
                        cmd.Parameters.AddWithValue("@pTelefono", pClienteBE.Telefono);

                        cnx.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int ActualizarPedidoCabecera(PedidoCabeceraBE pPedidoBE)
        {
            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_tblpedidocabecera_update", cnx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@pPedidoCabeceraId", pPedidoBE.PedidoCabeceraId);
                        cmd.Parameters.AddWithValue("@pClienteId", pPedidoBE.ClienteId);
                        cmd.Parameters.AddWithValue("@pTipoComprobanteId", pPedidoBE.TipoComprobanteId);
                        cmd.Parameters.AddWithValue("@pEstadoId", pPedidoBE.EstadoId);
                        cmd.Parameters.AddWithValue("@pSubtotal", pPedidoBE.Subtotal);
                        cmd.Parameters.AddWithValue("@pImpuestos", pPedidoBE.Impuestos);
                        cmd.Parameters.AddWithValue("@pTotal", pPedidoBE.Total);

                        cnx.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }            
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int EliminarCliente(int pClienteId)
        {
            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_tblcliente_delete", cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@pClienteId", SqlDbType.Int)).Value = pClienteId;

                        cnx.Open();
                        return cmd.ExecuteNonQuery(); // Éxito = 1
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int EliminarProducto(int pProductoId)
        {
            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_tblproducto_delete", cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@pProductoId", SqlDbType.Int)).Value = pProductoId;

                        cnx.Open();
                        return cmd.ExecuteNonQuery(); // Devuelve 1 si tuvo éxito
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int ActualizarTipoComprobante(TipoComprobanteBE pTipoBE)
        {
            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_tbltipocomprobante_update", cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.AddWithValue("@pTipoComprobanteId", pTipoBE.TipoComprobanteId);
                        cmd.Parameters.AddWithValue("@pNombre", pTipoBE.Nombre);
                        cnx.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int EliminarTipoComprobante(int pTipoComprobanteId)
        {
            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_tbltipocomprobante_delete", cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.AddWithValue("@pTipoComprobanteId", pTipoComprobanteId);
                        cnx.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int ActualizarEstadoPedido(int pPedidoCabeceraId, int pEstadoId)
        {
            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_tblpedidocabecera_estado_update", cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.AddWithValue("@pPedidoCabeceraId", pPedidoCabeceraId);
                        cmd.Parameters.AddWithValue("@pEstadoId", pEstadoId);
                        cnx.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void ActualizarStock(int pProductoId, int pCantidad, string pOperacion)
        {
            using (SqlConnection cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
            {
                using (SqlCommand cmd = new SqlCommand("usp_tblproducto_stockactualiza", cnx))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pProductoId", pProductoId);
                    cmd.Parameters.AddWithValue("@pCantidad", pCantidad);
                    cmd.Parameters.AddWithValue("@pOperacion", pOperacion); // 'D' o 'I'

                    cnx.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int ActualizarPedidoDetalle(PedidoDetalleBE detalle)
        {
            try
            {
                using (var cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
                {
                    using (var cmd = new SqlCommand("usp_tblpedidodetalle_update", cnx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pPedidoDetalleId", detalle.PedidoDetalleId);
                        cmd.Parameters.AddWithValue("@pProductoId", detalle.ProductoId);
                        cmd.Parameters.AddWithValue("@pCantidadItem", detalle.CantidadItem);
                        cmd.Parameters.AddWithValue("@pPrecioUnitario", detalle.PrecioUnitario);
                        cmd.Parameters.AddWithValue("@pDescuentoItem", detalle.DescuentoItem);
                        cmd.Parameters.AddWithValue("@pTotalItem", detalle.TotalItem);
                        cnx.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int EliminarPedidoDetalle(int detalleId)
        {
            try
            {
                using (var cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
                {
                    using (var cmd = new SqlCommand("usp_tblpedidodetalle_delete", cnx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pPedidoDetalleId", detalleId);
                        cnx.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
