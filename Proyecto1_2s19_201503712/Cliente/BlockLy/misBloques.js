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
        .appendField("Insert");
    this.appendDummyInput()
        .appendField("Into")
        .appendField(new Blockly.FieldTextInput("tabla"), "tabla");
    this.appendValueInput("campos")
        .setCheck("Array")
        .appendField("Campos");
    this.appendValueInput("NAME")
        .setCheck("Array")
        .appendField("Values");
    this.setColour(0);
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
        .appendField(new Blockly.FieldDropdown([["=","igual"], ["+=","mas_igual"], ["-=","menos_igual"]]), "asignacion");
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
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['valinstancia'] = {
  init: function() {
    this.appendValueInput("expresion")
        .setCheck(null)
        .appendField("=");
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
  var value_campos = Blockly.JavaScript.valueToCode(block, 'campos', Blockly.JavaScript.ORDER_ATOMIC);
  var value_name = Blockly.JavaScript.valueToCode(block, 'NAME', Blockly.JavaScript.ORDER_ATOMIC);
  // TODO: Assemble JavaScript into code variable.
  if(value_campos.length!=0){
  	value_campos = value_campos.substring(1, value_campos.length-1);
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
  var code = dropdown_tipo + ' '+value_identifier+' '+value_valorinstancia+';\n';
  return code;
};

Blockly.JavaScript['valinstancia'] = function(block) {
  var value_expresion = Blockly.JavaScript.valueToCode(block, 'expresion', Blockly.JavaScript.ORDER_ATOMIC);
  // TODO: Assemble JavaScript into code variable.
  var code = value_expresion;
  // TODO: Change ORDER_NONE to the correct strength.
  return [code, Blockly.JavaScript.ORDER_NONE];
};