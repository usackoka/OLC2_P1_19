using Server.AST.DBMS;
using Server.AST.ExpresionesCQL;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class Select_Type
    {
        List<Expresion> expresiones; 

        public Select_Type() {
            this.expresiones = null;
        }

        public Select_Type(List<Expresion> expresiones) {
            this.expresiones = expresiones;
        }

        public Object getResult(String idTabla, AST_CQL arbol) {
            List<ColumnCQL> data = new List<ColumnCQL>();

            //Obtengo la tabla y veo si existe
            TableCQL tabla = arbol.dbms.getTable(idTabla);
            if (tabla==null) {
                return Catch.EXCEPTION.TableDontExists;
            }



            return data;
        }
    }
}