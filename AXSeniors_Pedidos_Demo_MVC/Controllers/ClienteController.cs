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
        private readonly IConsultaBL consultaBL;
        private readonly IInsertBL insertBL;
        private readonly IUpdateBL updateBL;

        public ClienteController()
        {
            this.consultaBL = new ConsultaBL();
            this.insertBL = new InsertBL();
            this.updateBL = new UpdateBL();
        }

        public ActionResult Index()
        {
            return View(consultaBL.ConsultaCliente());
        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(ClienteBE clienteBE)
        {
            insertBL.InsertarCliente(clienteBE);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            var cliente = consultaBL.ConsultaCliente().FirstOrDefault(x => x.ClienteId == id);
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Editar(ClienteBE p)
        {
            updateBL.ActualizarCliente(p);
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            updateBL.EliminarCliente(id);
            return RedirectToAction("Index");
        }
    }

}