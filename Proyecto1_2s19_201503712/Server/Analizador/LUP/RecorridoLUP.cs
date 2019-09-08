using Irony.Parsing;
using Server.AST.DBMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace Server.Analizador.LUP
{
    public class RecorridoLUP
    {
        Management dbms;

        public RecorridoLUP()
        {
        }

        public Object ejecutarLUP(ParseTreeNode padre, Management dbms) {
            this.dbms = dbms;
            return recorrido(padre);
        }

        public enum TIPO_REQUEST {
            LOGOUT
        }

        Object recorrido(ParseTreeNode raiz)
        {
            if (CompararNombre(raiz, "S"))
            {
                return recorrido(raiz.ChildNodes[0]);
            }
            else if (CompararNombre(raiz, "LOGIN"))
            {
                //res_loginOpen + res_userOpen + id + res_userClose + res_pass + res_loginClose;
                return this.dbms.login(getLexema(raiz, 2), getLexema(raiz, 4).Replace("[+PASS]", "").Replace("[-PASS]", ""));
            }
            else if (CompararNombre(raiz,"LOGOUT")) {
                //res_logoutOpen + res_userOpen + id + res_userClose + res_logoutClose;
                return TIPO_REQUEST.LOGOUT;
            }
            else if (CompararNombre(raiz, "QUERY")) {
                //res_queryOpen + res_userOpen + id + res_userClose + res_data + res_queryClose

                String cadena = getLexema(raiz, 4).Replace("[+DATA]", "").Replace("[-DATA]", "");
                Generador parserCQL = new Generador();
                if (parserCQL.esCadenaValida(cadena, new GramaticaCQL()))
                {
                    if (parserCQL.padre.Root != null)
                    {
                        //Graficar.ConstruirArbol(parserCQL.padre.Root, "AST_CQL", "");
                        RecorridoCQL recorrido = new RecorridoCQL(parserCQL.padre.Root, this.dbms);

                        ThreadStart threadDelegate = new ThreadStart(recorrido.ast.Ejecutar);

                        Thread T = new Thread(threadDelegate, 1000000000);
                        T.Start();

                        while (!recorrido.ast.finalizado)
                        {
                            Console.WriteLine("Esperando..............");
                        }
                        return recorrido.ast.getLUP();
                    }
                    return "[+ERROR]\n" +
                        "[+DESC]\n" +
                        "Padre CQL Null\n" +
                        "[-DESC]\n" +
                        "[-ERROR]\n";
                }
                else
                {
                    String respuesta = "";
                    foreach (clsToken error in parserCQL.ListaErrores)
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
            else
            {
                return null;
            }
        }


        bool CompararNombre(ParseTreeNode nodo, string nombre)
        {
            return nodo.Term.Name.Equals(nombre, System.StringComparison.InvariantCultureIgnoreCase);
        }

        string getLexema(ParseTreeNode nodo, int num)
        {
            return nodo.ChildNodes[num].Token.Text.ToLower();
        }

        int getFila(ParseTreeNode nodo, int num)
        {
            return nodo.ChildNodes[num].Token.Location.Line;
        }

        int getColumna(ParseTreeNode nodo, int num)
        {
            return nodo.ChildNodes[num].Token.Location.Column;
        }

        Boolean ContainsString(String match, String search)
        {
            return Regex.IsMatch(search, Regex.Escape(match), RegexOptions.IgnoreCase);
        }
    }
}