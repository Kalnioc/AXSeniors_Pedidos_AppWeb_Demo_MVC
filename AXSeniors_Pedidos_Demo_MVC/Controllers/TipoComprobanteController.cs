using AXSeniors_Pedidos_Demo_BUSINESS;
using AXSeniors_Pedidos_Demo_ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AXSeniors_Pedidos_Demo_MVC.Controllers
{
    public class TipoComprobanteController : Controller
    {
        ConsultaBL consultaBL = new ConsultaBL();
        InsertBL insertBL = new InsertBL();
        UpdateBL updateBL = new UpdateBL();

        public ActionResult Index()
        {
            return View(consultaBL.ConsultaTipoComprobante());
        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(TipoComprobanteBE tipoComprobanteBE)
        {
            insertBL.InsertarTipoComprobante(tipoComprobanteBE);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            var tipo = consultaBL.ConsultaTipoComprobante().FirstOrDefault(x => x.TipoComprobanteId == id);
            return View(tipo);
        }

        [HttpPost]
        public ActionResult Editar(TipoComprobanteBE tipoComprobanteBE)
        {
            updateBL.ActualizarTipoComprobante(tipoComprobanteBE);
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            updateBL.EliminarTipoComprobante(id);
            return RedirectToAction("Index");
        }
    }

}