using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace AXSeniors_Pedidos_Demo_DATA.Connection
{
    public class ConnectionDL
    {
        public enum EConexion
        {
            PruebaAXSeniorConnection = 1
        }

        public static string Conexion(EConexion pEConexion = EConexion.PruebaAXSeniorConnection)
        {
            string wConexionPruebaAXSeniorConnection = ConfigurationManager.ConnectionStrings["PruebaAXSeniorConnection"].ConnectionString;
            string wConexion = string.Empty;

            switch (pEConexion)
            {
                case EConexion.PruebaAXSeniorConnection:
                    wConexion = wConexionPruebaAXSeniorConnection;
                    break;
            }
            return wConexion;
        }
    }
}