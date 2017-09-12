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

function beginReq(sender, args)
{   //muestra el popup  
    $find(ModalProgress0).show();
    $find(ModalProgress1).show();
    $find(ModalProgress2).show();
    $find(ModalProgress3).show();
    $find(ModalProgress4).show();
    $find(ModalProgress5).show();
    $find(ModalProgress6).show();
    $find(ModalProgress7).show();
    $find(ModalProgress8).show();
    $find(ModalProgress9).show();
}

function endReq(sender, args) 
{   
    //esconde el popup
    $find(ModalProgress0).hide();
    $find(ModalProgress1).hide();
    $find(ModalProgress2).hide();
    $find(ModalProgress3).hide();
    $find(ModalProgress4).hide();
    $find(ModalProgress5).hide();
    $find(ModalProgress6).hide();
    $find(ModalProgress7).hide();
    $find(ModalProgress8).hide();
    $find(ModalProgress9).hide();
}

