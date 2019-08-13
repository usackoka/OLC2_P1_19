using Server.Analizador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Server
{
    /// <summary>
    /// Descripción breve de Rutas
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Rutas : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hola Mundo";
        }

        [WebMethod]
        public string AnalizarPruebaCQL(String cadena) {
            Generador parserCollete = new Generador();
            if (parserCollete.esCadenaValida(cadena, new GramaticaCQL()))
            {
                if (parserCollete.padre.Root != null)
                {
                    return "Analizado con éxito";
                }
                return "Padre null";
            }
            else
            {
                return "Errores CQL";
            }
        }
    }
}
