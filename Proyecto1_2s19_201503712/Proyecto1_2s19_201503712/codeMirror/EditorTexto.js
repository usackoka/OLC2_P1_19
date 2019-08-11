﻿var sel_top = document.getElementById("buffers_top");
CodeMirror.on(sel_top, "change", function () {
    selectBuffer(ed_top, sel_top.options[sel_top.selectedIndex].value);
});

var buffers = {};
openBuffer("untitled", "", "javascript");
selectBuffer(ed_top, "js");

function openBuffer(name, text, mode) {
    buffers[name] = CodeMirror.Doc(text, mode);
    var opt = document.createElement("option");
    opt.appendChild(document.createTextNode(name));
    sel_top.appendChild(opt);
}

function newBuf() {
    var name = prompt("Nombre pestaña nueva", "*untitled*");
    if (name == null) return;
    if (buffers.hasOwnProperty(name)) {
        alert("There's already a buffer by that name.");
        return;
    }
    openBuffer(name, "", "javascript");
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

var ed_top = CodeMirror(document.getElementById("code_top"), {
    lineNumbers: true,
    mode: "javascript",
    keyMap: "sublime",
    autoCloseBrackets: true,
    matchBrackets: true,
    showCursorWhenSelecting: true,
    theme: "monokai"
});