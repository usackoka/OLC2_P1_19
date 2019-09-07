using Server.AST;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Analizador.Chison
{
    public class UserCHISON:Sentencia
    {
        public List<User> users;
        public UserCHISON(List<User> users, int fila, int columna) {
            this.fila = fila;
            this.columna = columna;
            this.users = users;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            //agrego los users
            foreach (User user in this.users) {
                arbol.dbms.createUser(user.getName(arbol),user.getPassword(arbol),arbol,fila,columna);
            }

            //agrego sus permisos
            foreach (User user in this.users) {
                arbol.dbms.getUser(user.getName(arbol)).basesGrant.AddRange(user.getGrants(arbol));
            }

            return null;
        }
    }
}