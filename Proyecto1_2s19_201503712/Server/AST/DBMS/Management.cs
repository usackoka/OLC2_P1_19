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
        //////////////////////////////////////// PROCEDURES
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public Object createProcedure(CreateProcedure cp, AST_CQL arbol, int fila, int columna) {
            if (getProcedure(cp.id, cp.getFirma()) != null)
            {
                arbol.addError("ProcedureAlreadyExists", "Ya existe el procedure con nombre: " + cp.id, fila, columna);
                return Catch.EXCEPTION.ProcedureAlreadyExists;
            }

            system.procedures.Add(new Procedure(cp));
            return null;
        }

        public Procedure getProcedure(String id, String firma)
        {
            foreach (Procedure ut in system.procedures)
            {
                if (ut.id.Equals(id) && ut.getFirma().Equals(firma))
                {
                    return ut;
                }
            }
            return null;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////// ACCIONES TABLAS
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public Object insertInto(Insert insert, AST_CQL arbol, int fila, int columna) {
            TableCQL table = getTable(insert.idTabla);
            if (table == null) {
                arbol.addError("TableDontExists", "Insert-Into No existe la tabla: " + insert.idTabla,fila,columna);
                return Catch.EXCEPTION.TableDontExists;
            }

            return table.insertValues(insert.columnNames, insert.values, arbol, fila, columna);
        }

        public Object updateTable(Update upd, AST_CQL arbol, int fila, int columna) {
            TableCQL table = getTable(upd.idTabla);
            if (table == null)
            {
                arbol.addError("TableDontExists", "Update No existe la tabla: " + upd.idTabla,fila,columna);
                return Catch.EXCEPTION.TableDontExists;
            }

            return table.updateValues(upd.asignaciones,upd.where, arbol, fila, columna);
        }

        public Object deleteFrom(DeleteFrom delete, AST_CQL arbol, int fila, int columna) {
            TableCQL table = getTable(delete.idTabla);
            if (table == null)
            {
                arbol.addError("TableDontExists", "Delete-From No existe la tabla: " + delete.idTabla,fila,columna);
                return Catch.EXCEPTION.TableDontExists;
            }

            return table.deleteFrom(delete.acceso,delete.where, arbol, fila, columna);
        }

        public Object alterTableAdd(AlterTableAdd alter, AST_CQL arbol, int fila, int columna) {
            TableCQL table = getTable(alter.idTabla);
            if (table == null)
            {
                arbol.addError("TableDontExists", "Alter-Table-Add - No existe la tabla: " + alter.idTabla,fila,columna);
                return Catch.EXCEPTION.TableDontExists;
            }

            return table.Add(alter.atributos);
        }

        public Object alterTableDrop(AlterTableDrop alter, AST_CQL arbol, int fila, int columna) {
            TableCQL table = getTable(alter.idTabla);
            if (table == null)
            {
                arbol.addError("TableDontExists", "Alter-Table-Drop - No existe la tabla: " + alter.idTabla,fila,columna);
                return Catch.EXCEPTION.TableDontExists;
            }

            return table.Drop(alter.atributos, arbol, fila, columna);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////// TABLAS
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public Object createTable(CreateTable ct, AST_CQL arbol, int fila, int columna) {
            if (getTable(ct.id) != null) {
                if (ct.IfNotExists)
                {
                    return null;
                }
                else
                {
                    arbol.addError("EXCEPTION.TableAlreadyExists", "Ya existe la tabla: " + ct.id,fila,columna);
                    return Catch.EXCEPTION.TableAlreadyExists;
                }
            }

            system.tables.Add(new TableCQL(ct.id, ct.columnDefinitions));
            return null;
        }

        public Object truncateTable(String id, AST_CQL arbol, int fila, int columna) {
            TableCQL tb = getTable(id);
            if (tb == null)
            {
                arbol.addError("EXCEPTION.TableDontExists", "No existe la tabla: " + id,fila,columna);
                return Catch.EXCEPTION.TableDontExists;
            }

            tb.restartTable();
            return null;
        }

        public Object dropTable(DropTable dt, AST_CQL arbol, int fila, int columna) {
            TableCQL tb = getTable(dt.id);
            if (tb == null) {
                if (dt.IfExists)
                {
                    return null;
                }
                else {
                    arbol.addError("EXCEPTION.TableDontExists","No existe la tabla: "+dt.id,fila,columna);
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
        public Object createUser(String id, String contraseña, AST_CQL arbol, int fila, int columna) {
            if (getUser(id)!=null)
            {
                arbol.addError("EXCEPTION.UserAlreadyExists", "El user: " + id + " Ya existe ",fila,columna);
                return Catch.EXCEPTION.UserAlreadyExists;
            }

            this.users.Add(new User(id,contraseña));
            return null;
        }

        public Object revokeUser(String idUser, String idBd, AST_CQL arbol, int fila, int columna) {
            if (!usuarioActivo.basesGrant.Contains(idBd))
            {
                arbol.addError("EXCEPTION.UseBDException", "El user: " + idUser + " No contiene permisos para utilizar la base de datos: " + idBd,fila,columna);
                return Catch.EXCEPTION.UseBDException;
            }

            User user = getUser(idUser);
            if (user == null)
            {
                arbol.addError("EXCEPTION.UserDontExists", "El user: " + idUser + " No existe ",fila,columna);
                return Catch.EXCEPTION.UserDontExists;
            }

            if (getDataBase(idBd) == null)
            {
                arbol.addError("EXCEPTION.BDDontExists", "La base de datos: " + idBd + " No existe ",fila,columna);
                return Catch.EXCEPTION.BDDontExists;
            }

            user.basesGrant.Remove(idBd);
            return null;
        }

        public Object grantUser(String idUser, String idBd, AST_CQL arbol, int fila, int columna) {

            if (!usuarioActivo.basesGrant.Contains(idBd)) {
                arbol.addError("EXCEPTION.UseBDException", "El user: "+idUser+" No contiene permisos para utilizar la base de datos: " + idBd,fila,columna);
                return Catch.EXCEPTION.UseBDException;
            }

            User user = getUser(idUser);
            if (user==null) {
                arbol.addError("EXCEPTION.UserDontExists", "El user: " + idUser + " No existe ",fila,columna);
                return Catch.EXCEPTION.UserDontExists;
            }

            if (getDataBase(idBd)==null)
            {
                arbol.addError("EXCEPTION.BDDontExists", "La base de datos: " + idBd + " No existe ",fila,columna);
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
        public Object createDataBase(CreateDataBase createDataBase, AST_CQL arbol, int fila, int columna) {
            //pregunto si existe
            if (getDataBase(createDataBase.id) != null)
            {
                if (createDataBase.IfNotExists)
                {
                    return null;
                }
                else
                {
                    arbol.addError("EXCEPTION.BDAlreadyExists", "Ya existe la base de datos: " + createDataBase.id,fila,columna);
                    return Catch.EXCEPTION.BDAlreadyExists;
                }
            }
            DataBase db = new DataBase(createDataBase.id);
            this.usuarioActivo.basesGrant.Add(db.id);
            bases.Add(db);
            return null;
        }

        public Object dropDataBase(DropDataBase db, AST_CQL arbol, int fila, int columna) {
            DataBase dbEliminar = getDataBase(db.id);
            if (dbEliminar == null)
            {
                arbol.addError("EXCEPTION.BDDontExists", "No existe la base de datos: " + db.id,fila,columna);
                return Catch.EXCEPTION.BDDontExists;
            }

            bases.Remove(dbEliminar);
            return null;
        }

        public Object useDataBase(String id, AST_CQL arbol, int fila, int columna) {
            DataBase db = getDataBase(id);
            if (db==null) {
                arbol.addError("EXCEPTION.BDDontExists","No existe la base de datos: "+id,fila,columna);
                return Catch.EXCEPTION.BDDontExists;
            }

            if (!usuarioActivo.basesGrant.Contains(id))
            {
                arbol.addError("EXCEPTION.UseBDException", "No posee permisos para utilizar la base de datos: " + id,fila,columna);
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
        public Object createUserType(CreateUserType createUserType, AST_CQL arbol, int fila, int columna)
        {
            //pregunto si existe
            if (getUserType(createUserType.id)!=null) {
                if (createUserType.IfNotExists) {
                    return null;
                }
                else
                {
                    arbol.addError("EXCEPTION.TypeAlreadyExists", "Ya existe el userType: " + createUserType.id,fila,columna);
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