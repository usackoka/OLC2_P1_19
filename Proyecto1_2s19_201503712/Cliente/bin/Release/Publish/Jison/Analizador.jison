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


"[+DATABASES]"										return 'res_dataBasesOpen';
"[-DATABASES]"										return 'res_dataBasesClose';
"[+ATTRIBUTES]"										return 'res_atributtesOpen';
"[-ATTRIBUTES]"										return 'res_atributtesClose';
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
"[+TYPECQL]"										return 'res_typeCQLOpen';
"[-TYPECQL]"										return 'res_typeCQLClose';
"[+PROCEDURES]"										return 'res_proceduresOpen';
"[-PROCEDURES]"										return 'res_proceduresClose';
"[+PROCEDURE]"										return 'res_procedureOpen';
"[-PROCEDURE]"										return 'res_procedureClose';

"[+ERROR]"											return 'res_errorOpen';
"[-ERROR]"											return 'res_errorClose';
"[+LINE]"											return 'res_lineOpen';
"[-LINE]"											return 'res_lineClose';
"[+COLUMN]"											return 'res_columnOpen';
"[-COLUMN]"											return 'res_columnClose';
"[+LOGIN]"											return 'res_loginOpen';
"[-LOGIN]"											return 'res_loginClose';
"[+LOGOUT]"											return 'res_logoutOpen';
"[-LOGOUT]"											return 'res_logoutClose';
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
    	this.logout = false;
    	this.contMess = 0;
    	this.contData = 0;
    	this.contErr = 0;
    	this.contDBMS = 0;
    	this.data = {};
    	this.errores = {};
	    this.mensajes = {};
	    this.dbms = new Estruct();
    }

    function Estruct(){
    	this.contProcedures = 0;
    	this.contAtrs = 0;
    	this.contColumns = 0;
    	this.contTypes = 0;
    	this.contTab = 0;
    	this.contDataBase = 0;
    	this.dataBases = {};
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
	| DBMS
	| LOGOUT
	| LOGIN
;

LOGIN : 'res_loginOpen' STATUS 'res_loginClose'
;

LOGOUT : 'res_logoutOpen' STATUS2 'res_logoutClose'
;

STATUS2 : res_success {ast.logout = true;}
	| res_fail {ast.logout = false;}
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

DBMS : res_dataBasesOpen DATA_BASES res_dataBasesClose
	| res_dataBasesOpen res_dataBasesClose
;

DATA_BASES : DATA_BASES res_dataBaseOpen CONTENIDO res_dataBaseClose 
	| res_dataBaseOpen CONTENIDO res_dataBaseClose
;

CONTENIDO : res_nameOpen id res_nameClose res_tablesOpen TABLES res_typesOpen TYPES res_proceduresOpen PROCEDURES {
	var db = {};
	db.name = $2;
	db.tables = $5;
	db.types = $7;
	db.procedures = $9;
	ast.dbms.dataBases[ast.dbms.contDataBase++] = db;
}
;

TABLES : TABLE res_tablesClose {
	$$ = $1;
}
	| res_tablesClose {
		$$ = {};
	}
;

TABLE : TABLE res_tableOpen res_nameOpen id res_nameClose COLUMNAS res_tableClose {
		var tab = {};
		var tables = $1;
		tab.columns = $6;
		tab.name = $4;
		tables[ast.dbms.contTab++] = tab;
		$$ = tables;
	}
	| res_tableOpen res_nameOpen id res_nameClose COLUMNAS res_tableClose {
		var tab = {};
		var tables = {};
		tab.columns = $5;
		tab.name = $3;
		ast.dbms.contTab = 0;
		tables[ast.dbms.contTab++] = tab;
		$$ = tables;
	}
;

COLUMNAS : COLUMNAS res_columnOpen id res_columnClose {
		var columns = $1;
		columns[ast.dbms.contColumns++] = $3;
		$$ = columns;
	}
	| res_columnOpen id res_columnClose {
		var columns = {};
		ast.dbms.contColumns = 0;
		columns[ast.dbms.contColumns++] = $2;
		$$ = columns;
	}
;

TYPES : TYPE res_typesClose {
	$$ = $1;
}
	| res_typesClose {
		$$ = {};
	}
;

TYPE : TYPE res_typeCQLOpen res_nameOpen id res_nameClose ATRIBUTOS res_typeCQLClose {
	var typ = {};
	var types = $1;
	typ.atrs = $6;
	typ.name = $4;
	types[ast.dbms.contTypes++] = typ;
	$$ = types;
}
	| res_typeCQLOpen res_nameOpen id res_nameClose ATRIBUTOS res_typeCQLClose {
		var typ = {};
		var types = {};
		typ.atrs = $5;
		typ.name = $3;
		ast.dbms.contTypes = 0;
		types[ast.dbms.contTypes++] = typ;
		$$ = types;
	}
;

PROCEDURES : PROCEDURE res_proceduresClose {
	$$ = $1;
}
	| res_proceduresClose {
		$$ = {};
	}
;

PROCEDURE : PROCEDURE res_procedureOpen id res_procedureClose {
	var procedures = $1;
	procedures[ast.dbms.contProcedures++] = $3;
	$$ = procedures;
}
	| res_procedureOpen id res_procedureClose {
		var procedures = {};
		ast.dbms.contProcedures = 0;
		procedures[ast.dbms.contProcedures++] = $2;
		$$ = procedures;
	}
;

ATRIBUTOS : ATRIBUTOS res_atributtesOpen id res_atributtesClose {
	var atrs = $1;
	atrs[ast.dbms.contAtrs++] = $3;
	$$ = atrs;
}
	| res_atributtesOpen id res_atributtesClose {
		var atrs = {};
		ast.dbms.contAtrs = 0;
		atrs[ast.dbms.contAtrs++] = $2;
		$$ = atrs;
	}
;

T1 : T1 'TODO' {$$ = $1+$2;}
	| T1 'WS' {$$ = $1+$2;}
	| 'TODO' {$$=$1;}
	| 'WS' {$$=$1;}
;
