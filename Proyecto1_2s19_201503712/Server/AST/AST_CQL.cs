using Server.Analizador;
using Server.AST.CQL;
using Server.AST.DBMS;
using Server.AST.ExpresionesCQL;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST
{
    public class AST_CQL
    {
        public List<NodoCQL> nodos { get; set; }
        public List<String> mensajes { get; set; }
        private List<List<ColumnCQL>> res_consultas { get; set; }
        public List<String> result_consultas { get; set; }
        public List<clsToken> errores { get; set; }
        public Entorno entorno { get; set; }
        public List<Funcion> funciones { get; set; }
        public Boolean finalizado { get; set; }
        public Management dbms { get; set; }

        public AST_CQL() {
            this.funciones = new List<Funcion>();
            this.nodos = new List<NodoCQL>();
            this.mensajes = new List<String>();
            this.res_consultas = new List<List<ColumnCQL>>();
            this.result_consultas = new List<string>();
            this.errores = new List<clsToken>();
            this.entorno = new Entorno(null);
            this.finalizado = false;
            this.dbms = new Management();
        }

        public void Ejecutar() {
            //sentencias CQL
            foreach(NodoCQL nodo in nodos) {
                if (nodo is Expresion)
                {
                    ((Expresion)nodo).getValor(this);
                }
                else {
                    ((Sentencia)nodo).Ejecutar(this);
                }
            }
            this.finalizado = true;
        }

        //==================================================================================================================
        //==================================================================================================================
        //==================================================================================================================
        //============================================ METODOS LUP =========================================================
        //==================================================================================================================
        //==================================================================================================================
        //==================================================================================================================
        public String getLUP() {
            String respuesta = "";

            //==== mensajes ======
            foreach (String msm in mensajes) {
                respuesta += "\n[+MESSAGE]\n";
                respuesta += msm;
                respuesta += "\n[-MESSAGE]\n";
            }

            //==== data selects ====
            foreach (String consulta in result_consultas) {
                respuesta += "\n[+DATA]\n";
                respuesta += consulta;
                respuesta += "\n[-DATA]\n";
            }

            //======== errores ========
            foreach (clsToken error in errores) {
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

        /*
        private String getTablaSelect(List<ColumnCQL> data) {
            String res = "<table border=\"2\" style=\"margin: 0 auto;\" class=\"table table-striped table-bordered table-responsive table-dark\">\n";

            //nombre de las columnas
            foreach (ColumnCQL column in data) {

                res += "<td>"+column.id+"</td>\n";
            }

            int index = data.Count != 0 ? data[0].valores.Count : 0;
            for (int i = 0; i < index; i++)
            {
                //filas

                res += "    <tr>\n";
                foreach (ColumnCQL column in data)
                {
                    res += "        <td>" + column.valores[i] + "</td>\n";
                }
                res += "    </tr>\n";
            }
            res += "</table>\n";
            return res;
        }*/

        public void addError(String lexema, String descripcion, int fila, int columna) {
            this.errores.Add(new clsToken(lexema, descripcion, fila, columna, "Semántico",""));
        }
    }
}