using Server.AST;
using Server.AST.DBMS;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Analizador.Chison
{
    public class UserCHISON
    {
        public List<User> users;
        int fila, columna;
        public UserCHISON(List<User> users, int fila, int columna) {
            this.fila = fila;
            this.columna = columna;
            this.users = users;
        }

        public object Ejecutar(Management dbms)
        {
            //agrego los users
            foreach (User user in this.users) {
                dbms.createUser(user.getName(dbms),user.getPassword(dbms),fila,columna);
            }

            //agrego sus permisos
            foreach (User user in this.users) {
                dbms.getUser(user.getName(dbms)).basesGrant.AddRange(user.getGrants(dbms));
            }

            return null;
        }
    }
}