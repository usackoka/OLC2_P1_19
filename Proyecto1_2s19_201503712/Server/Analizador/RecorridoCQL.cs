using Irony.Parsing;
using Server.AST;
using Server.AST.ColeccionesCQL;
using Server.AST.CQL;
using Server.AST.DBMS;
using Server.AST.ExpresionesCQL;
using Server.AST.ExpresionesCQL.Tipos;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Server.Analizador
{
    public class RecorridoCQL
    {
        public AST_CQL ast { get; set; }
        ParseTreeNode padre;

        public RecorridoCQL(ParseTreeNode padre, Management dbms)
        {
            ast = new AST_CQL();
            this.padre = padre;
            ast.dbms = dbms;
        }

        public void Ejecutar() {
            ast.nodos = (List<NodoCQL>)recorrido(padre);
            ast.Ejecutar();
        }

        public List<NodoCQL> RecorridoDesdeChison() {
            return (List<NodoCQL>)recorrido(padre);
        }

        bool agregarDolar;
        Object recorrido(ParseTreeNode raiz)
        {
            if (CompararNombre(raiz, "BLOCK"))
            {
                List<NodoCQL> lista = new List<NodoCQL>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes)
                {
                    try
                    {
                        NodoCQL nod = (NodoCQL)recorrido(nodo);
                        if (nod is Funcion)
                        {
                            ast.funciones.Add((Funcion)nod);
                        }
                        else
                        {
                            lista.Add(nod);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex);
                    }
                }
                return lista;
            }
            else if (CompararNombre(raiz, "GLOBAL"))
            {
                return recorrido(raiz.ChildNodes[0]);
            }
            else if (CompararNombre(raiz, "DECLARACION"))
            {
                return new Declaracion(recorrido(raiz.ChildNodes[0]), (List<KeyValuePair<String, Expresion>>)recorrido(raiz.ChildNodes[1]), getFila(raiz.ChildNodes[0], 0), getColumna(raiz.ChildNodes[0], 0));
            }
            else if (CompararNombre(raiz, "LISTA_DECLARACION_E"))
            {
                List<KeyValuePair<String, Expresion>> lista = new List<KeyValuePair<String, Expresion>>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes)
                {
                    lista.Add((KeyValuePair<String, Expresion>)recorrido(nodo));
                }
                return lista;
            }
            else if (CompararNombre(raiz, "KEY_VALUE_LIST"))
            {
                List<KeyValuePair<Expresion, Expresion>> lista = new List<KeyValuePair<Expresion, Expresion>>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes)
                {
                    lista.Add((KeyValuePair<Expresion, Expresion>)recorrido(nodo));
                }
                return lista;
            }
            else if (CompararNombre(raiz, "LISTA_CQLTIPOS"))
            {
                List<KeyValuePair<String, Object>> list = new List<KeyValuePair<string, object>>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes)
                {
                    list.Add((KeyValuePair<String, Object>)recorrido(nodo));
                }
                return list;
            }
            else if (CompararNombre(raiz, "CQLTIPO"))
            {
                // id + TIPO;
                return new KeyValuePair<String, Object>(getLexema(raiz, 0), recorrido(raiz.ChildNodes[1]));
            }
            else if (CompararNombre(raiz, "INE"))
            {
                return raiz.ChildNodes.Count != 0 ? true : false;
            }
            else if (CompararNombre(raiz, "IE"))
            {
                return raiz.ChildNodes.Count != 0 ? true : false;
            }
            else if (CompararNombre(raiz, "FUN_AGR"))
            {
                //TIPO_AGR + l_parent + menor_que + SELECT + mayor_que + r_parent;
                return new Agregacion((Agregacion.TIPO_AGR)recorrido(raiz.ChildNodes[0]), (Select)recorrido(raiz.ChildNodes[3]),
                     getFila(raiz, 1), getColumna(raiz, 1));
            }
            else if (CompararNombre(raiz, "TIPO_AGR"))
            {
                String tipo = getLexema(raiz, 0);
                /*res_count
                | res_min
                | res_max
                | res_sum
                | res_avg;*/
                if (tipo.Equals("count", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return Agregacion.TIPO_AGR.COUNT;
                }
                else if (tipo.Equals("min", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return Agregacion.TIPO_AGR.MIN;
                }
                else if (tipo.Equals("max", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return Agregacion.TIPO_AGR.MAX;
                }
                else if (tipo.Equals("sum", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return Agregacion.TIPO_AGR.SUM;
                }
                else {
                    return Agregacion.TIPO_AGR.AVG;
                }
            }
            else if (CompararNombre(raiz, "TCL")) {
                /*res_commit
                | res_rollback;*/
                String tipo = getLexema(raiz, 0);
                if (tipo.Equals("commit", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return new Commit(getFila(raiz, 0), getColumna(raiz, 0),false);
                }
                else {
                    return new RollBack(getFila(raiz, 0), getColumna(raiz, 0),false);
                }
            }
            else if (CompararNombre(raiz, "TYPES"))
            {
                /*res_create + res_type + INE + id + l_parent + LISTA_CQLTIPOS + r_parent*/
                return new CreateUserType(Convert.ToBoolean(recorrido(raiz.ChildNodes[2])), getLexema(raiz, 3),
                        (List<KeyValuePair<String, Object>>)recorrido(raiz.ChildNodes[5]), getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "KEY_VALUE"))
            {
                //E dospuntos E
                return new KeyValuePair<Expresion, Expresion>((Expresion)recorrido(raiz.ChildNodes[0]), (Expresion)recorrido(raiz.ChildNodes[2]));
            }
            else if (CompararNombre(raiz, "LISTA_COLDEF"))
            {
                List<ColumnCQL> lista = new List<ColumnCQL>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes)
                {
                    lista.Add((ColumnCQL)recorrido(nodo));
                }
                return lista;
            }
            else if (CompararNombre(raiz, "LISTA_IDS"))
            {
                List<String> lista = new List<string>();
                for (int i = 0; i < raiz.ChildNodes.Count; i++)
                {
                    lista.Add(getLexema(raiz, i));
                }
                return lista;
            }
            else if (CompararNombre(raiz, "COLDEF"))
            {
                /*id + TIPO + res_primary + res_key
                | id + TIPO
                | res_primary + res_key + l_parent + LISTA_IDS + r_parent;*/
                if (raiz.ChildNodes.Count == 5)
                {
                    return new ColumnCQL((List<String>)recorrido(raiz.ChildNodes[3]),
                        getFila(raiz, 0), getColumna(raiz, 0));
                }
                else if (raiz.ChildNodes.Count == 2)
                {
                    return new ColumnCQL(getLexema(raiz, 0), recorrido(raiz.ChildNodes[1]), false,
                        getFila(raiz, 0), getColumna(raiz, 0));
                }
                else
                {
                    return new ColumnCQL(getLexema(raiz, 0), recorrido(raiz.ChildNodes[1]), true,
                        getFila(raiz, 0), getColumna(raiz, 0));
                }
            }
            else if (CompararNombre(raiz, "DCL"))
            {
                /*res_create + res_user + id + res_with + res_password + E
                | res_grant + id + res_on + id
                | res_revoke + id + res_on + id;*/
                if (ContainsString(getLexema(raiz, 0), "create"))
                {
                    return new CreateUser(getLexema(raiz, 2), (Expresion)recorrido(raiz.ChildNodes[5]),
                        getFila(raiz, 0), getColumna(raiz, 0));
                }
                else if (ContainsString(getLexema(raiz, 0), "grant"))
                {
                    return new Grant(getLexema(raiz, 1), getLexema(raiz, 3), getFila(raiz, 0), getColumna(raiz, 0));
                }
                else
                {
                    return new Revoke(getLexema(raiz, 1), getLexema(raiz, 3), getFila(raiz, 0), getColumna(raiz, 0));
                }
            }
            else if (CompararNombre(raiz, "DDL"))
            {
                /*res_create + res_table + INE + id + l_parent + LISTA_COLDEF + r_parent
                | res_alter + res_table + id + res_add + LISTA_CQLTIPOS
                | res_alter + res_table + id + res_drop + LISTA_IDS
                | res_create + res_database + INE + id
                | res_drop + res_table + IE + id
                | res_truncate + res_table + id
                | res_drop + res_database + id
                | res_use + id;*/
                if (ContainsString(getLexema(raiz, 0), "create") && ContainsString(getLexema(raiz, 1), "database"))
                {
                    return new CreateDataBase(Convert.ToBoolean(recorrido(raiz.ChildNodes[2])), getLexema(raiz, 3),
                        getFila(raiz, 0), getColumna(raiz, 0));
                }
                else if (ContainsString(getLexema(raiz, 0), "drop") && ContainsString(getLexema(raiz, 1), "database"))
                {
                    return new DropDataBase(getLexema(raiz, 2), getFila(raiz, 0), getColumna(raiz, 0));
                }
                else if (ContainsString(getLexema(raiz, 0), "drop") && ContainsString(getLexema(raiz, 1), "table"))
                {
                    return new DropTable(Convert.ToBoolean(recorrido(raiz.ChildNodes[2])), getLexema(raiz, 3),
                        getFila(raiz, 0), getColumna(raiz, 0));
                }
                else if (ContainsString(getLexema(raiz, 0), "create") && ContainsString(getLexema(raiz, 1), "table"))
                {
                    return new CreateTable(Convert.ToBoolean(recorrido(raiz.ChildNodes[2])), getLexema(raiz, 3),
                        (List<ColumnCQL>)recorrido(raiz.ChildNodes[5]), getFila(raiz, 0), getColumna(raiz, 0));
                }
                else if (ContainsString(getLexema(raiz, 0), "truncate") && ContainsString(getLexema(raiz, 1), "table"))
                {
                    return new TruncateTable(getLexema(raiz, 2), getFila(raiz, 0), getColumna(raiz, 0));
                }
                else if (ContainsString(getLexema(raiz, 0), "use"))
                {
                    return new UseDataBase(getLexema(raiz, 1), getFila(raiz, 0), getColumna(raiz, 0));
                }
                else if (ContainsString(getLexema(raiz, 0), "alter") && ContainsString(getLexema(raiz, 3), "add"))
                {
                    return new AlterTableAdd(getLexema(raiz, 2), (List<ColumnCQL>)recorrido(raiz.ChildNodes[4])
                        , getFila(raiz, 0), getColumna(raiz, 0));
                }
                else if (ContainsString(getLexema(raiz, 0), "alter") && ContainsString(getLexema(raiz, 3), "drop"))
                {
                    return new AlterTableDrop(getLexema(raiz, 2), (List<String>)recorrido(raiz.ChildNodes[4])
                        , getFila(raiz, 0), getColumna(raiz, 0));
                }
                else
                {
                    return null;
                }
            }
            else if (CompararNombre(raiz, "BATCH"))
            {
                //res_begin + res_bath + LISTA_DML + res_apply + res_bath
                return new Batch((List<NodoCQL>)recorrido(raiz.ChildNodes[2]), getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "LISTA_DML"))
            {
                List<NodoCQL> list = new List<NodoCQL>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes)
                {
                    list.Add((NodoCQL)recorrido(nodo));
                }
                return list;
            }
            else if (CompararNombre(raiz, "DML2"))
            {
                //DML + puntcoma
                return recorrido(raiz.ChildNodes[0]);
            }
            else if (CompararNombre(raiz, "TRY"))
            {
                /*res_try + l_llave + BLOCK + r_llave + res_catch + l_parent + EXCEPTION + arroba
                                    + id + r_parent + l_llave + BLOCK + r_llave;*/
                return new Try((List<NodoCQL>)recorrido(raiz.ChildNodes[2]), new Catch(getLexema(raiz, 8),
                    (ExceptionCQL.EXCEPTION)recorrido(raiz.ChildNodes[6]),
                    (List<NodoCQL>)recorrido(raiz.ChildNodes[11]), getFila(raiz, 0), getColumna(raiz, 0)),
                    getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "THROW"))
            {
                //res_throw + res_new + EXCEPTION;
                return new Throw((ExceptionCQL.EXCEPTION)recorrido(raiz.ChildNodes[2]), getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "CURSOR"))
            {
                /*res_cursor + arroba + id + res_is + SELECT
                | res_open + arroba + id
                | res_close + arroba + id;*/
                if (ContainsString(getLexema(raiz, 0), "cursor"))
                {
                    return new Cursor(getLexema(raiz, 2), (NodoCQL)recorrido(raiz.ChildNodes[4]),
                        Cursor.TIPO_CURSOR.INSTANCE, getFila(raiz, 0), getColumna(raiz, 0));
                }
                else if (ContainsString(getLexema(raiz, 0), "open"))
                {
                    return new Cursor(getLexema(raiz, 2), null,
                        Cursor.TIPO_CURSOR.OPEN, getFila(raiz, 0), getColumna(raiz, 0));
                }
                else
                {
                    return new Cursor(getLexema(raiz, 2), null,
                        Cursor.TIPO_CURSOR.CLOSE, getFila(raiz, 0), getColumna(raiz, 0));
                }
            }
            else if (CompararNombre(raiz, "ASIG_CURSOR")) {
                return recorrido(raiz.ChildNodes[0]);
            }
            else if (CompararNombre(raiz, "PROCEDURE")) {
                //res_procedure + id + l_parent + LISTA_PARAMETROS + r_parent + coma +
                //l_parent + LISTA_PARAMETROS + r_parent + l_llave + BLOCK + r_llave;
                return new CreateProcedure(getLexema(raiz, 1), (List<KeyValuePair<String, Object>>)recorrido(raiz.ChildNodes[3]),
                    (List<KeyValuePair<String, Object>>)recorrido(raiz.ChildNodes[7]), (List<NodoCQL>)recorrido(raiz.ChildNodes[10]),
                    getArbolString(raiz.ChildNodes[10]), getFila(raiz, 0), getColumna(raiz, 0));
                //.Replace("\"", "\\\"")  //al getArbolString
            }
            else if (CompararNombre(raiz, "CASTEOS"))
            {
                //l_parent + TIPO + r_parent + E;
                return new Casteo(recorrido(raiz.ChildNodes[1]), (Expresion)recorrido(raiz.ChildNodes[3]),
                    getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "EXCEPTION"))
            {
                if (CompararNombre(raiz.ChildNodes[0], "arithmeticexception"))
                {
                    return ExceptionCQL.EXCEPTION.ArithmeticException;
                }
                else if (CompararNombre(raiz.ChildNodes[0], "countertypeexception"))
                {
                    return ExceptionCQL.EXCEPTION.CounterTypeException;
                }
                else if (CompararNombre(raiz.ChildNodes[0], "useralreadyexists"))
                {
                    return ExceptionCQL.EXCEPTION.UserAlreadyExists;
                }
                else if (CompararNombre(raiz.ChildNodes[0], "userdontexists"))
                {
                    return ExceptionCQL.EXCEPTION.UserDontExists;
                }
                else if (CompararNombre(raiz.ChildNodes[0], "valuesexception"))
                {
                    return ExceptionCQL.EXCEPTION.ValuesException;
                }
                else if (CompararNombre(raiz.ChildNodes[0], "columnexception"))
                {
                    return ExceptionCQL.EXCEPTION.ColumnException;
                }
                else if (CompararNombre(raiz.ChildNodes[0], "batchexception"))
                {
                    return ExceptionCQL.EXCEPTION.BatchException;
                }
                else if (CompararNombre(raiz.ChildNodes[0], "indexoutexception"))
                {
                    return ExceptionCQL.EXCEPTION.IndexOutException;
                }
                else if (CompararNombre(raiz.ChildNodes[0], "nullpointerexception"))
                {
                    return ExceptionCQL.EXCEPTION.NullPointerException;
                }
                else if (CompararNombre(raiz.ChildNodes[0], "numberreturnsexception"))
                {
                    return ExceptionCQL.EXCEPTION.NumberReturnsException;
                }
                else if (CompararNombre(raiz.ChildNodes[0], "functionalreadyexists"))
                {
                    return ExceptionCQL.EXCEPTION.FunctionAlreadyExists;
                }
                else if (CompararNombre(raiz.ChildNodes[0], "procedurealreadyexists"))
                {
                    return ExceptionCQL.EXCEPTION.ProcedureAlreadyExists;
                }
                else if (CompararNombre(raiz.ChildNodes[0], "objectalreadyexists"))
                {
                    return ExceptionCQL.EXCEPTION.ObjectAlreadyExists;
                }
                else if (CompararNombre(raiz.ChildNodes[0], "typealreadyexists"))
                {
                    return ExceptionCQL.EXCEPTION.TypeAlreadyExists;
                }
                else if (CompararNombre(raiz.ChildNodes[0], "typedontexists"))
                {
                    return ExceptionCQL.EXCEPTION.TypeDontExists;
                }
                else if (CompararNombre(raiz.ChildNodes[0], "bdalreadyexists"))
                {
                    return ExceptionCQL.EXCEPTION.BDAlreadyExists;
                }
                else if (CompararNombre(raiz.ChildNodes[0], "bddontexists"))
                {
                    return ExceptionCQL.EXCEPTION.BDDontExists;
                }
                else if (CompararNombre(raiz.ChildNodes[0], "usebdexception"))
                {
                    return ExceptionCQL.EXCEPTION.UseBDException;
                }
                else if (CompararNombre(raiz.ChildNodes[0], "tablealreadyexists"))
                {
                    return ExceptionCQL.EXCEPTION.TableAlreadyExists;
                }
                else if (CompararNombre(raiz.ChildNodes[0], "tabledontexists"))
                {
                    return ExceptionCQL.EXCEPTION.TableDontExists;
                }
                else if (CompararNombre(raiz.ChildNodes[0], "exception"))
                {
                    return ExceptionCQL.EXCEPTION.Exception;
                }
                else
                {
                    return null;
                }
            }
            else if (CompararNombre(raiz, "SELECT"))
            {
                //res_select + SELECT_TYPE + res_from + id + WHERE_Q + ORDERBY_Q + LIMIT_Q;
                return new Select((Select_Type)recorrido(raiz.ChildNodes[1]), getLexema(raiz, 3), (Where)recorrido(raiz.ChildNodes[4]),
                    (OrderBy)recorrido(raiz.ChildNodes[5]), (Expresion)recorrido(raiz.ChildNodes[6]),
                    getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "SELECT_TYPE"))
            {
                /*l_parent + por + r_parent
                | LISTA_E
                | por;*/
                if (CompararNombre(raiz.ChildNodes[0], "LISTA_E"))
                {
                    return new Select_Type((List<Expresion>)recorrido(raiz.ChildNodes[0]));
                }
                else
                {
                    return new Select_Type();
                }
            }
            else if (CompararNombre(raiz, "ORDERBY_Q"))
            {
                if (raiz.ChildNodes.Count == 0)
                {
                    return null;
                }
                else
                {
                    return (OrderBy)recorrido(raiz.ChildNodes[0]);
                }
            }
            else if (CompararNombre(raiz, "ORDERBY"))
            {
                //res_order + res_by + LISTA_ORDER
                return new OrderBy((List<Order>)recorrido(raiz.ChildNodes[2]));
            }
            else if (CompararNombre(raiz, "LISTA_ORDER"))
            {
                List<Order> list = new List<Order>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes)
                {
                    list.Add((Order)recorrido(nodo));
                }
                return list;
            }
            else if (CompararNombre(raiz, "LIMIT_Q"))
            {
                if (raiz.ChildNodes.Count == 0)
                {
                    return null;
                }
                else
                {
                    return recorrido(raiz.ChildNodes[0]);
                }
            }
            else if (CompararNombre(raiz, "LIMIT"))
            {
                //res_limit + E;
                return (Expresion)recorrido(raiz.ChildNodes[1]);
            }
            else if (CompararNombre(raiz, "ORDER"))
            {
                /* id
                | id + res_desc
                | id + res_asc;*/
                if (raiz.ChildNodes.Count == 2 && getLexema(raiz, 1).Equals("desc"))
                {
                    return new Order(getLexema(raiz, 0), Order.ORDER.DESC);
                }
                else if (raiz.ChildNodes.Count == 2 && getLexema(raiz, 1).Equals("asc"))
                {
                    return new Order(getLexema(raiz, 0), Order.ORDER.ASC);
                }
                else
                {
                    return new Order(getLexema(raiz, 0), Order.ORDER.ASC);
                }
            }
            else if (CompararNombre(raiz, "WHERE_Q"))
            {
                if (raiz.ChildNodes.Count == 0)
                {
                    return null;
                }
                else
                {
                    return (Where)recorrido(raiz.ChildNodes[0]);
                }
            }
            else if (CompararNombre(raiz, "WHERE"))
            {
                //res_where + E;
                return new Where((Expresion)recorrido(raiz.ChildNodes[1]));
            }
            else if (CompararNombre(raiz, "DML"))
            {
                /*res_insert + res_into + id + res_values + l_parent + LISTA_E + r_parent
                | res_insert + res_into + id + l_parent + LISTA_IDS_ARROBA + r_parent + res_values + l_parent + LISTA_E + r_parent
                | res_update + id + res_set + LISTA_ASIG_CQL + WHERE_Q
                | res_delete + res_from + id + WHERE_Q;*/
                if (ContainsString(getLexema(raiz, 0), "insert"))
                {
                    if (raiz.ChildNodes.Count == 10)
                    {
                        return new Insert(getLexema(raiz, 2), (List<String>)recorrido(raiz.ChildNodes[4]),
                            (List<Expresion>)recorrido(raiz.ChildNodes[8]), getFila(raiz, 0), getColumna(raiz, 0));
                    }
                    else
                    {
                        return new Insert(getLexema(raiz, 2), (List<Expresion>)recorrido(raiz.ChildNodes[5]),
                            getFila(raiz, 0), getColumna(raiz, 0));
                    }
                }
                else if (ContainsString(getLexema(raiz, 0), "delete"))
                {//res_delete + ACCESO_ARR_Q + res_from + id + WHERE_Q;
                    return new DeleteFrom((AccesoArreglo)recorrido(raiz.ChildNodes[1]), getLexema(raiz, 3),
                        (Where)recorrido(raiz.ChildNodes[4]), getFila(raiz, 0), getColumna(raiz, 0));
                }
                else if (ContainsString(getLexema(raiz, 0), "update"))
                {
                    return new Update(getLexema(raiz, 1), (List<AsignacionColumna>)recorrido(raiz.ChildNodes[3]),
                        (Where)recorrido(raiz.ChildNodes[4]), getFila(raiz, 0), getColumna(raiz, 0));
                }
                else
                {
                    return null;
                }
            }
            else if (CompararNombre(raiz, "LISTA_ASIG_CQL"))
            {
                List<AsignacionColumna> lista = new List<AsignacionColumna>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes)
                {
                    lista.Add((AsignacionColumna)recorrido(nodo));
                }
                return lista;
            }
            else if (CompararNombre(raiz, "ASIG_CQL"))
            {
                /* id + igual + E
                | ACCESO_ARR + igual + E
                | REFERENCIAS + igual + E*/
                if (raiz.ChildNodes[0].ToString().Equals("ACCESO_ARR"))
                {
                    return new AsignacionColumna((AccesoArreglo)recorrido(raiz.ChildNodes[0]), (Expresion)recorrido(raiz.ChildNodes[2]));
                }
                else if (raiz.ChildNodes[0].ToString().Equals("REFERENCIAS")) {
                    return new AsignacionColumna((Referencia)recorrido(raiz.ChildNodes[0]), (Expresion)recorrido(raiz.ChildNodes[2]));
                }
                else
                {
                    return new AsignacionColumna(getLexema(raiz, 0), (Expresion)recorrido(raiz.ChildNodes[2]));
                }
            }
            else if (CompararNombre(raiz, "ACCESO_ARR")) {
                //id + l_corchete + E + r_corchete;
                return new AccesoArreglo(getLexema(raiz, 0), (Expresion)recorrido(raiz.ChildNodes[2]), getFila(raiz, 1), getColumna(raiz, 1));
            }
            else if (CompararNombre(raiz, "ACCESO_ARR_Q")) {
                if (raiz.ChildNodes.Count != 0) {
                    if (CompararNombre(raiz.ChildNodes[0], "ACCESO_ARR"))
                    {
                        return recorrido(raiz.ChildNodes[0]);
                    }
                    else {
                        return new AccesoArreglo(getLexema(raiz,0),null, getFila(raiz, 0), getColumna(raiz, 0));
                    }
                }
                return null;
            }
            else if (CompararNombre(raiz, "ID_ARROBA"))
            {
                return getLexema(raiz, 1);
            }
            else if (CompararNombre(raiz, "LISTA_IDS_ARROBA"))
            {
                List<String> lista = new List<string>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes)
                {
                    lista.Add(recorrido(nodo).ToString());
                }
                return lista;
            }
            else if (CompararNombre(raiz, "DECLARACION_E"))
            {
                if (raiz.ChildNodes.Count == 4)
                {
                    return new KeyValuePair<String, Expresion>(getLexema(raiz, 1), (Expresion)recorrido(raiz.ChildNodes[3]));
                }
                else
                {
                    return new KeyValuePair<String, Expresion>(getLexema(raiz, 1), null);
                }
            }
            else if (CompararNombre(raiz, "FUNCION"))
            {
                //TIPO + id + l_parent + LISTA_PARAMETROS + r_parent + l_llave + BLOCK + r_llave;
                return new Funcion(recorrido(raiz.ChildNodes[0]), getLexema(raiz, 1), (List<KeyValuePair<String, Object>>)recorrido(raiz.ChildNodes[3]),
                    (List<NodoCQL>)recorrido(raiz.ChildNodes[6]), getFila(raiz, 1), getColumna(raiz, 1));
            }
            else if (CompararNombre(raiz, "NATIVAS"))
            {
                return recorrido(raiz.ChildNodes[0]);
            }
            else if (CompararNombre(raiz, "DATE_NOW"))
            {
                if (getLexema(raiz, 0).ToLower().Contains("today"))
                {
                    return new TodayNow(TodayNow.TIPO.DATE, getFila(raiz, 1), getColumna(raiz, 1));
                }
                else
                {
                    return new TodayNow(TodayNow.TIPO.TIME, getFila(raiz, 1), getColumna(raiz, 1));
                }
            }
            else if (CompararNombre(raiz, "LLAMADA_FUNCION"))
            {
                /*id + l_parent + LISTA_E + r_parent
                | res_call + id + l_parent + LISTA_E + r_parent;*/
                if (raiz.ChildNodes.Count == 4)
                {
                    return new LlamadaFuncion(getLexema(raiz, 0), (List<Expresion>)recorrido(raiz.ChildNodes[2]),
                        LlamadaFuncion.TIPO_LLAMADA.LLAMADA, getFila(raiz, 0), getColumna(raiz, 0));
                }
                else
                {
                    return new LlamadaFuncion(getLexema(raiz, 1), (List<Expresion>)recorrido(raiz.ChildNodes[3]),
                        LlamadaFuncion.TIPO_LLAMADA.CALL, getFila(raiz, 0), getColumna(raiz, 0));
                }
            }
            else if (CompararNombre(raiz, "LISTA_PARAMETROS"))
            {
                List<KeyValuePair<String, Object>> lista = new List<KeyValuePair<string, object>>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes)
                {
                    lista.Add((KeyValuePair<string, object>)recorrido(nodo));
                }
                return lista;
            }
            else if (CompararNombre(raiz, "UNPARAMETRO"))
            {
                //TIPO + arroba + id;
                return new KeyValuePair<String, Object>(getLexema(raiz, 2), recorrido(raiz.ChildNodes[0]));
            }
            else if (CompararNombre(raiz, "REFERENCIAS"))
            {
                agregarDolar = true;
                List<Object> lista = new List<object>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes)
                {
                    lista.Add(recorrido(nodo));
                }
                return new Referencia(lista, null, getFila2(raiz), getColumna2(raiz));
            }
            else if (CompararNombre(raiz, "REFERENCIA"))
            {
                //id | arroba + id | LLAMADA_FUNCION | ACCESO_ARR
                if (raiz.ChildNodes[0].ToString().Equals("LLAMADA_FUNCION") || raiz.ChildNodes[0].ToString().Equals("ACCESO_ARR"))
                {
                    agregarDolar = false;
                    return recorrido(raiz.ChildNodes[0]);
                }
                else
                {
                    String lex = getLexema(raiz, raiz.ChildNodes.Count - 1);
                    if (agregarDolar && raiz.ChildNodes.Count == 1) {
                        lex = "$" + lex;
                    }
                    agregarDolar = false;
                    return lex;
                }
            }
            else if (CompararNombre(raiz, "TIPO"))
            {
                String tipo = getLexema(raiz, 0);
                if (tipo.Equals("string", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return Primitivo.TIPO_DATO.STRING;
                }
                else if (tipo.Equals("int", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return Primitivo.TIPO_DATO.INT;
                }
                else if (tipo.Equals("cursor", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return Primitivo.TIPO_DATO.CURSOR;
                }
                else if (tipo.Equals("boolean", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return Primitivo.TIPO_DATO.BOOLEAN;
                }
                else if (tipo.Equals("double", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return Primitivo.TIPO_DATO.DOUBLE;
                }
                else if (tipo.Equals("date", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return Primitivo.TIPO_DATO.DATE;
                }
                else if (tipo.Equals("time", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return Primitivo.TIPO_DATO.TIME;
                }
                else if (tipo.Equals("list", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    if (raiz.ChildNodes.Count > 1)
                    {
                        return new TipoList(recorrido(raiz.ChildNodes[2]));
                    }
                    return new TipoList();
                }
                else if (tipo.Equals("set", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    if (raiz.ChildNodes.Count > 1) {
                        return new TipoSet(recorrido(raiz.ChildNodes[2]));
                    }
                    return new TipoSet();
                }
                else if (tipo.Equals("map", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    if (raiz.ChildNodes.Count > 1) {
                        //res_map + menor_que + TIPO + coma + TIPO + mayor_que;
                        return new TipoMAP(recorrido(raiz.ChildNodes[2]), recorrido(raiz.ChildNodes[4]));
                    }
                    return new TipoMAP();
                }
                else if (tipo.Equals("counter", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return Primitivo.TIPO_DATO.COUNTER;
                }
                else
                {
                    return tipo;
                }
            }
            else if (CompararNombre(raiz, "INSTRUCCION"))
            {
                /*
                 INSTRUCCION.Rule = res_log + l_parent + E + r_parent
                        //========== ver aquí ambiguedad entre referencias y reasignacion
                        | REFERENCIAS + igual + E
                        | res_return + LISTA_E
                        | REASIGNACION
                        | ACTUALIZACION2
                        | CORTE
                        | REFERENCIAS;*/
                if (raiz.ChildNodes.Count == 4)
                {
                    return new Print((Expresion)recorrido(raiz.ChildNodes[2]), getFila(raiz, 1), getColumna(raiz, 1));
                }
                else if (raiz.ChildNodes.Count == 3)
                {
                    List<Object> lista = new List<object>();
                    foreach (ParseTreeNode nodo in raiz.ChildNodes[0].ChildNodes)
                    {
                        lista.Add(recorrido(nodo));
                    }
                    return new Referencia(lista, (Expresion)recorrido(raiz.ChildNodes[2]), getFila2(raiz), getColumna2(raiz));
                }
                else if (raiz.ChildNodes.Count == 2)
                {
                    return new Return((List<Expresion>)recorrido(raiz.ChildNodes[1]), getFila(raiz, 0), getColumna(raiz, 0));
                }
                else if (raiz.ChildNodes.Count == 1)
                {
                    return recorrido(raiz.ChildNodes[0]);
                }
                else
                {
                    ast.addError("", "RecorridoCQL no soportado: " + raiz.ChildNodes.Count, 0, 0);
                    return "NULL";
                }
            }
            else if (CompararNombre(raiz, "FOR"))
            {
                if (ContainsString(getLexema(raiz, 1), "each"))
                {
                    //res_for + res_each + l_parent + LISTA_PARAMETROS + r_parent + res_in + arroba + id + l_llave + BLOCK + r_llave
                    return new ForEach((List<KeyValuePair<String, Object>>)recorrido(raiz.ChildNodes[3]),
                        getLexema(raiz, 7), (List<NodoCQL>)recorrido(raiz.ChildNodes[9]), getFila(raiz, 0), getColumna(raiz, 0));
                }
                else
                {
                    //res_for + l_parent + FUENTE_FOR + coma + E + coma + ACTUALIZACION2 + r_parent + l_llave + BLOCK + r_llave;
                    return new For((Sentencia)recorrido(raiz.ChildNodes[2]), (Expresion)recorrido(raiz.ChildNodes[4]),
                        (NodoCQL)recorrido(raiz.ChildNodes[6]), (List<NodoCQL>)recorrido(raiz.ChildNodes[9]),
                        getFila(raiz, 0), getColumna(raiz, 0));
                }
            }
            else if (CompararNombre(raiz, "COLECCION"))
            {
                /*l_corchete + LISTA_E + r_corchete
                | l_corchete + KEY_VALUE_LIST + r_corchete
                | l_llave + KEY_VALUE_LIST + r_llave //instancia de map
                | l_llave + LISTA_E + r_llave
                | l_llave + LISTA_E + r_llave + res_as + id;*/
                if (CompararNombre(raiz.ChildNodes[1], "KEY_VALUE_LIST"))
                {
                    return new ValorColeccion((List<KeyValuePair<Expresion, Expresion>>)recorrido(raiz.ChildNodes[1]),
                        getFila(raiz, 0), getColumna(raiz, 0));
                }
                String tip;
                if (raiz.ChildNodes.Count == 5)
                {
                    tip = getLexema(raiz, 4);
                }
                else
                {
                    tip = getLexema(raiz, 0);
                }
                return new ValorColeccion(tip, (List<Expresion>)recorrido(raiz.ChildNodes[1]),
                    getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "INSTANCIA"))
            {
                /*res_new + id
                | res_new + res_list + menor_que + TIPO + mayor_que
                | res_new + res_set + menor_que + TIPO + mayor_que
                | res_new + res_map + menor_que + TIPO + coma + TIPO + mayor_que;*/
                if (raiz.ChildNodes.Count == 2)
                {
                    return new InstanciaUserType(getLexema(raiz, 1), getFila(raiz, 0), getColumna(raiz, 0));
                }
                else if (raiz.ChildNodes.Count == 5)
                {
                    if (ContainsString(getLexema(raiz, 1), "list"))
                    {
                        return new InstanciaListCQL(recorrido(raiz.ChildNodes[3]), getFila(raiz, 0), getColumna(raiz, 0));
                        //return new ListCQL(recorrido(raiz.ChildNodes[3]), getFila(raiz, 0), getColumna(raiz, 0));
                    }
                    else
                    {
                        return new InstanciaSetCQL(recorrido(raiz.ChildNodes[3]), getFila(raiz, 0), getColumna(raiz, 0));
                        //return new SetCQL(recorrido(raiz.ChildNodes[3]), getFila(raiz, 0), getColumna(raiz, 0));
                    }
                }
                else if (raiz.ChildNodes.Count == 7)
                {
                    //res_new + res_map + menor_que + TIPO + coma + TIPO + mayor_que
                    return new InstanciaMapCQL(recorrido(raiz.ChildNodes[3]), recorrido(raiz.ChildNodes[5]), getFila(raiz, 0), getColumna(raiz, 0));
                    //return new MapCQL(recorrido(raiz.ChildNodes[3]), recorrido(raiz.ChildNodes[5]), getFila(raiz, 0), getColumna(raiz, 0));
                }
                else
                {
                    return null;
                }
            }
            else if (CompararNombre(raiz, "ACTUALIZACION2"))
            {
                return recorrido(raiz.ChildNodes[0]);
            }
            else if (CompararNombre(raiz, "ACTUALIZACION"))
            {
                /*arroba + id + mas + mas
                | arroba + id + menos + menos;*/
                String operador = "-";
                if (getLexema(raiz, 2).Equals("++"))
                {
                    operador = "+";
                }
                return new Actualizar(getLexema(raiz, 1), operador, new Primitivo("1 (numero)", 0, 0)
                    , getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "FUENTE_FOR"))
            {
                return recorrido(raiz.ChildNodes[0]);
            }
            else if (CompararNombre(raiz, "ACTUALIZAR"))
            {
                //arroba + id + OPERADOR + igual + E;
                return new Actualizar(getLexema(raiz, 1), getLexema(raiz.ChildNodes[2], 0), (Expresion)recorrido(raiz.ChildNodes[4]),
                    getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "CORTE"))
            {
                return new Corte(getLexema(raiz, 0), getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "REASIGNACION"))
            {
                //arroba + id + igual + E;
                int fila = getFila(raiz, 0);
                int columna = getColumna(raiz, 0);
                return new Reasignacion(getLexema(raiz, 1), (Expresion)recorrido(raiz.ChildNodes[3]), fila, columna);
            }
            else if (CompararNombre(raiz, "REASIGNACION2")) {
                // LISTA_IDS_ARROBA + igual + E;
                return new Reasignacion((List<String>)recorrido(raiz.ChildNodes[0]), (Expresion)recorrido(raiz.ChildNodes[2]),
                    getFila(raiz, 1), getColumna(raiz, 1));
            }
            else if (CompararNombre(raiz, "E"))
            {
                if (raiz.ChildNodes.Count == 3)
                {
                    return new Binaria((Expresion)recorrido(raiz.ChildNodes[0]), getLexema(raiz, 1),
                    (Expresion)recorrido(raiz.ChildNodes[2]), getFila(raiz, 1), getColumna(raiz, 1));
                }
                else if (raiz.ChildNodes.Count == 5)
                {// E + interrogacion + E + dospuntos + E;
                    return new Ternaria((Expresion)recorrido(raiz.ChildNodes[0]), (Expresion)recorrido(raiz.ChildNodes[2]),
                        (Expresion)recorrido(raiz.ChildNodes[4]), getFila(raiz, 1), getColumna(raiz, 1));
                }
                else
                {
                    return recorrido(raiz.ChildNodes[0]);
                }
            }
            else if (CompararNombre(raiz, "TERMINO"))
            {
                return recorrido(raiz.ChildNodes[0]);
            }
            else if (CompararNombre(raiz, "SENTENCIA"))
            {
                return recorrido(raiz.ChildNodes[0]);
            }
            else if (CompararNombre(raiz, "IF"))
            {
                /*res_if + l_parent + E + r_parent + l_llave + BLOCK + r_llave + ELSEIFS + ELSE
                            | res_if + l_parent + E + r_parent + l_llave + BLOCK + r_llave + ELSEIFS;*/
                Else else_ = null;
                if (raiz.ChildNodes.Count == 9)
                {
                    else_ = (Else)recorrido(raiz.ChildNodes[8]);
                }
                return new If((Expresion)recorrido(raiz.ChildNodes[2]), (List<NodoCQL>)recorrido(raiz.ChildNodes[5]),
                    (List<ElseIf>)recorrido(raiz.ChildNodes[7]), else_,
                     getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "ELSEIFS"))
            {
                List<ElseIf> lista = new List<ElseIf>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes)
                {
                    lista.Add((ElseIf)recorrido(nodo));
                }
                return lista;
            }
            else if (CompararNombre(raiz, "ELSEIF"))
            {
                return new ElseIf((Expresion)recorrido(raiz.ChildNodes[2]), (List<NodoCQL>)recorrido(raiz.ChildNodes[5]),
                    getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "ELSE"))
            {
                return new Else((List<NodoCQL>)recorrido(raiz.ChildNodes[2]), getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "WHILE"))
            {
                if (raiz.ChildNodes.Count > 8)
                {
                    return new While((Expresion)recorrido(raiz.ChildNodes[6]), (List<NodoCQL>)recorrido(raiz.ChildNodes[2]),
                        While.TIPO_WHILE.WHILE, getFila(raiz, 0), getColumna(raiz, 0));
                }
                else
                {

                    return new While((Expresion)recorrido(raiz.ChildNodes[2]), (List<NodoCQL>)recorrido(raiz.ChildNodes[5]),
                        While.TIPO_WHILE.WHILE, getFila(raiz, 0), getColumna(raiz, 0));
                }
            }
            else if (CompararNombre(raiz, "SWITCH"))
            {
                return new Switch((Expresion)recorrido(raiz.ChildNodes[2]), (List<Case>)recorrido(raiz.ChildNodes[5]),
                    recorrido(raiz.ChildNodes[6]), getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "LISTA_CASOS"))
            {
                List<Case> lista = new List<Case>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes)
                {
                    lista.Add((Case)recorrido(nodo));
                }
                return lista;
            }
            else if (CompararNombre(raiz, "CASO_DEF"))
            {
                if (raiz.ChildNodes.Count != 0)
                {
                    return new Default((List<NodoCQL>)recorrido(raiz.ChildNodes[2]), getFila(raiz, 0), getColumna(raiz, 0));
                }
                return null;
            }
            else if (CompararNombre(raiz, "CASO"))
            {
                return new Case((Expresion)recorrido(raiz.ChildNodes[1]), (List<NodoCQL>)recorrido(raiz.ChildNodes[3]),
                    getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "PRIMITIVO"))
            {
                String valor = "";
                if (raiz.ChildNodes.Count == 2)
                {
                    valor = raiz.ChildNodes[1].ToString();
                }
                else
                {
                    valor = raiz.ChildNodes[0].ToString();
                    if (valor.ToString().Contains("Identifier"))
                    {
                        valor = "$" + valor;
                    }
                }
                return new Primitivo(valor, getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "E_PARENT"))
            {
                return recorrido(raiz.ChildNodes[1]);
            }
            else if (CompararNombre(raiz, "OPERADOR"))
            {
                if (raiz.ChildNodes.Count == 2)
                {
                    return getLexema(raiz, 0) + getLexema(raiz, 1);
                }
                else
                {
                    return getLexema(raiz, 0);
                }
            }
            else if (CompararNombre(raiz, "UNARIO"))
            {
                return new Unario(getLexema(raiz, 0), (Expresion)recorrido(raiz.ChildNodes[1]), getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "LISTA_E"))
            {
                List<Expresion> lista = new List<Expresion>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes)
                {
                    lista.Add((Expresion)recorrido(nodo));
                }
                return lista;
            }
            else
            {
                ast.addError("", "RecorridoCQL no soportado: " + raiz.ToString(), getFila2(raiz), getColumna2(raiz));
                return "NULL";
            }
        }

        bool CompararNombre(ParseTreeNode nodo, string nombre)
        {
            return nodo.Term.Name.Equals(nombre, System.StringComparison.InvariantCultureIgnoreCase);
        }

        string getLexema(ParseTreeNode nodo, int num)
        {
            return nodo.ChildNodes[num].Token.Text.ToLower();
        }

        int getFila(ParseTreeNode nodo, int num)
        {
            return nodo.ChildNodes[num].Token.Location.Line;
        }

        int getColumna(ParseTreeNode nodo, int num)
        {
            return nodo.ChildNodes[num].Token.Location.Column;
        }

        int getFila2(ParseTreeNode raiz) {
            if (raiz.ChildNodes.Count > 0)
            {
                return getFila2(raiz.ChildNodes[0]);
            }
            else {
                return raiz.Token.Location.Line;
            }
        }

        int getColumna2(ParseTreeNode raiz) {
            if (raiz.ChildNodes.Count > 0)
            {
                return getColumna2(raiz.ChildNodes[0]);
            }
            else
            {
                return raiz.Token.Location.Column;
            }
        }

        String getArbolString(ParseTreeNode raiz) {
            String trad = "";
            if (raiz.ChildNodes.Count > 0)
            {
                if (raiz.ToString().Equals("LISTA_ORDER") || raiz.ToString().Equals("LISTA_ASIG_CQL") || raiz.ToString().Equals("LISTA_COLDEF")
                    || raiz.ToString().Equals("LISTA_CQLTIPOS") || raiz.ToString().Equals("LISTA_PARAMETROS")
                    || raiz.ToString().Equals("LISTA_IDS_ARROBA") || raiz.ToString().Equals("LISTA_IDS")
                    || raiz.ToString().Equals("LISTA_DECLARACION_E") || raiz.ToString().Equals("KEY_VALUE_LIST") || raiz.ToString().Equals("LISTA_E"))
                {
                    foreach (ParseTreeNode nodo in raiz.ChildNodes)
                    {
                        trad += getArbolString(nodo) + ",";
                    }
                    trad = trad.TrimEnd(',');
                }
                else if (raiz.ToString().Equals("REFERENCIAS")) {
                    foreach (ParseTreeNode nodo in raiz.ChildNodes)
                    {
                        trad += getArbolString(nodo) + ".";
                    }
                    trad = trad.TrimEnd('.');
                }
                else
                {
                    foreach (ParseTreeNode nodo in raiz.ChildNodes)
                    {
                        trad += getArbolString(nodo);
                    }
                }
            }
            else {
                try
                {
                    trad += raiz.Token.Text+" ";
                }
                catch (Exception)
                {
                    Console.Write("hola");
                }
            }
            return trad;
        }

        Boolean ContainsString(String match, String search)
        {
            return Regex.IsMatch(search, Regex.Escape(match), RegexOptions.IgnoreCase);
        }
    }
}