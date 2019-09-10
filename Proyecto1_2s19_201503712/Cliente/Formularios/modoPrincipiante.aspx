<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="modoPrincipiante.aspx.cs" Inherits="Cliente.Formularios.modoPrincipiante" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="../BlockLy/blockly_compressed.js"></script>
    <script src="../BlockLy/blocks_compressed.js"></script>
    <script src="../BlockLy/javascript_compressed.js"></script>
    <script src="../BlockLy/misBloques.js"></script>
    <script src="../BlockLy/es.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="DivBlockly" style="height:400px;width:1400px"></div>
        <input type="button" onclick="MostrarCodigo()" value="MostrarCodigo"/>
        <textarea id="txtSalida"></textarea>
        <xml id="toolbox" style="display: none">
             <category name="Sentencias">
                <block type="select"></block>
                <block type="where"></block>
                <block type="where_2"></block>
                <block type="orderby"></block>
                <block type="limit"></block>
                <block type="insert"></block>
                <block type="update"></block>
                <block type="delete"></block>
             </category>
             <category name="Atributos">
                <block type="text"></block>
                <block type="seleccion"></block>
             </category>
             <category name="Valores">
                <block type="text"></block>
                <block type="lists_create_with"></block>
                <block type="math_number"></block>
            </category>
            <category name="Operadores">
                <block type="logic_compare"></block>
                <block type="math_arithmetic"></block>
            </category>
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
