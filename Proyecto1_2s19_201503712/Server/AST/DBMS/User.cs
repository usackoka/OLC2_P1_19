using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.DBMS
{
    public class User
    {
        public String id { get; set; }
        public String contraseña { get; set; }
        public List<String> basesGrant { get; set; }

        public User(String id, String contraseña) {
            this.id = id;
            this.contraseña = contraseña;
            this.basesGrant = new List<string>();
        }
        
    }
}