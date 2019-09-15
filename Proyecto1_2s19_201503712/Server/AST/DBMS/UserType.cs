using Server.AST.CQL;
using Server.AST.ExpresionesCQL;
using Server.AST.ExpresionesCQL.Tipos;
using System;
using System.Collections.Generic;

namespace Server.AST.DBMS
{
    public class UserType
    {
        public String id;
        public Boolean IfNotExists;
        public List<KeyValuePair<String, Object>> atributos;
        public List<Atributo> valores;

        //=============================== DESDE CHISON ======================================================
        //cuando lo genero desde Chison
        public UserType(String id, List<KeyValuePair<String, Object>> atributos) {
            this.id = id;
            this.atributos = atributos;
            this.valores = new List<Atributo>();
        }
        //Cuando hay instancia de UserType con New
        public UserType(UserType ut,List<KeyValuePair<String,object>> valores ,Management dbms)
        {
            this.id = ut.id;
            this.IfNotExists = ut.IfNotExists;
            this.atributos = ut.atributos;
            iniciarValores(dbms,valores);
        }

        //====================================== DESDE CQL ===================================================
        //Cuando se encuentra la sentencia create user Type...
        public UserType(CreateUserType createUserType)
        {
            this.id = createUserType.id;
            this.IfNotExists = createUserType.IfNotExists;
            this.atributos = createUserType.atributos;
            this.valores = new List<Atributo>();
        }

        //Cuando hay instancia de UserType con New
        public UserType(UserType ut, AST_CQL arbol)
        {
            this.id = ut.id;
            this.IfNotExists = ut.IfNotExists;
            this.atributos = ut.atributos;
            iniciarValores(arbol);
        }

        //Cuando hay instancia de UserType con lista de parámetros
        public UserType(UserType ut, List<Expresion> expresiones, AST_CQL arbol)
        {
            this.id = ut.id;
            this.IfNotExists = ut.IfNotExists;
            this.atributos = ut.atributos;
            iniciarValores(arbol, expresiones);
        }

        public override string ToString()
        {
            String trad = "";
            trad += "   <\n";
            trad += "   \"CQL-TYPE\"=\"OBJECT\",\n";
            trad += "   \"NAME\"=\"" + this.id + "\",\n";
            trad += "   \"ATTRS\"=[" + getAtributos() + "]\n";
            trad += "   >\n";
            return trad;
        }

        string getAtributos() {
            String trad = "";
            foreach (KeyValuePair<String,Object> kvp in this.atributos) {
                trad += "\n       <";
                trad += "\"NAME\"=\""+kvp.Key+"\",\n";
                trad += "\"TYPE\"=\"" + kvp.Value + "\"";
                trad += "\n" + "        >,";
            }
            trad = trad.TrimEnd(',');
            return trad;
        }

        public string getData() {
            String trad = "";
            trad += "       <\n";
            foreach (Atributo atr in this.valores) {
                String valor = "";
                if (atr.valor is String)
                {
                    valor += "\"" + atr.valor + "\",";
                }
                else if (atr.valor is Date)
                {
                    valor += "'" + atr.valor + "',";
                }
                else if (atr.valor is TimeSpan)
                {
                    valor += "'" + atr.valor + "',";
                }
                else if (atr.valor is UserType)
                {
                    valor += ((UserType)atr.valor).getData();
                }
                else
                {
                    valor += atr.valor + ",";
                }

                trad += "\""+atr.id+"\"="+ valor;
            }
            trad = trad.TrimEnd(',');
            trad += "       >\n";
            return trad;
        }

        void iniciarValores(AST_CQL arbol, List<Expresion> expresiones)
        {
            if (this.atributos.Count != expresiones.Count) {
                arbol.addError(id+" UserType","No se enviaron la misma cantidad de parámetros con cantidad de atributos que hay",0,0);
                return;
            }

            this.valores = new List<Atributo>();
            for (int i = 0; i < atributos.Count; i++)
            {
                KeyValuePair<String, Object> kvp = atributos[i];
                this.valores.Add(new Atributo(kvp.Key, kvp.Value, expresiones[i].getValor(arbol)));
            }
        }

        //================== desde chison
        void iniciarValores(Management dbms, List<KeyValuePair<String,object>> valores)
        {
            this.valores = new List<Atributo>();
            foreach (KeyValuePair<String,Object> kvp in valores) {
                foreach (KeyValuePair<String,Object> atr in this.atributos) {
                    if (atr.Key.Equals(kvp.Key)) {
                        this.valores.Add(new Atributo(kvp.Key,atr.Value,kvp.Value));
                    }
                }
            }
        }
        //====================================

        void iniciarValores(AST_CQL arbol)
        {
            this.valores = new List<Atributo>();
            foreach (KeyValuePair<String, Object> kvp in atributos)
            {
                this.valores.Add(new Atributo(kvp.Key, kvp.Value, Primitivo.getDefecto(kvp.Value, arbol)));
            }
        }

        public Atributo getAtributo(String id, AST_CQL arbol)
        {
            foreach (Atributo atr in this.valores)
            {
                if (atr.id.Equals(id))
                {
                    return atr;
                }
            }
            arbol.addError("UserType, getAtributo", "No existe el atributo: " + id + " en " + this.id, 0, 0);
            return null;
        }

        public Object getTipoAtributo(String id, Management arbol)
        {
            foreach (Atributo atr in this.valores)
            {
                if (atr.id.Equals(id))
                {
                    return atr.tipoDato;
                }
            }
            arbol.addError("UserType, getAtributo", "No existe el atributo: " + id + " en " + this.id, 0, 0);
            return null;
        }

        public void addAtributos(List<KeyValuePair<String,Object>> atributos) {
            this.atributos.AddRange(atributos);
        }

    }
}