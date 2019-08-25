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
            User user = new User("admin", "admin");
            this.users.Add(user);

            //base de datos (defecto)
            system = new DataBase("virtual");

            //para mientras que no hay login
            usuarioActivo = user;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////// ACCIONES TABLAS
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public Object insertInto(Insert insert, AST_CQL arbol) {
            TableCQL table = getTable(insert.idTabla);
            if (table==null) {
                return Catch.EXCEPTION.TableDontExists;
            }

            return table.insertValues(insert.columnNames, insert.values, arbol);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////// TABLAS
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public Object createTable(CreateTable ct) {
            if (getTable(ct.id) != null) {
                if (ct.IfNotExists)
                {
                    return null;
                }
                else {
                    return Catch.EXCEPTION.TableAlreadyExists;
                }
            }

            system.tables.Add(new TableCQL(ct.id, ct.columnDefinitions));
            return null;
        }

        public Object truncateTable(String id) {
            TableCQL tb = getTable(id);
            if (tb == null) {
                return Catch.EXCEPTION.TableDontExists;
            }

            tb.restartTable();
            return null;
        }

        public Object dropTable(DropTable dt) {
            TableCQL tb = getTable(dt.id);
            if (tb == null) {
                if (dt.IfExists)
                {
                    return null;
                }
                else {
                    return Catch.EXCEPTION.TableDontExists;
                }
            }

            system.tables.Remove(tb);
            return null;
        }

        public TableCQL getTable(String id)
        {
            foreach (TableCQL ut in system.tables)
            {
                if (ut.id.Equals(id))
                {
                    return ut;
                }
            }
            return null;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////// USUARIOS
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public Object createUser(String id, String contraseña) {
            if (getUser(id)!=null) {
                return Catch.EXCEPTION.UserAlreadyExists;
            }

            this.users.Add(new User(id,contraseña));
            return null;
        }

        public Object revokeUser(String idUser, String idBd) {
            if (!usuarioActivo.basesGrant.Contains(idBd))
            {
                return Catch.EXCEPTION.UseBDException;
            }

            User user = getUser(idUser);
            if (user == null)
            {
                return Catch.EXCEPTION.UserDontExists;
            }

            if (getDataBase(idBd) == null)
            {
                return Catch.EXCEPTION.BDDontExists;
            }

            user.basesGrant.Remove(idBd);
            return null;
        }

        public Object grantUser(String idUser, String idBd) {
            if (!usuarioActivo.basesGrant.Contains(idBd)) {
                return Catch.EXCEPTION.UseBDException;
            }

            User user = getUser(idUser);
            if (user==null) {
                return Catch.EXCEPTION.UserDontExists;
            }

            if (getDataBase(idBd)==null) {
                return Catch.EXCEPTION.BDDontExists;
            }

            user.basesGrant.Add(idBd);
            return null;
        }

        public User getUser(String id) {
            foreach (User user in this.users) {
                if (user.id.Equals(id)) {
                    return user;
                }
            }
            return null;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////// BASE DE DATOS
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public Object createDataBase(CreateDataBase createDataBase) {
            //pregunto si existe
            if (getDataBase(createDataBase.id) != null)
            {
                if (createDataBase.IfNotExists)
                {
                    return null;
                }
                else
                {
                    return Catch.EXCEPTION.BDAlreadyExists;
                }
            }

            bases.Add(new DataBase(createDataBase.id));
            return null;
        }

        public Object dropDataBase(DropDataBase db) {
            DataBase dbEliminar = getDataBase(db.id);
            if (dbEliminar != null)
            {
                return Catch.EXCEPTION.BDDontExists;
            }

            bases.Remove(dbEliminar);
            return null;
        }

        public Object useDataBase(String id) {
            DataBase db = getDataBase(id);
            if (db==null) {
                return Catch.EXCEPTION.BDDontExists;
            }

            if (!usuarioActivo.basesGrant.Contains(id)) {
                return Catch.EXCEPTION.UseBDException;
            }

            this.system = db;
            return null;
        }

        public DataBase getDataBase(String id)
        {
            foreach (DataBase ut in bases)
            {
                if (ut.id.Equals(id))
                {
                    return ut;
                }
            }
            return null;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////// CREATE TYPES 
        ////////////////////////////////////////////////////////////////////////////////////////////////////
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
            system.userTypes.Add(new UserType(createUserType));
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