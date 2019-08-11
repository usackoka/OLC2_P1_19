<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagBlockly.aspx.cs" Inherits="Proyecto1_2s19_201503712.pagBlockly" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../BlockLy/blockly_compressed.js"></script>
    <script src="../BlockLy/blocks_compressed.js"></script>
    <script src="../BlockLy/javascript_compressed.js"></script>
    <script src="../BlockLy/es.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="DivBlockly" style="height:400px;width:700px"></div>
        <input type="button" onclick="MostrarCodigo()" value="MostrarCodigo"/>
        <textarea id="txtSalida"></textarea>
        <xml id="toolbox" style="display: none">
            <block type="controls_if"></block>
            <block type="controls_repeat_ext"></block>
            <block type="logic_compare"></block>
            <block type="math_number"></block>
            <block type="math_arithmetic"></block>
            <block type="text"></block>
            <block type="text_print"></block>
        </xml>
        <script>
            var EspacioTrabajo = Blockly.inject('DivBlockly', { toolbox: document.getElementById('toolbox') });
            function MostrarCodigo() {
                var codigo = Blockly.JavaScript.workspaceToCode(EspacioTrabajo);
                document.getElementById('txtSalida').innerHTML = codigo;
            }
            </script>
    </form>
</body>
</html>
