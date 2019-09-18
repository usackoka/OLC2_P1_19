using Server.AST.ExpresionesCQL;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class Select : Sentencia
    {
        Select_Type selectType;
        String idTabla;
        Where where;
        OrderBy orderBy;
        Expresion limit;

        public Select(Select_Type selectType, String idTabla, Where where, OrderBy orderBy, 
            Expresion limit, int fila, int columna) {
            this.selectType = selectType;
            this.idTabla = idTabla;
            this.where = where;
            this.orderBy = orderBy;
            this.limit = limit;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            List<ColumnCQL> resultado = new List<ColumnCQL>();

            //Select de todo primero
            Object o = selectType.getResult(this.idTabla, this.where, arbol);
            if (o is List<ColumnCQL>)
            {
                resultado = (List<ColumnCQL>)o;
            }
            else {
                return o;
            }

            //orderBy
            if (orderBy != null)
            {
                resultado = orderBy.getResult(resultado, arbol);
            }

            //limit
            if (limit!=null) {
                Object intt = limit.getValor(arbol);
                if (intt is Int32)
                {
                    int corte = Convert.ToInt32(intt);
                    foreach (ColumnCQL column in resultado) {
                        while (true) {
                            try
                            {
                                column.valores.RemoveAt(corte);
                            }
                            catch (Exception)
                            {
                                break;
                            }
                        }
                    }
                }
                else {
                    arbol.addError("Limit","Se esperaba un valor de tipo entero para el limite",fila,columna);
                }
            }

            //agrego la lista a los print de tables
            arbol.result_consultas.Add(getString(resultado));

            return resultado;
        }

        String getString(List<ColumnCQL> data) {
            String res = "<table border=\"2\" style=\"margin: 0 auto;\" class=\"table table-striped table-bordered table-responsive table-dark\">\n";

            //nombre de las columnas
            foreach (ColumnCQL column in data)
            {

                res += "<td>" + column.id + "</td>\n";
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
        }
    }
}