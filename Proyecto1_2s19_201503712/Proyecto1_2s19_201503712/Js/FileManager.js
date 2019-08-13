var fileInput = document.getElementById('openfile');
var fileDisplayArea = document.getElementById('fileDisplayArea');

fileInput.addEventListener('change', function () {
    var fr = new FileReader();
    fr.onload = function () {
        document.getElementById("filecontent").textContent = this.result;

    }
    fr.readAsText(this.files[0]);
});