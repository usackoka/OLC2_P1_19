%lex
%{

%}
%options case-insensitive
%locations
digito = [0-9]
letras = [A-Za-zÑñ]
%s res_message
%s res_lexema
%s res_descripcion
%s res_type
%s res_data
%%
//============================== ESTADOS =================================
"[+MESSAGE]"	{this.begin("res_message"); return 'res_messageOpen'}
<res_message>"[-MESSAGE]"	{this.begin('INITIAL');  return 'res_messageClose';}
<res_message>.	{return 'TODO'}
<res_message>[\t\r\n\f]      {return 'WS'}
//-----------------------------------------------------------------------
//-----------------------------------------------------------------------
"[+LEXEMA]"	{this.begin("res_lexema"); return 'res_lexemaOpen'}
<res_lexema>"[-LEXEMA]"	{this.begin('INITIAL');  return 'res_lexemaClose';}
<res_lexema>.	{return 'TODO'}
<res_lexema>[\t\r\n\f]      {return 'WS'}
//-----------------------------------------------------------------------
//-----------------------------------------------------------------------
"[+DESC]"	{this.begin("res_descripcion"); return 'res_descripcionOpen'}
<res_descripcion>"[-DESC]"	{this.begin('INITIAL');  return 'res_descripcionClose';}
<res_descripcion>.	{return 'TODO'}
<res_descripcion>[\t\r\n\f]      {return 'WS'}
//-----------------------------------------------------------------------
//-----------------------------------------------------------------------
"[+TYPE]"	{this.begin("res_type"); return 'res_typeOpen'}
<res_type>"[-TYPE]"	{this.begin('INITIAL');  return 'res_typeClose';}
<res_type>.	{return 'TODO'}
<res_type>[\t\r\n\f]      {return 'WS'}
//-----------------------------------------------------------------------
//-----------------------------------------------------------------------
"[+DATA]"	{this.begin("res_data"); return 'res_dataOpen'}
<res_data>"[-DATA]"	{this.begin('INITIAL');  return 'res_dataClose';}
<res_data>.	{return 'TODO'}
<res_data>[\t\r\n\f]      {return 'WS'}
//========================================================================
\s+                   /* skip whitespace */

/*
"[+DATABASES]"										return 'res_dataBasesOpen';
"[-DATABASES]"										return 'res_dataBasesClose';
"[+DATABASE]"										return 'res_dataBaseOpen';
"[-DATABASE]"										return 'res_dataBaseClose';
"[+NAME]"											return 'res_nameOpen';
"[-NAME]"											return 'res_nameClose';
"[+TABLES]"											return 'res_tablesOpen';
"[-TABLES]"											return 'res_tablesClose';
"[+TABLE]"											return 'res_tableOpen';
"[-TABLE]"											return 'res_tableClose';
"[+TYPES]"											return 'res_typesOpen';
"[-TYPES]"											return 'res_typesClose';
"[+TYPE]"											return 'res_typeOpen';
"[-TYPE]"											return 'res_typeClose';
"[+PROCEDURES]"										return 'res_procedureOpen';
"[-PROCEDURES]"										return 'res_procedureClose';
*/

"[+ERROR]"											return 'res_errorOpen';
"[-ERROR]"											return 'res_errorClose';
"[+LINE]"											return 'res_lineOpen';
"[-LINE]"											return 'res_lineClose';
"[+COLUMN]"											return 'res_columnOpen';
"[-COLUMN]"											return 'res_columnClose';
"[+LOGIN]"											return 'res_loginOpen';
"[-LOGIN]"											return 'res_loginClose';
"[SUCCESS]"											return 'res_success';
"[FAIL]"											return 'res_fail';
{digito}+											return 'numero';
({letras}|"_")({letras}+|{digito}*|"_")*          	return 'id';
<<EOF>>                 							return 'EOF';
.                       { console.error('Este es un error léxico: ' + yytext + ', en la linea: ' + yylloc.first_line + ', en la columna: ' + yylloc.first_column); }

/lex

%define parse.error verbose
%option bison-locations
%{
    function TokenError(){
    	this.lexema = "";
    	this.descripcion = "";
    	this.fila = "";
    	this.columna = "";
    	this.tipo = "";
    }

    function AST_LUP(){
    	this.login = false;
    	this.contMess = 0;
    	this.contData = 0;
    	this.contErr = 0;
    	this.contDBMS = 0;
    	this.data = {};
    	this.errores = {};
	    this.mensajes = {};
	    this.dbms = {};
    }

    var ast = new AST_LUP();
%}


%start S

%% /* Definición de la gramática */

S : LIST_BLOCK EOF{
	return ast;
}
;

LIST_BLOCK : LIST_BLOCK BLOCK
	| BLOCK
;

BLOCK : MENSAJE {ast.mensajes[ast.contMess++] = $1;}
	| DATA {ast.data[ast.contData++] = $1;}
	| ERROR {ast.errores[ast.contErr++] = $1;}
	//| DBMS
	| LOGIN
;

LOGIN : 'res_loginOpen' STATUS 'res_loginClose'
;

STATUS : res_success {ast.login = true;}
	| res_fail {ast.login = false;}
;

MENSAJE : 'res_messageOpen' T1 'res_messageClose'
	{$$ = $2;}
; 

DATA : 'res_dataOpen' T1 'res_dataClose'
	{$$=$2;}
;

ERROR : 'res_errorOpen' 
		'res_lexemaOpen' T1 'res_lexemaClose' 
		'res_lineOpen' 'numero' 'res_lineClose'
		'res_columnOpen' 'numero' 'res_columnClose'
		'res_typeOpen'	T1 'res_typeClose'
		'res_descripcionOpen' T1 'res_descripcionClose'
		'res_errorClose'
		{var error = new TokenError(); error.lexema = $3; error.fila = $6; error.columna=$9; 
			error.tipo = $12; error.descripcion = $15;
			$$ = error;}
;

/*
DBMS : res_dataBasesOpen DATA_BASES res_dataBasesClose
;

DATA_BASES : DATA_BASES res_dataBaseOpen + CONTENIDO + res_dataBaseClose {
	var data_base = {};
	data_base.
	ast.dbms[ast.contDBMS++] = $1;
}
	| res_dataBaseOpen + CONTENIDO + res_dataBaseClose
;

CONTENIDO : 
*/


T1 : T1 'TODO' {$$ = $1+$2;}
	| T1 'WS' {$$ = $1+$2;}
	| 'TODO' {$$=$1;}
	| 'WS' {$$=$1;}
;
