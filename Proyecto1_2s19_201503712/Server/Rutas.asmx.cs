using Server.Analizador;
using Server.Otros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Threading;
using Server.Analizador.LUP;
using Server.AST.DBMS;

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
        static Management dbms;

        public Rutas() {
            dbms = new Management();
            dbms.analizarChison("");
        }

        [WebMethod]
        public string getErroresChison() {
            String respuesta = "";
            foreach (clsToken error in dbms.errores)
            {
                respuesta += "\n[+ERROR]\n";
                respuesta += "\n[+LEXEMA]\n";
                respuesta += error.lexema;
                respuesta += "\n[-LEXEMA]\n";
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
            dbms.errores = new List<clsToken>();
            return respuesta;
        }

        [WebMethod(EnableSession = true)]
        public string AnalizarPruebaCQL(String cadena) {
            Generador parserLUP = new Generador();
            if (parserLUP.esCadenaValida(cadena, new GramaticaLUP()))
            {
                if (parserLUP.padre.Root != null)
                {
                    //Graficar.ConstruirArbol(parserLUP.padre.Root, "AST_LUP", "");
                    RecorridoLUP recorrido = new RecorridoLUP();

                    if (Session["user"]!=null) {
                        dbms.usuarioActivo = (AST.DBMS.User)Session["user"];
                    }

                    Object o = recorrido.ejecutarLUP(parserLUP.padre.Root, dbms);
                    if (o is AST.DBMS.User)
                    {
                        Session["user"] = o;
                        return "[+LOGIN]\n  [SUCCESS]\n[-LOGIN]";
                    }
                    else if (o is RecorridoLUP.TIPO_REQUEST && ((RecorridoLUP.TIPO_REQUEST)o).Equals(RecorridoLUP.TIPO_REQUEST.LOGOUT)) {

                        Session["user"] = null;
                        return "[+LOGOUT]\n  [SUCCESS]\n[-LOGOUT]";
                    }
                    else
                    {
                        return o.ToString();
                    }
                }
                else {
                    return "[+ERROR]\n" +
                        "[+DESC]\n" +
                        "Padre LUP Null\n" +
                        "[-DESC]\n" +
                        "[-ERROR]\n";
                }
            }
            else {
                String respuesta = "";
                foreach (clsToken error in parserLUP.ListaErrores)
                {
                    respuesta += "\n[+ERROR]\n";
                    respuesta += "\n[+LEXEMA]\n";
                    respuesta += error.lexema;
                    respuesta += "\n[-LEXEMA]\n";
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
