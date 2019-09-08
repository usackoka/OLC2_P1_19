%lex
%options case-insensitive
%locations
%%
\s+                   /* skip whitespace */


<<EOF>>                 return 'EOF';
//.                       { console.error('Este es un error léxico: ' + yytext + ', en la linea: ' + yylloc.first_line + ', en la columna: ' + yylloc.first_column); }

("[+DATA]")(.|\t|\r|\n)+("[-DATA]") 					return "res_data";
("\[\+MESSAGE\]")(.|\s)*("\[-MESSAGE\]") 				return "res_message";
("[+ERROR]")						 					return "res_errorAbre";
("[-ERROR]")											return "res_errorCierra";
("[+DESC]")(.|\t|\r|\n)+("[-DESC]")						return "res_descripcion";
("[+LEXEMA]")(.|\t|\r|\n)+("[-LEXEMA]")					return "res_lexema";
("[+LINE]")(.|\t|\r|\n)+("[-LINE]")						return "res_fila";
("[+COLUMN]")(.|\t|\r|\n)+("[-COLUMN]")					return "res_columna";
("[+TYPE]")(.|\t|\r|\n)+("[-TYPE]")						return "res_tipo";
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
	    this.errores = new Array();
	    this.mensajes = {};
	    this.data = new Array();
    }

    var ast = new AST_LUP();
%}


%start S

%% /* Definición de la gramática */

S : LIST_BLOCK EOF{
	return ast;
};

LIST_BLOCK : LIST_BLOCK BLOCK
	| BLOCK;

BLOCK : res_message {
		ast.mensajes[ast.mensajes.length] = $1;
	}
	| res_errorAbre ERROR res_errorCierra {
		ast.errores.push($2);
	}
	| res_data {
		ast.data.push($1);
};

ERROR : ERROR res_fila{
		var e = $1;
		e.fila = $2;
		$$ = e;
	}
	| ERROR res_columna{
		var e = $1;
		e.columna = $2;
		$$ = e;
	}
	| ERROR res_lexema
	{
		var e = $1;
		e.lexema = $2;
		$$ = e;
	}
	| ERROR res_tipo{
		var e = $1;
		e.tipo = $2;
		$$ = e;
	}
	| ERROR res_descripcion{
		var e = $1;
		e.descripcion = $2;
		$$ = e;
	}
	| res_fila{
		var e = new TokenError();
		e.fila = $1;
		$$ = e;
	}
	| res_columna{
		var e = new TokenError();
		e.columna = $1;
		$$ = e;
	}
	| res_lexema{
		var e = new TokenError();
		e.lexema = $1;
		$$ = e;
	}
	| res_tipo{
		var e = new TokenError();
		e.tipo = $1;
		$$ = e;
	}
	| res_descripcion{
		var e = new TokenError();
		e.descripcion = $1;
		$$ = e;
	};