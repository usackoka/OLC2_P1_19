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
    public class Referencia : Expresion
    {
        public List<Object> referencias;
        public Expresion valor;

        public Referencia(List<Object> referencias, Expresion valor) {
            this.referencias = referencias;
            this.valor = valor;
        }

        public override object getTipo(AST_CQL arbol)
        {
            //================= para las que entran con ambiguedad
            if (referencias.Count == 1)
            {
                if (referencias[0] is LlamadaFuncion)
                {
                    return ((LlamadaFuncion)referencias[0]).getTipo(arbol);
                }
            }

            return getTipoRecursivo(arbol);
        }

        public override object getValor(AST_CQL arbol)
        {
            //================= para las que entran con ambiguedad
            if (referencias.Count == 1) {
                if (referencias[0] is LlamadaFuncion) {
                    return ((LlamadaFuncion)referencias[0]).getValor(arbol);
                }
            }

            //================== si es un set valor 
            if (this.valor != null) {
                setValorRecursivo(arbol);
                return null;
            }

            //=================== valor a retornar
            Object valorRetorno = getValorRecursivo(arbol);
            if (valorRetorno is Atributo) {
                return ((Atributo)valorRetorno).valor;
            }

            return valorRetorno!=null?valorRetorno:new Null();
        }

        void setValorRecursivo(AST_CQL arbol) {
            Object ret =  getValorRecursivo(arbol);

            if (ret is Atributo) {
                ((Atributo)ret).setValor(valor.getValor(arbol),valor.getTipo(arbol),arbol);
            }
        }

        Object getValorRecursivo(AST_CQL arbol) {

            Object valorRetorno = null;

            for (int i = 0; i<referencias.Count; i++) {
                Object obj = referencias[i];
                if (obj is String)
                {
                    if (valorRetorno != null)
                    {
                        if (valorRetorno is UserType) {
                            if (i != referencias.Count - 1)
                            {
                                valorRetorno = ((UserType)valorRetorno).getAtributo(obj.ToString().ToLower(), arbol).valor;
                            }
                            //si es última iteración
                            else
                            {
                                valorRetorno = ((UserType)valorRetorno).getAtributo(obj.ToString().ToLower(), arbol);
                            }
                        }
                    }
                    else {
                        valorRetorno = (new Primitivo(obj + " (Identifier)", fila, columna)).getValor(arbol);
                    }
                }
                else if (obj is LlamadaFuncion)
                {
                    LlamadaFuncion llf = (LlamadaFuncion)obj;
                    //verifico el valor de retono
                    if (valorRetorno is String) {
                        ClaseString cs = new ClaseString();
                        cs.expresiones = llf.expresiones;
                        valorRetorno = cs.getMetodoString(llf.idLlamada, valorRetorno.ToString(), arbol);
                    }
                    else if (valorRetorno is Date)
                    {
                        ClaseDateTime cs = new ClaseDateTime();
                        cs.expresiones = llf.expresiones;
                        valorRetorno = cs.getMetodoDateTime(llf.idLlamada, (Date)valorRetorno, arbol);
                    }
                    else if (valorRetorno is TimeSpan)
                    {
                        ClaseDateTime cs = new ClaseDateTime();
                        cs.expresiones = llf.expresiones;
                        valorRetorno = cs.getMetodoTime(llf.idLlamada, (TimeSpan)valorRetorno, arbol);
                    }
                    else if (valorRetorno is ListCQL) {
                        ListCQL list = (ListCQL)valorRetorno;
                        //verifico el nombre del método para aplicarlo
                        list.expresiones = llf.expresiones;
                        valorRetorno = list.getMetodo(arbol, llf.idLlamada);
                    }
                    else if (valorRetorno is SetCQL)
                    {
                        SetCQL list = (SetCQL)valorRetorno;
                        //verifico el nombre del método para aplicarlo
                        list.expresiones = llf.expresiones;
                        valorRetorno = list.getMetodo(arbol, llf.idLlamada);
                    }
                    else if (valorRetorno is MapCQL)
                    {
                        MapCQL list = (MapCQL)valorRetorno;
                        //verifico el nombre del método para aplicarlo
                        list.expresiones = llf.expresiones;
                        valorRetorno = list.getMetodo(arbol, llf.idLlamada);
                    }
                }
            }

            return valorRetorno;
        }

        Object getTipoRecursivo(AST_CQL arbol) {
            Object valorRetorno = null;
            Object tipoRetorno =new Null();
            
            for(int i = 0; i < referencias.Count; i++)
            {
                Object obj = referencias[i];
                //============== saco el tipo
                if (obj is String)
                {
                    if (valorRetorno != null)
                    {
                        if (valorRetorno is UserType)
                        {
                            if (i != referencias.Count - 1)
                            {
                                tipoRetorno = ((UserType)valorRetorno).getTipoAtributo(obj.ToString().ToLower(), arbol.dbms);
                                valorRetorno = ((UserType)valorRetorno).getAtributo(obj.ToString().ToLower(), arbol).valor;
                            }
                            //si es última iteración
                            else
                            {
                                tipoRetorno = ((UserType)valorRetorno).getTipoAtributo(obj.ToString().ToLower(), arbol.dbms);
                                valorRetorno = ((UserType)valorRetorno).getAtributo(obj.ToString().ToLower(), arbol);
                            }
                        }
                    }
                    else
                    {
                        valorRetorno = (new Primitivo(obj + " (Identifier)", fila, columna)).getValor(arbol);
                        tipoRetorno = (new Primitivo(obj + " (Identifier)", fila, columna)).getTipo(arbol);
                    }
                }
                else if (obj is LlamadaFuncion)
                {
                    LlamadaFuncion llf = (LlamadaFuncion)obj;
                    //verifico el valor de retono
                    if (valorRetorno is String)
                    {
                        ClaseString cs = new ClaseString();
                        cs.expresiones = llf.expresiones;
                        valorRetorno = cs.getMetodoString(llf.idLlamada, valorRetorno.ToString(), arbol);
                        tipoRetorno = cs.getTipoMetodo(llf.idLlamada, arbol);
                    }
                    else if (valorRetorno is Date)
                    {
                        ClaseDateTime cs = new ClaseDateTime();
                        cs.expresiones = llf.expresiones;
                        valorRetorno = cs.getMetodoDateTime(llf.idLlamada, (Date)valorRetorno, arbol);
                        tipoRetorno = Primitivo.TIPO_DATO.DATE;
                    }
                    else if (valorRetorno is TimeSpan)
                    {
                        ClaseDateTime cs = new ClaseDateTime();
                        cs.expresiones = llf.expresiones;
                        valorRetorno = cs.getMetodoTime(llf.idLlamada, (TimeSpan)valorRetorno, arbol);
                        tipoRetorno = Primitivo.TIPO_DATO.TIME;
                    }
                    else if (valorRetorno is ListCQL)
                    {
                        ListCQL list = (ListCQL)valorRetorno;
                        //verifico el nombre del método para aplicarlo
                        list.expresiones = llf.expresiones;
                        valorRetorno = list.getMetodo(arbol, llf.idLlamada);
                        tipoRetorno = list.getTipoMetodo(llf.idLlamada);
                    }
                    else if (valorRetorno is SetCQL)
                    {
                        SetCQL list = (SetCQL)valorRetorno;
                        //verifico el nombre del método para aplicarlo
                        list.expresiones = llf.expresiones;
                        valorRetorno = list.getMetodo(arbol, llf.idLlamada);
                        tipoRetorno = list.getTipoMetodo(llf.idLlamada);
                    }
                    else if (valorRetorno is MapCQL)
                    {
                        MapCQL list = (MapCQL)valorRetorno;
                        //verifico el nombre del método para aplicarlo
                        list.expresiones = llf.expresiones;
                        valorRetorno = list.getMetodo(arbol, llf.idLlamada);
                        tipoRetorno = list.getTipoMetodo(llf.idLlamada);
                    }
                }
            }

            /*
            foreach (Object obj in referencias)
            {
                //============== saco el tipo
                if (obj is String)
                {
                    tipoRetorno = (new Primitivo(obj + " (Identifier)", fila, columna)).getTipo(arbol);
                    valorRetorno = (new Primitivo(obj + " (Identifier)", fila, columna)).getValor(arbol);
                }
                else if (obj is LlamadaFuncion)
                {
                    LlamadaFuncion llf = (LlamadaFuncion)obj;
                    //verifico el valor de retono
                    if (valorRetorno is String)
                    {
                        ClaseString cs = new ClaseString();
                        cs.expresiones = llf.expresiones;
                        valorRetorno = cs.getMetodoString(llf.idLlamada, valorRetorno.ToString(), arbol);
                        tipoRetorno = cs.getTipoMetodo(llf.idLlamada, arbol);
                    }
                    else if (valorRetorno is Date)
                    {
                        ClaseDateTime cs = new ClaseDateTime();
                        cs.expresiones = llf.expresiones;
                        valorRetorno = cs.getMetodoDateTime(llf.idLlamada, (Date)valorRetorno, arbol);
                        tipoRetorno = Primitivo.TIPO_DATO.DATE;
                    }
                    else if (valorRetorno is TimeSpan)
                    {
                        ClaseDateTime cs = new ClaseDateTime();
                        cs.expresiones = llf.expresiones;
                        valorRetorno = cs.getMetodoTime(llf.idLlamada, (TimeSpan)valorRetorno, arbol);
                        tipoRetorno = Primitivo.TIPO_DATO.TIME;
                    }
                    else if (valorRetorno is ListCQL)
                    {
                        ListCQL list = (ListCQL)valorRetorno;
                        //verifico el nombre del método para aplicarlo
                        list.expresiones = llf.expresiones;
                        valorRetorno = list.getMetodo(arbol, llf.idLlamada);
                        tipoRetorno = list.getTipoMetodo(llf.idLlamada);
                    } else if (valorRetorno is SetCQL) {
                        SetCQL list = (SetCQL)valorRetorno;
                        //verifico el nombre del método para aplicarlo
                        list.expresiones = llf.expresiones;
                        valorRetorno = list.getMetodo(arbol, llf.idLlamada);
                        tipoRetorno = list.getTipoMetodo(llf.idLlamada);
                    } else if (valorRetorno is MapCQL)
                    {
                        MapCQL list = (MapCQL)valorRetorno;
                        //verifico el nombre del método para aplicarlo
                        list.expresiones = llf.expresiones;
                        valorRetorno = list.getMetodo(arbol, llf.idLlamada);
                        tipoRetorno = list.getTipoMetodo(llf.idLlamada);
                    }
                }
            }*/

            return tipoRetorno;
        }

        Boolean ContainsString(String match, String search)
        {
            return match.ToLower().Equals(search);
            //return Regex.IsMatch(search, Regex.Escape(match), RegexOptions.IgnoreCase);
        }
    }
}