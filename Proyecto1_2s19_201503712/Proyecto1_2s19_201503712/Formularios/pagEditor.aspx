<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pagEditor.aspx.cs" Inherits="Proyecto1_2s19_201503712.Formularios.PruebaJison" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <link rel="stylesheet" href="../codeMirror/doc/docs.css" />

    <link rel="stylesheet" href="../codeMirror/lib/codemirror.css" />

    <link rel="stylesheet" href="../codeMirror/addon/fold/foldgutter.css" />
    <link rel="stylesheet" href="../codeMirror/addon/dialog/dialog.css" />

    <link rel="stylesheet" href="../codeMirror/theme/monokai.css" />

    <script src="../Jison/Parser.js"></script>

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

    <div>
        Seleccionar Pestaña:
        <select id="desplegable"></select>
        &nbsp; &nbsp;
        <input type="file" id="openfile" value="Nueva Pestaña"/>
    </div>

    <form id="form_txtEntrada">
        <textarea runat="server" id="txtEntrada"></textarea>
    </form>

    <form id="form1" runat="server">
        <asp:HiddenField id="hdCadena" runat="server"/>
        <asp:button  runat="server" OnClientClick="AlmacenarTexto()" OnClick="Unnamed_Click" Text="Analizar CQL"></asp:button>
    </form>

    <script>
        var cont = 0;
        var fileInput = document.getElementById('openfile');
        var fileDisplayArea = document.getElementById('fileDisplayArea');

        fileInput.addEventListener('change', function () {
            var fr = new FileReader();
            fr.onload = function () {
                newBuf(this.result, "Pestaña"+(cont++));
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

        function newBuf(texto,name) {
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

</body>
</html>
