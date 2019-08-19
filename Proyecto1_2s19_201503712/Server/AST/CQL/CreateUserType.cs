﻿using Server.AST.ExpresionesCQL;
using Server.AST.SentenciasCQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.AST.CQL
{
    public class CreateUserType : Sentencia
    {
        String id;
        Boolean IfNotExists;
        List<KeyValuePair<String, Object>> atributos;

        public CreateUserType(Boolean IfNotExists, String id, List<KeyValuePair<String,Object>> atributos,
            int fila, int columna) {
            this.id = id;
            this.IfNotExists = IfNotExists;
            this.atributos = atributos;
            this.fila = fila;
            this.columna = columna;
        }

        public override object Ejecutar(AST_CQL arbol)
        {
            throw new NotImplementedException();
        }
    }
}