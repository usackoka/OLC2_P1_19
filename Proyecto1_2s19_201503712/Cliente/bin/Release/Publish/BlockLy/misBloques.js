//====================== CODIGO HTML =====================================
/*
            <category name="Otros">
                <block type="controls_repeat_ext"></block>
                <block type="logic_compare"></block>
                <block type="math_number"></block>
                <block type="math_arithmetic"></block>
                <block type="text"></block>
                <block type="text_print"></block>
            </category>
*/

//====================== DEFINICIONES =====================================
//========================= SELECT ========================================
Blockly.Blocks['where'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Where")
        .appendField(new Blockly.FieldTextInput("expresion"), "expresion");
    this.setOutput(true, "String");
    this.setColour(230);
 this.setTooltip("Where");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['select'] = {
  init: function() {
    this.appendValueInput("Select")
        .setCheck(null)
        .appendField("Select");
    this.appendDummyInput()
        .appendField("From")
        .appendField(new Blockly.FieldTextInput("Tabla"), "tabla");
    this.appendValueInput("Where")
        .setCheck(null)
        .appendField("Where");
    this.appendValueInput("OrderBy")
        .setCheck(null)
        .appendField("OrderBy");
    this.appendValueInput("Limit")
        .setCheck("Number")
        .appendField("Limit");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(345);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['seleccion'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Seleccion")
        .appendField(new Blockly.FieldTextInput("*"), "expresion");
    this.setOutput(true, null);
    this.setColour(45);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['limit'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Limit")
        .appendField(new Blockly.FieldNumber(10, 0), "limit");
    this.setOutput(true, null);
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['orderby'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("OrderBy")
        .appendField(new Blockly.FieldTextInput("expresion"), "orderBy");
    this.setOutput(true, null);
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['insert'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Insert Into")
        .appendField(new Blockly.FieldTextInput("tabla"), "tabla");
    this.appendDummyInput()
        .appendField("campos")
        .appendField(new Blockly.FieldTextInput(""), "campos");
    this.appendValueInput("NAME")
        .setCheck("Array")
        .appendField("Values");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(20);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['update'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Update")
        .appendField(new Blockly.FieldTextInput("tabla"), "tabla");
    this.appendDummyInput()
        .appendField("Set")
        .appendField(new Blockly.FieldTextInput("expresionSet"), "expresionSet");
    this.appendValueInput("Where")
        .setCheck(null)
        .appendField("Where");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['delete'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Delete")
        .appendField(new Blockly.FieldTextInput("expresion"), "expresion")
        .appendField("From")
        .appendField(new Blockly.FieldTextInput("tabla"), "tabla");
    this.appendValueInput("Where")
        .setCheck(null)
        .appendField("Where");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['where_2'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Where");
    this.appendValueInput("where")
        .setCheck(null);
    this.setOutput(true, null);
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['use'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Use")
        .appendField(new Blockly.FieldTextInput("DataBase"), "dataBase");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(60);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['reasignar'] = {
  init: function() {
    this.appendValueInput("expresion")
        .setCheck(null)
        .appendField("Reasignar")
        .appendField(new Blockly.FieldTextInput("@id"), "var")
        .appendField(new Blockly.FieldDropdown([["="," = "], ["+="," += "], ["-="," -= "]]), "asignacion");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['var'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Variable")
        .appendField(new Blockly.FieldTextInput("@id"), "variable");
    this.setOutput(true, null);
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['create_var'] = {
  init: function() {
    this.appendValueInput("identifier")
        .setCheck("var")
        .appendField("Tipo")
        .appendField(new Blockly.FieldDropdown([["INT","int"], ["STRING","string"], ["DOUBLE","double"], ["DATE","date"], ["TIME","time"], ["BOOLEAN","boolean"], ["CURSOR","cursor"]]), "tipo")
        .appendField("Id");
    this.appendValueInput("ValorInstancia")
        .setCheck("valinstancia")
        .appendField("Valor Instancia");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['string'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("String")
        .appendField(new Blockly.FieldTextInput("string"), "string");
    this.setOutput(true, null);
    this.setColour(65);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['date_time'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Date-Time")
        .appendField(new Blockly.FieldTextInput("formato"), "string");
    this.setOutput(true, null);
    this.setColour(290);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['booleano'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Booleano")
        .appendField(new Blockly.FieldDropdown([["true","TRUE"], ["false","FALSE"]]), "true_false");
    this.setOutput(true, null);
    this.setColour(120);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['id'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("id")
        .appendField(new Blockly.FieldTextInput("id"), "id");
    this.setOutput(true, null);
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};
/////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////CODE GENERATOR///////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////
//=============================== CODIGO ===========================================================
//========================= SELECT ========================================
Blockly.JavaScript['use'] = function(block) {
  var text_database = block.getFieldValue('dataBase');
  // TODO: Assemble JavaScript into code variable.
  var code = 'USE '+text_database+';\n';
  return code;
};

Blockly.JavaScript['where'] = function(block) {
  var text_expresion = block.getFieldValue('expresion');
  // TODO: Assemble JavaScript into code variable.
  var code = ' WHERE '+text_expresion;
  // TODO: Change ORDER_NONE to the correct strength.
  return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.JavaScript['select'] = function(block) {
  var value_select = Blockly.JavaScript.valueToCode(block, 'Select', Blockly.JavaScript.ORDER_ATOMIC);
  var text_tabla = block.getFieldValue('tabla');
  var value_where = Blockly.JavaScript.valueToCode(block, 'Where', Blockly.JavaScript.ORDER_ATOMIC);
  var value_orderby = Blockly.JavaScript.valueToCode(block, 'OrderBy', Blockly.JavaScript.ORDER_ATOMIC);
  var value_limit = Blockly.JavaScript.valueToCode(block, 'Limit', Blockly.JavaScript.ORDER_ATOMIC);
  // TODO: Assemble JavaScript into code variable.
  var code = 'SELECT '+value_select.substring(1, value_select.length-1)+' FROM '+text_tabla+value_where.substring(1, value_where.length-1)+
  value_orderby.substring(1, value_orderby.length-1)+value_limit.substring(1, value_limit.length-1)+';\n';
  return code;
};

Blockly.JavaScript['seleccion'] = function(block) {
  var text_expresion = block.getFieldValue('expresion');
  // TODO: Assemble JavaScript into code variable.
  var code = text_expresion;
  // TODO: Change ORDER_NONE to the correct strength.
  return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.JavaScript['limit'] = function(block) {
  var number_limit = block.getFieldValue('limit');
  // TODO: Assemble JavaScript into code variable.
  var code = ' LIMIT '+number_limit;
  // TODO: Change ORDER_NONE to the correct strength.
  return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.JavaScript['orderby'] = function(block) {
  var text_orderby = block.getFieldValue('orderBy');
  // TODO: Assemble JavaScript into code variable.
  var code = ' ORDER BY '+text_orderby;
  // TODO: Change ORDER_NONE to the correct strength.
  return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.JavaScript['insert'] = function(block) {
  var text_tabla = block.getFieldValue('tabla');
  var value_campos = block.getFieldValue('campos');
  var value_name = Blockly.JavaScript.valueToCode(block, 'NAME', Blockly.JavaScript.ORDER_ATOMIC);
  // TODO: Assemble JavaScript into code variableW.
  if(value_campos.length!=0){
  	value_campos = " ("+value_campos+")";
  }
  var code = 'INSERT INTO '+text_tabla+value_campos+' VALUES ('+value_name.substring(1, value_name.length-1)+');\n';
  return code;
};

Blockly.JavaScript['update'] = function(block) {
  var text_tabla = block.getFieldValue('tabla');
  var text_expresionset = block.getFieldValue('expresionSet');
  var value_where = Blockly.JavaScript.valueToCode(block, 'Where', Blockly.JavaScript.ORDER_ATOMIC);
  // TODO: Assemble JavaScript into code variable.
  var code = 'UPDATE '+text_tabla+' SET '+text_expresionset+value_where.substring(1, value_where.length-1)+';\n';
  return code;
};

Blockly.JavaScript['delete'] = function(block) {
  var text_expresion = block.getFieldValue('expresion');
  var text_tabla = block.getFieldValue('tabla');
  var value_where = Blockly.JavaScript.valueToCode(block, 'Where', Blockly.JavaScript.ORDER_ATOMIC);
  // TODO: Assemble JavaScript into code variable.
  var code = 'DELETE '+text_expresion+' FROM '+text_tabla+value_where.substring(1, value_where.length-1)+';\n';
  return code;
};

Blockly.JavaScript['where_2'] = function(block) {
  var value_where = Blockly.JavaScript.valueToCode(block, 'where', Blockly.JavaScript.ORDER_ATOMIC);
  // TODO: Assemble JavaScript into code variable.
  var code = ' WHERE '+value_where;
  // TODO: Change ORDER_NONE to the correct strength.
  return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.JavaScript['reasignar'] = function(block) {
  var text_var = block.getFieldValue('var');
  var dropdown_asignacion = block.getFieldValue('asignacion');
  var value_expresion = Blockly.JavaScript.valueToCode(block, 'expresion', Blockly.JavaScript.ORDER_ATOMIC);
  // TODO: Assemble JavaScript into code variable.
  var code = text_var + dropdown_asignacion + value_expresion+';\n';
  return code;
};

Blockly.JavaScript['var'] = function(block) {
  var text_variable = block.getFieldValue('variable');
  // TODO: Assemble JavaScript into code variable.
  var code = text_variable;
  return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.JavaScript['create_var'] = function(block) {
  var dropdown_tipo = block.getFieldValue('tipo');
  var value_identifier = Blockly.JavaScript.valueToCode(block, 'identifier', Blockly.JavaScript.ORDER_ATOMIC);
  var value_valorinstancia = Blockly.JavaScript.valueToCode(block, 'ValorInstancia', Blockly.JavaScript.ORDER_ATOMIC);
  // TODO: Assemble JavaScript into code variable.
  var code = dropdown_tipo + ' '+value_identifier.substring(1, value_identifier.length-1)+' '+value_valorinstancia.substring(1, value_valorinstancia.length-1)+';\n';
  return code;
};

Blockly.JavaScript['string'] = function(block) {
  var text_string = block.getFieldValue('string');
  // TODO: Assemble JavaScript into code variable.
  var code = "\"" + text_string + "\"";
  // TODO: Change ORDER_NONE to the correct strength.
  return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.JavaScript['date_time'] = function(block) {
  var text_string = block.getFieldValue('string');
  // TODO: Assemble JavaScript into code variable.
  var code = "'" + text_string + "'";
  // TODO: Change ORDER_NONE to the correct strength.
  return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.JavaScript['booleano'] = function(block) {
  var dropdown_true_false = block.getFieldValue('true_false');
  // TODO: Assemble JavaScript into code variable.
  var code = dropdown_true_false;
  // TODO: Change ORDER_NONE to the correct strength.
  return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.JavaScript['id'] = function(block) {
  var text_id = block.getFieldValue('id');
  // TODO: Assemble JavaScript into code variable.
  var code = text_id;
  // TODO: Change ORDER_NONE to the correct strength.
  return [code, Blockly.JavaScript.ORDER_NONE];
};


/////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////MODO INTERMEDIO///////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////
Blockly.Blocks['if'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("If")
        .appendField(new Blockly.FieldTextInput("condicion"), "condicion");
    this.appendStatementInput("instrucciones")
        .setCheck(null);
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.JavaScript['if'] = function(block) {
  var text_condicion = block.getFieldValue('condicion');
  var statements_instrucciones = Blockly.JavaScript.statementToCode(block, 'instrucciones');
  // TODO: Assemble JavaScript into code variable.
  if(statements_instrucciones==undefined){
  	statements_instrucciones = "";
  }
  var code = ' If ('+text_condicion+'){\n'+statements_instrucciones+'\n}\n';
  return code;
};

Blockly.Blocks['else'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Else");
    this.appendStatementInput("instrucciones")
        .setCheck(null);
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.JavaScript['else'] = function(block) {
  var statements_instrucciones = Blockly.JavaScript.statementToCode(block, 'instrucciones');
  // TODO: Assemble JavaScript into code variable.
  if(statements_instrucciones==undefined){
  	statements_instrucciones = "";
  }
  var code = ' Else{\n'+statements_instrucciones+'\n}\n';
  return code;
};

Blockly.Blocks['igual'] = {
  init: function() {
    this.appendValueInput("NAME")
        .setCheck(null)
        .appendField("=");
    this.setOutput(true, null);
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.JavaScript['igual'] = function(block) {
  var value_name = Blockly.JavaScript.valueToCode(block, 'NAME', Blockly.JavaScript.ORDER_ATOMIC);
  // TODO: Assemble JavaScript into code variable.
  var code = ' = '+value_name;
  // TODO: Change ORDER_NONE to the correct strength.
  return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.Blocks['for'] = {
  init: function() {
    this.appendValueInput("Declaracion")
        .setCheck(null)
        .appendField("For")
        .appendField("Declaracion")
        .appendField(new Blockly.FieldDropdown([["int","int"], ["double","double"]]), "tipo")
        .appendField(new Blockly.FieldTextInput("@i"), "iterador")
        .appendField("=");
    this.appendDummyInput()
        .appendField("Condicion")
        .appendField(new Blockly.FieldTextInput("@i<10"), "condicion");
    this.appendValueInput("Actualizador")
        .setCheck(null)
        .appendField("Actualizacion")
        .appendField(new Blockly.FieldTextInput("@i"), "var")
        .appendField(new Blockly.FieldDropdown([["+=","+="], ["-=","-="], ["*=","*="]]), "actualizador");
    this.appendStatementInput("NAME")
        .setCheck(null);
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(65);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.JavaScript['for'] = function(block) {
  var dropdown_tipo = block.getFieldValue('tipo');
  var text_iterador = block.getFieldValue('iterador');
  var value_declaracion = Blockly.JavaScript.valueToCode(block, 'Declaracion', Blockly.JavaScript.ORDER_ATOMIC);
  var text_condicion = block.getFieldValue('condicion');
  var text_var = block.getFieldValue('var');
  var dropdown_actualizador = block.getFieldValue('actualizador');
  var value_actualizador = Blockly.JavaScript.valueToCode(block, 'Actualizador', Blockly.JavaScript.ORDER_ATOMIC);
  var statements_name = Blockly.JavaScript.statementToCode(block, 'NAME');
  // TODO: Assemble JavaScript into code variable.
  if(statements_name==undefined){
  	statements_name = "";
  }
  //var code = ';';
  var code = ' For ('+dropdown_tipo+' '+text_iterador+' = '+value_declaracion+'; '+text_condicion+'; '+
  			text_var+dropdown_actualizador+value_actualizador+'){\n'+statements_name+'\n}\n';
  return code;
};

Blockly.Blocks['llamadafuncion'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("LlamadaFuncion");
    this.appendDummyInput()
        .appendField("id")
        .appendField(new Blockly.FieldTextInput("function"), "function");
    this.appendValueInput("NAME")
        .setCheck("Array")
        .appendField("Parametros");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.JavaScript['llamadafuncion'] = function(block) {
  var text_function = block.getFieldValue('function');
  var value_name = Blockly.JavaScript.valueToCode(block, 'NAME', Blockly.JavaScript.ORDER_ATOMIC);
  if(value_name==undefined){
  	value_name = "()";
  }
  // TODO: Assemble JavaScript into code variable.
  var code = text_function+value_name+';\n';
  return code;
};

Blockly.Blocks['call'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Call");
    this.appendDummyInput()
        .appendField("id")
        .appendField(new Blockly.FieldTextInput("function"), "function");
    this.appendValueInput("NAME")
        .setCheck("Array")
        .appendField("Parametros");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(20);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.JavaScript['call'] = function(block) {
  var text_function = block.getFieldValue('function');
  var value_name = Blockly.JavaScript.valueToCode(block, 'NAME', Blockly.JavaScript.ORDER_ATOMIC);
  if(value_name==undefined){
  	value_name = "()";
  }
  // TODO: Assemble JavaScript into code variable.
  var code = "call "+text_function+value_name+';\n';
  return code;
};

Blockly.Blocks['log'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("LOG")
        .appendField(new Blockly.FieldTextInput("expresion"), "expresion");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(120);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.JavaScript['log'] = function(block) {
  var text_expresion = block.getFieldValue('expresion');
  // TODO: Assemble JavaScript into code variable.
  var code = 'LOG('+text_expresion+');\n';
  return code;
};

Blockly.Blocks['switch'] = {
  init: function() {
    this.appendValueInput("valor")
        .setCheck(null)
        .appendField("Switch");
    this.appendStatementInput("NAME")
        .setCheck(null);
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(75);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.JavaScript['switch'] = function(block) {
  var value_valor = Blockly.JavaScript.valueToCode(block, 'valor', Blockly.JavaScript.ORDER_ATOMIC);
  var statements_name = Blockly.JavaScript.statementToCode(block, 'NAME');
  // TODO: Assemble JavaScript into code variable.
  var code = 'switch ('+value_valor+'){\n'+statements_name+'\n}\n';
  return code;
};


Blockly.Blocks['case'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Case")
        .appendField(new Blockly.FieldTextInput("expresion"), "expresion");
    this.appendStatementInput("NAME")
        .setCheck(null);
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.JavaScript['case'] = function(block) {
  var text_expresion = block.getFieldValue('expresion');
  var statements_name = Blockly.JavaScript.statementToCode(block, 'NAME');
  // TODO: Assemble JavaScript into code variable.
  var code = 'case '+text_expresion+':\n'+statements_name+'\n';
  return code;
};

Blockly.Blocks['break'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Break");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.JavaScript['break'] = function(block) {
  // TODO: Assemble JavaScript into code variable.
  var code = 'break;\n';
  return code;
};

Blockly.Blocks['default'] = {
  init: function() {
    this.appendValueInput("default")
        .setCheck(null)
        .appendField("default");
    this.appendStatementInput("NAME")
        .setCheck(null);
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.JavaScript['default'] = function(block) {
  var value_default = Blockly.JavaScript.valueToCode(block, 'default', Blockly.JavaScript.ORDER_ATOMIC);
  var statements_name = Blockly.JavaScript.statementToCode(block, 'NAME');
  // TODO: Assemble JavaScript into code variable.
  var code = 'default: \n'+statements_name+'\n';
  return code;
};

Blockly.Blocks['while'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("While")
        .appendField(new Blockly.FieldTextInput("expresion"), "expresion");
    this.appendStatementInput("NAME")
        .setCheck(null);
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.JavaScript['while'] = function(block) {
  var text_expresion = block.getFieldValue('expresion');
  var statements_name = Blockly.JavaScript.statementToCode(block, 'NAME');
  // TODO: Assemble JavaScript into code variable.
  var code = 'while('+text_expresion+'){\n'+statements_name+'}\n';
  return code;
};

Blockly.Blocks['do'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("do");
    this.appendStatementInput("NAME")
        .setCheck(null);
    this.appendDummyInput()
        .appendField("While")
        .appendField(new Blockly.FieldTextInput("expresion"), "expresion");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.JavaScript['do'] = function(block) {
  var statements_name = Blockly.JavaScript.statementToCode(block, 'NAME');
  var text_expresion = block.getFieldValue('expresion');
  // TODO: Assemble JavaScript into code variable.
  var code = 'do{\n'+statements_name+'\n}while('+text_expresion+');\n';
  return code;
};