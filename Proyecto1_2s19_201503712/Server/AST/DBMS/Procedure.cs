using Server.AST.ExpresionesCQL;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class Procedure : Sentencia
    {
        public String id { get; set; }
        List<NodoCQL> instrucciones;
        List<KeyValuePair<String, Object>> retornos;
        List<KeyValuePair<String, Object>> parametros;
        public List<Object> valoresParametros { get; set; }

        public Procedure(String id, List<KeyValuePair<String, Object>> parametros, List<KeyValuePair<String, Object>> retornos, 
            List<NodoCQL> instrucciones, int fila, int columna) {
            this.id = id;
            this.parametros = parametros;
            this.retornos = retornos;
            this.instrucciones = instrucciones;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            //arbol.entorno = new Entorno(arbol.entorno);
            Entorno temp = arbol.entorno;

            //para cuando hago una llamada global que no se pierda el padre
            if (arbol.entorno.padre != null)
            {
                arbol.entorno = new Entorno(arbol.entorno.padre);
            }
            else
            {
                arbol.entorno = new Entorno(arbol.entorno);
            }

            crearParametros(arbol);

            foreach (NodoCQL nodo in this.instrucciones)
            {
                if (nodo is Sentencia)
                {
                    Object val = ((Sentencia)nodo).Ejecutar(arbol);
                    if (val != null)
                    {
                        //arbol.entorno = arbol.entorno.padre;
                        arbol.entorno = temp;
                        return val;
                    }
                }
                else
                {
                    ((Expresion)nodo).getValor(arbol);
                }
            }
            //arbol.entorno = arbol.entorno.padre;
            arbol.entorno = temp;

            return null;
        }

        void crearParametros(AST_CQL arbol)
        {
            if (this.parametros.Count != this.valoresParametros.Count)
            {
                arbol.addError("Procedure: " + id, "La cantidad de parámetros enviada no coincide con las del procedure", fila, columna);
                return;
            }
            for (int i = 0; i < this.parametros.Count; i++)
            {
                KeyValuePair<String, Object> kvp = this.parametros[i];
                Object value = this.valoresParametros[i];
                arbol.entorno.addVariable(kvp.Key, new Variable(value, kvp.Value));
            }
        }

        public String getFirma()
        {
            String firma = "";
            foreach (KeyValuePair<String, object> kvp in this.parametros)
            {
                firma += "_" + kvp.Value;
            }
            return firma;
        }
    }
}