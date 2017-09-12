//NombreArchivo:    jsUpdateProgressIP.js
//Proyecto:         25
//Requerimiento:    72
//Fecha Creacion:   07/08/2010
//Autor:            Benito Armando Colón Ruiz
//Descripcion:      Se realizaron adaptaciones a la pagina para se inhabilite, cada ves que este procesando alguna informacion y no haya 
//                  cargado completamente

//Código JavaScript incluido en un archivo denominado jsUpdateProgressIP.js 
Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);

function beginReq(sender, args) {   //muestra el popup
    $find(ModalProgress).show();
}

function endReq(sender, args) {
    //esconde el popup
    $find(ModalProgress).hide();
}