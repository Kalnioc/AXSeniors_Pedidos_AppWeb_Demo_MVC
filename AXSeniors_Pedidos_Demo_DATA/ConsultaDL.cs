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
    public class ConsultaDL
    {
        public List<ProductoBE> ConsultaProducto()
        {
            List<ProductoBE> wProductoListBE = new List<ProductoBE>();

            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_tblproducto_consulta", cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cnx.Open();

                        using (SqlDataReader dtr = cmd.ExecuteReader())
                        {
                            if (dtr.HasRows)
                            {
                                while (dtr.Read())
                                {
                                    ProductoBE productoBE = new ProductoBE
                                    {
                                        ProductoId = Convert.ToInt32(dtr["ProductoId"]),
                                        Nombre = Convert.ToString(dtr["Nombre"]),
                                        Descripcion = Convert.ToString(dtr["Descripcion"]),
                                        Precio = Convert.ToDecimal(dtr["Precio"]),
                                        Stock = Convert.ToInt32(dtr["Stock"]),
                                        FechaRegistro = Convert.ToDateTime(dtr["FechaRegistro"]),
                                        FechaModificacion = Convert.ToDateTime(dtr["FechaModificacion"]),
                                    };

                                    wProductoListBE.Add(productoBE);
                                }
                                dtr.NextResult();
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return wProductoListBE;
        }

        public List<PedidoCabeceraBE> ConsultaPedidoCabecera()
        {
            List<PedidoCabeceraBE> wPedidoCabeceraListBE = new List<PedidoCabeceraBE>();

            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_tblpedidocabecera_consulta", cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cnx.Open();

                        using (SqlDataReader dtr = cmd.ExecuteReader())
                        {
                            if (dtr.HasRows)
                            {
                                while (dtr.Read())
                                {
                                    PedidoCabeceraBE wPedidoCabeceraBE = new PedidoCabeceraBE
                                    {
                                        PedidoCabeceraId = Convert.ToInt32(dtr["PedidoCabeceraId"]),
                                        FechaPedido = Convert.ToDateTime(dtr["FechaPedido"]),
                                        Subtotal = Convert.ToDecimal(dtr["Subtotal"]),
                                        Impuestos = Convert.ToDecimal(dtr["Impuestos"]),
                                        Total = Convert.ToDecimal(dtr["Total"]),
                                        Cliente = new ClienteBE
                                        {
                                            Nombre = Convert.ToString(dtr["Cliente"])                                            
                                        },
                                        EstadoNombre = Convert.ToString(dtr["Estado"])
                                    };

                                    wPedidoCabeceraListBE.Add(wPedidoCabeceraBE);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return wPedidoCabeceraListBE;
        }

        public List<ClienteBE> ConsultaCliente()
        {
            List<ClienteBE> wClienteListBE = new List<ClienteBE>();

            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_tblcliente_consulta", cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cnx.Open();
                        using (SqlDataReader dtr = cmd.ExecuteReader())
                        {
                            while (dtr.Read())
                            {
                                ClienteBE wClienteBE = new ClienteBE
                                {
                                    ClienteId = Convert.ToInt32(dtr["ClienteId"]),
                                    Nombre = Convert.ToString(dtr["Nombre"]),
                                    Apellido = Convert.ToString(dtr["Apellido"]),
                                    NombreCompleto = Convert.ToString(dtr["Nombre"]) + " " + Convert.ToString(dtr["Apellido"]),
                                    NumeroDocumento = Convert.ToString(dtr["NumeroDocumento"]),
                                    Direccion = Convert.ToString(dtr["Direccion"]),
                                    Email = Convert.ToString(dtr["Email"]),
                                    Telefono = Convert.ToString(dtr["Telefono"]),
                                };
                                wClienteListBE.Add(wClienteBE);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return wClienteListBE;
        }

        public List<TipoComprobanteBE> ConsultaTipoComprobante()
        {
            List<TipoComprobanteBE> wTipoComprobanteListBE = new List<TipoComprobanteBE>();

            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_tbltipocomprobante_consulta", cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cnx.Open();
                        using (SqlDataReader dtr = cmd.ExecuteReader())
                        {
                            while (dtr.Read())
                            {
                                TipoComprobanteBE wTipoComprobanteBE = new TipoComprobanteBE
                                {
                                    TipoComprobanteId = Convert.ToInt32(dtr["TipoComprobanteId"]),
                                    Nombre = Convert.ToString(dtr["Nombre"])
                                };
                                wTipoComprobanteListBE.Add(wTipoComprobanteBE);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return wTipoComprobanteListBE;
        }

        public PedidoCabeceraBE ConsultaPedidoDetalle(int pPedidoCabeceraId)
        {
            PedidoCabeceraBE wPedidoCabeceraBE = new PedidoCabeceraBE();
            List<PedidoDetalleBE> wDetalleListBE = new List<PedidoDetalleBE>();

            try
            {
                using (SqlConnection cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_tblpedidocabecera_detalle_consulta", cnx) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.AddWithValue("@pPedidoCabeceraId", pPedidoCabeceraId);
                        cnx.Open();

                        using (SqlDataReader dtr = cmd.ExecuteReader())
                        {
                            if (dtr.Read())
                            {
                                wPedidoCabeceraBE.PedidoCabeceraId = pPedidoCabeceraId;
                                wPedidoCabeceraBE.FechaPedido = Convert.ToDateTime(dtr["FechaPedido"]);

                                wPedidoCabeceraBE.ClienteId = Convert.ToInt32(dtr["ClienteId"]);
                                wPedidoCabeceraBE.Cliente = new ClienteBE
                                {
                                    NombreCompleto = Convert.ToString(dtr["Cliente"])
                                };

                                wPedidoCabeceraBE.TipoComprobanteId = Convert.ToInt32(dtr["TipoComprobanteId"]);
                                wPedidoCabeceraBE.Subtotal = Convert.ToDecimal(dtr["Subtotal"]);
                                wPedidoCabeceraBE.Impuestos = Convert.ToDecimal(dtr["Impuestos"]);
                                wPedidoCabeceraBE.Total = Convert.ToDecimal(dtr["Total"]);
                                wPedidoCabeceraBE.EstadoId = Convert.ToInt32(dtr["EstadoId"]);
                                wPedidoCabeceraBE.EstadoNombre = Convert.ToString(dtr["Estado"]);
                            }

                            if (dtr.NextResult())
                            {
                                while (dtr.Read())
                                {
                                    PedidoDetalleBE wDetalle = new PedidoDetalleBE
                                    {
                                        PedidoDetalleId = Convert.ToInt32(dtr["PedidoDetalleId"]),
                                        ProductoId = Convert.ToInt32(dtr["ProductoId"]),
                                        Producto = new ProductoBE { Nombre = Convert.ToString(dtr["Producto"]) },
                                        CantidadItem = Convert.ToInt32(dtr["CantidadItem"]),
                                        PrecioUnitario = Convert.ToDecimal(dtr["PrecioUnitario"]),
                                        DescuentoItem = Convert.ToInt32(dtr["DescuentoItem"]),
                                        TotalItem = Convert.ToDecimal(dtr["TotalItem"])
                                    };
                                    wDetalleListBE.Add(wDetalle);
                                }
                            }
                        }
                    }
                }

                wPedidoCabeceraBE.PedidoDetalleLista = wDetalleListBE;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

            return wPedidoCabeceraBE;
        }
        public List<EstadoBE> ConsultaEstado()
        {
            List<EstadoBE> wEstadoListBE = new List<EstadoBE>();

            using (SqlConnection cnx = new SqlConnection(ConnectionDL.Conexion(ConnectionDL.EConexion.PruebaAXSeniorConnection)))
            {
                using (SqlCommand cmd = new SqlCommand("usp_tblestado_consulta", cnx) { CommandType = CommandType.StoredProcedure })
                {
                    cnx.Open();
                    using (SqlDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            EstadoBE wEstadoBE = new EstadoBE
                            {
                                EstadoId = Convert.ToInt32(dtr["EstadoId"]),
                                Nombre = Convert.ToString(dtr["Nombre"])
                            };
                            wEstadoListBE.Add(wEstadoBE);
                        }
                    }
                }
            }

            return wEstadoListBE;
        }

    }
}