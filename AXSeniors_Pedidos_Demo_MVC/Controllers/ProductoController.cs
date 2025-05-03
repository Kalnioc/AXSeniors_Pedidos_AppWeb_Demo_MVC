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
    public class ProductoController : Controller
    {
        private readonly IConsultaBL _consultaBL;
        private readonly IInsertBL _insertBL;
        private readonly IUpdateBL _updateBL;

        public ProductoController(ConsultaBL consultaBL, InsertBL insertBL, UpdateBL updateBL)
        {
            _consultaBL = consultaBL;
            _insertBL = insertBL;
            _updateBL = updateBL;
        }
        public ActionResult Index()
        {
            return View(_consultaBL.ConsultaProducto());
        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(ProductoBE productoBE)
        {
            _insertBL.InsertarProducto(productoBE);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            var wProducto = _consultaBL.ConsultaProducto().FirstOrDefault(x => x.ProductoId == id);
            return View(wProducto);
        }

        [HttpPost]
        public ActionResult Editar(ProductoBE productoBE)
        {
            _updateBL.ActualizarProducto(productoBE);
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            _updateBL.EliminarProducto(id);
            return RedirectToAction("Index");
        }
    }


}