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
    public class TipoComprobanteController : Controller
    {
        private readonly IConsultaBL _consultaBL;
        private readonly IInsertBL _insertBL;
        private readonly IUpdateBL _updateBL;

        public TipoComprobanteController(ConsultaBL consultaBL, InsertBL insertBL, UpdateBL updateBL)
        {
            _consultaBL = consultaBL;
            _insertBL = insertBL;
            _updateBL = updateBL;
        }

        public ActionResult Index()
        {
            return View(_consultaBL.ConsultaTipoComprobante());
        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(TipoComprobanteBE tipoComprobanteBE)
        {
            _insertBL.InsertarTipoComprobante(tipoComprobanteBE);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            var tipo = _consultaBL.ConsultaTipoComprobante().FirstOrDefault(x => x.TipoComprobanteId == id);
            return View(tipo);
        }

        [HttpPost]
        public ActionResult Editar(TipoComprobanteBE tipoComprobanteBE)
        {
            _updateBL.ActualizarTipoComprobante(tipoComprobanteBE);
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            _updateBL.EliminarTipoComprobante(id);
            return RedirectToAction("Index");
        }
    }

}