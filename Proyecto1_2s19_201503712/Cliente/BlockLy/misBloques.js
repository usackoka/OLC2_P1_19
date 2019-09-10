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
  var code = 'INSERT INTO '+text_tabla+value_campos+' VALUES ('+value_name+');\n';
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