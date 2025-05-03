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
    public class InsertDL
    {
        ProductoBE productoBE = new ProductoBE();
        public int InsertarProducto(ProductoBE pProductoBE)
        {
            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_tblproducto_insert", cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@pNombre", SqlDbType.VarChar)).Value = pProductoBE.Nombre;
                        cmd.Parameters.Add(new SqlParameter("@pDescripcion", SqlDbType.VarChar)).Value = pProductoBE.Descripcion;
                        cmd.Parameters.Add(new SqlParameter("@pPrecio", SqlDbType.Decimal)).Value = pProductoBE.Precio;
                        cmd.Parameters.Add(new SqlParameter("@pStock", SqlDbType.Int)).Value = pProductoBE.Stock;

                        cnx.Open();
                        int wResult = Convert.ToInt32(cmd.ExecuteScalar());
                        return wResult;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int InsertarPedidoCabecera(PedidoCabeceraBE pPedidoCabeceraBE)
        {
            int wPedidoCabeceraId = 0;

            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_tblpedidocabecera_insert", cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@pClienteId", SqlDbType.Int)).Value = pPedidoCabeceraBE.ClienteId;
                        cmd.Parameters.Add(new SqlParameter("@pTipoComprobanteId", SqlDbType.Int)).Value = pPedidoCabeceraBE.TipoComprobanteId;
                        cmd.Parameters.Add(new SqlParameter("@pSubtotal", SqlDbType.Decimal)).Value = pPedidoCabeceraBE.Subtotal;
                        cmd.Parameters.Add(new SqlParameter("@pImpuestos", SqlDbType.Decimal)).Value = pPedidoCabeceraBE.Impuestos;
                        cmd.Parameters.Add(new SqlParameter("@pTotal", SqlDbType.Decimal)).Value = pPedidoCabeceraBE.Total;
                        cmd.Parameters.Add(new SqlParameter("@pEstadoId", SqlDbType.Int)).Value = pPedidoCabeceraBE.EstadoId;

                        SqlParameter outputId = new SqlParameter("@pPedidoCabeceraId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputId);

                        cnx.Open();
                        cmd.ExecuteNonQuery();

                        wPedidoCabeceraId = Convert.ToInt32(outputId.Value);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return wPedidoCabeceraId;
        }

        //public void InsertarPedidoDetalle(PedidoDetalleBE pPedidoDetalleBE)
        //{
        //    try
        //    {
        //        using (SqlConnection cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
        //        {
        //            using (SqlCommand cmd = new SqlCommand("usp_tblpedidodetalle_insert", cnx) { CommandType = CommandType.StoredProcedure })
        //            {
        //                cmd.Parameters.Add(new SqlParameter("@pPedidoCabeceraId", SqlDbType.Int)).Value = pPedidoDetalleBE.PedidoCabeceraId;
        //                cmd.Parameters.Add(new SqlParameter("@pProductoId", SqlDbType.Int)).Value = pPedidoDetalleBE.ProductoId;
        //                cmd.Parameters.Add(new SqlParameter("@pCantidadItem", SqlDbType.Int)).Value = pPedidoDetalleBE.CantidadItem;
        //                cmd.Parameters.Add(new SqlParameter("@pPrecioUnitario", SqlDbType.Decimal)).Value = pPedidoDetalleBE.PrecioUnitario;
        //                cmd.Parameters.Add(new SqlParameter("@pDescuentoItem", SqlDbType.Int)).Value = pPedidoDetalleBE.DescuentoItem;
        //                cmd.Parameters.Add(new SqlParameter("@pTotalItem", SqlDbType.Decimal)).Value = pPedidoDetalleBE.TotalItem;

        //                cnx.Open();
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public void InsertarPedidoDetalle(List<PedidoDetalleBE> detalles)
        {
            using (SqlConnection cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
            {
                cnx.Open();
                foreach (var det in detalles)
                {
                    using (SqlCommand cmd = new SqlCommand("usp_tblpedidodetalle_insert", cnx))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pPedidoCabeceraId", det.PedidoCabeceraId);
                        cmd.Parameters.AddWithValue("@pProductoId", det.ProductoId);
                        cmd.Parameters.AddWithValue("@pCantidadItem", det.CantidadItem);
                        cmd.Parameters.AddWithValue("@pPrecioUnitario", det.PrecioUnitario);
                        cmd.Parameters.AddWithValue("@pDescuentoItem", det.DescuentoItem);
                        cmd.Parameters.AddWithValue("@pTotalItem", det.TotalItem);

                        SqlParameter outputId = new SqlParameter("@pPedidoDetalleId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputId);

                        cmd.ExecuteNonQuery();
                        det.PedidoDetalleId = (int)outputId.Value; // capturamos el ID generado
                    }
                }
            }
        }

        public int InsertarCliente(ClienteBE pClienteBE)
        {
            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_tblcliente_insert", cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@pNombre", SqlDbType.NVarChar)).Value = pClienteBE.Nombre;
                        cmd.Parameters.Add(new SqlParameter("@pApellido", SqlDbType.NVarChar)).Value = pClienteBE.Apellido;
                        cmd.Parameters.Add(new SqlParameter("@pNumeroDocumento", SqlDbType.VarChar)).Value = pClienteBE.NumeroDocumento;
                        cmd.Parameters.Add(new SqlParameter("@pDireccion", SqlDbType.NVarChar)).Value = pClienteBE.Direccion;
                        cmd.Parameters.Add(new SqlParameter("@pEmail", SqlDbType.NVarChar)).Value = pClienteBE.Email;
                        cmd.Parameters.Add(new SqlParameter("@pTelefono", SqlDbType.VarChar)).Value = pClienteBE.Telefono;

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
        public int InsertarTipoComprobante(TipoComprobanteBE pTipoBE)
        {
            using (SqlConnection cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
            {
                using (SqlCommand cmd = new SqlCommand("usp_tbltipocomprobante_insert", cnx) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.AddWithValue("@pNombre", pTipoBE.Nombre);
                    cnx.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
