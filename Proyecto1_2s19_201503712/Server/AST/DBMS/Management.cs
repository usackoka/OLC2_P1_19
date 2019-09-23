using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Server.Analizador;
using Server.Analizador.Chison;
using Server.AST.CQL;
using Server.AST.ExpresionesCQL;
using Server.AST.SentenciasCQL;
using Server.Otros;

namespace Server.AST.DBMS
{
    public class Management
    {
        public DataBase system;
        public User usuarioActivo { get; set; }
        List<User> users;
        public List<DataBase> bases;
        public List<clsToken> errores;

        public Management() {
            this.bases = new List<DataBase>();
            this.users = new List<User>();
            this.errores = new List<clsToken>();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////// OTROS
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public void addError(String lexema, String descripcion, int fila, int columna)
        {
            this.errores.Add(new clsToken(lexema, descripcion, fila, columna, "Semántico", ""));
        }

        public Object login(String id, String pass) {
            User user = getUser(id);
            if (user != null)
            {
                if (user.contraseña.Equals(pass))
                {
                    return user;
                }
                else
                {
                    return "[+LOGIN]\n  [FAIL]\n[-LOGIN]";
                }
            }
            else
            {
                return "[+LOGIN]\n  [FAIL]\n[-LOGIN]";
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////// CHISON
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public void analizarChison(String ruta) {
            //============ creo el archivo chison principal
            if(ruta=="")
                ruta = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Chisons\principal.chison");

            if (!File.Exists(ruta)) {
                addError("Analizar Chison","No existe el chison principal.chison en: "+ruta,0,0);
                return;
            }

            System.IO.StreamReader f = new System.IO.StreamReader(ruta);

            String lineas = f.ReadToEnd();
            f.Close();

            Generador parserChison = new Generador();
            if (parserChison.esCadenaValida(lineas, new GramaticaChison()))
            {
                if (parserChison.padre.Root != null)
                {
                    //Graficar.ConstruirArbol(parserChison.padre.Root, "AST_CHISON", "");
                    RecorridoChison recorrido = new RecorridoChison(parserChison.padre.Root, this);
                    recorrido.ejecutarChison();
                }
            }
            else
            {
                foreach (clsToken error in parserChison.ListaErrores)
                {//errores
                    errores.Add(new clsToken(error.lexema+" - Analizador Chison",error.descripcion, error.fila, error.columna, error.tipo,error.ambito));
                }
            }
        }

        public void createChisons(String ruta) {

            //============ creo el archivo chison principal
            if (ruta.Equals(""))
                ruta = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Chisons\principal.chison");

            try
            {
                System.IO.File.Delete(ruta);
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
            System.IO.StreamWriter f = new System.IO.StreamWriter(ruta);

            String trad = "$<\n";
            trad += "\"DATABASES\"=[" +getDatabases()+ "],\n";
            trad += "\"USERS\"=["+getUsers()+"]\n";
            trad += "\n>$";
            f.Write(trad);

            f.Close();
        }

        public string getUsers() {
            String trad = "";
            foreach (User db in this.users)
            {
                trad += "\n" + db + ",";
            }
            trad = trad.TrimEnd(',');
            return trad;
        }

        public string getDatabases() {
            String trad = "";
            foreach (DataBase db in this.bases) {
                trad += "\n"+db+",";
            }
            trad = trad.TrimEnd(',');
            return trad;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////// PROCEDURES
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public Object createProcedure(CreateProcedure cp, AST_CQL arbol, int fila, int columna) {

            if (this.system == null)
            {
                arbol.addError("System-NULL", "No hay ninguna base de datos en uso", fila, columna);
                return null;
            }

            if (getProcedure(cp.id, cp.getFirma(),arbol) != null)
            {
                arbol.addError("ProcedureAlreadyExists", "Ya existe el procedure con nombre: " + cp.id, fila, columna);
                return new ExceptionCQL(ExceptionCQL.EXCEPTION.ProcedureAlreadyExists, "Ya existe el procedure con nombre: " + cp.id,fila,columna);
            }

            system.procedures.Add(new Procedure(cp));
            return null;
        }

        public Procedure getProcedure(String id, String firma, AST_CQL arbol)
        {
            if (this.system == null)
            {
                arbol.addError("System-NULL", "No hay ninguna base de datos en uso", 0, 0);
                return null;
            }

            foreach (Procedure ut in system.procedures)
            {
                if (ut.id.ToLower().Equals(id.ToLower()) && ut.getFirma().Equals(firma))
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
            TableCQL table = getTable(insert.idTabla,arbol);
            if (table == null) {
                arbol.addError("TableDontExists", "Insert-Into No existe la tabla: " + insert.idTabla,fila,columna);
                return new ExceptionCQL(ExceptionCQL.EXCEPTION.TableDontExists, "Insert-Into No existe la tabla: " + insert.idTabla,fila,columna);
            }

            return table.insertValues(insert.columnNames, insert.values, arbol, fila, columna);
        }

        public Object updateTable(Update upd, AST_CQL arbol, int fila, int columna) {
            TableCQL table = getTable(upd.idTabla,arbol);
            if (table == null)
            {
                arbol.addError("TableDontExists", "Update No existe la tabla: " + upd.idTabla,fila,columna);
                return new ExceptionCQL(ExceptionCQL.EXCEPTION.TableDontExists, "Update No existe la tabla: " + upd.idTabla,fila,columna);
            }

            return table.updateValues(upd.asignaciones,upd.where, arbol, fila, columna);
        }

        public Object deleteFrom(DeleteFrom delete, AST_CQL arbol, int fila, int columna) {
            TableCQL table = getTable(delete.idTabla,arbol);
            if (table == null)
            {
                arbol.addError("TableDontExists", "Delete-From No existe la tabla: " + delete.idTabla,fila,columna);
                return new ExceptionCQL(ExceptionCQL.EXCEPTION.TableDontExists, "Delete-From No existe la tabla: " + delete.idTabla,fila,columna);
            }

            return table.deleteFrom(delete.acceso,delete.where, arbol, fila, columna);
        }

        public Object alterTableAdd(AlterTableAdd alter, AST_CQL arbol, int fila, int columna) {
            TableCQL table = getTable(alter.idTabla,arbol);
            if (table == null)
            {
                arbol.addError("TableDontExists", "Alter-Table-Add - No existe la tabla: " + alter.idTabla,fila,columna);
                return new ExceptionCQL(ExceptionCQL.EXCEPTION.TableDontExists, "Alter-Table-Add - No existe la tabla: " + alter.idTabla,fila,columna);
            }

            return table.Add(alter.atributos);
        }

        public Object alterTableDrop(AlterTableDrop alter, AST_CQL arbol, int fila, int columna) {
            TableCQL table = getTable(alter.idTabla,arbol);
            if (table == null)
            {
                arbol.addError("TableDontExists", "Alter-Table-Drop - No existe la tabla: " + alter.idTabla,fila,columna);
                return new ExceptionCQL(ExceptionCQL.EXCEPTION.TableDontExists, "Alter-Table-Drop - No existe la tabla: " + alter.idTabla,fila,columna);
            }

            return table.Drop(alter.atributos, arbol, fila, columna);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////// TABLAS
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public Object createTable(CreateTable ct, AST_CQL arbol, int fila, int columna) {

            if (this.system == null)
            {
                arbol.addError("System-NULL", "No hay ninguna base de datos en uso", fila, columna);
                return null;
            }

            if (getTable(ct.id,arbol) != null) {
                if (ct.IfNotExists)
                {
                    return null;
                }
                else
                {
                    arbol.addError("EXCEPTION.TableAlreadyExists", "Ya existe la tabla: " + ct.id,fila,columna);
                    return new ExceptionCQL(ExceptionCQL.EXCEPTION.TableAlreadyExists, "Ya existe la tabla: " + ct.id,fila,columna);
                }
            }

            system.tables.Add(new TableCQL(ct.id, ct.columnDefinitions));
            return null;
        }

        public Object truncateTable(String id, AST_CQL arbol, int fila, int columna) {
            TableCQL tb = getTable(id,arbol);
            if (tb == null)
            {
                arbol.addError("EXCEPTION.TableDontExists", "No existe la tabla: " + id,fila,columna);
                return new ExceptionCQL(ExceptionCQL.EXCEPTION.TableDontExists, "No existe la tabla: " + id,fila,columna);
            }

            tb.restartTable();
            return null;
        }

        public Object dropTable(DropTable dt, AST_CQL arbol, int fila, int columna) {
            if (this.system == null)
            {
                arbol.addError("System-NULL", "No hay ninguna base de datos en uso", fila, columna);
                return null;
            }

            TableCQL tb = getTable(dt.id, arbol);
            if (tb == null) {
                if (dt.IfExists)
                {
                    return null;
                }
                else {
                    arbol.addError("EXCEPTION.TableDontExists","No existe la tabla: "+dt.id,fila,columna);
                    return new ExceptionCQL(ExceptionCQL.EXCEPTION.TableDontExists, "No existe la tabla: " + dt.id,fila,columna);
                }
            }

            system.tables.Remove(tb);
            return null;
        }

        public TableCQL getTable(String id, AST_CQL arbol)
        {
            if (this.system == null)
            {
                arbol.addError("System-NULL", "No hay ninguna base de datos en uso", 0, 0);
                return null;
            }

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
                return new ExceptionCQL(ExceptionCQL.EXCEPTION.UserAlreadyExists, "El user: " + id + " Ya existe ", fila, columna);
            }

            this.users.Add(new User(id,contraseña));
            return null;
        }

        //para el que se crea desde chison
        public Object createUser(String id, String contraseña, int fila, int columna)
        {
            if (getUser(id) != null)
            {
                addError("EXCEPTION.UserAlreadyExists", "El user: " + id + " Ya existe ", fila, columna);
                return new ExceptionCQL(ExceptionCQL.EXCEPTION.UserAlreadyExists, "El user: " + id + " Ya existe ",fila,columna);
            }

            this.users.Add(new User(id, contraseña));
            return null;
        }

        public Object revokeUser(String idUser, String idBd, AST_CQL arbol, int fila, int columna) {
            if (!usuarioActivo.basesGrant.Contains(idBd))
            {
                arbol.addError("EXCEPTION.UseBDException", "El user: " + idUser + " No contiene permisos para utilizar la base de datos: " + idBd,fila,columna);
                return new ExceptionCQL(ExceptionCQL.EXCEPTION.UseBDException, "El user: " + idUser + " No contiene permisos para utilizar la base de datos: " + idBd,fila,columna);
            }

            User user = getUser(idUser);
            if (user == null)
            {
                arbol.addError("EXCEPTION.UserDontExists", "El user: " + idUser + " No existe ",fila,columna);
                return new ExceptionCQL(ExceptionCQL.EXCEPTION.UserDontExists, "El user: " + idUser + " No existe ",fila,columna);
            }

            if (getDataBase(idBd) == null)
            {
                arbol.addError("EXCEPTION.BDDontExists", "La base de datos: " + idBd + " No existe ",fila,columna);
                return new ExceptionCQL(ExceptionCQL.EXCEPTION.BDDontExists, "La base de datos: " + idBd + " No existe ",fila,columna);
            }

            user.basesGrant.Remove(idBd);
            return null;
        }

        public Object grantUser(String idUser, String idBd, AST_CQL arbol, int fila, int columna) {

            if (!usuarioActivo.basesGrant.Contains(idBd)) {
                arbol.addError("EXCEPTION.UseBDException", "El user: "+idUser+" No contiene permisos para utilizar la base de datos: " + idBd,fila,columna);
                return new ExceptionCQL(ExceptionCQL.EXCEPTION.UseBDException, "El user: " + idUser + " No contiene permisos para utilizar la base de datos: " + idBd,fila,columna);
            }

            User user = getUser(idUser);
            if (user==null) {
                arbol.addError("EXCEPTION.UserDontExists", "El user: " + idUser + " No existe ",fila,columna);
                return new ExceptionCQL(ExceptionCQL.EXCEPTION.UserDontExists, "El user: " + idUser + " No existe ",fila,columna);
            }

            if (getDataBase(idBd)==null)
            {
                arbol.addError("EXCEPTION.BDDontExists", "La base de datos: " + idBd + " No existe ",fila,columna);
                return new ExceptionCQL(ExceptionCQL.EXCEPTION.BDDontExists, "La base de datos: " + idBd + " No existe ",fila,columna);
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
        ///
        /// =============== para el que viene desde chison
        public Object createDataBase(String id, int fila, int columna)
        {
            //pregunto si existe
            if (getDataBase(id) != null)
            {
                addError("EXCEPTION.BDAlreadyExists - Chison", "Ya existe la base de datos: " + id, fila, columna);
                return new ExceptionCQL(ExceptionCQL.EXCEPTION.BDAlreadyExists, "Ya existe la base de datos: " + id,fila,columna);
            }
            DataBase db = new DataBase(id);
            //this.usuarioActivo.basesGrant.Add(db.id);
            bases.Add(db);
            return null;
        }

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
                    return new ExceptionCQL(ExceptionCQL.EXCEPTION.BDAlreadyExists, "Ya existe la base de datos: " + createDataBase.id,fila,columna);
                }
            }
            DataBase db = new DataBase(createDataBase.id);
            this.usuarioActivo.basesGrant.Add(db.id);

            //=================== hago el grant para el admin
            User admin = getUser("admin");
            if (admin!=null && !admin.basesGrant.Contains(db.id)) {
                admin.basesGrant.Add(db.id);
            }

            bases.Add(db);
            return null;
        }

        public Object dropDataBase(DropDataBase db, AST_CQL arbol, int fila, int columna) {
            DataBase dbEliminar = getDataBase(db.id);
            if (dbEliminar == null)
            {
                arbol.addError("EXCEPTION.BDDontExists", "No existe la base de datos: " + db.id,fila,columna);
                return new ExceptionCQL(ExceptionCQL.EXCEPTION.BDDontExists, "No existe la base de datos: " + db.id,fila,columna);
            }

            bases.Remove(dbEliminar);
            return null;
        }

        public Object useDataBase(String id, AST_CQL arbol, int fila, int columna) {
            DataBase db = getDataBase(id);
            if (db==null) {
                arbol.addError("EXCEPTION.BDDontExists","No existe la base de datos: "+id,fila,columna);
                return new ExceptionCQL(ExceptionCQL.EXCEPTION.BDDontExists, "No existe la base de datos: " + id,fila,columna);
            }

            if (!usuarioActivo.basesGrant.Contains(id))
            {
                arbol.addError("EXCEPTION.UseBDException", "No posee permisos para utilizar la base de datos: " + id,fila,columna);
                return new ExceptionCQL(ExceptionCQL.EXCEPTION.UseBDException, "No posee permisos para utilizar la base de datos: " + id,fila,columna);
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
            if (this.system == null)
            {
                arbol.addError("System-NULL", "No hay ninguna base de datos en uso", 0, 0);
                return null;
            }

            //pregunto si existe
            if (getUserType(createUserType.id,arbol)!=null) {
                if (createUserType.IfNotExists) {
                    return null;
                }
                else
                {
                    arbol.addError("EXCEPTION.TypeAlreadyExists", "Ya existe el userType: " + createUserType.id,fila,columna);
                    return new ExceptionCQL(ExceptionCQL.EXCEPTION.TypeAlreadyExists, "Ya existe el userType: " + createUserType.id,fila,columna);
                }
            }
            system.userTypes.Add(new UserType(createUserType));
            return null;
        }

        //para cuando viene de chison
        public UserType getUserType(String id)
        {

            if (this.system == null)
            {
                addError("System-NULL", "No hay ninguna base de datos en uso", 0, 0);
                return null;
            }

            foreach (UserType ut in system.userTypes)
            {
                if (ut.id.Equals(id))
                {
                    return ut;
                }
            }
            return null;
        }

        public UserType getUserType(String id, AST_CQL arbol) {

            if (this.system==null) {
                arbol.addError("System-NULL","No hay ninguna base de datos en uso",0,0);
                return null;
            }

            foreach (UserType ut in system.userTypes) {
                if (ut.id.Equals(id)) {
                    return ut;
                }
            }
            return null;
        }
    }
}