using AXSeniors_Pedidos_Demo_BUSINESS;
using AXSeniors_Pedidos_Demo_ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AXSeniors_Pedidos_Demo_MVC.Controllers
{
    public class HomeController : Controller
    {
        ConsultaBL consultaBL = new ConsultaBL();
        InsertBL insertBL = new InsertBL();
        ProductoBE productoBE = new ProductoBE();

        public ActionResult Index()
        {
            ViewBag.ListaProductos = consultaBL.ConsultaProducto().ToList();

            return View();
        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(ProductoBE productoBE)
        {
            //if (string.IsNullOrEmpty(productoBE.Nombre)|| string.IsNullOrEmpty(productoBE.Descripcion))
            //{
            //    ModelState.AddModelError("", "No pueden existir campos vacíos");
            //    return View();
            //}
            //else if(productoBE.Precio < 0 || productoBE.Stock < 0)
            //{
            //    ModelState.AddModelError("", "El precio y el stock no pueden ser negativos.");
            //    return View();
            //}
            insertBL.InsertarProducto(productoBE);

            return RedirectToAction("Index");
        }
    }
}