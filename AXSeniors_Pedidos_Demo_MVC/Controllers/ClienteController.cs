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
    public class ClienteController : Controller
    {
        private readonly IConsultaBL _consultaBL;
        private readonly IInsertBL _insertBL;
        private readonly IUpdateBL _updateBL;

        public ClienteController(ConsultaBL consultaBL, InsertBL insertBL, UpdateBL updateBL)
        {
            _consultaBL = consultaBL;
            _insertBL = insertBL;
            _updateBL = updateBL;
        }

        public ActionResult Index()
        {
            return View(_consultaBL.ConsultaCliente());
        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(ClienteBE clienteBE)
        {
            _insertBL.InsertarCliente(clienteBE);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            var cliente = _consultaBL.ConsultaCliente().FirstOrDefault(x => x.ClienteId == id);
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Editar(ClienteBE p)
        {
            _updateBL.ActualizarCliente(p);
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            _updateBL.EliminarCliente(id);
            return RedirectToAction("Index");
        }
    }

}