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
        
    }
}