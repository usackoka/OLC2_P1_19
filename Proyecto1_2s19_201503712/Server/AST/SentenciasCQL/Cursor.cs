using Server.AST.CQL;
using Server.AST.ExpresionesCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.SentenciasCQL
{
    public class Cursor : Sentencia
    {
        public String id;
        public Select select;
        public List<ColumnCQL> data;
        TIPO_CURSOR tipoCursor;

        public Cursor(String id, Select select, TIPO_CURSOR tipoCursor ,int fila, int columna) {
            this.id = id;
            this.select = select;
            this.fila = fila;
            this.columna = columna;
            this.tipoCursor = tipoCursor;
        }

        public enum TIPO_CURSOR {
            OPEN, CLOSE, INSTANCE
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            switch (this.tipoCursor) {
                case TIPO_CURSOR.INSTANCE:
                    arbol.entorno.addVariable(this.id, new Variable(this, Primitivo.TIPO_DATO.CURSOR));
                    return null;
                case TIPO_CURSOR.OPEN:
                    Cursor cursor = (Cursor)arbol.entorno.getValorVariable(this.id, arbol, fila, columna);
                    Object o = cursor.select.Ejecutar(arbol);
                    arbol.res_consultas.RemoveAt(arbol.res_consultas.Count-1);
                    if (o is List<ColumnCQL>)
                    {
                        cursor.data = (List<ColumnCQL>)o;
                        arbol.entorno.reasignarVariable(this.id, cursor, Primitivo.TIPO_DATO.CURSOR, arbol,fila, columna);
                        return null;
                    }
                    return o;
                default:
                    Cursor cursor2 = (Cursor)arbol.entorno.getValorVariable(this.id, arbol, fila, columna);
                    cursor2.data = null;
                    arbol.entorno.reasignarVariable(this.id, cursor2, Primitivo.TIPO_DATO.CURSOR, arbol, fila, columna);
                    return null;
            }
        }
    }
}