using Server.AST.ColeccionesCQL;
using Server.AST.DBMS;
using Server.AST.ExpresionesCQL.Tipos;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Server.AST.ExpresionesCQL
{
    public class Primitivo : Expresion
    {
        private Object value;
        private Object tipoDato;

        public Primitivo(String value, int fila, int columna)
        {
            this.value = value;
            this.fila = fila;
            this.columna = columna;
            setTipoDato();
        }

        private void setTipoDato() {
            if (this.value.ToString().Contains(" (numero)"))
            {
                this.value = this.value.ToString().Replace(" (numero)", "");
                if (this.value.ToString().Contains("."))
                {
                    this.value = Convert.ToDouble(this.value);
                    this.tipoDato = TIPO_DATO.DOUBLE;
                }
                else
                {
                    this.value = Convert.ToInt32(this.value);
                    this.tipoDato = TIPO_DATO.INT;
                }
            }
            else if (this.value.ToString().Contains(" (Keyword)"))
            {
                this.value = this.value.ToString().Replace(" (Keyword)", "");
                if (ContainsString(this.value.ToString(), "true") || ContainsString(this.value.ToString(), "false"))
                {
                    this.value = ContainsString(this.value.ToString(), "true") ? true : false;
                    this.tipoDato = TIPO_DATO.BOOLEAN;
                }
                else
                {
                    this.value = new Null();
                    this.tipoDato = new Null();
                }
            }
            else if (this.value.ToString().Contains(" (Identifier)"))
            {
                this.value = this.value.ToString().Replace(" (Identifier)", "").ToLower();
                this.tipoDato = TIPO_DATO.ID;
            }
            else if (this.value.ToString().Contains(" (cadena)"))
            {
                this.value = this.value.ToString().Replace(" (cadena)", "").Replace(" (cadena2)", "");
                this.tipoDato = TIPO_DATO.STRING;
            }
            else if (this.value.ToString().Contains(" (cadena2)")) {
                this.value = this.value.ToString().Replace(" (cadena)", "").Replace(" (cadena2)", "");
                if (this.value.ToString().Contains("-") || this.value.ToString().Contains("/"))
                {
                    this.value = new Date(this.value.ToString());
                    this.tipoDato = Primitivo.TIPO_DATO.DATE;
                }
                else {
                    String[] arr = this.value.ToString().Split(':');
                    if (arr.Length == 3)
                    {
                        this.value = new TimeSpan(Convert.ToInt32(arr[0]), Convert.ToInt32(arr[1]),
                            Convert.ToInt32(arr[2]));
                    }
                    else {
                        this.value = new TimeSpan();
                    }
                    this.tipoDato = Primitivo.TIPO_DATO.TIME;
                }
            }
        }

        public static Object getDefecto(Object tipoDato, AST_CQL arbol) {

            if (tipoDato is String || tipoDato is TipoMAP || tipoDato is TipoList || tipoDato is TipoSet) {
                return new Null();
            }

            switch (tipoDato) {
                case TIPO_DATO.TIME:
                    return new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                case TIPO_DATO.DATE:
                    return new Date();
                case TIPO_DATO.INT:
                case TIPO_DATO.COUNTER:
                    return 0;
                case TIPO_DATO.BOOLEAN:
                    return false;
                case TIPO_DATO.DOUBLE:
                    return 0.0;
                case TIPO_DATO.STRING:
                case TIPO_DATO.STRUCT:
                case TIPO_DATO.CURSOR:
                    return new Null();
                default:
                    arbol.addError(tipoDato.ToString(),"No hay defecto para el tipo de dato: "+tipoDato,0,0);
                    return new Null();
            }
        }

        public enum TIPO_DATO {
            INT, BOOLEAN, DOUBLE, STRING, ID, COUNTER, STRUCT, CURSOR, TIME, DATE
        }

        public override object getTipo(AST_CQL arbol)
        {
            if (this.tipoDato.Equals(TIPO_DATO.ID))
            {
                return arbol.entorno.getTipoVariable(this.value.ToString(), arbol, fila, columna);
            }
            else {
                return this.tipoDato;
            }
        }

        public override object getValor(AST_CQL arbol)
        {
            if (this.tipoDato is Null) {
                return new Null();
            }

            switch (this.tipoDato)
            {
                case TIPO_DATO.BOOLEAN:
                case TIPO_DATO.DOUBLE:
                case TIPO_DATO.INT:
                case TIPO_DATO.STRING:
                case TIPO_DATO.DATE:
                case TIPO_DATO.TIME:
                    return this.value;
                case TIPO_DATO.ID:
                    return arbol.entorno.getValorVariable(this.value.ToString(), arbol, fila, columna);
                default:
                    arbol.addError("", "(Primitivo) no soportado tipo: " + this.tipoDato, fila, columna);
                    return this.value;
            }
        }

        Boolean ContainsString(String match, String search)
        {
            return Regex.IsMatch(search, Regex.Escape(match), RegexOptions.IgnoreCase);
        }

        static Boolean CompararNombre(String n1, String n2)
        {
            return n1.Equals(n1, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// //////////////////////////////////////// METODOS CHISON ///////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <returns></returns>
        public static Object getObjectByList(object obj, Object tipoDato, Management dbms)
        {
            if (obj is List<Object>)
            {
                List<Object> lista = (List<Object>)obj;
                if (tipoDato is TipoList)
                {
                    ListCQL list = new ListCQL(((TipoList)tipoDato).tipo, 0, 0);
                    //convierto los valores de la lista
                    foreach (Object o in lista) {
                        list.valores.Add(getObjectByList(o, ((TipoList)tipoDato).tipo, dbms));
                    }
                    return list;
                }
                else if (tipoDato is TipoSet) {
                    SetCQL list = new SetCQL(((TipoSet)tipoDato).tipo, 0, 0);
                    //convierto los valores de la lista
                    foreach (Object o in lista)
                    {
                        list.valores.Add(getObjectByList(o, ((TipoSet)tipoDato).tipo, dbms));
                    }
                    return list;
                }
                else
                {
                    dbms.addError("getObjectByList - List obj", "No se encontró el tipo: " + tipoDato, 0, 0);
                    return new Null();
                }
            }
            else if (obj is List<KeyValuePair<Object, Object>>) //mapa u objeto
            {
                List<KeyValuePair<Object, Object>> lista = (List<KeyValuePair<Object, Object>>)obj;
                if (tipoDato is TipoMAP)
                {
                    MapCQL list = new MapCQL(((TipoMAP)tipoDato).tipoClave, ((TipoMAP)tipoDato).tipoValor, 0, 0);
                    //convierto los valores de la lista
                    foreach (KeyValuePair<Object, Object> o in lista)
                    {
                        list.valores.Add(getObjectByList(o.Key, ((TipoMAP)tipoDato).tipoClave, dbms),
                                        getObjectByList(o.Value, ((TipoMAP)tipoDato).tipoValor, dbms));
                    }
                    return list;
                }
                else if (tipoDato is String) {
                    UserType modeloUt = dbms.getUserType(tipoDato.ToString());
                    if (modeloUt == null)
                    {
                        dbms.addError("TypeDontExists - List obj,obj", "No se encontró el UserType: " + tipoDato.ToString(), 0, 0);
                        return new Null();
                    }

                    List<KeyValuePair<String, Object>> list = new List<KeyValuePair<String, Object>>();
                    foreach (KeyValuePair<Object, Object> o in lista)
                    {
                        String key = o.Key.ToString();
                        Object ob1 = modeloUt.getTipoAtributo(o.Key.ToString(), dbms);
                        Object ob = getObjectByList(o.Value, ob1, dbms);
                        list.Add(new KeyValuePair<String, Object>(key, ob));
                    }
                    return new UserType(modeloUt, list, dbms);
                }
                else
                {
                    dbms.addError("getObjectByList - List obj,obj", "No se encontró el tipo: " + tipoDato, 0, 0);
                    return new Null();
                }
            }
            else if (obj is Null) {
                if (tipoDato is String || tipoDato is SetCQL || tipoDato is MapCQL || tipoDato is ListCQL || tipoDato.Equals(Primitivo.TIPO_DATO.STRING))
                {
                    return obj;
                }
                else {
                    dbms.addError("getObjectByList - obj is Null", "No se encontró el tipo: " + tipoDato, 0, 0);
                    return new Null();
                }
            }
            else {//primitivo
                if (tipoDato is Primitivo.TIPO_DATO)
                {
                    return obj;
                }
                else {
                    dbms.addError("getObjectByList - Primitivo", "No se encontró el tipo: " + tipoDato, 0, 0);
                    return new Null();
                }
            }
            
        }

        public static Object getTipoString(String nombre, Management dbms) {

            String carColl = "";
            try
            {
                carColl = nombre.Substring(0, 3);
            }
            catch (Exception)
            {
                Console.Write("...");
            }

            if (carColl.Equals("lis")) {
                nombre = nombre.Substring(4);
                nombre = nombre.TrimStart('<').TrimEnd('>');
                return new TipoList(getTipoString(nombre, dbms));
            } else if (carColl.Equals("set")) {
                nombre = nombre.Substring(3);
                nombre = nombre.TrimStart('<').TrimEnd('>');
                return new TipoList(getTipoString(nombre, dbms));
            } else if (carColl.Equals("map"))
            {
                nombre = nombre.Substring(3);
                nombre = nombre.TrimStart('<').TrimEnd('>');
                String[] tips = nombre.Split(',');
                return new TipoMAP(getTipoString(tips[0], dbms),getTipoString(tips[1],dbms));
            }

            if (nombre.Equals("INT",StringComparison.InvariantCultureIgnoreCase))
            {
                return TIPO_DATO.INT;
            }
            else if (nombre.Equals("STRING", StringComparison.InvariantCultureIgnoreCase))
            {
                return TIPO_DATO.STRING;
            }
            else if (nombre.Equals("BOOLEAN", StringComparison.InvariantCultureIgnoreCase))
            {
                return TIPO_DATO.BOOLEAN;
            }
            else if (nombre.Equals("DOUBLE", StringComparison.InvariantCultureIgnoreCase))
            {
                return TIPO_DATO.DOUBLE;
            }
            else if (nombre.Equals("COUNTER", StringComparison.InvariantCultureIgnoreCase))
            {
                return TIPO_DATO.COUNTER;
            }
            else if (nombre.Equals("CURSOR", StringComparison.InvariantCultureIgnoreCase))
            {
                return TIPO_DATO.CURSOR;
            }
            else if (nombre.Equals("TIME", StringComparison.InvariantCultureIgnoreCase)) {
                return TIPO_DATO.TIME;
            }
            else if (nombre.Equals("DATE", StringComparison.InvariantCultureIgnoreCase)) {
                return TIPO_DATO.DATE;
            }
            else {
                return nombre;
            }
        }

        public static Object getDefecto(Object tipoDato, Management arbol)
        {

            if (tipoDato is String || tipoDato is TipoMAP || tipoDato is TipoList || tipoDato is TipoSet)
            {
                return new Null();
            }

            switch (tipoDato)
            {
                case TIPO_DATO.TIME:
                    return new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                case TIPO_DATO.DATE:
                    return new Date();
                case TIPO_DATO.INT:
                case TIPO_DATO.COUNTER:
                    return 0;
                case TIPO_DATO.BOOLEAN:
                    return false;
                case TIPO_DATO.DOUBLE:
                    return 0.0;
                case TIPO_DATO.STRING:
                case TIPO_DATO.STRUCT:
                case TIPO_DATO.CURSOR:
                    return new Null();
                default:
                    arbol.addError(tipoDato.ToString(), "No hay defecto para el tipo de dato: " + tipoDato, 0, 0);
                    return new Null();
            }
        }
    }
}