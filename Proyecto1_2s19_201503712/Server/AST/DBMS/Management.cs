using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.DBMS
{
    public class Management
    {
        DataBase baseUse;
        List<User> users;
        List<DataBase> bases;

        public Management() {
            this.bases = new List<DataBase>();
            this.users = new List<User>();
            baseUse = null;
        }


    }
}