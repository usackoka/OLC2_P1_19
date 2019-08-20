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
        public List<String> usersGrant;

        public DataBase(String id) {
            this.id = id;
            this.userTypes = new List<UserType>();
            this.usersGrant = new List<string>();
        }
    }
}