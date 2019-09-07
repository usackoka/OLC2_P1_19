using Server.AST.DBMS;
using Server.AST.ExpresionesCQL.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class ColumnCQL
    {
        int fila, columna;
        public List<Object> valores;
        public int counter;

        public String id;
        public Object tipoDato;
        public Boolean primaryKey;
        public ColumnCQL(String id, Object tipoDato, Boolean primaryKey ,int fila, int columna) {
            this.id = id;
            this.tipoDato = tipoDato;
            this.primaryKey = primaryKey;
            this.fila = fila;
            this.columna = columna;
            this.counter = 0;
            this.primaryKeys = null;
            this.valores = new List<object>();
        }

        public List<String> primaryKeys;
        public ColumnCQL(List<String> primaryKeys, int fila, int columna) {
            this.primaryKeys = primaryKeys;
            this.fila = fila;
            this.counter = 0;
            this.columna = columna;
        }

        //para la copia
        public ColumnCQL(ColumnCQL copia) {
            this.id = copia.id;
            this.tipoDato = copia.tipoDato;
            this.primaryKey = copia.primaryKey;
            this.fila = copia.fila;
            this.columna = copia.columna;
            this.primaryKeys = copia.primaryKeys;
            this.valores = copia.valores;
        }

        public string getData(int i) {
            String trad = "";
            if (this.valores[i] is String)
            {
                trad += "       \"" + this.id + "\"=\"" + this.valores[i] + "\"";
            }
            else if (this.valores[i] is UserType) {
                trad += "       \"" + this.id + "\"="+((UserType)this.valores[i]).getData();
            }
            else if (this.valores[i] is TimeSpan)
            {
                trad += "       \"" + this.id + "\"='" + this.valores[i] + "'";
            }
            else if (this.valores[i] is Date)
            {
                trad += "       \"" + this.id + "\"='" + this.valores[i] + "'";
            }
            else
            {
                trad += "       \"" + this.id + "\"=" + this.valores[i];
            }
            return trad;
        }

        public override string ToString()
        {
            String trad = "";
            trad += "       <\n";
            trad += "       \"NAME\"=\""+this.id+"\",\n";
            trad += "       \"TYPE\"=\""+this.tipoDato+"\",\n";
            trad += "       \"PK\"="+(this.primaryKey?"TRUE":"FALSE")+"\n";
            trad += "       >";
            return trad;
        }
    }
}