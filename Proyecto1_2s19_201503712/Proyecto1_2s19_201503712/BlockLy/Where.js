Blockly.JavaScript['where'] = function(block) {
  var value_condicion = Blockly.JavaScript.valueToCode(block, 'Condicion', Blockly.JavaScript.ORDER_ATOMIC);
  // TODO: Assemble JavaScript into code variable.
  var code = '...';
  // TODO: Change ORDER_NONE to the correct strength.
  return [code, Blockly.JavaScript.ORDER_NONE];
};

Blockly.Blocks['where'] = {
  init: function() {
    this.appendValueInput("Condicion")
        .setCheck("Boolean")
        .setAlign(Blockly.ALIGN_CENTRE);
    this.setOutput(true, "String");
    this.setColour(230);
 this.setTooltip("Where");
 this.setHelpUrl("");
  }
};