using Server.AST.ExpresionesCQL;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class AlterTableAdd : Sentencia
    {
        public String idTabla;
        public List<ColumnCQL> atributos;

        public AlterTableAdd(String idTabla, List<ColumnCQL> atributos, int fila, int columna) {
            this.idTabla = idTabla;
            this.atributos = atributos;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            //pregunto si alguno es de tipo counter
            foreach (ColumnCQL column in this.atributos) {
                if (column.tipoDato.Equals(Primitivo.TIPO_DATO.COUNTER)) {
                    arbol.addError("EXCEPTION.ValuesException","No se puede hacer un alter add de un tipo COUNTER",fila, columna);
                    return Catch.EXCEPTION.ValuesException;
                }
            }

            return arbol.dbms.alterTableAdd(this, arbol, fila,columna);
        }
    }
}