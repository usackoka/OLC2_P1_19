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

        public override string ToString()
        {
            String trad = "";
            trad += "<\n";
            trad += "\"NAME\"=\"" + this.id + "\",\n";
            trad += "\"PASSWORD\"=\"" + this.contraseña + "\",\n";
            trad += "\"PERMISSIONS\"= [" + getPermisos() +"]";
            trad += "\n>";
            return trad;
        }

        public string getPermisos(){
            String trad = "";
            foreach (String grant in basesGrant) {
                trad += "\n   <";
                trad += "\"NAME\"=\""+grant+"\"";
                trad += "\n   >,";
            }
            trad = trad.TrimEnd(',');
            return trad;
        }

    }
}