using Server.AST.CQL;
using Server.AST.ExpresionesCQL;
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

        public void addAtributos(List<KeyValuePair<String,Object>> atributos) {
            this.atributos.AddRange(atributos);
        }

    }
}