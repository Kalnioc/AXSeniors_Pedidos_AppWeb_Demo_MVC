using AXSeniors_Pedidos_Demo_BUSINESS;
using AXSeniors_Pedidos_Demo_ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AXSeniors_Pedidos_Demo_MVC.Controllers
{
    public class PedidoController : Controller
    {
        ConsultaBL consultaBL = new ConsultaBL();
        InsertBL insertBL = new InsertBL();
        UpdateBL updateBL = new UpdateBL();

        // GET: Pedido
        public ActionResult Index()
        {
            List<PedidoCabeceraBE> wPedidoCabeceraListBE = consultaBL.ConsultaPedidoCabecera();
            return View(wPedidoCabeceraListBE);
        }

        public ActionResult Agregar()
        {
            ViewBag.Clientes = consultaBL.ConsultaCliente() ?? new List<ClienteBE>();
            ViewBag.Comprobantes = consultaBL.ConsultaTipoComprobante() ?? new List<TipoComprobanteBE>();
            ViewBag.Productos = consultaBL.ConsultaProducto() ?? new List<ProductoBE>();

            return View();
        }

        public ActionResult Details(int id)
        {
            PedidoCabeceraBE wPedidoCabeceraBE = consultaBL.ConsultaPedidoDetalle(id);
            return View(wPedidoCabeceraBE);
        }

        public ActionResult Edit(int id)
        {
            var wPedido = consultaBL.ConsultaPedidoDetalle(id);
            ViewBag.Clientes = consultaBL.ConsultaCliente();
            ViewBag.Comprobantes = consultaBL.ConsultaTipoComprobante();
            ViewBag.Estados = consultaBL.ConsultaEstado();
            ViewBag.Productos = consultaBL.ConsultaProducto();
            return View("Editar", wPedido);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            try
            {
                PedidoCabeceraBE wPedidoCabeceraBE = new PedidoCabeceraBE();
                List<PedidoDetalleBE> wNuevos = new List<PedidoDetalleBE>();
                List<PedidoDetalleBE> wEditados = new List<PedidoDetalleBE>();
                List<PedidoDetalleBE> wEliminados = new List<PedidoDetalleBE>();

                wPedidoCabeceraBE.PedidoCabeceraId = Convert.ToInt32(form["PedidoCabeceraId"]);
                wPedidoCabeceraBE.ClienteId = Convert.ToInt32(form["ClienteId"]);
                wPedidoCabeceraBE.TipoComprobanteId = Convert.ToInt32(form["TipoComprobanteId"]);
                wPedidoCabeceraBE.EstadoId = Convert.ToInt32(form["EstadoId"]);

                string[] detalleIds = form.GetValues("PedidoDetalleId[]");
                string[] estados = form.GetValues("Estado[]");
                string[] productos = form.GetValues("ProductoId[]");
                string[] cantidades = form.GetValues("CantidadItem[]");
                string[] precios = form.GetValues("PrecioUnitario[]");
                string[] descuentos = form.GetValues("DescuentoItem[]");
                string[] totales = form.GetValues("TotalItem[]");

                decimal wSubtotal = 0;

                for (int i = 0; i < productos.Length; i++)
                {
                    var estado = estados[i];
                    var id = Convert.ToInt32(detalleIds[i]);
                    var productoId = Convert.ToInt32(productos[i]);
                    var cantidad = Convert.ToInt32(cantidades[i]);

                    if (estado == "eliminar")
                    {
                        if (id > 0)
                        {
                            wEliminados.Add(new PedidoDetalleBE
                            {
                                PedidoDetalleId = id,
                                ProductoId = productoId,
                                CantidadItem = cantidad
                            });
                        }
                        continue;
                    }

                    // Validación de stock antes de agregar nuevo producto
                    if (estado == "nuevo")
                    {
                        var stockDisponible = consultaBL.ConsultaProducto()
                            .FirstOrDefault(p => p.ProductoId == productoId)?.Stock ?? 0;

                        if (cantidad > stockDisponible)
                        {
                            ModelState.AddModelError("", $"No hay suficiente stock para el producto ID {productoId}. Stock disponible: {stockDisponible}");
                            ViewBag.Clientes = consultaBL.ConsultaCliente();
                            ViewBag.Comprobantes = consultaBL.ConsultaTipoComprobante();
                            ViewBag.Productos = consultaBL.ConsultaProducto();
                            ViewBag.Estados = consultaBL.ConsultaEstado();
                            return View("Editar", consultaBL.ConsultaPedidoDetalle(wPedidoCabeceraBE.PedidoCabeceraId));
                        }
                    }

                    var detalle = new PedidoDetalleBE
                    {
                        PedidoDetalleId = id,
                        PedidoCabeceraId = wPedidoCabeceraBE.PedidoCabeceraId,
                        ProductoId = productoId,
                        CantidadItem = cantidad,
                        PrecioUnitario = Convert.ToDecimal(precios[i]),
                        DescuentoItem = Convert.ToInt32(descuentos[i]),
                        TotalItem = Convert.ToDecimal(totales[i])
                    };

                    wSubtotal += detalle.TotalItem;

                    if (estado == "nuevo")
                        wNuevos.Add(detalle);
                    else if (estado == "modificar")
                        wEditados.Add(detalle);
                }

                wPedidoCabeceraBE.Subtotal = wSubtotal;
                wPedidoCabeceraBE.Impuestos = Math.Round(wSubtotal * 0.18M, 2);
                wPedidoCabeceraBE.Total = wPedidoCabeceraBE.Subtotal + wPedidoCabeceraBE.Impuestos;

                // Actualizar cabecera con totales
                updateBL.ActualizarPedidoCabecera(wPedidoCabeceraBE);

                // Insertar nuevos
                insertBL.InsertarPedidoDetalle(wNuevos);
                foreach (var item in wNuevos)
                    updateBL.ActualizarStock(item.ProductoId, item.CantidadItem, "D");

                // Actualizar modificados
                foreach (var item in wEditados)
                    updateBL.ActualizarPedidoDetalle(item);

                // Eliminar líneas y devolver stock
                foreach (var item in wEliminados)
                {
                    updateBL.EliminarPedidoDetalle(item.PedidoDetalleId);
                    updateBL.ActualizarStock(item.ProductoId, item.CantidadItem, "I");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al actualizar: " + ex.Message;
                ViewBag.Clientes = consultaBL.ConsultaCliente();
                ViewBag.Comprobantes = consultaBL.ConsultaTipoComprobante();
                ViewBag.Productos = consultaBL.ConsultaProducto();
                ViewBag.Estados = consultaBL.ConsultaEstado();
                return View("Editar", consultaBL.ConsultaPedidoDetalle(Convert.ToInt32(form["PedidoCabeceraId"])));
            }
        }

        [HttpPost]
        public ActionResult Agregar(FormCollection form)
        {
            try
            {
                PedidoCabeceraBE wPedidoCabeceraBE = new PedidoCabeceraBE();
                List<PedidoDetalleBE> wPedidoDetalleListBE = new List<PedidoDetalleBE>();

                wPedidoCabeceraBE.ClienteId = Convert.ToInt32(form["ClienteId"]);
                wPedidoCabeceraBE.TipoComprobanteId = Convert.ToInt32(form["TipoComprobanteId"]);
                wPedidoCabeceraBE.EstadoId = Convert.ToInt32(form["EstadoId"] ?? "1");

                // Extraer arrays
                string[] productos = form.GetValues("ProductoId");
                string[] cantidades = form.GetValues("CantidadItem");
                string[] precios = form.GetValues("PrecioUnitario");
                string[] descuentos = form.GetValues("DescuentoItem");
                string[] totales = form.GetValues("TotalItem");

                decimal wSubtotal = 0;

                var listaProductos = consultaBL.ConsultaProducto();

                // Validación de stock antes de guardar
                for (int i = 0; i < productos.Length; i++)
                {
                    int productoId = Convert.ToInt32(productos[i]);
                    int cantidad = Convert.ToInt32(cantidades[i]);

                    var stockDisponible = listaProductos.FirstOrDefault(p => p.ProductoId == productoId)?.Stock ?? 0;

                    if (cantidad > stockDisponible)
                    {
                        ModelState.AddModelError("", $"Stock insuficiente para el producto ID {productoId}. Stock disponible: {stockDisponible}");

                        ViewBag.Clientes = consultaBL.ConsultaCliente();
                        ViewBag.Comprobantes = consultaBL.ConsultaTipoComprobante();
                        ViewBag.Productos = listaProductos;
                        ViewBag.Estados = consultaBL.ConsultaEstado();

                        return View();
                    }

                    PedidoDetalleBE wDetalleBE = new PedidoDetalleBE
                    {
                        ProductoId = productoId,
                        CantidadItem = cantidad,
                        PrecioUnitario = Convert.ToDecimal(precios[i]),
                        DescuentoItem = Convert.ToInt32(descuentos[i]),
                        TotalItem = Convert.ToDecimal(totales[i])
                    };

                    wSubtotal += wDetalleBE.TotalItem;
                    wPedidoDetalleListBE.Add(wDetalleBE);
                }

                wPedidoCabeceraBE.Subtotal = wSubtotal;
                wPedidoCabeceraBE.Impuestos = Math.Round(wSubtotal * 0.18M, 2);
                wPedidoCabeceraBE.Total = wPedidoCabeceraBE.Subtotal + wPedidoCabeceraBE.Impuestos;

                int wPedidoCabeceraId = insertBL.InsertarPedidoCabecera(wPedidoCabeceraBE);

                foreach (var wDetalleBE in wPedidoDetalleListBE)
                {
                    wDetalleBE.PedidoCabeceraId = wPedidoCabeceraId;
                }

                insertBL.InsertarPedidoDetalle(wPedidoDetalleListBE);

                foreach (var wDetalleBE in wPedidoDetalleListBE)
                {
                    updateBL.ActualizarStock(wDetalleBE.ProductoId, wDetalleBE.CantidadItem, "D");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al guardar el pedido: " + ex.Message;
                ViewBag.Clientes = consultaBL.ConsultaCliente();
                ViewBag.Comprobantes = consultaBL.ConsultaTipoComprobante();
                ViewBag.Productos = consultaBL.ConsultaProducto();
                ViewBag.Estados = consultaBL.ConsultaEstado();
                return View();
            }
        }
    }
}