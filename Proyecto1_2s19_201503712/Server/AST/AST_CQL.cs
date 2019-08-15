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

        public AST_CQL() {
            this.nodos = new List<NodoCQL>();
            this.mensajes = new List<String>();
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

            return respuesta;
        }
    }
}