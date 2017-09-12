

function ValidaFechas() {
    var mensaje = "";
    var Regreso = false;
    mensaje = ComparaFechas(getControl(document, "dpFInicial", "input").value, getControl(document, "dpFFinal", "input").value);
    if (mensaje == "") {
        Regreso = true;
    } else {

        alert(mensaje);
        Regreso = false;
    }
    return Regreso;
}