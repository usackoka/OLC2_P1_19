using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.DBMS
{
    public class User
    {
        String id;
        String contraseña;
        List<String> grants;

        public User(String id, String contraseña) {
            this.id = id;
            this.contraseña = contraseña;
            this.grants = new List<string>();
        }


    }
}