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
        <select id="buffers_top"></select>
        &nbsp; &nbsp;
        <input type="file" id="openfile" value="Nueva Pestaña"/>
        <asp:button  runat="server" OnClick="Unnamed_Click" Text="Prueba CQL"></asp:button>
        <%--<button onclick="newBuf('')">Nueva Pestaña</button>--%>
    </div>
    <div id="code_top"></div>

    <script>
        var cont = 0;
        var fileInput = document.getElementById('openfile');
        var fileDisplayArea = document.getElementById('fileDisplayArea');

        fileInput.addEventListener('change', function () {
            var fr = new FileReader();
            fr.onload = function () {
                newBuf(this.result, "Pestaña"+(cont++));
                //document.getElementById("filecontent").textContent = this.result;
            }
            fr.readAsText(this.files[0]);
        });
    </script>

    <script>
        var sel_top = document.getElementById("buffers_top");
        CodeMirror.on(sel_top, "change", function () {
            selectBuffer(ed_top, sel_top.options[sel_top.selectedIndex].value);
        });

        var buffers = {};

        function openBuffer(name, text, mode) {
            buffers[name] = CodeMirror.Doc(text, mode);
            var opt = document.createElement("option");
            opt.appendChild(document.createTextNode(name));
            sel_top.appendChild(opt);
        }

        function newBuf(texto,name) {
            //var name = prompt("Nombre pestaña nueva", "untitled");
            if (name == null) return;
            if (buffers.hasOwnProperty(name)) {
                alert("Ya hay una pestaña con este nombre");
                return;
            }
            openBuffer(name, texto, "javascript");
            selectBuffer(ed_top, name);
            var sel = sel_top;
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
        openBuffer("untitled", "", "javascript");

        var ed_top = CodeMirror(document.getElementById("code_top"), {
            lineNumbers: true,
            mode: "javascript",
            keyMap: "sublime",
            autoCloseBrackets: true,
            matchBrackets: true,
            showCursorWhenSelecting: true,
            theme: "monokai"
        });
        selectBuffer(ed_top, "js");
    </script>

    <form id="form1" runat="server">
    </form>

</body>
</html>
