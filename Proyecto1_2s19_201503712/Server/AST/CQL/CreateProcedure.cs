using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class CreateProcedure : Sentencia
    {
        public String id;
        public List<KeyValuePair<String, Object>> parametros;
        public List<KeyValuePair<String, Object>> retornos;
        public List<NodoCQL> instrucciones;

        public CreateProcedure(String id, List<KeyValuePair<String,Object>> parametros, List<KeyValuePair<String,Object>> retornos,
            List<NodoCQL> instrucciones, int fila, int columna) {
            this.id = id;
            this.parametros = parametros;
            this.instrucciones = instrucciones;
            this.retornos = retornos;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            return arbol.dbms.createProcedure(this,arbol, fila, columna);
        }

        public String getFirma()
        {
            String firma = "";
            foreach (KeyValuePair<String, Object> kvp in parametros)
            {
                firma += "_" + kvp.Value;
            }
            return firma;
        }
    }
}