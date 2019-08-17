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
                    //Graficar.ConstruirArbol(parserCQL.padre.Root, "AST_CQL", "");
                    RecorridoCQL recorrido = new RecorridoCQL(parserCQL.padre.Root);
                    recorrido.ast.Ejecutar();
                    return recorrido.ast.getLUP();
                }
                return "\n[+ERROR]\nPadre Null\n[-ERROR]\n";
            }
            else
            {
                String respuesta = "";
                foreach (clsToken error in parserCQL.ListaErrores)
                {
                    respuesta += "\n[+ERROR]\n";
                    respuesta += "\n[+LINE]\n";
                    respuesta += error.fila;
                    respuesta += "\n[-LINE]\n";
                    respuesta += "\n[+COLUMN]\n";
                    respuesta += error.columna;
                    respuesta += "\n[-COLUMN]\n";
                    respuesta += "\n[+TYPE]\n";
                    respuesta += error.tipo;
                    respuesta += "\n[-TYPE]\n";
                    respuesta += "\n[+DESC]\n";
                    respuesta += error.descripcion;
                    respuesta += "\n[-DESC]\n";
                    respuesta += "\n[-ERROR]\n";
                }
                return respuesta;
            }
        }
    }
}
