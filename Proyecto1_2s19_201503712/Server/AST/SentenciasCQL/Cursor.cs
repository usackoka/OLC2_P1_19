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
        public NodoCQL select;
        public List<ColumnCQL> data;
        TIPO_CURSOR tipoCursor;
        Entorno entornoEjecucion;

        public Cursor(String id, NodoCQL valorCursor, TIPO_CURSOR tipoCursor ,int fila, int columna) {
            this.id = id;
            this.fila = fila;
            this.select = valorCursor;
            this.columna = columna;
            this.tipoCursor = tipoCursor;
            this.entornoEjecucion = null;
        }

        public enum TIPO_CURSOR {
            OPEN, CLOSE, INSTANCE
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            switch (this.tipoCursor) {
                case TIPO_CURSOR.INSTANCE:
                    this.entornoEjecucion = arbol.entorno;
                    arbol.entorno.addVariable(this.id, new Variable(this, Primitivo.TIPO_DATO.CURSOR),arbol,fila,columna);
                    return null;
                case TIPO_CURSOR.OPEN:
                    Cursor cursor = (Cursor)arbol.entorno.getValorVariable(this.id, arbol, fila, columna);
                    Object o;
                    Entorno temp = arbol.entorno;
                    arbol.entorno = cursor.entornoEjecucion;
                    if (cursor.select is Select)
                    {
                        o = ((Select)cursor.select).Ejecutar(arbol);
                    }
                    else {
                        o = ((Expresion)cursor.select).getValor(arbol);
                    }

                    if (o is List<ColumnCQL>)
                    {
                        cursor.data = (List<ColumnCQL>)o;
                        //arbol.entorno.reasignarVariable(this.id, cursor, Primitivo.TIPO_DATO.CURSOR, arbol, fila, columna);
                        arbol.result_consultas.RemoveAt(arbol.result_consultas.Count - 1);
                        o = null;
                    }
                    else if (o is Cursor) {
                        cursor.data = ((Cursor)o).getSelect(arbol);
                        //arbol.entorno.reasignarVariable(this.id, cursor, Primitivo.TIPO_DATO.CURSOR, arbol, fila, columna);
                        arbol.result_consultas.RemoveAt(arbol.result_consultas.Count - 1);
                        o = null;
                    }
                    else {
                        arbol.addError("SELECT-OPEN-" + this.id, "El open devolvió un objeto de tipo: " + o, fila, columna);
                    }

                    arbol.entorno = temp;
                    return o;
                default:
                    Cursor cursor2 = (Cursor)arbol.entorno.getValorVariable(this.id, arbol, fila, columna);
                    cursor2.data = null;
                    //arbol.entorno.reasignarVariable(this.id, cursor2, Primitivo.TIPO_DATO.CURSOR, arbol, fila, columna);
                    return null;
            }
        }

        List<ColumnCQL> getSelect(AST_CQL arbol) {
            if (this.select != null)
            {
                Entorno te = arbol.entorno;
                arbol.entorno = this.entornoEjecucion;
                Object o;
                if (this.select is Select)
                {
                    o = ((Select)select).Ejecutar(arbol);
                }
                else
                {
                    o = ((Expresion)select).getValor(arbol);
                }
                arbol.entorno = te;

                if (o is List<ColumnCQL>)
                {
                    return (List<ColumnCQL>)o;
                }
                else if (o is Cursor)
                {
                    Entorno temp = arbol.entorno;
                    arbol.entorno = ((Cursor)o).entornoEjecucion;
                    List<ColumnCQL> oo = ((Cursor)o).getSelect(arbol);
                    arbol.entorno = temp;
                    return oo;
                }
                else {
                    arbol.addError("SELECT-OPEN-" + this.id, "El open devolvió un objeto de tipo: " + o, fila, columna);
                    return new List<ColumnCQL>();
                }
            }
            else {
                arbol.addError("Cursor: "+this.id,"No tiene un select asociado el cursor",fila,columna);
                return new List<ColumnCQL>();
            }
        }
    }
}