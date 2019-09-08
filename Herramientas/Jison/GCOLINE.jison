//-----------------------------------------------------------------------
//------------------------------ LEXICO ---------------------------------
//-----------------------------------------------------------------------
%lex
%{

%}
%locations
terminadorLinea \r|\n|\r\n
caracteresComentario [^\r\n]
letras = [A-Za-zÑñ]
digito = [0-9]
%s COMENT_MULTI
%x COMENT_SIMPLE
%%
\s+                   /* skip whitespace */
//-------------------------- Comentario multilinea ---------------------
"/*"                {this.begin("COMENT_MULTI");}
<COMENT_MULTI>"*/"  %{this.begin('INITIAL');%}
<COMENT_MULTI>.                {}
<COMENT_MULTI>[\t\r\n\f]      {}
//-----------------------------------------------------------------------
//-------------------------- Comentario multilinea ---------------------
"//"                {this.begin("COMENT_SIMPLE");}
<COMENT_SIMPLE>[\r\n]   %{this.begin('INITIAL');%}
<COMENT_SIMPLE>.     {}
<COMENT_SIMPLE>[\t\f]      {}
//-----------------------------------------------------------------------
//----visibilidad----->
"abstract"                                        return 'Tabstract'
"private"                                         return 'Tprivate'
"protected"                                       return 'Tprotected'
"public"                                          return 'Tpublic'
"final"                                           return 'Tfinal'
"static"                                          return 'Tstatic'
//-----tipos---------->
"String"                                          return 'TString'
"char"                                            return 'Tchar'
"int"                                             return 'Tint'
"double"                                          return 'Tdouble'
"boolean"                                         return 'Tboolean'
"void"                                            return 'Tvoid'
"LinkedList"                                      return 'Tlinkedlist'
//-------ciclos------->
"switch"                                          return 'Tswitch'
"case"                                            return 'Tcase'
"default"                                         return 'Tdefault'
"while"                                           return 'Twhile'
"do"                                              return 'Tdo'
"for"                                             return 'Tfor'
"if"                                              return 'Tif'
"else"                                            return 'Telse'
//--------------->
"import"                                          return 'Timport'
"class"                                           return 'Tclass'
"extends"                                         return 'Textends'
"Override"                                        return 'Toverride'
//--------------->
"try"                                             return 'Ttry'
"catch"                                           return 'Tcatch'
"instanceof"                                      return 'Tinstanceof'
"graph"                                           return 'Tgraph'
"print"                                           return 'Tprint'
"println"                                         return 'Tprintln'
"read_file"                                       return 'Tread_file'
"write_file"                                      return 'Twrite_file'
//--------------->
"str"                                             return 'Tstr'
"pow"                                             return 'Tpow'
"toChar"                                          return 'TtoChar'
"toDouble"                                        return 'TtoDouble'
"toInt"                                           return 'TtoInt'
"throw"                                           return 'Tthrow'
//--------------->
"new"                                             return 'Tnew'
"super"                                           return 'Tsuper'
"this"                                            return 'Tthis'
"continue"                                        return 'Tcontinue'
"return"                                          return 'Treturn'
"break"                                           return 'Tbreak'
//-----------------------------------------------------------------------
"null"                                            return 'ERnull'
"true"                                            return 'ERtrue'
"false"                                           return 'ERfalse'
\"([^\"\n\\\\]|\\\"|\\)*\"  					  return 'ERstring'
\'([^'\n\t]|'\n'|'\t'|'\0'|'\"')?\'     return 'ERchar'
({letras}|"_")({letras}+|{digito}*|"_")*          return 'Identificador'
{digito}+"."{digito}+                             return 'ERdouble'
{digito}+                                         return 'ERint'
//-----------------------------------------------------------------------
"@"                                               return 'Tarroba'
"%"                                               return '%'
"++"                                              return '++'
"--"                                              return '--'
"!="                                              return '!=';
"=="                                              return '=='
">="                                              return '>='
"<="                                              return '<='
"?"                                               return '?'
":"                                               return ':'
"&&"                                              return '&&'
"||"                                              return '||'
"^"                                               return '^'
"!"                                               return '!'
"("                                               return '('
")"                                               return ')'
"["                                               return '['
"]"                                               return ']'
";"                                               return ';'
"{"                                               return '{'
"}"                                               return '}'
"."                                               return '.'
","                                               return ','
"+"                                               return '+'
"-"                                               return '-'
"*"                                               return '*'
"/"                                               return '/'
"="                                               return '='
">"                                               return '>'
"<"                                               return '<'
//-----------------------------------------------------------------------
<<EOF>>                                           return 'EOF'
. %{console.log("FILA: " + (yylloc.first_line) + " COL: " + (yylloc.first_column) + " Lexico " + "Caracter Invalido cerca de: \""+ yytext + "\""); %}
//-----------------------------------------------------------------------
//---------------------------SINTACTICO----------------------------------
//-----------------------------------------------------------------------
%%
/lex
%{
  let Nodo = require("./Nodo.js");
%}

/* operator associations and precedence */
%left '+' '-'
%left '*' '/' '%'
%right Ttypecast
%left Tpow
%right '!' UTmenos UTmas '++' '--'
%left Tincremento Tdecremento

%define parse.error verbose
%option bison-locations
%{
    let padre;
    let cont = 0;
    function CrearRaiz(NEtiqueta, Columna, Fila){
        let nd = new Nodo();
        nd.setEtiqueta(NEtiqueta);
        nd.setId(cont);
        nd.setFila(Fila);
        nd.setColumna(Columna);
        cont++;
        return nd;
    }
    function CrearHijo(NEtiqueta, Valor, Columna, Fila){
        let nd = new Nodo();
        nd.setEtiqueta(NEtiqueta);
        nd.setId(cont);
        nd.setFila(Fila);
        nd.setColumna(Columna);
        nd.setValor(Valor);
        cont++;
        return nd;
    }
    function Replace(Cadena){
        let len = Cadena.length;
        let result = Cadena.substring(1, len-1);
        return result;
    }
%}

%start INICIO

%% /* language grammar */

INICIO: SO EOF {
    var Raiz = CrearRaiz("INICIO", @1.first_column, @1.first_line);
    Raiz.AddHijos($1);
    padre = Raiz;
    return Raiz;
  };

SO: LCUERPOCOLINE {
    var Raiz = CrearRaiz("SO", @1.first_column, @1.first_line);
    Raiz.AddHijos($1);
    $$ = Raiz;
};

LCUERPOCOLINE: LCUERPOCOLINE CUERPOCOLINE {
    var Raiz = CrearRaiz("LCUERPOCOLINE", @1.first_column, @1.first_line);
    Raiz.AddHijos($1);
    Raiz.AddHijos($2);
    $$ = Raiz;
}
              |CUERPOCOLINE {
    var Raiz = CrearRaiz("LCUERPOCOLINE", @1.first_column, @1.first_line);
    Raiz.AddHijos($1);
    $$ = Raiz;
};

CUERPOCOLINE:  IMPORTAR {
    $$ = $1;
}
              |CLASE {
    $$ = $1;
};
//------------------>
IMPORTAR: Timport ERstring ';' {
    var Raiz = CrearHijo("IMPORTAR", Replace($2),@1.first_column, @1.first_line);
	   $$ = Raiz;
};
//------------------>
CLASE: LMODIFICADORES Tclass Identificador EXTENDER '{' OPCCLASE1 {
    var Raiz = CrearRaiz("CLASE", @1.first_column, @1.first_line);
    //--------------MODIFICADORES-----------------
    var Hijo1 = CrearHijo("MODIFICADORES", $1, @1.first_column, @1.first_line);
    Raiz.AddHijos(Hijo1);
    //--------------IDENTIFICADOR------------------
    var Hijo2 = CrearHijo("ID", $3, @3.first_column, @3.first_line);
    Raiz.AddHijos(Hijo2);
    //--------------EXTENDER-----------------------
    var Hijo3 = CrearHijo("EXTENDER", $4, @4.first_column, @4.first_line);
    Raiz.AddHijos(Hijo3);
    //-------------CONTENIDO CLASE------------------
    Raiz.AddHijos($6);
    //----------------------------------------------
    $$ = Raiz;
}
	  |Tclass Identificador EXTENDER '{' OPCCLASE1{
    var Raiz = CrearRaiz("CLASE", @1.first_column, @1.first_line);
    //--------------IDENTIFICADOR------------------
    var Hijo2 = CrearHijo("ID", $2, @2.first_column, @2.first_line);
    Raiz.AddHijos(Hijo2);
    //--------------EXTENDER-----------------------
    var Hijo3 = CrearHijo("EXTENDER", $3, @3.first_column, @3.first_line);
    Raiz.AddHijos(Hijo3);
	Raiz.AddHijos($5);
	$$ = Raiz;
};
//------------------>
EXTENDER: Textends Identificador {$$ = $2;}
          |{$$="";};
//------------------>
LMODIFICADORES1: LMODIFICADORES {$$ = $1;}
                |{$$ = "";};

LMODIFICADORES: LMODIFICADORES MODIFICADORES {$$ = $1+ " " + $2;}
                |MODIFICADORES {$$ = $1;};

MODIFICADORES: Tpublic {$$ = yytext;}
              |Tprivate {$$ = yytext;}
              |Tprotected {$$ = yytext;}
              |Tstatic {$$ = yytext;}
              |Tfinal {$$ = yytext;}
              |Tabstract {$$ = yytext;};
//---------------->
OPCCLASE1: LCONTENIDOCLASE '}' {
    $$ = $1;
}
          |'}'{
    var Hijo1 = CrearRaiz("LCONTENIDOCLASE", @1.first_column, @1.first_line);
    $$ = Hijo1;
};

LCONTENIDOCLASE: LCONTENIDOCLASE CONTENIDOCLASE {
    var Raiz = CrearRaiz("LCONTENIDOCLASE", @1.first_column, @1.first_line);
    Raiz.AddHijos($1);
    Raiz.AddHijos($2);
    $$ = Raiz;
}
                |CONTENIDOCLASE {
    var Raiz = CrearRaiz("LCONTENIDOCLASE", @1.first_column, @1.first_line);
    Raiz.AddHijos($1);
    $$ = Raiz;
};

CONTENIDOCLASE: CLASE {$$ = $1;}
                |METODO {$$ = $1;}
                |CONSTRUCTOR {$$ = $1;}
                |DECLARACION ';' {$$ = $1;};
//--------------->
CONSTRUCTOR: LMODIFICADORES1 Identificador '(' LPARAMETROS1 ')' '{' CONTENIDOMETODO1 {
    var Raiz = CrearRaiz("CONSTRUCTOR", @1.first_column, @1.first_line);
    //--------------MODIFICADORES-----------------
    var Hijo1 = CrearHijo("MODIFICADORES", $1, @1.first_column, @1.first_line);
    //--------------IDENTIFICADOR------------------
    var Hijo2 = CrearHijo("ID", $2, @2.first_column, @2.first_line);
    Raiz.AddHijos(Hijo1);
    Raiz.AddHijos(Hijo2);
    //-----------LISTA DE PARAMETROS----------------
    Raiz.AddHijos($4);
    //-----------CONTENIDO CONSTRUCTOR ------------
    Raiz.AddHijos($7);
    //----------------------------------------------
    $$ = Raiz;
};
//--------------->
LPARAMETROS1: LPARAMETROS {$$ = $1;}
              |{
    var Hijo1 = CrearRaiz("LPARAMETROS", 0, 0);
    $$ = Hijo1;
};

LPARAMETROS: LPARAMETROS ',' PARAMETRO {
    var Raiz = CrearRaiz("LPARAMETROS", @1.first_column, @1.first_line);
    Raiz.AddHijos($1);
    Raiz.AddHijos($3);
    $$ = Raiz;
}
            |PARAMETRO {
    var Raiz = CrearRaiz("LPARAMETROS", @1.first_column, @1.first_line);
    Raiz.AddHijos($1);
    $$ = Raiz;
};

PARAMETRO:  PARAM {
    var Raiz = CrearRaiz("PARAMETRO", @1.first_column, @1.first_line);
    Raiz.AddHijos($1.hijo1);
    Raiz.AddHijos($1.hijo2);
    $$ = Raiz;
}
          |Tfinal PARAM {
    var Raiz = CrearRaiz("PARAM_FINAL", @1.first_column, @1.first_line);
    Raiz.AddHijos($2.hijo1);
    Raiz.AddHijos($2.hijo2);
    $$ = Raiz;
};

PARAM: TIPOS TIPODECLARACIONVAR {
    var Hijo1 = CrearHijo("TIPOS", $1, @1.first_column, @1.first_line);
    var Hijo2 = CrearHijo("ID", $2, @2.first_column, @2.first_line);
    var Retorno = {"hijo1": Hijo1, "hijo2": Hijo2};
    $$ = Retorno;
};		

LCORCHETE:  LCORCHETE '[' ']' {$$ = $1 + "[]";}
			|'[' ']' {$$ = "[]";};
					
TIPOS1:  Tint {$$ = $1;}
      |Tdouble {$$ = $1;}
      |TString {$$ = $1;}
      |Tchar {$$ = $1;}
      |Tboolean {$$ = $1;}
      |Tvoid {$$ = $1;} 
	  |Tlinkedlist '<' TIPOS '>' {$$ = $1 + "<" + $3 + ">";};

TIPOS:TIPOS1 {$$ = $1;}
      |Identificador {$$ = $1;};
//---------------->
DECLARACION: LMODIFICADORES1 TIPOS LDECLARACIONVAR {
    var Raiz = CrearRaiz("DECLARACION", @1.first_column, @1.first_line);
    //--------------MODIFICADORES-----------------
    var Hijo1 = CrearHijo("MODIFICADORES", $1, @1.first_column, @1.first_line);
    Raiz.AddHijos(Hijo1);
    //------------TIPOS----------------------------
    var Hijo2 = CrearHijo("TIPOS", $2, @2.first_column, @2.first_line);
    Raiz.AddHijos(Hijo2);
    //---------------------------------------------
    Raiz.AddHijos($3);
    //---------------------------------------------
    $$ = Raiz;
};

LDECLARACIONVAR: LDECLARACIONVAR ',' DECLARACIONVAR {
    var Raiz = CrearRaiz("LDECLARACIONVAR", @1.first_column, @1.first_line);
    Raiz.AddHijos($1);
    Raiz.AddHijos($3);
    $$ = Raiz;
}
                |DECLARACIONVAR {
    var Raiz = CrearRaiz("LDECLARACIONVAR", @1.first_column, @1.first_line);
    Raiz.AddHijos($1);
    $$ = Raiz;
};

DECLARACIONVAR: TIPODECLARACIONVAR '=' INICIALIZARVARIABLE {
    var Raiz = CrearRaiz("DECLARACIONVAR", @1.first_column, @1.first_line);
    var Hijo1 = CrearHijo("ID", $1, @1.first_column, @1.first_line);
    Raiz.AddHijos(Hijo1);
    Raiz.AddHijos($3);
    $$ = Raiz;
}
                |TIPODECLARACIONVAR{
    var Raiz = CrearRaiz("DECLARACIONVAR", @1.first_column, @1.first_line);
    var Hijo1 = CrearHijo("ID", $1, @1.first_column, @1.first_line);
    Raiz.AddHijos(Hijo1);
    $$ = Raiz;
};

TIPODECLARACIONVAR: Identificador LCORCHETE {$$ = $1 + $2;}
                    |Identificador {$$ = $1;};

INICIALIZARVARIABLE: EXPRESION {$$ = $1;}
                    |F1INICIALIZARARREGLO {$$ = $1;};

F1INICIALIZARARREGLO: '{' OPCCONTARREGLO '}' {
    $$ = $2;
};

OPCCONTARREGLO: LCONTARREGLOS{
    var Raiz = CrearRaiz("OPCCONTARREGLO", @1.first_column, @1.first_line);
    Raiz.AddHijos($1);
    $$ = Raiz;
}
                |LEXPRESIONES1 {
    var Raiz = CrearRaiz("OPCCONTARREGLO", @1.first_column, @1.first_line);
    Raiz.AddHijos($1);
    $$ = Raiz;
};

LCONTARREGLOS: LCONTARREGLOS ',' CONTARREGLO{
    var Raiz = CrearRaiz("LCONTARREGLOS", @1.first_column, @1.first_line);
    Raiz.AddHijos($1);
    Raiz.AddHijos($3);
    $$ = Raiz;
}
              |CONTARREGLO{
    var Raiz = CrearRaiz("LCONTARREGLOS", @1.first_column, @1.first_line);
    Raiz.AddHijos($1);
    $$ = Raiz;
};

CONTARREGLO: '{' LEXPRESIONES1 '}' {
    var Raiz = CrearRaiz("CONTARREGLO", @1.first_column, @1.first_line);
    Raiz.AddHijos($2);
    $$ = Raiz;
}
            |'{' LCONTARREGLOS '}'{
    var Raiz = CrearRaiz("CONTARREGLO", @1.first_column, @1.first_line);
    Raiz.AddHijos($2);
    $$ = Raiz;
};

LEXPRESIONES1: LEXPRESIONES {$$ = $1}
              |{
    var Hijo1 = CrearRaiz("LEXPRESIONES", @1.first_column, @1.first_line);
    $$ = Hijo1;
};

LEXPRESIONES: LEXPRESIONES ',' EXPRESION {
    var Raiz = CrearRaiz("LEXPRESIONES", @1.first_column, @1.first_line);
    Raiz.AddHijos($1);
    Raiz.AddHijos($3);
    $$ = Raiz;
}
            |EXPRESION {
    var Raiz = CrearRaiz("LEXPRESIONES", @1.first_column, @1.first_line);
    Raiz.AddHijos($1);
    $$ = Raiz;
};
//-------------->
METODO: Tarroba Toverride LMODIFICADORES1 TIPOS ENCABEZADOMETODO1 OPCMETODO {
    var Raiz = CrearRaiz("METODO_OVERRIDE", @1.first_column, @1.first_line);
    //--------------MODIFICADORES-----------------
	var Hijo1 = CrearHijo("MODIFICADORES", $3, @3.first_column, @3.first_line);
	Raiz.AddHijos(Hijo1);
    //------------TIPOS----------------------------
    var Hijo2 = CrearHijo("TIPOS", $4, @4.first_column, @4.first_line);
    Raiz.AddHijos(Hijo2);
    //------------ENCABEZADOMETODO1-----------------
    Raiz.AddHijos($5.hijo1);
    Raiz.AddHijos($5.hijo2);
    //----------------------------------------------
    Raiz.AddHijos($6);
    $$ = Raiz;
}
        |LMODIFICADORES1 TIPOS ENCABEZADOMETODO1 OPCMETODO {
    var Raiz = CrearRaiz("METODO", @1.first_column, @1.first_line);
    //--------------MODIFICADORES-----------------
    var Hijo1 = CrearHijo("MODIFICADORES", $1, @1.first_column, @1.first_line);
    Raiz.AddHijos(Hijo1);
    //------------TIPOS----------------------------
    var Hijo2 = CrearHijo("TIPOS", $2, @2.first_column, @2.first_line);
    Raiz.AddHijos(Hijo2);
    //------------ENCABEZADOMETODO1-----------------
    Raiz.AddHijos($3.hijo1);
    Raiz.AddHijos($3.hijo2);
    //----------------------------------------------
    Raiz.AddHijos($4);
    $$ = Raiz;
};

ENCABEZADOMETODO1: Identificador '(' LPARAMETROS1 ')' {
    var Hijo1 = CrearHijo("ID", $1, @1.first_column, @1.first_line);
    var Hijo2 = $3;
    var Retorno = {"hijo1": Hijo1, "hijo2": Hijo2};
    $$ = Retorno;
}
|LCORCHETE Identificador '(' LPARAMETROS1 ')' {
    var Nombre = $1 + $2;
    var Hijo1 = CrearHijo("ID", Nombre, @2.first_column, @2.first_line);
    var Hijo2 = $4;
    var Retorno = {"hijo1": Hijo1, "hijo2": Hijo2};
    $$ = Retorno;
};

OPCMETODO: '{' CONTENIDOMETODO1 {
    $$ = $2;
}
          |';'{
    var Hijo1 = CrearHijo("LCONTENIDOMETODO_PyC", ";", @1.first_column, @1.first_line);
    $$ = Hijo1;
};

CONTENIDOMETODO1:LCONTENIDOMETODO '}' {
    $$ = $1;
}
                | '}' {
    var Objeto = CrearRaiz("LCONTENIDOMETODO" ,@1.first_column,@1.first_line);
    $$ = Objeto;
};

LCONTENIDOMETODO: LCONTENIDOMETODO CONTENIDOMETODO{
    var Objeto = CrearRaiz("LCONTENIDOMETODO" ,@1.first_column,@1.first_line);
    Objeto.AddHijos($1);
    Objeto.AddHijos($2);
    $$ = Objeto;
                  }
                  |CONTENIDOMETODO{
    var Objeto = CrearRaiz("LCONTENIDOMETODO" ,@1.first_column,@1.first_line);
    Objeto.AddHijos($1);
    $$ = Objeto;
                  };

CONTENIDOMETODO: DECLARACIONDENTROMETODO ';'{
                $$ = $1;
}
				|CLASE {
				$$ = $1;
}
                |RETORNO ';'{
                $$ = $1;
}
                |IMPRIMIR ';'{
                $$ = $1;
}
                |SENTENCIAS{
                $$ = $1;
}
                |NATIVAS ';'{
                $$ = $1;
}
                |ASIGNACION ';'{
                $$ = $1;
}
                |NOMBREOBJETO ';' {
                $$ = $1;
};

INCR_DECR: '++'{
          var Objeto = CrearHijo("INCREMENTO","++",@1.first_column,@1.first_line);
          $$ = Objeto;
}
          |'--'{
          var Objeto = CrearHijo("DECREMENTO","--",@1.first_column,@1.first_line);
          $$ = Objeto;
};

ASIGNACION: NOMBREOBJETO OPCASIGNACION{
      var Objeto = CrearRaiz("ASIGNACION",@1.first_column,@1.first_line);
      Objeto.AddHijos($1);
      Objeto.AddHijos($2);
      $$ = Objeto;
}
                |INCR_DECR NOMBREOBJETO{
                  var Objeto = CrearRaiz("ASIGNACIONPREFIJO",@1.first_column,@1.first_line);
                  Objeto.AddHijos($1);
                  Objeto.AddHijos($2);
                  $$ = Objeto;
};

OPCASIGNACION: INCR_DECR{
                  $$ = $1;
              }
              |'=' EXPRESION{
                  $$ = $2;
              }
              |'=' F1INICIALIZARARREGLO{
                  $$ = $2;
              };

NOMBREOBJETO: Tsuper{
    var Objeto = CrearRaiz("NOMBREOBJETO",@1.first_column,@1.first_line);
    var Objeto1 = CrearRaiz("SUPER",@1.first_column,@1.first_line);
    Objeto.AddHijos(Objeto1);
    $$ = Objeto;
}
              |Tthis{
    var Objeto = CrearRaiz("NOMBREOBJETO",@1.first_column,@1.first_line);
    var Objeto1 = CrearRaiz("THIS",@1.first_column,@1.first_line);
    Objeto.AddHijos(Objeto1);
    $$ = Objeto;
}
              |Identificador TIPONOMBREOBJETO{
    var Objeto = CrearRaiz("NOMBREOBJETO",@1.first_column,@1.first_line);
    var Objeto1 = CrearHijo("ID",$1,@1.first_column,@1.first_line);
    Objeto.AddHijos(Objeto1);
    Objeto.AddHijos($2);
    $$ = Objeto;
}
              |NOMBREOBJETO '.' Identificador TIPONOMBREOBJETO{
    var Objeto = CrearRaiz("NOMBREOBJETO",@1.first_column,@1.first_line);
    var Hijo1 = CrearHijo("ID",$3.toString(),@1.first_column,@1.first_line);
    Objeto.AddHijos($1);
	Objeto.AddHijos(Hijo1);
    Objeto.AddHijos($4);
    $$ = Objeto;
}
              |NOMBREOBJETO '.' Tsuper{
    var Objeto = CrearRaiz("NOMBREOBJETO",@1.first_column,@1.first_line);
    Objeto.AddHijos($1);
    var Objeto1 = CrearRaiz("SUPER",@1.first_column,@1.first_line);
    Objeto.AddHijos(Objeto1);
    $$ = Objeto;
}
              |Tsuper OPCCONSTRUCTORES{
    var Objeto = CrearRaiz("NOMBREOBJETO",@1.first_column,@1.first_line);
    var Objeto1 = CrearRaiz("SUPER",@1.first_column,@1.first_line);
    Objeto1.AddHijos($2);
    Objeto.AddHijos(Objeto1);
    $$ = Objeto;
}
              |Tthis OPCCONSTRUCTORES{
    var Objeto = CrearRaiz("NOMBREOBJETO",@1.first_column,@1.first_line);
    var Objeto1 = CrearRaiz("THIS",@1.first_column,@1.first_line);
    Objeto1.AddHijos($2);
    Objeto.AddHijos(Objeto1);
    $$ = Objeto;
};


TIPONOMBREOBJETO: OPCCONSTRUCTORES{
            $$ = $1;
}
                  |LDIMENSIONES{
            $$ = $1;
}
                  |{
	  var Objeto = CrearHijo("vacio","vacio",0,0);
	  $$ = Objeto;
};

OPCCONSTRUCTORES: '(' LEXPRESIONES1 ')'{
            $$ = $2;
};

DECLARACIONDENTROMETODO: TIPOS LDECLARACIONVAR{
	  var Objeto = CrearHijo("DECLARACION","",@1.first_column,@1.first_line);
	  //------------TIPOS----------------------------
	  var Hijo2 = CrearHijo("TIPOS", $1, @1.first_column, @1.first_line);
	  Objeto.AddHijos(Hijo2);
	  Objeto.AddHijos($2);
	  $$ = Objeto;
}
                        |Tfinal TIPOS LDECLARACIONVAR{
	  var Objeto = CrearHijo("DECLARACION","FINAL",@1.first_column,@1.first_line);
	  //------------TIPOS----------------------------
	  var Hijo2 = CrearHijo("TIPOS", $2, @2.first_column, @2.first_line);
	  //---------------------------------------------
	  Objeto.AddHijos(Hijo2);
	  Objeto.AddHijos($3);
	  $$ = Objeto;
};

RETORNO: Treturn EXPRESION{
                 var Objeto = CrearRaiz("RETORNO" ,@1.first_column,@1.first_line);
                 Objeto.AddHijos($2);
                 $$ = Objeto;
              }
        |Treturn{
                 var Objeto = CrearRaiz("RETORNO" ,@1.first_column,@1.first_line);
                 $$ = Objeto;
              };

IMPRIMIR:  Tprint '(' EXPRESION ')'{
	 var Objeto = CrearRaiz("PRINT" ,@1.first_column,@1.first_line);
	 Objeto.AddHijos($3);
	 $$ = Objeto;
}
          |Tprintln '(' EXPRESION ')'{
	 var Objeto = CrearRaiz("PRINTLN" ,@1.first_column,@1.first_line);
	 Objeto.AddHijos($3);
	 $$ = Objeto;
};

SENTENCIAS:  IF{
    $$ = $1;
}
            |WHILE{
    $$ = $1;
}
            |DOWHILE{
    $$ = $1;
}
            |SWITCH{
    $$ = $1;
}
            |FOR{
    $$ = $1;
}
            |TRY_CATCH{
    $$ = $1;
}
            |THROW{
    $$ = $1;
};

NATIVAS: GRAPH{
    $$ = $1;
}
        |WRITE{
    $$ = $1;
};
//-------------------------------->
GRAPH: Tgraph '(' EXPRESION ',' EXPRESION ')'{
	 var Objeto = CrearRaiz("GRAPH" ,@1.first_column,@1.first_line);
	 Objeto.AddHijos($3);
	 Objeto.AddHijos($5);
	 $$ = Objeto;
};

WRITE: Twrite_file '(' EXPRESION ',' EXPRESION ')'{
	 var Objeto = CrearRaiz("WRITE" ,@1.first_column,@1.first_line);
	 Objeto.AddHijos($3);
	 Objeto.AddHijos($5);
	 $$ = Objeto;
};
//-------------------------------->
THROW: Tthrow EXPRESION ';'{
	 var Objeto = CrearRaiz("THROW" ,@1.first_column,@1.first_line);
	 Objeto.AddHijos($2);
	 $$ = Objeto;
};

TRY_CATCH: Ttry '{' CONTENIDOMETODO1 Tcatch '(' TIPOS Identificador ')' '{' CONTENIDOMETODO1{
	var Objeto = CrearRaiz("TRYCATCH" ,@1.first_column,@1.first_line);
    Objeto.AddHijos($3);
	//------------TIPOS----------------------------
    var Hijo2 = CrearHijo("TIPOS", $6, @6.first_column, @6.first_line);
    //---------------------------------------------
    var Hijo3 = CrearHijo("ID", $7, @7.first_column,@7.first_line);
    //---------------------------------------------
    Objeto.AddHijos(Hijo2);
    Objeto.AddHijos(Hijo3);
    Objeto.AddHijos($10);
    $$ = Objeto;
};

FOR: Tfor '(' CONTENIDOFOR{
      $$ = $3;
};

CONTENIDOFOR: OPCFOR  ';' EXPRESION ';' ASIGNACION ')' '{' LCSENTENCIAS1{
   var Objeto = CrearRaiz("FOR" ,@1.first_column,@1.first_line);
    Objeto.AddHijos($1);
    Objeto.AddHijos($3);
    Objeto.AddHijos($5);
    Objeto.AddHijos($8);
    $$ = Objeto;
}
              |PARAMETRO ':' EXPRESION ')' '{' LCSENTENCIAS1{
   var Objeto = CrearRaiz("FOREACH" ,@1.first_column,@1.first_line);
    Objeto.AddHijos($1);
    Objeto.AddHijos($3);
    Objeto.AddHijos($6);
    $$ = Objeto;
};

OPCFOR: DECLARACIONDENTROMETODO{
		$$ = $1;
}
        |ASIGNACION{
          $$ = $1;
        };

SWITCH: Tswitch '(' EXPRESION ')' '{' LCASOS1{
   var Objeto = CrearRaiz("SWITCH" ,@1.first_column,@1.first_line);
    Objeto.AddHijos($3);
    Objeto.AddHijos($6);
    $$ = Objeto;
};

LCASOS1: LCASOS '}'{
                $$ = $1;
}
        |'}'{
	 var Objeto = CrearRaiz("LCASOS" ,@1.first_column,@1.first_line);
	 $$ = Objeto;
};

LCASOS: LCASOS POSBCASOS{
                var Objeto = CrearRaiz("LCASOS" ,@1.first_column,@1.first_line);
                 Objeto.AddHijos($1);
                 Objeto.AddHijos($2);
                 $$ = Objeto;
}
        |POSBCASOS{
                 var Objeto = CrearRaiz("LCASOS" ,@1.first_column,@1.first_line);
                 Objeto.AddHijos($1);
                 $$ = Objeto;
              };

POSBCASOS: Tcase EXPRESION ':' CASOSS{
   var Objeto = CrearRaiz("CASE" ,@1.first_column,@1.first_line);
    Objeto.AddHijos($2);
    Objeto.AddHijos($4);
    $$ = Objeto;
}
          |Tdefault ':' CASOSS{
   var Objeto = CrearRaiz("DEFAULT" ,@1.first_column,@1.first_line);
    Objeto.AddHijos($3);
    $$ = Objeto;
};

CASOSS: LCSENTENCIAS{
  $$ = $1;
}
|'{' LCSENTENCIAS '}' {
	$$ = $2;
}
|'{' '}' {
  var Objeto = CrearRaiz("LCSENTENCIAS" , @1.first_column, @1.first_line);
  $$ = Objeto;
}
|{
  var Objeto = CrearRaiz("LCSENTENCIAS" ,0,0);
  $$ = Objeto;
};
//------------------------------->
WHILE: Twhile '(' EXPRESION ')' '{' LCSENTENCIAS1{
   var Objeto = CrearRaiz("WHILE" , @1.first_column,@1.first_line);
    Objeto.AddHijos($3);
    Objeto.AddHijos($6);
    $$ = Objeto;
};

DOWHILE:  Tdo '{' LCSENTENCIAS1 Twhile '(' EXPRESION ')' ';'{
   var Objeto = CrearRaiz("DOWHILE" ,@1.first_column,@1.first_line);
    Objeto.AddHijos($3);
    Objeto.AddHijos($6);
    $$ = Objeto;
};

IF:  IF_ELSEIF Telse '{' LCSENTENCIAS1{

                var Objeto = CrearRaiz("IF_ELSEIF" ,@1.first_column,@1.first_line);
                 Objeto.AddHijos($1);

                 var Objeto1 = CrearRaiz("ELSE" ,@1.first_column,@1.first_line);
                  Objeto1.AddHijos($4);
                 Objeto.AddHijos(Objeto1);

                 $$ = Objeto;

}
    |IF_ELSEIF{
      $$ = $1;
    };

IF_ELSEIF: IF_ELSEIF Telse Tif IF1{
                var Objeto = CrearRaiz("IF_ELSEIF" ,@1.first_column,@1.first_line);
                 Objeto.AddHijos($1);
                 Objeto.AddHijos($4);
                 $$ = Objeto;
}
          |Tif IF1{
                 var Objeto = CrearRaiz("IF_ELSEIF" ,@1.first_column,@1.first_line);
                 Objeto.AddHijos($2);
                 $$ = Objeto;
              };

IF1: '(' EXPRESION ')' '{' LCSENTENCIAS1{
   var Objeto = CrearRaiz("IF" ,@1.first_column,@1.first_line);
    Objeto.AddHijos($2);
    Objeto.AddHijos($5);
    $$ = Objeto;
};

//------------------------------------------------------
LCSENTENCIAS1: LCSENTENCIAS '}'{
                $$ = $1;
}
              |'}'{
                 var Objeto = CrearRaiz("LCSENTENCIAS" ,@1.first_column,@1.first_line);
                 $$ = Objeto;
              };

LCSENTENCIAS: LCSENTENCIAS CSENTENCIAS{
                var Objeto = CrearRaiz("LCSENTENCIAS" ,@1.first_column,@1.first_line);
                 Objeto.AddHijos($1);
                 Objeto.AddHijos($2);
                 $$ = Objeto;
}
              |CSENTENCIAS{
                 var Objeto = CrearRaiz("LCSENTENCIAS" ,@1.first_column,@1.first_line);
                 Objeto.AddHijos($1);
                 $$ = Objeto;
              };

CSENTENCIAS: CONTENIDOMETODO{
              $$ = $1;
            }
            |Tbreak ';'{
              var Objeto = CrearHijo("BREAK", "break", @1.first_column,@1.first_line);
              $$ = Objeto;
          }
            |Tcontinue ';'{
              var Objeto = CrearHijo("CONTINUE", "continue" ,@1.first_column,@1.first_line);
              $$ = Objeto;
          };

//-------------------------------------------------------
//-------------------------------------------------------
EXPRESION: CONDICIONAL{
              var Objeto = CrearRaiz("EXPRESION" ,@1.first_column,@1.first_line);
              Objeto.AddHijos($1);
              $$ = Objeto;
          }
          |CONDICIONAL '?' EXPRESION ':' EXPRESION{
              var Objeto = CrearRaiz("EXPRESION" ,@1.first_column,@1.first_line);
              var Objeto1 = CrearRaiz("TERNARIO" ,@1.first_column,@1.first_line);
              Objeto1.AddHijos($1);
              Objeto1.AddHijos($3);
              Objeto1.AddHijos($5);
              Objeto.AddHijos(Objeto1);
              $$ = Objeto;
          }
          |Tnew OPCINSTANCIA{
              var Objeto = CrearRaiz("EXPRESION" ,@1.first_column,@1.first_line);
              Objeto.AddHijos($2);
              $$ = Objeto;
          };

OPCINSTANCIA: Identificador '(' LEXPRESIONES1 ')'{
                var Objeto = CrearRaiz("INSTANCIAOBJETO", @1.first_column,@1.first_line);
				var Hijo1 = CrearHijo("ID", $1, @1.first_column, @1.first_line);
				Objeto.AddHijos(Hijo1);
                Objeto.AddHijos($3);
                 $$ = Objeto;
              }
              |Tlinkedlist '<''>' '('')'{
                var Objeto = CrearHijo("LINKEDLIST", "INSTANCIA", @1.first_column, @1.first_line);
                 $$ = Objeto;
              }
              |TIPOS LDIMENSIONES{
                var Objeto = CrearRaiz("INSTANCIAARREGLO" ,@1.first_column,@1.first_line);
                //------------TIPOS----------------------------
                var Hijo1 = CrearHijo("TIPOS", $1, @1.first_column, @1.first_line);
                //---------------------------------------------
                Objeto.AddHijos(Hijo1);
                Objeto.AddHijos($2);
                 $$ = Objeto;
              };

LDIMENSIONES: LDIMENSIONES '['EXPRESION']'{
                var Objeto = CrearRaiz("LDIMENSIONES" ,@1.first_column,@1.first_line);
                 Objeto.AddHijos($1);
                 Objeto.AddHijos($3);
                 $$ = Objeto;

}
              |'['EXPRESION']'{
                 var Objeto = CrearRaiz("LDIMENSIONES" ,@1.first_column,@1.first_line);
                 Objeto.AddHijos($2);
                 $$ = Objeto;
              };

CONDICIONAL: CONDICIONAL '||' CONDICIONAL1{

    var Objeto = CrearRaiz("OR" ,@2.first_column,@2.first_line);
    Objeto.AddHijos($1);
    Objeto.AddHijos($3);
    $$ = Objeto;

  }
            |CONDICIONAL1{
              $$ = $1;
            };

CONDICIONAL1: CONDICIONAL1 '&&' CONDICIONAL2{

    var Objeto = CrearRaiz("AND" ,@2.first_column,@2.first_line);
    Objeto.AddHijos($1);
    Objeto.AddHijos($3);
    $$ = Objeto;

  }
              |CONDICIONAL2{
                $$ =$1;
              };

CONDICIONAL2: CONDICIONAL2 '^' CONDICIONAL3{

    var Objeto = CrearRaiz("XOR" ,@2.first_column,@2.first_line);
    Objeto.AddHijos($1);
    Objeto.AddHijos($3);
    $$ = Objeto;

  }
              |CONDICIONAL3{
                $$ = $1;
              };

CONDICIONAL3:  E '==' E{

    var Objeto = CrearRaiz("IGUALIGUAL" ,@2.first_column,@2.first_line);
    Objeto.AddHijos($1);
    Objeto.AddHijos($3);
    $$ = Objeto;

  }
              |E '!=' E{

    var Objeto = CrearRaiz("DIFERENTE" ,@2.first_column,@2.first_line);
    Objeto.AddHijos($1);
    Objeto.AddHijos($3);
    $$ = Objeto;

  }
              |E '<'  E{

    var Objeto = CrearRaiz("MENOR" ,@2.first_column,@2.first_line);
    Objeto.AddHijos($1);
    Objeto.AddHijos($3);
    $$ = Objeto;

  }
              |E '>'  E{

    var Objeto = CrearRaiz("MAYOR" ,@2.first_column,@2.first_line);
    Objeto.AddHijos($1);
    Objeto.AddHijos($3);
    $$ = Objeto;

  }
              |E '<=' E{

    var Objeto = CrearRaiz("MENORIGUAL" ,@2.first_column,@2.first_line);
    Objeto.AddHijos($1);
    Objeto.AddHijos($3);
    $$ = Objeto;

  }
              |E '>=' E{

    var Objeto = CrearRaiz("MAYORIGUAL" ,@2.first_column,@2.first_line);
    Objeto.AddHijos($1);
    Objeto.AddHijos($3);
    $$ = Objeto;

  }
              |E Tinstanceof E{
                var Objeto = CrearRaiz("INSTANCEOF" ,@2.first_column,@2.first_line);
                Objeto.AddHijos($1);
                Objeto.AddHijos($3);
                $$ = Objeto;
              }
              |E{
                $$ = $1;
              };

E: E '+' E{

    var Objeto = CrearRaiz("SUMA" ,@2.first_column,@2.first_line);
    Objeto.AddHijos($1);
    Objeto.AddHijos($3);
    $$ = Objeto;

  }
  |E '-' E{

    var Objeto = CrearRaiz("RESTA" ,@2.first_column,@2.first_line);
    Objeto.AddHijos($1);
    Objeto.AddHijos($3);
    $$ = Objeto;

  }
  |E '*' E{

    var Objeto = CrearRaiz("MULTIPLICACION" ,@2.first_column,@2.first_line);
    Objeto.AddHijos($1);
    Objeto.AddHijos($3);
    $$ = Objeto;

  }
  |E '/' E{

    var Objeto = CrearRaiz("DIVISION" ,@2.first_column,@2.first_line);
    Objeto.AddHijos($1);
    Objeto.AddHijos($3);
    $$ = Objeto;

  }
  |E '%' E{
    var Objeto = CrearRaiz("MODULO" ,@2.first_column,@2.first_line);
    Objeto.AddHijos($1);
    Objeto.AddHijos($3);
    $$ = Objeto;
  }
  |Tpow '(' E ',' E ')'{
    var Objeto = CrearRaiz("POTENCIA" ,@1.first_column,@1.first_line);
    Objeto.AddHijos($3);
    Objeto.AddHijos($5);
    $$ = Objeto;
  }
  |NOMBREOBJETO '++' %prec Tincremento {
    var Objeto = CrearRaiz("INCREMENTOPOSTFIJO" ,@2.first_column,@2.first_line);
    Objeto.AddHijos($1);
    $$ = Objeto;
  }
  |NOMBREOBJETO '--' %prec Tdecremento {
    var Objeto = CrearRaiz("DECREMENTOPOSTFIJO" ,@2.first_column,@2.first_line);
    Objeto.AddHijos($1);
    $$ = Objeto;
  }
  |'++' NOMBREOBJETO{
    var Objeto = CrearRaiz("INCREMENTOPREFIJO" ,@1.first_column,@1.first_line);
    Objeto.AddHijos($2);
    $$ = Objeto;
  }
  |'--' NOMBREOBJETO{
    var Objeto = CrearRaiz("DECREMENTOPREFIJO" ,@1.first_column,@1.first_line);
    Objeto.AddHijos($2);
    $$ = Objeto;
  }
  |'-' E %prec UTmenos {
    var Objeto = CrearRaiz("NEGATIVO" ,@1.first_column,@1.first_line);
    Objeto.AddHijos($2);
    $$ = Objeto;
  }
  |'+' E %prec UTmas {

    var Objeto = CrearRaiz("POSITIVO" ,@1.first_column,@1.first_line);
    Objeto.AddHijos($2);
    $$ = Objeto;

  }
  |F{
  $$ = $1;
};

F: G{
  $$ = $1;
}
  |'!' G {
    var Objeto = CrearRaiz("NOT" ,@1.first_column,@1.first_line);
    Objeto.AddHijos($2);
    $$ = Objeto;
  };

G: '(' EXPRESION ')'{
  $$ = $2;
}
  |ERint {
    $$ = CrearHijo("INT",$1.toString(),@1.first_column,@1.first_line);
  }
  |ERdouble {
    $$ = CrearHijo("DOUBLE",$1.toString(),@1.first_column,@1.first_line);
  }
  |ERstring {
    $$ = CrearHijo("STRING",Replace($1.toString()) ,@1.first_column,@1.first_line);
  }
  |ERchar {
    $$ = CrearHijo("CHAR",Replace($1.toString()) ,@1.first_column,@1.first_line);
  }
  |ERtrue {
    $$ = CrearHijo("BOOLEAN","TRUE" ,@1.first_column,@1.first_line);
  }
  |ERfalse {
    $$ = CrearHijo("BOOLEAN","FALSE" ,@1.first_column,@1.first_line);
  }
  |ERnull {
    $$ = CrearRaiz("NULL" ,@1.first_column,@1.first_line);
  }
  |Tstr '(' EXPRESION ')' {
    var Objeto = CrearRaiz("STR" ,@1.first_column,@1.first_line);
    Objeto.AddHijos($3);
    $$ = Objeto;
  }
  |TtoDouble '(' EXPRESION ')' {
    var Objeto = CrearRaiz("TODOUBLE" ,@1.first_column,@1.first_line);
    Objeto.AddHijos($3);
    $$ = Objeto;
  }
  |TtoInt '(' EXPRESION ')' {
    var Objeto = CrearRaiz("TOINT" ,@1.first_column,@1.first_line);
    Objeto.AddHijos($3);
    $$ = Objeto;
  }
  |TtoChar '(' EXPRESION ')' {
    var Objeto = CrearRaiz("TOCHAR" ,@1.first_column,@1.first_line);
    Objeto.AddHijos($3);
    $$ = Objeto;
  }
  |Tread_file '(' EXPRESION ')' {
    var Objeto = CrearRaiz("READFILE" ,@1.first_column,@1.first_line);
    Objeto.AddHijos($3);
    $$ = Objeto;
  }

  |'(' TIPOS1 ')' E %prec Ttypecast {
    var Objeto2 = CrearRaiz("EXPRESION" ,@1.first_column,@1.first_line);
    /* ------------------------------------------------ */
    var Objeto = CrearRaiz("CASTEO" , @1.first_column,@1.first_line);
    var Hijo1 = CrearHijo("TIPO", $2, @2.first_column,@2.first_line);
    Objeto.AddHijos(Hijo1);
    Objeto.AddHijos($4);
    /* ------------------------------------------------ */
    Objeto2.AddHijos(Objeto);
    $$ = Objeto2;
  }
  |NOMBREOBJETO {
    $$ = $1;
  };