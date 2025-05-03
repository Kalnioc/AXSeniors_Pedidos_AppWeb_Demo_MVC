using AXSeniors_Pedidos_Demo_BUSINESS;
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
        ConsultaBL consultaBL = new ConsultaBL();
        InsertBL insertBL = new InsertBL();
        UpdateBL updateBL = new UpdateBL();

        public ActionResult Index()
        {
            return View(consultaBL.ConsultaProducto());
        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(ProductoBE productoBE)
        {
            insertBL.InsertarProducto(productoBE);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            var wProducto = consultaBL.ConsultaProducto().FirstOrDefault(x => x.ProductoId == id);
            return View(wProducto);
        }

        [HttpPost]
        public ActionResult Editar(ProductoBE productoBE)
        {
            updateBL.ActualizarProducto(productoBE);
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            updateBL.EliminarProducto(id);
            return RedirectToAction("Index");
        }
    }


}