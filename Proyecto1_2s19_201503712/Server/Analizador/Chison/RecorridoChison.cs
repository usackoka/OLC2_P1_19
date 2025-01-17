﻿using Irony.Parsing;
using Server.AST;
using Server.AST.DBMS;
using Server.AST.ExpresionesCQL;
using Server.AST.ExpresionesCQL.Tipos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Server.Analizador.Chison
{
    public class RecorridoChison
    {
        Management dbms;
        public List<object> nodosChison;
        public List<clsToken> errores;

        public RecorridoChison(ParseTreeNode padre, Management dbms)
        {
            this.errores = new List<clsToken>();
            this.nodosChison = new List<object>();
            this.dbms = dbms;
            recorrido(padre);
        }

        public void ejecutarChison() {
            foreach (Object obj in nodosChison) {
                if (obj is UserCHISON) {
                    ((UserCHISON)obj).Ejecutar(this.dbms);
                } else if (obj is DataBaseCHISON) {
                    ((DataBaseCHISON)obj).Ejecutar(this.dbms);
                }
            }
        }

        Object recorrido(ParseTreeNode raiz)
        {
            if (CompararNombre(raiz, "S"))
            {
                if (raiz.ChildNodes.Count == 0) {
                    return null;
                }
                // dolar + menor_que + DATABASES + coma + USERS + mayor_que + dolar
                nodosChison.AddRange((List<object>)recorrido(raiz.ChildNodes[2]));
                nodosChison.Add((UserCHISON)recorrido(raiz.ChildNodes[4]));
                return null;
            }
            else if (CompararNombre(raiz, "INICIO_IMPORT")) {
                return recorrido(raiz.ChildNodes[0]);
            }
            else if (CompararNombre(raiz, "DATABASES"))
            {
                // res_databases + igual + l_corchete + LISTA_BASES + r_corchete;
                return recorrido(raiz.ChildNodes[3]);
            }
            else if (CompararNombre(raiz, "LISTA_BASES"))
            {
                if (raiz.ChildNodes.Count != 0 && CompararNombre(raiz.ChildNodes[0], "IMPORT"))
                {
                    //IMPORT.Rule = dolar + l_llave + id + punto + res_chison + r_llave + dolar;
                    Object o = AnalizarImport(getLexema(raiz.ChildNodes[0], 2));
                    if (!(o is List<object>)) {
                        addError("Analizar-Chison", "Se recibió: " + o + " - No se pudo leer bien el import: " + getLexema(raiz, 2),
                            getFila(raiz, 0), getColumna(raiz, 0));
                        return new List<object>();
                    }
                    return o;
                }
                else if (raiz.ChildNodes.Count==7 && getTextoHijo(raiz.ChildNodes[4]).Equals("chison")) {
                    Object o = AnalizarImport(getLexema(raiz, 2));
                    if (!(o is List<object>))
                    {
                        addError("Analizar-Chison", "Se recibió: " + o + " - No se pudo leer bien el import: " + getLexema(raiz, 2),
                            getFila(raiz, 0), getColumna(raiz, 0));
                        return new List<object>();
                    }
                    return o;
                }
                else
                {
                    List<object> lista = new List<object>();
                    foreach (ParseTreeNode nodo in raiz.ChildNodes)
                    {
                        lista.Add(recorrido(nodo));
                    }
                    return lista;
                }
            }
            else if (CompararNombre(raiz, "BASE"))
            {
                /*menor_que + res_name + igual + cadena + coma + 
                res_data + igual + l_corchete + LISTA_DATA_BASE + r_corchete + mayor_que
                | menor_que + res_data + igual + l_corchete + LISTA_DATA_BASE + r_corchete +
                 res_name + igual + cadena + coma + mayor_que;*/
                if (getLexema(raiz, 1).Replace("\"", "").ToLower().Equals("name")) {
                    return new DataBaseCHISON(getLexema(raiz, 3).Replace("\"", ""), (List<Data_Base_CHISON>)recorrido(raiz.ChildNodes[8]),
                        getFila(raiz, 0), getColumna(raiz, 0));
                }
                return new DataBaseCHISON(getLexema(raiz, 8).Replace("\"", ""), (List<Data_Base_CHISON>)recorrido(raiz.ChildNodes[3]),
                    getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "LISTA_DATA_BASE")) {
                if (raiz.ChildNodes.Count != 0 && CompararNombre(raiz.ChildNodes[0], "IMPORT"))
                {
                    //IMPORT.Rule = dolar + l_llave + id + punto + res_chison + r_llave + dolar;
                    Object o = AnalizarImport(getLexema(raiz.ChildNodes[0], 2));
                    if (!(o is List<Data_Base_CHISON>))
                    {
                        addError("Analizar-Chison", "Se recibió: "+o+" - No se pudo leer bien el import: " + getLexema(raiz, 2),
                            getFila(raiz, 0), getColumna(raiz, 0));
                        return new List<Data_Base_CHISON>();
                    }
                    return o;
                }
                else if (raiz.ChildNodes.Count == 7 && getTextoHijo(raiz.ChildNodes[4]).Equals("chison"))
                {
                    Object o = AnalizarImport(getLexema(raiz, 2));
                    if (!(o is List<Data_Base_CHISON>))
                    {
                        addError("Analizar-Chison", "Se recibió: " + o + " - No se pudo leer bien el import: " + getLexema(raiz, 2),
                            getFila(raiz, 0), getColumna(raiz, 0));
                        return new List<Data_Base_CHISON>();
                    }
                    return o;
                }
                else
                {
                    List<Data_Base_CHISON> lista = new List<Data_Base_CHISON>();
                    foreach (ParseTreeNode nodo in raiz.ChildNodes)
                    {
                        lista.Add((Data_Base_CHISON)recorrido(nodo));
                    }
                    return lista;
                }
            }
            else if (CompararNombre(raiz, "DATA_BASE")) {
                //menor_que + LISTA_CUERPO_DB + mayor_que;
                return recorrido(raiz.ChildNodes[1]);
            }
            else if (CompararNombre(raiz, "LISTA_CUERPO_DB")) {
                Data_Base_CHISON dbChison = new Data_Base_CHISON();
                foreach (ParseTreeNode nodo in raiz.ChildNodes)
                {
                    dbChison.addRange((Data_Base_CHISON)recorrido(nodo));
                }
                return dbChison;
            }
            else if (CompararNombre(raiz, "CUERPO_DB")) {
                /*res_name + igual + cadena
                | res_cql_type + igual + cadena
                | res_instr + igual + cadena3;
                | res_columns + igual + l_corchete + LISTA_COLUMNAS + r_corchete
                | res_data + igual + l_corchete + LISTA_DATA_COLUMNAS1 + r_corchete
                | res_attrs + igual + l_corchete + LISTA_ATTRS + r_corchete
                | res_parameters + igual + l_corchete + LISTA_PARAMETERS + r_corchete*/
                if (raiz.ChildNodes.Count == 3) {
                    if (getLexema(raiz,0).Replace("\"", "").ToLower().Equals("instr"))
                    {
                        return new Data_Base_CHISON(getLexema(raiz, 0).Replace("\"", ""), getLexema(raiz, 2), getFila(raiz, 0), getColumna(raiz, 0));
                    }
                    return new Data_Base_CHISON(getLexema(raiz, 0).Replace("\"", ""), getLexema(raiz, 2).Replace("\"", ""), getFila(raiz, 0), getColumna(raiz, 0));
                }
                else {
                    return new Data_Base_CHISON(getLexema(raiz, 0).Replace("\"", ""), recorrido(raiz.ChildNodes[3]), getFila(raiz, 0), getColumna(raiz, 0));
                }
            }
            else if (CompararNombre(raiz, "LISTA_PARAMETERS")) {
                if (raiz.ChildNodes.Count != 0 && CompararNombre(raiz.ChildNodes[0], "IMPORT"))
                {
                    //IMPORT.Rule = dolar + l_llave + id + punto + res_chison + r_llave + dolar;
                    Object o = AnalizarImport(getLexema(raiz.ChildNodes[0], 2));
                    if (!(o is List<Parametro>))
                    {
                        addError("Analizar-Chison", "Se recibió: "+o+" - No se pudo leer bien el import: " + getLexema(raiz, 2),
                            getFila(raiz, 0), getColumna(raiz, 0));
                        return new List<Parametro>();
                    }
                    return o;
                }
                else if (raiz.ChildNodes.Count == 7 && getTextoHijo(raiz.ChildNodes[4]).Equals("chison"))
                {
                    Object o = AnalizarImport(getLexema(raiz, 2));
                    if (!(o is List<Parametro>))
                    {
                        addError("Analizar-Chison", "Se recibió: " + o + " - No se pudo leer bien el import: " + getLexema(raiz, 2),
                            getFila(raiz, 0), getColumna(raiz, 0));
                        return new List<Parametro>();
                    }
                    return o;
                }
                else
                {
                    List<Parametro> lista = new List<Parametro>();
                    foreach (ParseTreeNode nodo in raiz.ChildNodes)
                    {
                        lista.Add((Parametro)recorrido(nodo));
                    }
                    return lista;
                }
            }
            else if (CompararNombre(raiz, "PARAMETER")) {
                //menor_que + LISTA_CARACT_PARAMETER + mayor_que
                return recorrido(raiz.ChildNodes[1]);
            }
            else if (CompararNombre(raiz, "LISTA_CARACT_PARAMETER")) {
                Parametro p = new Parametro();
                foreach (ParseTreeNode nodo in raiz.ChildNodes) {
                    p.addRange((Parametro)recorrido(nodo));
                }
                return p;
            }
            else if (CompararNombre(raiz, "CARACT_PARAMETER")) {
                /*res_name + igual + cadena
                | res_type + igual + cadena
                | res_as + igual + res_in
                | res_as + igual + res_out;*/
                return new Parametro(getLexema(raiz, 0).Replace("\"", ""), getLexema(raiz, 2).Replace("\"", ""));
            }
            else if (CompararNombre(raiz, "LISTA_ATTRS")) {
                if (raiz.ChildNodes.Count != 0 && CompararNombre(raiz.ChildNodes[0], "IMPORT"))
                {
                    //IMPORT.Rule = dolar + l_llave + id + punto + res_chison + r_llave + dolar;
                    Object o = AnalizarImport(getLexema(raiz.ChildNodes[0], 2));
                    if (!(o is List<List<KeyValuePair<String, String>>>))
                    {
                        addError("Analizar-Chison", "Se recibió: "+o+" - No se pudo leer bien el import: " + getLexema(raiz, 2),
                            getFila(raiz, 0), getColumna(raiz, 0));
                        return new List<List<KeyValuePair<String, String>>>();
                    }
                    return o;
                }
                else if (raiz.ChildNodes.Count == 7 && getTextoHijo(raiz.ChildNodes[4]).Equals("chison"))
                {
                    Object o = AnalizarImport(getLexema(raiz, 2));
                    if (!(o is List<List<KeyValuePair<String, String>>>))
                    {
                        addError("Analizar-Chison", "Se recibió: " + o + " - No se pudo leer bien el import: " + getLexema(raiz, 2),
                            getFila(raiz, 0), getColumna(raiz, 0));
                        return new List<List<KeyValuePair<String, String>>>();
                    }
                    return o;
                }
                else
                {
                    List<List<KeyValuePair<String, String>>> lista = new List<List<KeyValuePair<String, String>>>();
                    foreach (ParseTreeNode nodo in raiz.ChildNodes)
                    {
                        lista.Add((List<KeyValuePair<String, String>>)recorrido(nodo));
                    }
                    return lista;
                }
            }
            else if (CompararNombre(raiz,"ATTRS")) {
                return recorrido(raiz.ChildNodes[1]);
            }
            else if (CompararNombre(raiz, "LISTA_ATTRS_VALS"))
            {
                List<KeyValuePair<String, String>> lista = new List<KeyValuePair<String, String>>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes)
                {
                    lista.Add((KeyValuePair<String, String>)recorrido(nodo));
                }
                return lista;
            }
            else if (CompararNombre(raiz, "ATTRS_VALS")) {
                //cadena + igual + cadena
                return new KeyValuePair<String, String>(getLexema(raiz, 0).Replace(" (cadena)", "").Replace("\"", "").ToLower(),
                    getLexema(raiz, 2).Replace(" (cadena)", "").Replace("\"", ""));
            }
            else if (CompararNombre(raiz, "LISTA_KEY_VALUE_PAIR")) {
                List<KeyValuePair<Object, Object>> lista = new List<KeyValuePair<Object, Object>>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes) {
                    lista.Add((KeyValuePair<Object, Object>)recorrido(nodo));
                }
                return lista;
            }
            else if (CompararNombre(raiz, "KEY_VALUE_PAIR")) {
                //VALOR + igual + VALOR
                return new KeyValuePair<Object, Object>(recorrido(raiz.ChildNodes[0]), recorrido(raiz.ChildNodes[2]));
            }
            else if (CompararNombre(raiz, "LISTA_COLUMNAS")) {
                if (raiz.ChildNodes.Count != 0 && CompararNombre(raiz.ChildNodes[0], "IMPORT"))
                {
                    //IMPORT.Rule = dolar + l_llave + id + punto + res_chison + r_llave + dolar;
                    Object o = AnalizarImport(getLexema(raiz.ChildNodes[0], 2));
                    if (!(o is List<Columna>))
                    {
                        addError("Analizar-Chison", "Se recibió: "+o+" - No se pudo leer bien el import: " + getLexema(raiz, 2),
                            getFila(raiz, 0), getColumna(raiz, 0));
                        return new List<Columna>();
                    }
                    return o;
                }
                else if (raiz.ChildNodes.Count == 7 && getTextoHijo(raiz.ChildNodes[4]).Equals("chison"))
                {
                    Object o = AnalizarImport(getLexema(raiz, 2));
                    if (!(o is List<Columna>))
                    {
                        addError("Analizar-Chison", "Se recibió: " + o + " - No se pudo leer bien el import: " + getLexema(raiz, 2),
                            getFila(raiz, 0), getColumna(raiz, 0));
                        return new List<Columna>();
                    }
                    return o;
                }
                else
                {
                    List<Columna> lista = new List<Columna>();
                    foreach (ParseTreeNode nodo in raiz.ChildNodes)
                    {
                        lista.Add((Columna)recorrido(nodo));
                    }
                    return lista;
                }
            }
            else if (CompararNombre(raiz, "COLUMNA")) {
                return recorrido(raiz.ChildNodes[1]);
            }
            else if (CompararNombre(raiz, "LISTA_CARACT_COLUMNA")) {
                Columna c = new Columna();
                foreach (ParseTreeNode nodo in raiz.ChildNodes)
                {
                    c.addRange((Columna)recorrido(nodo));
                }
                return c;
            }
            else if (CompararNombre(raiz, "CARACT_COLUMNA"))
            {
                /*res_name + igual + cadena
                | res_type + igual + cadena
                | res_pk + igual + PRIMITIVO*/
                if (raiz.ChildNodes[0].ToString().ToLower().Contains("pk"))
                {
                    String lex = getLexema(raiz, 2).Replace(" (Keyword)", "").ToLower();
                    Boolean esPk = lex.Equals("true");
                    return new Columna(getLexema(raiz, 0).Replace("\"", ""), esPk);
                }
                else {
                    return new Columna(getLexema(raiz, 0).Replace("\"", ""), getLexema(raiz, 2).Replace("\"", ""));
                }
            }
            else if (CompararNombre(raiz, "LISTA_DATA_COLUMNAS1")) {
                if (raiz.ChildNodes.Count != 0 && CompararNombre(raiz.ChildNodes[0], "IMPORT"))
                {
                    //IMPORT.Rule = dolar + l_llave + id + punto + res_chison + r_llave + dolar;
                    Object o = AnalizarImport(getLexema(raiz.ChildNodes[0], 2));
                    if (!(o is List<List<KeyValuePair<String, Object>>>))
                    {
                        addError("Analizar-Chison", "Se recibió: "+o+" - No se pudo leer bien el import: " + getLexema(raiz, 2),
                            getFila(raiz, 0), getColumna(raiz, 0));
                        return new List<List<KeyValuePair<String, Object>>>();
                    }
                    return o;
                }
                else if (raiz.ChildNodes.Count == 7 && getTextoHijo(raiz.ChildNodes[4]).Equals("chison"))
                {
                    Object o = AnalizarImport(getLexema(raiz, 2));
                    if (!(o is List<List<KeyValuePair<String, Object>>>))
                    {
                        addError("Analizar-Chison", "Se recibió: " + o + " - No se pudo leer bien el import: " + getLexema(raiz, 2),
                            getFila(raiz, 0), getColumna(raiz, 0));
                        return new List<List<KeyValuePair<String, Object>>>();
                    }
                    return o;
                }
                else
                {
                    List<List<KeyValuePair<String, Object>>> lista = new List<List<KeyValuePair<String, Object>>>();
                    foreach (ParseTreeNode nodo in raiz.ChildNodes)
                    {
                        lista.Add((List<KeyValuePair<String, Object>>)recorrido(nodo));
                    }
                    return lista;
                }
            }
            else if (CompararNombre(raiz, "DATA_COLUMNAS1")) {
                return recorrido(raiz.ChildNodes[1]);
            }
            else if (CompararNombre(raiz, "LISTA_DATA_COLUMNAS")) {
                List<KeyValuePair<String, Object>> c = new List<KeyValuePair<String, Object>>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes)
                {
                    c.Add((KeyValuePair<String, Object>)recorrido(nodo));
                }
                return c;
            }
            else if (CompararNombre(raiz, "DATA_COLUMNAS")) {
                //cadena + igual + VALOR
                return new KeyValuePair<String, Object>(getLexema(raiz, 0).Replace("\"", ""), recorrido(raiz.ChildNodes[2]));
            }
            else if (CompararNombre(raiz, "LISTA")) {
                return recorrido(raiz.ChildNodes[1]);
            }
            else if (CompararNombre(raiz, "LISTA_VALORES")) {
                List<Object> lista = new List<object>();
                foreach (ParseTreeNode nodo in raiz.ChildNodes) {
                    lista.Add(recorrido(nodo));
                }
                return lista;
            }
            else if (CompararNombre(raiz, "MAP")) {
                if (raiz.ChildNodes.Count != 0 && CompararNombre(raiz.ChildNodes[0], "IMPORT"))
                {
                    //IMPORT.Rule = dolar + l_llave + id + punto + res_chison + r_llave + dolar;
                    Object o = AnalizarImport(getLexema(raiz.ChildNodes[0], 2));
                    if (!(o is List<KeyValuePair<Object, Object>>))
                    {
                        addError("Analizar-Chison", "Se recibió: "+o+" - No se pudo leer bien el import: " + getLexema(raiz, 2),
                            getFila(raiz, 0), getColumna(raiz, 0));
                        return new List<KeyValuePair<Object, Object>>();
                    }
                    return o;
                }
                else if (raiz.ChildNodes.Count == 7 && getTextoHijo(raiz.ChildNodes[4]).Equals("chison"))
                {
                    Object o = AnalizarImport(getLexema(raiz, 2));
                    if (!(o is List<KeyValuePair<Object, Object>>))
                    {
                        addError("Analizar-Chison", "Se recibió: " + o + " - No se pudo leer bien el import: " + getLexema(raiz, 2),
                            getFila(raiz, 0), getColumna(raiz, 0));
                        return new List<KeyValuePair<Object, Object>>();
                    }
                    return o;
                }
                else
                {
                    //menor_que + LISTA_KEY_VALUE_PAIR + mayor_que
                    return recorrido(raiz.ChildNodes[1]);
                }
            }
            else if (CompararNombre(raiz, "PRIMITIVO")) {
                /*numero
                | cadena
                | cadena2
                | res_true
                | res_false
                | res_null;*/
                String value = raiz.ChildNodes[0].ToString();
                if (value.Contains(" (numero)"))
                {
                    if (value.Contains("."))
                    {
                        return Convert.ToDouble(value.Replace(" (numero)", ""));
                    }
                    else
                    {
                        return Convert.ToInt32(value.Replace(" (numero)", ""));
                    }
                }
                else if (value.Contains(" (cadena)"))
                {
                    return value.Replace(" (cadena)", "");
                }
                else if (value.Contains(" (Keyword)"))
                {
                    if (value.Replace(" (Keyword)", "").ToLower().Equals("null"))
                    {
                        return new Null();
                    }

                    if (value.Replace(" (Keyword)", "").ToLower().Equals("true")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    //cadena2
                    value = value.Replace(" (cadena)", "").Replace(" (cadena2)", "");
                    if (value.Contains("-") || value.Contains("/"))
                    {
                        return new Date(value.ToString());
                    }
                    else
                    {
                        String[] arr = value.Split(':');
                        if (arr.Length == 3)
                        {
                            return new TimeSpan(Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]),
                                Convert.ToInt32(arr[2]));
                        }
                        else
                        {
                            return new TimeSpan();
                        }
                    }
                }
            }
            else if (CompararNombre(raiz, "VALOR")) {
                /*PRIMITIVO
                | LISTA
                | MAP;*/
                return recorrido(raiz.ChildNodes[0]);
            }
            //====================================== USUARIOS =====================================
            else if (CompararNombre(raiz, "USERS")) {
                //res_users + igual + l_corchete + LISTA_USER + r_corchete;
                return new UserCHISON((List<User>)recorrido(raiz.ChildNodes[3]), getFila(raiz, 0), getColumna(raiz, 0));
            }
            else if (CompararNombre(raiz, "LISTA_USER")) {
                if (raiz.ChildNodes.Count != 0 && CompararNombre(raiz.ChildNodes[0], "IMPORT"))
                {
                    //IMPORT.Rule = dolar + l_llave + id + punto + res_chison + r_llave + dolar;
                    Object o = AnalizarImport(getLexema(raiz.ChildNodes[0], 2));
                    if (!(o is List<User>))
                    {
                        addError("Analizar-Chison", "Se recibió: "+o+" - No se pudo leer bien el import: " + getLexema(raiz, 2),
                            getFila(raiz, 0), getColumna(raiz, 0));
                        return new List<User>();
                    }
                    return o;
                }
                else if (raiz.ChildNodes.Count == 7 && getTextoHijo(raiz.ChildNodes[4]).Equals("chison"))
                {
                    Object o = AnalizarImport(getLexema(raiz, 2));
                    if (!(o is List<User>))
                    {
                        addError("Analizar-Chison", "Se recibió: " + o + " - No se pudo leer bien el import: " + getLexema(raiz, 2),
                            getFila(raiz, 0), getColumna(raiz, 0));
                        return new List<User>();
                    }
                    return o;
                }
                else
                {
                    //menor_que + LISTA_KEY_VALUE_PAIR + mayor_que
                    List<User> list = new List<User>();
                    foreach (ParseTreeNode nodo in raiz.ChildNodes)
                    {
                        list.Add((User)recorrido(nodo));
                    }
                    return list;
                }
            }
            else if (CompararNombre(raiz, "USER")) {
                return recorrido(raiz.ChildNodes[1]);
            }
            else if (CompararNombre(raiz, "LISTA_DATA_USER")) {
                User user = new User();
                foreach (ParseTreeNode nodo in raiz.ChildNodes) {
                    user.addRange((User)recorrido(nodo));
                }
                return user;
            }
            else if (CompararNombre(raiz, "DATA_USER")) {
                /*res_name + igual + cadena
                | res_password + igual + cadena
                | res_permissions + igual + l_corchete + LISTA_PERMISOS + r_corchete;*/
                if (raiz.ChildNodes.Count > 4)
                {
                    return new User(getLexema(raiz, 0).Replace("\"", ""), recorrido(raiz.ChildNodes[3]), getFila(raiz, 0), getColumna(raiz, 0));
                }
                else {
                    return new User(getLexema(raiz, 0).Replace("\"", ""), getLexema(raiz, 2), getFila(raiz, 0), getColumna(raiz, 0));
                }
            }
            else if (CompararNombre(raiz, "LISTA_PERMISOS")) {
                if (raiz.ChildNodes.Count != 0 && CompararNombre(raiz.ChildNodes[0], "IMPORT"))
                {
                    //IMPORT.Rule = dolar + l_llave + id + punto + res_chison + r_llave + dolar;
                    Object o = AnalizarImport(getLexema(raiz, 2));
                    if (!(o is List<String>))
                    {
                        addError("Analizar-Chison", "Se recibió: "+o+" - No se pudo leer bien el import: " + getLexema(raiz, 2),
                            getFila(raiz, 0), getColumna(raiz, 0));
                        return new List<String>();
                    }
                    return o;
                }
                else
                {
                    //menor_que + LISTA_KEY_VALUE_PAIR + mayor_que
                    List<String> list = new List<String>();
                    foreach (ParseTreeNode nodo in raiz.ChildNodes)
                    {
                        list.Add((String)recorrido(nodo));
                    }
                    return list;
                }
            }
            else if (CompararNombre(raiz, "PERMISO")) {
                // menor_que + res_name + igual + cadena + mayor_que
                return getLexema(raiz, 3).Replace("\"", "");
            }
            else
            {
                addError("Analizar-Chison-Recorrido","No soportado: "+raiz,getFila2(raiz), getColumna2(raiz));
                return new Null();
            }
        }

        bool CompararNombre(ParseTreeNode nodo, string nombre)
        {
            return nodo.Term.Name.Equals(nombre, System.StringComparison.InvariantCultureIgnoreCase);
        }

        string getLexema(ParseTreeNode nodo, int num)
        {
            try
            {
                return nodo.ChildNodes[num].Token.Text.ToLower();
            }
            catch (Exception)
            {
                Console.WriteLine("");
                return "NULL";
            }
        }

        int getFila(ParseTreeNode nodo, int num)
        {
            return nodo.ChildNodes[num].Token.Location.Line;
        }

        int getColumna(ParseTreeNode nodo, int num)
        {
            return nodo.ChildNodes[num].Token.Location.Column;
        }

        int getFila2(ParseTreeNode raiz)
        {
            if (raiz.ChildNodes.Count > 0)
            {
                return getFila2(raiz.ChildNodes[0]);
            }
            else
            {
                return raiz.Token.Location.Line;
            }
        }

        int getColumna2(ParseTreeNode raiz)
        {
            if (raiz.ChildNodes.Count > 0)
            {
                return getColumna2(raiz.ChildNodes[0]);
            }
            else
            {
                return raiz.Token.Location.Column;
            }
        }

        String getTextoHijo(ParseTreeNode raiz) {
            if (raiz.ChildNodes.Count != 0)
            {
                return "";
            }
            else {
                return raiz.Token.Text.Replace(" (Keyword)","").ToLower();
            }
        }

        Boolean ContainsString(String match, String search)
        {
            return Regex.IsMatch(search, Regex.Escape(match), RegexOptions.IgnoreCase);
        }

        public void addError(String lexema, String descripcion, int fila, int columna)
        {
            this.errores.Add(new clsToken(lexema, descripcion, fila, columna, "Semántico", ""));
        }

        Object AnalizarImport(String nombre) {
            String ruta = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Chisons\"+nombre+".chison");

            if (!File.Exists(ruta))
            {
                addError("Analizar Chison - "+nombre, "No existe el chison "+nombre+".chison en: " + ruta, 0, 0);
                return null;
            }

            System.IO.StreamReader f = new System.IO.StreamReader(ruta);
            String lineas = f.ReadToEnd();
            f.Close();

            Generador parserChison = new Generador();
            if (parserChison.esCadenaValida(lineas, new GramaticaChison2()))
            {
                if (parserChison.padre.Root != null)
                {
                    //Graficar.ConstruirArbol(parserChison.padre.Root, "AST_CHISON", "");
                    RecorridoChison recorrido = new RecorridoChison(parserChison.padre.Root,this.dbms, true);
                    Object v = recorrido.getImport();
                    this.errores.AddRange(recorrido.errores);
                    return v;
                }
            }
            else
            {
                foreach (clsToken error in parserChison.ListaErrores)
                {//errores
                    errores.Add(new clsToken(error.lexema + " - Analizador Chison - "+nombre+".chison", error.descripcion, error.fila, error.columna, error.tipo, error.ambito));
                }
            }

            return null;
        }

        ParseTreeNode padre;
        public RecorridoChison(ParseTreeNode padre, Management dbms, bool val)
        {
            this.errores = new List<clsToken>();
            this.nodosChison = new List<object>();
            this.dbms = dbms;
            this.padre = padre;
        }

        public Object getImport()
        {
            return recorrido(padre);
        }
    }
}