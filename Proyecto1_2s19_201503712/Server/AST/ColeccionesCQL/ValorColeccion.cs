using Server.AST.ExpresionesCQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.ColeccionesCQL
{
    public class ValorColeccion:Expresion
    {
        List<Expresion> expresiones;
        List<KeyValuePair<Expresion, Expresion>> kvpList;
        public Object tipoDato { get; set; }

        public ValorColeccion(List<Expresion> expresiones ,int fila, int columna) {
            this.expresiones = expresiones;
            this.fila = fila;
            this.columna = columna;
        }

        public ValorColeccion(List<KeyValuePair<Expresion, Expresion>> kvpList, int fila, int columna) {
            this.kvpList = kvpList;
            this.fila = fila;
            this.columna = columna;
        }

        public override object getValor(AST_CQL arbol)
        {
            if (tipoDato.Equals(Primitivo.TIPO_DATO.LIST))
            {
                List<Object> listReturn = new List<object>();
                foreach (Expresion exp in expresiones)
                {
                    listReturn.Add(exp.getValor(arbol));
                }
                ListCQL list = new ListCQL(expresiones[0].getTipo(arbol), fila, columna);
                list.valores = listReturn;
                return list;
            }
            else if (tipoDato.Equals(Primitivo.TIPO_DATO.SET))
            {
                List<Object> listReturn = new List<object>();
                foreach (Expresion exp in expresiones)
                {
                    listReturn.Add(exp.getValor(arbol));
                }
                SetCQL list = new SetCQL(expresiones[0].getTipo(arbol), fila, columna);
                list.valores = listReturn;
                return list;
            }
            else if (tipoDato.Equals(Primitivo.TIPO_DATO.MAP))
            {
                Hashtable listReturn = new Hashtable();
                foreach (KeyValuePair<Expresion, Expresion> kvp in kvpList)
                {
                    listReturn.Add(kvp.Key.getValor(arbol), kvp.Value.getValor(arbol));
                }
                MapCQL list = new MapCQL(kvpList[0].Key.getTipo(arbol), kvpList[0].Value.getTipo(arbol), fila, columna);
                list.valores = listReturn;
                return list;
            }
            else {
                arbol.addError("(ValorColeccion, getValor)","No soportado tipo: "+tipoDato,fila,columna);
                return Primitivo.TIPO_DATO.NULL;
            }
        }

        public override object getTipo(AST_CQL arbol)
        {
            return tipoDato;
        }
    }
}