<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="modoAvanzado.aspx.cs" Inherits="Cliente.Formularios.modoAvanzado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <%-- ///////////////////// EDITOR //////////////////////////////////////// --%>
    <link rel="stylesheet" href="css/index_style.css" />
    <link rel="stylesheet" href="../codeMirror/doc/docs.css" />
    <link rel="stylesheet" href="../codeMirror/lib/codemirror.css" />
    <link rel="stylesheet" href="../codeMirror/addon/fold/foldgutter.css" />
    <link rel="stylesheet" href="../codeMirror/addon/dialog/dialog.css" />
    <link rel="stylesheet" href="../codeMirror/theme/monokai.css" />
    <script src="../codeMirror/lib/codemirror.js"></script>
    <script src="../codeMirror/addon/search/searchcursor.js"></script>
    <script src="../codeMirror/addon/search/search.js"></script>
    <script src="../codeMirror/addon/dialog/dialog.js"></script>
    <script src="../codeMirror/addon/edit/matchbrackets.js"></script>
    <script src="../codeMirror/addon/edit/closebrackets.js"></script>
    <script src="../codeMirror/addon/comment/comment.js"></script>
    <script src="../codeMirror/addon/wrap/hardwrap.js"></script>
    <script src="../codeMirror/addon/fold/foldcode.js"></script>
    <script src="../codeMirror/addon/fold/brace-fold.js"></script>
    <script src="../codeMirror/mode/javascript/javascript.js"></script>
    <script src="../codeMirror/mode/sql/sql.js"></script>
    <script src="../codeMirror/mode/css/css.js"></script>
    <script src="../codeMirror/keymap/sublime.js"></script>
</head>
<body>
    <div >
            <style>
                .CodeMirror {
                    border-top: 1px solid #eee;
                    border-bottom: 1px solid #eee;
                    line-height: 1.3;
                    height: 500px
                }

                .CodeMirror-linenumbers {
                    padding: 0 8px;
                }
            </style>

            <div id="div1">
                <div id="divControles">
                    Seleccionar Pestaña:
                    <select id="desplegable"></select>
                    &nbsp; &nbsp;
                    <input type="file" id="openfile" value="Nueva Pestaña" />
                    <button onclick="AnalizarLUP()">Analizar LUP</button>
                    <form id="form1" runat="server">
                        <asp:HiddenField ID="hdCadena" runat="server" />
                        <asp:Button runat="server" OnClientClick="AlmacenarTexto()" OnClick="Unnamed_Click" Text="Analizar CQL"></asp:Button>
                    </form>
                </div>

                <div id="divEntrada">
                    <h4>Respuesta Server</h4>
                    <textarea id="txtSalida" rows="14" cols="60" runat="server"></textarea>
                    <h4>Paquetes Enviados</h4>
                    <textarea id="txtEnviado" rows="14" cols="60" runat="server"></textarea>
                </div>

                <div id="divEditor">
                    <form id="form_txtEntrada">
                        <textarea id="txtEntrada"></textarea>
                    </form>
                    <div class="tab">
                        <button class="tablinks" onclick="openTab(event, 'Consola')">Consola</button>
                        <button class="tablinks" onclick="openTab(event, 'Errores')">Errores</button>
                    </div>
                    <div id="Consola" class="tabcontent">
                        <h3>Consola</h3>
                        <textarea id="txtConsola" rows="15" cols="80" runat="server"></textarea>
                    </div>
                    <div id="divErrores" class="tabcontent">
                        <h3>Errores</h3>
                    </div>
                </div>
            </div>

            <script src="../Jison/Analizador.js"></script>
            <script>
                function openTab(evt, cityName) {
                    // Declare all variables
                    var i, tabcontent, tablinks;

                    // Get all elements with class="tabcontent" and hide them
                    tabcontent = document.getElementsByClassName("tabcontent");
                    for (i = 0; i < tabcontent.length; i++) {
                        tabcontent[i].style.display = "none";
                    }

                    // Get all elements with class="tablinks" and remove the class "active"
                    tablinks = document.getElementsByClassName("tablinks");
                    for (i = 0; i < tablinks.length; i++) {
                        tablinks[i].className = tablinks[i].className.replace(" active", "");
                    }

                    // Show the current tab, and add an "active" class to the button that opened the tab
                    document.getElementById(cityName).style.display = "block";
                    evt.currentTarget.className += " active";
                }

                function AnalizarLUP() {
                    var text = document.getElementById("txtSalida").value;
                    var ast = Analizador.parse(text);

                    //imprimo los mensajes en la consola
                    for (i in ast.mensajes) {
                        document.getElementById("txtConsola").value +=
                            (ast.mensajes[i]).toString().replace("[+MESSAGE]", "").replace("[-MESSAGE]", "") + "\n";
                    }

                    //creo la tabla para las consultas
                    for (i in ast.data) {
                        var divTablaConsulta = document.createElement('div');
                        var data = ast.data[i];
                        divTablaConsulta.innerHTML += data.toString().replace("[+DATA]", "").replace("[-DATA]", "");
                        document.getElementById('divEditor').appendChild(divTablaConsulta);
                    }

                    //creo la tabla para los errores
                    var divTablaErrores = document.createElement('div');
                    var tablaErrores = "<table border=\"2\" style=\"margin: 0 auto;\">" +
                        "<td>No.</td>" +
                        "<td>Lexema</td>" +
                        "<td>Descripcion</td>" +
                        "<td>Tipo</td>" +
                        "<td>Fila</td>" +
                        "<td>Columna</td>";
                    for (i in ast.errores) {
                        var error = ast.errores[i];
                        tablaErrores += "<tr>";
                        tablaErrores += "<td>" + i + "</td>";
                        tablaErrores += "<td>" + error.lexema.toString().replace("[+LEXEMA]", "").replace("[-LEXEMA]", "") + "</td>";
                        tablaErrores += "<td>" + error.descripcion.toString().replace("[+DESC]", "").replace("[-DESC]", "") + "</td>";
                        tablaErrores += "<td>" + error.tipo.toString().replace("[+TYPE]", "").replace("[-TYPE]", "") + "</td>";
                        tablaErrores += "<td>" + error.fila.toString().replace("[+LINE]", "").replace("[-LINE]", "") + "</td>";
                        tablaErrores += "<td>" + error.columna.toString().replace("[+COLUMN]", "").replace("[-COLUMN]", "") + "</td>";
                        tablaErrores += "</tr>";
                    }
                    tablaErrores += "</table>";
                    divTablaErrores.innerHTML = tablaErrores;
                    document.getElementById('divEditor').appendChild(divTablaErrores);
                }
            </script>

            <script>
                var cont = 0;
                var fileInput = document.getElementById('openfile');
                var fileDisplayArea = document.getElementById('fileDisplayArea');

                fileInput.addEventListener('change', function () {
                    var fr = new FileReader();
                    fr.onload = function () {
                        newBuf(this.result, "Pestaña" + (cont++));
                    }
                    fr.readAsText(this.files[0]);
                });
            </script>

            <script>
                var desplegable = document.getElementById("desplegable");
                CodeMirror.on(desplegable, "change", function () {
                    selectBuffer(txtEntrada, desplegable.options[desplegable.selectedIndex].value);
                });

                var buffers = {};

                var txtEntrada = CodeMirror.fromTextArea(document.getElementById('txtEntrada'), {
                    lineNumbers: true,
                    mode: "text/x-mssql",
                    keyMap: "sublime",
                    autoCloseBrackets: true,
                    matchBrackets: true,
                    showCursorWhenSelecting: true,
                    theme: "monokai"
                });

                openBuffer("untitled", "", "text/x-mssql");
                selectBuffer(txtEntrada, "untitled");

                function AlmacenarTexto() {
                    document.getElementById('hdCadena').value = txtEntrada.getValue();
                }

                function openBuffer(name, text, mode) {
                    buffers[name] = CodeMirror.Doc(text, mode);
                    var opt = document.createElement("option");
                    opt.appendChild(document.createTextNode(name));
                    desplegable.appendChild(opt);
                }

                function newBuf(texto, name) {
                    if (name == null) return;
                    if (buffers.hasOwnProperty(name)) {
                        alert("Ya hay una pestaña con este nombre");
                        return;
                    }
                    openBuffer(name, texto, "text/x-mssql");
                    selectBuffer(txtEntrada, name);
                    var sel = desplegable;
                    sel.value = name;
                }

                function selectBuffer(editor, name) {
                    var buf = buffers[name];
                    if (buf.getEditor()) buf = buf.linkedDoc({ sharedHist: true });
                    var old = editor.swapDoc(buf);
                    var linked = old.iterLinkedDocs(function (doc) { linked = doc; });
                    if (linked) {
                        // Make sure the document in buffers is the one the other view is looking at
                        for (var name in buffers) if (buffers[name] == old) buffers[name] = linked;
                        old.unlinkDoc(linked);
                    }
                    editor.focus();
                }

                function nodeContent(id) {
                    var node = document.getElementById(id), val = node.textContent || node.innerText;
                    val = val.slice(val.match(/^\s*/)[0].length, val.length - val.match(/\s*$/)[0].length) + "\n";
                    return val;
                }
            </script>
        </div>
</body>
</html>
