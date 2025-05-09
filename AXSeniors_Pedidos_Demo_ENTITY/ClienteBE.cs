﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AXSeniors_Pedidos_Demo_ENTITY
{
    public class ClienteBE
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreCompleto { get; set; }        
        public string NumeroDocumento { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}