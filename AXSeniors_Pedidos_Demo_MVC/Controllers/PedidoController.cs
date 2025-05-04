using AXSeniors_Pedidos_Demo_BUSINESS;
using AXSeniors_Pedidos_Demo_BUSINESS.Interfaces;
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
        private readonly IConsultaBL _consultaBL;
        private readonly IInsertBL _insertBL;
        private readonly IUpdateBL _updateBL;
        public PedidoController(ConsultaBL consultaBL, InsertBL insertBL, UpdateBL updateBL)
        {
            _consultaBL = consultaBL;
            _insertBL = insertBL;
            _updateBL = updateBL;
        }

        public ActionResult Index()
        {
            List<PedidoCabeceraBE> pedidoCabeceraBE = _consultaBL.ConsultaPedidoCabecera();
            return View(pedidoCabeceraBE);
        }

        public ActionResult Agregar()
        {
            ViewBag.Clientes = _consultaBL.ConsultaCliente() ?? new List<ClienteBE>();
            ViewBag.Comprobantes = _consultaBL.ConsultaTipoComprobante() ?? new List<TipoComprobanteBE>();
            ViewBag.Productos = _consultaBL.ConsultaProducto() ?? new List<ProductoBE>();

            return View();
        }

        public ActionResult Details(int id)
        {
            PedidoCabeceraBE pedidoCabeceraBE = _consultaBL.ConsultaPedidoDetalle(id);
            return View(pedidoCabeceraBE);
        }

        public ActionResult Edit(int id)
        {
            var pedido = _consultaBL.ConsultaPedidoDetalle(id);
            ViewBag.Clientes = _consultaBL.ConsultaCliente();
            ViewBag.Comprobantes = _consultaBL.ConsultaTipoComprobante();
            ViewBag.Estados = _consultaBL.ConsultaEstado();
            ViewBag.Productos = _consultaBL.ConsultaProducto();
            return View("Editar", pedido);
        }

        private ActionResult ReenviarVistaConError(int pedidoId, List<ProductoBE> listaProductos)
        {
            ViewBag.Clientes = _consultaBL.ConsultaCliente();
            ViewBag.Comprobantes = _consultaBL.ConsultaTipoComprobante();
            ViewBag.Productos = listaProductos;
            ViewBag.Estados = _consultaBL.ConsultaEstado();
            return View("Editar", _consultaBL.ConsultaPedidoDetalle(pedidoId));
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            try
            {
                PedidoCabeceraBE pedidoCabeceraBE = new PedidoCabeceraBE();
                List<PedidoDetalleBE> nuevosPedidoDetalleBE = new List<PedidoDetalleBE>();
                List<PedidoDetalleBE> editadosPedidoDetalleBE = new List<PedidoDetalleBE>();
                List<PedidoDetalleBE> eliminadosDetalleBE = new List<PedidoDetalleBE>();

                pedidoCabeceraBE.PedidoCabeceraId = Convert.ToInt32(form["PedidoCabeceraId"]);
                pedidoCabeceraBE.ClienteId = Convert.ToInt32(form["ClienteId"]);
                pedidoCabeceraBE.TipoComprobanteId = Convert.ToInt32(form["TipoComprobanteId"]);
                pedidoCabeceraBE.EstadoId = Convert.ToInt32(form["EstadoId"]);

                string[] detalleIds = form.GetValues("PedidoDetalleId[]");
                string[] estados = form.GetValues("Estado[]");
                string[] productos = form.GetValues("ProductoId[]");
                string[] cantidades = form.GetValues("CantidadItem[]");
                string[] cantidadesOriginales = form.GetValues("CantidadItemOriginal[]");
                string[] precios = form.GetValues("PrecioUnitario[]");
                string[] descuentos = form.GetValues("DescuentoItem[]");
                string[] totales = form.GetValues("TotalItem[]");

                decimal wSubtotal = 0;

                var listaProductos = _consultaBL.ConsultaProducto();

                for (int i = 0; i < productos.Length; i++)
                {
                    var estado = estados[i];
                    var detalleId = Convert.ToInt32(detalleIds[i]);
                    var productoId = Convert.ToInt32(productos[i]);
                    var cantidad = Convert.ToInt32(cantidades[i]);
                    var cantidadOriginal = Convert.ToInt32(cantidadesOriginales[i]);

                    if (estado == "eliminar")
                    {
                        if (detalleId > 0)
                        {
                            eliminadosDetalleBE.Add(new PedidoDetalleBE
                            {
                                PedidoDetalleId = detalleId,
                                ProductoId = productoId,
                                CantidadItem = cantidad
                            });
                        }
                        continue;
                    }

                    var producto = listaProductos.FirstOrDefault(p => p.ProductoId == productoId);
                    if (producto == null)
                    {
                        ModelState.AddModelError("", $"Producto con ID {productoId} no encontrado.");
                        return ReenviarVistaConError(pedidoCabeceraBE.PedidoCabeceraId, listaProductos);
                    }

                    if (estado == "nuevo" && cantidad > producto.Stock)
                    {
                        ModelState.AddModelError("", $"Stock insuficiente para el producto '{producto.Nombre}'. Disponible: {producto.Stock}, solicitado: {cantidad}.");
                        return ReenviarVistaConError(pedidoCabeceraBE.PedidoCabeceraId, listaProductos);
                    }

                    if (estado == "modificar" && cantidad > cantidadOriginal)
                    {
                        int diferencia = cantidad - cantidadOriginal;
                        if (diferencia > producto.Stock)
                        {
                            ModelState.AddModelError("", $"Stock insuficiente para aumentar cantidad del producto '{producto.Nombre}'. Disponible: {producto.Stock}, se requiere: {diferencia}.");
                            return ReenviarVistaConError(pedidoCabeceraBE.PedidoCabeceraId, listaProductos);
                        }
                    }

                    var detalle = new PedidoDetalleBE
                    {
                        PedidoDetalleId = detalleId,
                        PedidoCabeceraId = pedidoCabeceraBE.PedidoCabeceraId,
                        ProductoId = productoId,
                        CantidadItem = cantidad,
                        PrecioUnitario = Convert.ToDecimal(precios[i]),
                        DescuentoItem = Convert.ToInt32(descuentos[i]),
                        TotalItem = Convert.ToDecimal(totales[i])
                    };

                    wSubtotal += detalle.TotalItem;

                    if (estado == "nuevo")
                    {
                        nuevosPedidoDetalleBE.Add(detalle);
                    }
                    else if (estado == "modificar")
                    {
                        editadosPedidoDetalleBE.Add(detalle);

                        if (cantidad != cantidadOriginal)
                        {
                            int diferencia = cantidad - cantidadOriginal;

                            if (diferencia > 0)
                                _updateBL.ActualizarStock(productoId, diferencia, "D");
                            else
                                _updateBL.ActualizarStock(productoId, -diferencia, "I");
                        }
                    }
                }

                pedidoCabeceraBE.Subtotal = wSubtotal;
                pedidoCabeceraBE.Impuestos = Math.Round(wSubtotal * 0.18M, 2);
                pedidoCabeceraBE.Total = pedidoCabeceraBE.Subtotal + pedidoCabeceraBE.Impuestos;

                _updateBL.ActualizarPedidoCabecera(pedidoCabeceraBE);

                _insertBL.InsertarPedidoDetalle(nuevosPedidoDetalleBE);
                foreach (var item in nuevosPedidoDetalleBE)
                    _updateBL.ActualizarStock(item.ProductoId, item.CantidadItem, "D");

                foreach (var item in editadosPedidoDetalleBE)
                    _updateBL.ActualizarPedidoDetalle(item);

                foreach (var item in eliminadosDetalleBE)
                {
                    _updateBL.EliminarPedidoDetalle(item.PedidoDetalleId);
                    _updateBL.ActualizarStock(item.ProductoId, item.CantidadItem, "I");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al actualizar: " + ex.Message;
                ViewBag.Clientes = _consultaBL.ConsultaCliente();
                ViewBag.Comprobantes = _consultaBL.ConsultaTipoComprobante();
                ViewBag.Productos = _consultaBL.ConsultaProducto();
                ViewBag.Estados = _consultaBL.ConsultaEstado();
                return View("Editar", _consultaBL.ConsultaPedidoDetalle(Convert.ToInt32(form["PedidoCabeceraId"])));
            }
        }



        [HttpPost]
        public ActionResult Agregar(FormCollection form)
        {
            try
            {
                PedidoCabeceraBE pedidoCabeceraBE = new PedidoCabeceraBE();
                List<PedidoDetalleBE> pedidoDetalleListBE = new List<PedidoDetalleBE>();

                pedidoCabeceraBE.ClienteId = Convert.ToInt32(form["ClienteId"]);
                pedidoCabeceraBE.TipoComprobanteId = Convert.ToInt32(form["TipoComprobanteId"]);
                pedidoCabeceraBE.EstadoId = Convert.ToInt32(form["EstadoId"] ?? "1");

                // Extraer arrays
                string[] productos = form.GetValues("ProductoId");
                string[] cantidades = form.GetValues("CantidadItem");
                string[] precios = form.GetValues("PrecioUnitario");
                string[] descuentos = form.GetValues("DescuentoItem");
                string[] totales = form.GetValues("TotalItem");

                decimal subtotal = 0;

                var listaProductos = _consultaBL.ConsultaProducto();

                // Validación de stock antes de guardar
                for (int i = 0; i < productos.Length; i++)
                {
                    int productoId = Convert.ToInt32(productos[i]);
                    int cantidad = Convert.ToInt32(cantidades[i]);

                    var stockDisponible = listaProductos.FirstOrDefault(p => p.ProductoId == productoId)?.Stock ?? 0;

                    if (cantidad > stockDisponible)
                    {
                        ModelState.AddModelError("", $"Stock insuficiente para el producto ID {productoId}. Stock disponible: {stockDisponible}");

                        ViewBag.Clientes = _consultaBL.ConsultaCliente();
                        ViewBag.Comprobantes = _consultaBL.ConsultaTipoComprobante();
                        ViewBag.Productos = listaProductos;
                        ViewBag.Estados = _consultaBL.ConsultaEstado();

                        return View();
                    }

                    PedidoDetalleBE pedidoDetalleBE = new PedidoDetalleBE
                    {
                        ProductoId = productoId,
                        CantidadItem = cantidad,
                        PrecioUnitario = Convert.ToDecimal(precios[i]),
                        DescuentoItem = Convert.ToInt32(descuentos[i]),
                        TotalItem = Convert.ToDecimal(totales[i])
                    };

                    subtotal += pedidoDetalleBE.TotalItem;
                    pedidoDetalleListBE.Add(pedidoDetalleBE);
                }

                pedidoCabeceraBE.Subtotal = subtotal;
                pedidoCabeceraBE.Impuestos = Math.Round(subtotal * 0.18M, 2);
                pedidoCabeceraBE.Total = pedidoCabeceraBE.Subtotal + pedidoCabeceraBE.Impuestos;

                int wPedidoCabeceraId = _insertBL.InsertarPedidoCabecera(pedidoCabeceraBE);

                foreach (var detalleList in pedidoDetalleListBE)
                {
                    detalleList.PedidoCabeceraId = wPedidoCabeceraId;
                }

                _insertBL.InsertarPedidoDetalle(pedidoDetalleListBE);

                foreach (var detalleList in pedidoDetalleListBE)
                {
                    _updateBL.ActualizarStock(detalleList.ProductoId, detalleList.CantidadItem, "D");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al guardar el pedido: " + ex.Message;
                ViewBag.Clientes = _consultaBL.ConsultaCliente();
                ViewBag.Comprobantes = _consultaBL.ConsultaTipoComprobante();
                ViewBag.Productos = _consultaBL.ConsultaProducto();
                ViewBag.Estados = _consultaBL.ConsultaEstado();
                return View();
            }
        }
    }
}