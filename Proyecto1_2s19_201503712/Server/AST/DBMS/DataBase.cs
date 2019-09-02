using Server.AST.CQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.DBMS
{
    public class DataBase
    {
        public String id;
        public List<UserType> userTypes;
        public List<TableCQL> tables;
        public List<Procedure> procedures;

        public DataBase(String id) {
            this.id = id;
            this.userTypes = new List<UserType>();
            this.tables = new List<TableCQL>();
            this.procedures = new List<Procedure>();
        }

        public override string ToString()
        {
            String trad = "";
            trad += "<\n";
            trad += "\"NAME\"=\""+this.id+"\",\n";
            String dat = getTablas() + getUserTypes() + getProcedures();
            dat = dat.TrimEnd(',');
            trad += "\"DATA\"= [" + dat +"]";
            trad += "\n>";
            return trad;
        }

        public string getUserTypes() {
            String trad = "";
            foreach (UserType ut in this.userTypes) {
                trad += ut+",";
            }
            return trad;
        }

        public string getProcedures()
        {
            String trad = "";
            foreach (Procedure ut in this.procedures)
            {
                trad += ut + ",";
            }
            return trad;
        }

        public string getTablas() {
            String trad = "";
            foreach (TableCQL tabla in this.tables) {
                trad += tabla + ",";
            }
            return trad;
        }
    }
}