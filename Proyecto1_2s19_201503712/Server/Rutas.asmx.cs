using Server.Analizador;
using Server.Otros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;

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
            Generador parserCQL = new Generador();
            if (parserCQL.esCadenaValida(cadena, new GramaticaCQL()))
            {
                if (parserCQL.padre.Root != null)
                {
                    Graficar.ConstruirArbol(parserCQL.padre.Root, "AST_CQL", "");
                    return "Analizado con éxito";
                }
                return "Padre null";
            }
            else
            {
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(parserCQL.ListaErrores);

                return json;
            }
        }
    }
}
