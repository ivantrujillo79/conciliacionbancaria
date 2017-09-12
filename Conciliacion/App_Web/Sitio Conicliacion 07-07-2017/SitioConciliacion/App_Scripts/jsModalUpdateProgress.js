//NombreArchivo:    jsUpdateProgressIP.js
//Fecha Creacion:   21/06/2013
//Autor:            Castellanos Mtz Christian Daniel
//Descripcion:      Lanza y Oculta el Modal depues de que se cargue completamente la pagina

function accionActualizando() {
    var modal = $find(modalId);
    modal.show();
}

function accionActualizado() {
    var modal = $find(modalId);
    modal.hide();
}