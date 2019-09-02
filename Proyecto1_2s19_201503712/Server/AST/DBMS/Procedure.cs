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
        public String instruccionesString;

        public Procedure(String id, List<KeyValuePair<String, Object>> parametros, List<KeyValuePair<String, Object>> retornos, 
            List<NodoCQL> instrucciones, String instruccionesString, int fila, int columna) {
            this.id = id;
            this.parametros = parametros;
            this.retornos = retornos;
            this.instrucciones = instrucciones;
            this.fila = fila;
            this.instruccionesString = instruccionesString;
            this.columna = columna;
        }

        public Procedure(CreateProcedure cp) {
            this.id = cp.id;
            this.parametros = cp.parametros;
            this.retornos = cp.retornos;
            this.instrucciones = cp.instrucciones;
            this.fila = cp.fila;
            this.columna = cp.columna;
            this.instruccionesString = cp.instruccionesString;
        }

        public override string ToString()
        {
            String trad = "";
            trad += "   <\n";
            trad += "   \"CQL-TYPE\"=\"PROCEDURE\",\n";
            trad += "   \"NAME\"=\"" + this.id + "\",\n";
            trad += "   \"PARAMETERS\"=[" + getAtributos() + "],\n";
            trad += "   \"INSTR\"=\"" +this.instruccionesString + "\"\n";
            trad += "   >\n";
            return trad;
        }

        string getAtributos() {
            String trad = "";

            foreach (KeyValuePair<String,Object> kvp in this.parametros) {
                trad += "\n   <";
                trad += "   \"NAME\"=\""+kvp.Key+"\",\n";
                trad += "   \"TYPE\"=\"" +kvp.Value + "\",\n";
                trad += "   \"AS\"=IN\n";
                trad += "   >,";
            }
            //trad = trad.TrimEnd(',');

            foreach (KeyValuePair<String, Object> kvp in this.retornos)
            {
                trad += "\n   <";
                trad += "   \"NAME\"=\"" + kvp.Key + "\",\n";
                trad += "   \"TYPE\"=\"" + kvp.Value + "\",\n";
                trad += "   \"AS\"=OUT\n";
                trad += "   >,";
            }
            trad = trad.TrimEnd(',');

            return trad;
        }

        public List<Object> getTipo(AST_CQL arbol) {
            List<Object> tipos = new List<object>();
            foreach (KeyValuePair<String,Object> kvp in this.retornos) {
                tipos.Add(kvp.Value);
            }
            return tipos;
        }

        public String getFirma() {
            String firma = "";
            foreach (KeyValuePair<String,Object> kvp in parametros)
            {
                firma += "_" + kvp.Value;
            }
            return firma;
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
                arbol.entorno.addVariable(kvp.Key, new Variable(value, kvp.Value),arbol,fila,columna);
            }
        }

    }
}