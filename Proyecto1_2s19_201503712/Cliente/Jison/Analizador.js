/* parser generated by jison 0.4.18 */
/*
  Returns a Parser object of the following structure:

  Parser: {
    yy: {}
  }

  Parser.prototype: {
    yy: {},
    trace: function(),
    symbols_: {associative list: name ==> number},
    terminals_: {associative list: number ==> name},
    productions_: [...],
    performAction: function anonymous(yytext, yyleng, yylineno, yy, yystate, $$, _$),
    table: [...],
    defaultActions: {...},
    parseError: function(str, hash),
    parse: function(input),

    lexer: {
        EOF: 1,
        parseError: function(str, hash),
        setInput: function(input),
        input: function(),
        unput: function(str),
        more: function(),
        less: function(n),
        pastInput: function(),
        upcomingInput: function(),
        showPosition: function(),
        test_match: function(regex_match_array, rule_index),
        next: function(),
        lex: function(),
        begin: function(condition),
        popState: function(),
        _currentRules: function(),
        topState: function(),
        pushState: function(condition),

        options: {
            ranges: boolean           (optional: true ==> token location info will include a .range[] member)
            flex: boolean             (optional: true ==> flex-like lexing behaviour where the rules are tested exhaustively to find the longest match)
            backtrack_lexer: boolean  (optional: true ==> lexer regexes are tested in order and for each matching regex the action code is invoked; the lexer terminates the scan when a token is returned by the action code)
        },

        performAction: function(yy, yy_, $avoiding_name_collisions, YY_START),
        rules: [...],
        conditions: {associative list: name ==> set},
    }
  }


  token location info (@$, _$, etc.): {
    first_line: n,
    last_line: n,
    first_column: n,
    last_column: n,
    range: [start_number, end_number]       (where the numbers are indexes into the input string, regular zero-based)
  }


  the parseError function receives a 'hash' object with these members for lexer and parser errors: {
    text:        (matched text)
    token:       (the produced terminal token, if any)
    line:        (yylineno)
  }
  while parser (grammar) errors will also provide these members, i.e. parser errors deliver a superset of attributes: {
    loc:         (yylloc)
    expected:    (string describing the set of expected tokens)
    recoverable: (boolean: TRUE when the parser has a error recovery rule available for this particular error)
  }
*/
var Analizador = (function(){
var o=function(k,v,o,l){for(o=o||{},l=k.length;l--;o[k[l]]=v);return o},$V0=[1,13],$V1=[1,12],$V2=[1,9],$V3=[1,10],$V4=[1,11],$V5=[5,12,15,20,23,25],$V6=[1,17],$V7=[1,18],$V8=[1,28],$V9=[1,29],$Va=[22,24,27,34,36,38,39];
var parser = {trace: function trace () { },
yy: {},
symbols_: {"error":2,"S":3,"LIST_BLOCK":4,"EOF":5,"BLOCK":6,"MENSAJE":7,"DATA":8,"ERROR":9,"LOGOUT":10,"LOGIN":11,"res_loginOpen":12,"STATUS":13,"res_loginClose":14,"res_logoutOpen":15,"STATUS2":16,"res_logoutClose":17,"res_success":18,"res_fail":19,"res_messageOpen":20,"T1":21,"res_messageClose":22,"res_dataOpen":23,"res_dataClose":24,"res_errorOpen":25,"res_lexemaOpen":26,"res_lexemaClose":27,"res_lineOpen":28,"numero":29,"res_lineClose":30,"res_columnOpen":31,"res_columnClose":32,"res_typeOpen":33,"res_typeClose":34,"res_descripcionOpen":35,"res_descripcionClose":36,"res_errorClose":37,"TODO":38,"WS":39,"$accept":0,"$end":1},
terminals_: {2:"error",5:"EOF",12:"res_loginOpen",14:"res_loginClose",15:"res_logoutOpen",17:"res_logoutClose",18:"res_success",19:"res_fail",20:"res_messageOpen",22:"res_messageClose",23:"res_dataOpen",24:"res_dataClose",25:"res_errorOpen",26:"res_lexemaOpen",27:"res_lexemaClose",28:"res_lineOpen",29:"numero",30:"res_lineClose",31:"res_columnOpen",32:"res_columnClose",33:"res_typeOpen",34:"res_typeClose",35:"res_descripcionOpen",36:"res_descripcionClose",37:"res_errorClose",38:"TODO",39:"WS"},
productions_: [0,[3,2],[4,2],[4,1],[6,1],[6,1],[6,1],[6,1],[6,1],[11,3],[10,3],[16,1],[16,1],[13,1],[13,1],[7,3],[8,3],[9,17],[21,2],[21,2],[21,1],[21,1]],
performAction: function anonymous(yytext, yyleng, yylineno, yy, yystate /* action[1] */, $$ /* vstack */, _$ /* lstack */) {
/* this == yyval */

var $0 = $$.length - 1;
switch (yystate) {
case 1:

	return ast;

break;
case 4:
ast.mensajes[ast.contMess++] = $$[$0];
break;
case 5:
ast.data[ast.contData++] = $$[$0];
break;
case 6:
ast.errores[ast.contErr++] = $$[$0];
break;
case 11:
ast.logout = true;
break;
case 12:
ast.logout = false;
break;
case 13:
ast.login = true;
break;
case 14:
ast.login = false;
break;
case 15:
this.$ = $$[$0-1];
break;
case 16:
this.$=$$[$0-1];
break;
case 17:
var error = new TokenError(); error.lexema = $$[$0-14]; error.fila = $$[$0-11]; error.columna=$$[$0-8]; 
			error.tipo = $$[$0-5]; error.descripcion = $$[$0-2];
			this.$ = error;
break;
case 18: case 19:
this.$ = $$[$0-1]+$$[$0];
break;
case 20: case 21:
this.$=$$[$0];
break;
}
},
table: [{3:1,4:2,6:3,7:4,8:5,9:6,10:7,11:8,12:$V0,15:$V1,20:$V2,23:$V3,25:$V4},{1:[3]},{5:[1,14],6:15,7:4,8:5,9:6,10:7,11:8,12:$V0,15:$V1,20:$V2,23:$V3,25:$V4},o($V5,[2,3]),o($V5,[2,4]),o($V5,[2,5]),o($V5,[2,6]),o($V5,[2,7]),o($V5,[2,8]),{21:16,38:$V6,39:$V7},{21:19,38:$V6,39:$V7},{26:[1,20]},{16:21,18:[1,22],19:[1,23]},{13:24,18:[1,25],19:[1,26]},{1:[2,1]},o($V5,[2,2]),{22:[1,27],38:$V8,39:$V9},o($Va,[2,20]),o($Va,[2,21]),{24:[1,30],38:$V8,39:$V9},{21:31,38:$V6,39:$V7},{17:[1,32]},{17:[2,11]},{17:[2,12]},{14:[1,33]},{14:[2,13]},{14:[2,14]},o($V5,[2,15]),o($Va,[2,18]),o($Va,[2,19]),o($V5,[2,16]),{27:[1,34],38:$V8,39:$V9},o($V5,[2,10]),o($V5,[2,9]),{28:[1,35]},{29:[1,36]},{30:[1,37]},{31:[1,38]},{29:[1,39]},{32:[1,40]},{33:[1,41]},{21:42,38:$V6,39:$V7},{34:[1,43],38:$V8,39:$V9},{35:[1,44]},{21:45,38:$V6,39:$V7},{36:[1,46],38:$V8,39:$V9},{37:[1,47]},o($V5,[2,17])],
defaultActions: {14:[2,1],22:[2,11],23:[2,12],25:[2,13],26:[2,14]},
parseError: function parseError (str, hash) {
    if (hash.recoverable) {
        this.trace(str);
    } else {
        var error = new Error(str);
        error.hash = hash;
        throw error;
    }
},
parse: function parse(input) {
    var self = this, stack = [0], tstack = [], vstack = [null], lstack = [], table = this.table, yytext = '', yylineno = 0, yyleng = 0, recovering = 0, TERROR = 2, EOF = 1;
    var args = lstack.slice.call(arguments, 1);
    var lexer = Object.create(this.lexer);
    var sharedState = { yy: {} };
    for (var k in this.yy) {
        if (Object.prototype.hasOwnProperty.call(this.yy, k)) {
            sharedState.yy[k] = this.yy[k];
        }
    }
    lexer.setInput(input, sharedState.yy);
    sharedState.yy.lexer = lexer;
    sharedState.yy.parser = this;
    if (typeof lexer.yylloc == 'undefined') {
        lexer.yylloc = {};
    }
    var yyloc = lexer.yylloc;
    lstack.push(yyloc);
    var ranges = lexer.options && lexer.options.ranges;
    if (typeof sharedState.yy.parseError === 'function') {
        this.parseError = sharedState.yy.parseError;
    } else {
        this.parseError = Object.getPrototypeOf(this).parseError;
    }
    function popStack(n) {
        stack.length = stack.length - 2 * n;
        vstack.length = vstack.length - n;
        lstack.length = lstack.length - n;
    }
    _token_stack:
        var lex = function () {
            var token;
            token = lexer.lex() || EOF;
            if (typeof token !== 'number') {
                token = self.symbols_[token] || token;
            }
            return token;
        };
    var symbol, preErrorSymbol, state, action, a, r, yyval = {}, p, len, newState, expected;
    while (true) {
        state = stack[stack.length - 1];
        if (this.defaultActions[state]) {
            action = this.defaultActions[state];
        } else {
            if (symbol === null || typeof symbol == 'undefined') {
                symbol = lex();
            }
            action = table[state] && table[state][symbol];
        }
                    if (typeof action === 'undefined' || !action.length || !action[0]) {
                var errStr = '';
                expected = [];
                for (p in table[state]) {
                    if (this.terminals_[p] && p > TERROR) {
                        expected.push('\'' + this.terminals_[p] + '\'');
                    }
                }
                if (lexer.showPosition) {
                    errStr = 'Parse error on line ' + (yylineno + 1) + ':\n' + lexer.showPosition() + '\nExpecting ' + expected.join(', ') + ', got \'' + (this.terminals_[symbol] || symbol) + '\'';
                } else {
                    errStr = 'Parse error on line ' + (yylineno + 1) + ': Unexpected ' + (symbol == EOF ? 'end of input' : '\'' + (this.terminals_[symbol] || symbol) + '\'');
                }
                this.parseError(errStr, {
                    text: lexer.match,
                    token: this.terminals_[symbol] || symbol,
                    line: lexer.yylineno,
                    loc: yyloc,
                    expected: expected
                });
            }
        if (action[0] instanceof Array && action.length > 1) {
            throw new Error('Parse Error: multiple actions possible at state: ' + state + ', token: ' + symbol);
        }
        switch (action[0]) {
        case 1:
            stack.push(symbol);
            vstack.push(lexer.yytext);
            lstack.push(lexer.yylloc);
            stack.push(action[1]);
            symbol = null;
            if (!preErrorSymbol) {
                yyleng = lexer.yyleng;
                yytext = lexer.yytext;
                yylineno = lexer.yylineno;
                yyloc = lexer.yylloc;
                if (recovering > 0) {
                    recovering--;
                }
            } else {
                symbol = preErrorSymbol;
                preErrorSymbol = null;
            }
            break;
        case 2:
            len = this.productions_[action[1]][1];
            yyval.$ = vstack[vstack.length - len];
            yyval._$ = {
                first_line: lstack[lstack.length - (len || 1)].first_line,
                last_line: lstack[lstack.length - 1].last_line,
                first_column: lstack[lstack.length - (len || 1)].first_column,
                last_column: lstack[lstack.length - 1].last_column
            };
            if (ranges) {
                yyval._$.range = [
                    lstack[lstack.length - (len || 1)].range[0],
                    lstack[lstack.length - 1].range[1]
                ];
            }
            r = this.performAction.apply(yyval, [
                yytext,
                yyleng,
                yylineno,
                sharedState.yy,
                action[1],
                vstack,
                lstack
            ].concat(args));
            if (typeof r !== 'undefined') {
                return r;
            }
            if (len) {
                stack = stack.slice(0, -1 * len * 2);
                vstack = vstack.slice(0, -1 * len);
                lstack = lstack.slice(0, -1 * len);
            }
            stack.push(this.productions_[action[1]][0]);
            vstack.push(yyval.$);
            lstack.push(yyval._$);
            newState = table[stack[stack.length - 2]][stack[stack.length - 1]];
            stack.push(newState);
            break;
        case 3:
            return true;
        }
    }
    return true;
}};

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
	    this.dbms = {};
    }

    var ast = new AST_LUP();
/* generated by jison-lex 0.3.4 */
var lexer = (function(){
var lexer = ({

EOF:1,

parseError:function parseError(str, hash) {
        if (this.yy.parser) {
            this.yy.parser.parseError(str, hash);
        } else {
            throw new Error(str);
        }
    },

// resets the lexer, sets new input
setInput:function (input, yy) {
        this.yy = yy || this.yy || {};
        this._input = input;
        this._more = this._backtrack = this.done = false;
        this.yylineno = this.yyleng = 0;
        this.yytext = this.matched = this.match = '';
        this.conditionStack = ['INITIAL'];
        this.yylloc = {
            first_line: 1,
            first_column: 0,
            last_line: 1,
            last_column: 0
        };
        if (this.options.ranges) {
            this.yylloc.range = [0,0];
        }
        this.offset = 0;
        return this;
    },

// consumes and returns one char from the input
input:function () {
        var ch = this._input[0];
        this.yytext += ch;
        this.yyleng++;
        this.offset++;
        this.match += ch;
        this.matched += ch;
        var lines = ch.match(/(?:\r\n?|\n).*/g);
        if (lines) {
            this.yylineno++;
            this.yylloc.last_line++;
        } else {
            this.yylloc.last_column++;
        }
        if (this.options.ranges) {
            this.yylloc.range[1]++;
        }

        this._input = this._input.slice(1);
        return ch;
    },

// unshifts one char (or a string) into the input
unput:function (ch) {
        var len = ch.length;
        var lines = ch.split(/(?:\r\n?|\n)/g);

        this._input = ch + this._input;
        this.yytext = this.yytext.substr(0, this.yytext.length - len);
        //this.yyleng -= len;
        this.offset -= len;
        var oldLines = this.match.split(/(?:\r\n?|\n)/g);
        this.match = this.match.substr(0, this.match.length - 1);
        this.matched = this.matched.substr(0, this.matched.length - 1);

        if (lines.length - 1) {
            this.yylineno -= lines.length - 1;
        }
        var r = this.yylloc.range;

        this.yylloc = {
            first_line: this.yylloc.first_line,
            last_line: this.yylineno + 1,
            first_column: this.yylloc.first_column,
            last_column: lines ?
                (lines.length === oldLines.length ? this.yylloc.first_column : 0)
                 + oldLines[oldLines.length - lines.length].length - lines[0].length :
              this.yylloc.first_column - len
        };

        if (this.options.ranges) {
            this.yylloc.range = [r[0], r[0] + this.yyleng - len];
        }
        this.yyleng = this.yytext.length;
        return this;
    },

// When called from action, caches matched text and appends it on next action
more:function () {
        this._more = true;
        return this;
    },

// When called from action, signals the lexer that this rule fails to match the input, so the next matching rule (regex) should be tested instead.
reject:function () {
        if (this.options.backtrack_lexer) {
            this._backtrack = true;
        } else {
            return this.parseError('Lexical error on line ' + (this.yylineno + 1) + '. You can only invoke reject() in the lexer when the lexer is of the backtracking persuasion (options.backtrack_lexer = true).\n' + this.showPosition(), {
                text: "",
                token: null,
                line: this.yylineno
            });

        }
        return this;
    },

// retain first n characters of the match
less:function (n) {
        this.unput(this.match.slice(n));
    },

// displays already matched input, i.e. for error messages
pastInput:function () {
        var past = this.matched.substr(0, this.matched.length - this.match.length);
        return (past.length > 20 ? '...':'') + past.substr(-20).replace(/\n/g, "");
    },

// displays upcoming input, i.e. for error messages
upcomingInput:function () {
        var next = this.match;
        if (next.length < 20) {
            next += this._input.substr(0, 20-next.length);
        }
        return (next.substr(0,20) + (next.length > 20 ? '...' : '')).replace(/\n/g, "");
    },

// displays the character position where the lexing error occurred, i.e. for error messages
showPosition:function () {
        var pre = this.pastInput();
        var c = new Array(pre.length + 1).join("-");
        return pre + this.upcomingInput() + "\n" + c + "^";
    },

// test the lexed token: return FALSE when not a match, otherwise return token
test_match:function(match, indexed_rule) {
        var token,
            lines,
            backup;

        if (this.options.backtrack_lexer) {
            // save context
            backup = {
                yylineno: this.yylineno,
                yylloc: {
                    first_line: this.yylloc.first_line,
                    last_line: this.last_line,
                    first_column: this.yylloc.first_column,
                    last_column: this.yylloc.last_column
                },
                yytext: this.yytext,
                match: this.match,
                matches: this.matches,
                matched: this.matched,
                yyleng: this.yyleng,
                offset: this.offset,
                _more: this._more,
                _input: this._input,
                yy: this.yy,
                conditionStack: this.conditionStack.slice(0),
                done: this.done
            };
            if (this.options.ranges) {
                backup.yylloc.range = this.yylloc.range.slice(0);
            }
        }

        lines = match[0].match(/(?:\r\n?|\n).*/g);
        if (lines) {
            this.yylineno += lines.length;
        }
        this.yylloc = {
            first_line: this.yylloc.last_line,
            last_line: this.yylineno + 1,
            first_column: this.yylloc.last_column,
            last_column: lines ?
                         lines[lines.length - 1].length - lines[lines.length - 1].match(/\r?\n?/)[0].length :
                         this.yylloc.last_column + match[0].length
        };
        this.yytext += match[0];
        this.match += match[0];
        this.matches = match;
        this.yyleng = this.yytext.length;
        if (this.options.ranges) {
            this.yylloc.range = [this.offset, this.offset += this.yyleng];
        }
        this._more = false;
        this._backtrack = false;
        this._input = this._input.slice(match[0].length);
        this.matched += match[0];
        token = this.performAction.call(this, this.yy, this, indexed_rule, this.conditionStack[this.conditionStack.length - 1]);
        if (this.done && this._input) {
            this.done = false;
        }
        if (token) {
            return token;
        } else if (this._backtrack) {
            // recover context
            for (var k in backup) {
                this[k] = backup[k];
            }
            return false; // rule action called reject() implying the next rule should be tested instead.
        }
        return false;
    },

// return next match in input
next:function () {
        if (this.done) {
            return this.EOF;
        }
        if (!this._input) {
            this.done = true;
        }

        var token,
            match,
            tempMatch,
            index;
        if (!this._more) {
            this.yytext = '';
            this.match = '';
        }
        var rules = this._currentRules();
        for (var i = 0; i < rules.length; i++) {
            tempMatch = this._input.match(this.rules[rules[i]]);
            if (tempMatch && (!match || tempMatch[0].length > match[0].length)) {
                match = tempMatch;
                index = i;
                if (this.options.backtrack_lexer) {
                    token = this.test_match(tempMatch, rules[i]);
                    if (token !== false) {
                        return token;
                    } else if (this._backtrack) {
                        match = false;
                        continue; // rule action called reject() implying a rule MISmatch.
                    } else {
                        // else: this is a lexer rule which consumes input without producing a token (e.g. whitespace)
                        return false;
                    }
                } else if (!this.options.flex) {
                    break;
                }
            }
        }
        if (match) {
            token = this.test_match(match, rules[index]);
            if (token !== false) {
                return token;
            }
            // else: this is a lexer rule which consumes input without producing a token (e.g. whitespace)
            return false;
        }
        if (this._input === "") {
            return this.EOF;
        } else {
            return this.parseError('Lexical error on line ' + (this.yylineno + 1) + '. Unrecognized text.\n' + this.showPosition(), {
                text: "",
                token: null,
                line: this.yylineno
            });
        }
    },

// return next match that has a token
lex:function lex () {
        var r = this.next();
        if (r) {
            return r;
        } else {
            return this.lex();
        }
    },

// activates a new lexer condition state (pushes the new lexer condition state onto the condition stack)
begin:function begin (condition) {
        this.conditionStack.push(condition);
    },

// pop the previously active lexer condition state off the condition stack
popState:function popState () {
        var n = this.conditionStack.length - 1;
        if (n > 0) {
            return this.conditionStack.pop();
        } else {
            return this.conditionStack[0];
        }
    },

// produce the lexer rule set which is active for the currently active lexer condition state
_currentRules:function _currentRules () {
        if (this.conditionStack.length && this.conditionStack[this.conditionStack.length - 1]) {
            return this.conditions[this.conditionStack[this.conditionStack.length - 1]].rules;
        } else {
            return this.conditions["INITIAL"].rules;
        }
    },

// return the currently active lexer condition state; when an index argument is provided it produces the N-th previous condition state, if available
topState:function topState (n) {
        n = this.conditionStack.length - 1 - Math.abs(n || 0);
        if (n >= 0) {
            return this.conditionStack[n];
        } else {
            return "INITIAL";
        }
    },

// alias for begin(condition)
pushState:function pushState (condition) {
        this.begin(condition);
    },

// return the number of states currently on the stack
stateStackSize:function stateStackSize() {
        return this.conditionStack.length;
    },
options: {"case-insensitive":true},
performAction: function anonymous(yy,yy_,$avoiding_name_collisions,YY_START) {


var YYSTATE=YY_START;
switch($avoiding_name_collisions) {
case 0:this.begin("res_message"); return 20
break;
case 1:this.begin('INITIAL');  return 22;
break;
case 2:return 38
break;
case 3:return 39
break;
case 4:this.begin("res_lexema"); return 26
break;
case 5:this.begin('INITIAL');  return 27;
break;
case 6:return 38
break;
case 7:return 39
break;
case 8:this.begin("res_descripcion"); return 35
break;
case 9:this.begin('INITIAL');  return 36;
break;
case 10:return 38
break;
case 11:return 39
break;
case 12:this.begin("res_type"); return 33
break;
case 13:this.begin('INITIAL');  return 34;
break;
case 14:return 38
break;
case 15:return 39
break;
case 16:this.begin("res_data"); return 23
break;
case 17:this.begin('INITIAL');  return 24;
break;
case 18:return 38
break;
case 19:return 39
break;
case 20:/* skip whitespace */
break;
case 21:return 25;
break;
case 22:return 37;
break;
case 23:return 28;
break;
case 24:return 30;
break;
case 25:return 31;
break;
case 26:return 32;
break;
case 27:return 12;
break;
case 28:return 14;
break;
case 29:return 15;
break;
case 30:return 17;
break;
case 31:return 18;
break;
case 32:return 19;
break;
case 33:return 29;
break;
case 34:return 'id';
break;
case 35:return 5;
break;
case 36: console.error('Este es un error léxico: ' + yy_.yytext + ', en la linea: ' + yy_.yylloc.first_line + ', en la columna: ' + yy_.yylloc.first_column); 
break;
}
},
rules: [/^(?:\[\+MESSAGE\])/i,/^(?:\[-MESSAGE\])/i,/^(?:.)/i,/^(?:[\t\r\n\f])/i,/^(?:\[\+LEXEMA\])/i,/^(?:\[-LEXEMA\])/i,/^(?:.)/i,/^(?:[\t\r\n\f])/i,/^(?:\[\+DESC\])/i,/^(?:\[-DESC\])/i,/^(?:.)/i,/^(?:[\t\r\n\f])/i,/^(?:\[\+TYPE\])/i,/^(?:\[-TYPE\])/i,/^(?:.)/i,/^(?:[\t\r\n\f])/i,/^(?:\[\+DATA\])/i,/^(?:\[-DATA\])/i,/^(?:.)/i,/^(?:[\t\r\n\f])/i,/^(?:\s+)/i,/^(?:\[\+ERROR\])/i,/^(?:\[-ERROR\])/i,/^(?:\[\+LINE\])/i,/^(?:\[-LINE\])/i,/^(?:\[\+COLUMN\])/i,/^(?:\[-COLUMN\])/i,/^(?:\[\+LOGIN\])/i,/^(?:\[-LOGIN\])/i,/^(?:\[\+LOGOUT\])/i,/^(?:\[-LOGOUT\])/i,/^(?:\[SUCCESS\])/i,/^(?:\[FAIL\])/i,/^(?:([0-9])+)/i,/^(?:(([A-Za-zÑñ])|_)(([A-Za-zÑñ])+|([0-9])*|_)*)/i,/^(?:$)/i,/^(?:.)/i],
conditions: {"res_data":{"rules":[0,4,8,12,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36],"inclusive":true},"res_type":{"rules":[0,4,8,12,13,14,15,16,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36],"inclusive":true},"res_descripcion":{"rules":[0,4,8,9,10,11,12,16,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36],"inclusive":true},"res_lexema":{"rules":[0,4,5,6,7,8,12,16,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36],"inclusive":true},"res_message":{"rules":[0,1,2,3,4,8,12,16,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36],"inclusive":true},"INITIAL":{"rules":[0,4,8,12,16,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36],"inclusive":true}}
});
return lexer;
})();
parser.lexer = lexer;
function Parser () {
  this.yy = {};
}
Parser.prototype = parser;parser.Parser = Parser;
return new Parser;
})();


if (typeof require !== 'undefined' && typeof exports !== 'undefined') {
exports.parser = Analizador;
exports.Parser = Analizador.Parser;
exports.parse = function () { return Analizador.parse.apply(Analizador, arguments); };
exports.main = function commonjsMain (args) {
    if (!args[1]) {
        console.log('Usage: '+args[0]+' FILE');
        process.exit(1);
    }
    var source = require('fs').readFileSync(require('path').normalize(args[1]), "utf8");
    return exports.parser.parse(source);
};
if (typeof module !== 'undefined' && require.main === module) {
  exports.main(process.argv.slice(1));
}
}