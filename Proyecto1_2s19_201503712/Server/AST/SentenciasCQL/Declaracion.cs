using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class Declaracion : Sentencia
    {
        Object tipoDato;
        List<KeyValuePair<String, Expresion>> kv;

        public Declaracion(Object tipoDato, List<KeyValuePair<String, Expresion>> kv, int fila, int columna) {
            this.tipoDato = tipoDato;
            this.kv = kv;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            foreach (KeyValuePair<String, Expresion> kvp in kv) {
                Object valor = Primitivo.getDefecto(tipoDato, arbol);
                if (kvp.Value!=null) {
                    valor = kvp.Value.getValor(arbol);
                    //falta verificar que el tipo a asignar sea igual que el tipo de la variable
                }
                arbol.entorno.addVariable(kvp.Key, new Variable(valor, tipoDato));
            }

            return null;
        }
    }
}