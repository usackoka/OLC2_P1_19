using Server.Analizador;
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
        public List<clsToken> errores { get; set; }
        public Entorno entorno { get; set; }
        public List<Funcion> funciones { get; set; }

        public AST_CQL() {
            this.funciones = new List<Funcion>();
            this.nodos = new List<NodoCQL>();
            this.mensajes = new List<String>();
            this.errores = new List<clsToken>();
            this.entorno = new Entorno(null);
        }

        public void Ejecutar() {
            foreach(NodoCQL nodo in nodos) {
                if (nodo is Expresion)
                {
                    ((Expresion)nodo).getValor(this);
                }
                else {
                    ((Sentencia)nodo).Ejecutar(this);
                }
            }
        }

        public String getLUP() {
            String respuesta = "";

            //==== mensajes ======
            foreach (String msm in mensajes) {
                respuesta += "\n[+MESSAGE]\n";
                respuesta += msm;
                respuesta += "\n[-MESSAGE]\n";
            }

            //======== errores ========
            foreach (clsToken error in errores) {
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

        public void addError(String lexema, String descripcion, int fila, int columna) {
            this.errores.Add(new clsToken(lexema, descripcion, fila, columna, "Semántico",""));
        }
    }
}