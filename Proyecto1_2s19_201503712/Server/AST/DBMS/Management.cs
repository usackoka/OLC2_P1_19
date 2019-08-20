using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.AST.CQL;
using Server.AST.SentenciasCQL;

namespace Server.AST.DBMS
{
    public class Management
    {
        DataBase system;
        User usuarioActivo;
        List<User> users;
        List<DataBase> bases;

        public Management() {
            this.bases = new List<DataBase>();
            this.users = new List<User>();
            this.usuarioActivo = null;

            //Usuario por defecto
            User user = new User("admin","admin");
            this.users.Add(user);

            //base de datos (defecto)
            system = new DataBase("virtual");
        }

        public Object createUserType(CreateUserType createUserType)
        {
            //pregunto si existe
            if (getUserType(createUserType.id)!=null) {
                if (createUserType.IfNotExists) {
                    return null;
                }
                else {
                    return Catch.EXCEPTION.TypeAlreadyExists;
                }
            }
            UserType ut = new UserType(createUserType);
            system.userTypes.Add(ut);
            return null;
        }

        public UserType getUserType(String id) {
            foreach (UserType ut in system.userTypes) {
                if (ut.id.Equals(id)) {
                    return ut;
                }
            }
            return null;
        }
    }
}