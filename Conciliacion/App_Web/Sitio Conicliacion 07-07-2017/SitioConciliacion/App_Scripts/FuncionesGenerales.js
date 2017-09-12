
/***********REGISTRO DE CREACION***********/
//NombreArchivo:    FuncionesGenerales.js
//Proyecto:         25
//Requerimiento:    67
//Fecha Creacion:   09/07/2010
//Autor:            Benito Armando Colón Ruiz
//Descripcion:      Archivo JavaScript que realizara las validaciones generales sobre el formato de algunos campos como son
//                  numericos, alfanumericos, caracteres, tipo correo, tipo RFC, tipo CURP, tipo NSS
/***********REGISTRO DE MODIFICACIONES***********/

/*
 *Valida que el formato introducido sea numerico
 */
function IsNumeric(sText)
{
    var ValidChars = "0123456789.";
    var IsNumber=true;
    var Char;

    for (i = 0; i < sText.length && IsNumber == true; i++) 
    { 
        Char = sText.charAt(i); 
        if (ValidChars.indexOf(Char) == -1) 
        {
            IsNumber = false;
        }
    }
    return IsNumber;

}
/*
 * Funcion que valida el formato del dato introducido en un tetxBox
 * si no es valido se borra y regresa el cursor a la posicion anterior
 * "campo" --> El campo a validar.
 * "indice" --> El tipo de dato que se debe validar.
 * INVOCACION --> *onKeyUp="return validaFormato(this, 'numerico');"*
 */
function validaFormato(campo, tipo) 
{

    var codigoChar = (event.which) ? event.which : event.keyCode;
    // Reviso que no se comprueben las flechas, para que se puedan mover con ellas, dentro de los campos.
    if (codigoChar != 37 && codigoChar != 38 && codigoChar != 39 && codigoChar != 40) 
    {
        if (tipo == 'numerico2Puntos' && campo.value.match(/[^\d^:]+/g)) 
        {
            var indice = campo.value.search(/[^\d]+/g);
            campo.value = campo.value.replace(/[^\d]+/g, '');
			establecerPosicionCursor(campo, indice);
        }
        if (tipo == 'numericoPunto' && campo.value.match(/[^\d^.]+/g)) 
        {
            var indice = campo.value.search(/[^\d]+/g);
            campo.value = campo.value.replace(/[^\d]+/g, '');
			establecerPosicionCursor(campo, indice);
        } 
        if (tipo == 'numerico' && campo.value.match(/[^\d]+/g)) 
        {
            var indice = campo.value.search(/[^\d]+/g);
            campo.value = campo.value.replace(/[^\d]+/g, '');
			establecerPosicionCursor(campo, indice);
        } 
		if (tipo == 'mail' && campo.value.match(/[^\a-z@_\-.0-9]+/g)) 
		{
			var bandera = true;
			var indiceMin = -1982;
			var indiceMinMax = 1;
			if (campo.value.match(/[A-Z]+/g)) 
			{
				bandera = false;
				indiceMin = campo.value.search(/[A-Z]+/g);
				var i = indiceMin + 1;
				while (campo.value.charAt(i).match(/[A-Z@_\-.0-9]+/g)) 
				{
					i++;
				}
				var j = 1;
				while (campo.value.charAt(indiceMin - j).match(/[^\A-Za-z@_\-.0-9]+/g)) 
				{
					i--;
					j++;
				}
				indiceMinMax = i;
				campo.value = campo.value.toLowerCase();
			}
			var indice = campo.value.search(/[^\a-z@_\-.0-9]+/g);
			campo.value = campo.value.replace(/[^\a-z@_\-.0-9]+/g, '');
			if (bandera) 
			{
				establecerPosicionCursor(campo, indice);
			}
			else 
				if (indiceMin >= 0) 
				{
					establecerPosicionCursor(campo, indiceMin + (indiceMinMax - indiceMin));
				}
		} 
    }
}

/*
 * Funcion que establece la posición del cursor en el indice indicado
 * "campo" --> El campo a validar.
 * "indice" --> El indice en el que se requiere el cursor.
 */
function establecerPosicionCursor(campo, indice) 
{
	var range = campo.createTextRange();
	range.collapse(true);
	range.moveEnd('character', indice);
	range.moveStart('character', indice);
	range.select();
}


/*
 * Funcion para verificar Emails regresando el string del error en caso de ser requerido
 * "cadenaEmail" --> String con la direccion de correo a validar.
 */
function validaEmail(cadenaEmail) 
{

	/* La siguiente variable le dice al resto de la función o no
    para verificar que la dirección termina en una de dos letras del pais o notoriamente conocida
    Eu ". 1 significa comprobarlo, 0 significa que no lo hacen*/
	var checkTLD=1;
	var knownDomsPat=/^(com|net|org|edu|int|mil|gov|arpa|biz|aero|name|coop|info|pro|museum)$/;
	var emailPat=/^(.+)@(.+)$/;
	var specialChars="\\(\\)><@,;:\\\\\\\"\\.\\[\\]";
	var validChars="\[^\\s" + specialChars + "\]";
	var quotedUser="(\"[^\"]*\")";
	var ipDomainPat=/^\[(\d{1,3})\.(\d{1,3})\.(\d{1,3})\.(\d{1,3})\]$/;
	var atom=validChars + '+';
	var word="(" + atom + "|" + quotedUser + ")";
	var userPat=new RegExp("^" + word + "(\\." + word + ")*$");
	var domainPat=new RegExp("^" + atom + "(\\." + atom +")*$");
	var matchArray=cadenaEmail.match(emailPat);

	if (matchArray==null) 
	{
		return "La direccion de correo es incorrecta (verifique @ y puntos)";
	}
	var user=matchArray[1];
	var domain=matchArray[2];

	for (i=0; i<user.length; i++) 
	{
		if (user.charCodeAt(i)>127) 
		{
			return "El nombre de Usuario del correo contiene caracteres invalidos";
		}
	}
	for (i=0; i<domain.length; i++) 
	{
		if (domain.charCodeAt(i)>127) 
		{
			return "El dominio del correo contiene caracteres invalidos";
		}
	}

	if (user.match(userPat)==null) 
	{
		return "El nombre de usuario del correo no es valido.";
	}

	var IPArray=domain.match(ipDomainPat);
	if (IPArray!=null) 
	{
		for (var i=1;i<=4;i++) 
		{
			if (IPArray[i]>255) 
			{
				return "La IP destino del correo no es valida";
		    }
		}
		return "";
	}

	var atomPat=new RegExp("^" + atom + "$");
	var domArr=domain.split(".");
	var len=domArr.length;
	for (i=0;i<len;i++) 
	{
		if (domArr[i].search(atomPat)==-1) 
		{
			return "El nombre del dominio del correo parece ser invalido";
		}
	}

	if (checkTLD && domArr[domArr.length-1].length!=2 && 
	domArr[domArr.length-1].search(knownDomsPat)==-1) 
	{
		return "La direccion de correo debe terminar con un dominio conocido o dos letras de pais";
	}
	if (len<2) 
	{
		return "La direccion de correo no posee hostname";
	}
	return "";
}

/*
 * Funcion para verificar un RFC regresando el string del error en caso de ser requerido
 * "cadenaRFC" --> String con el RFC a validar.
 */
function validaRFC(cadenaRFC)
{
    //alert(cadenaRFC);
    var regExpRFC = new RegExp(/^([a-zA-Z]{3,4})(\d{6})((\D|\d){3})?$/);
    if (!cadenaRFC.match(regExpRFC))
    {
        return "El formato del RFC es incorrecto. \nEl formato correcto es: (aAAA######aaa)";
    }
    
    return "";
}

/*
 * Funcion para verificar un CURP regresando el string del error en caso de ser requerido
 * "cadenaCURP" --> String con el CURP a validar.
 */
function validaCURP(cadenaCURP)
{
    //alert(cadenaCURP);
    var regExpCURP = new RegExp(/^([a-zA-Z]{4})([0-9]{6})([a-zA-Z]{6})([0-9]{2})$/);
    if (!cadenaCURP.match(regExpCURP))
    {
        return "El formato del CURP es incorrecto. \nEl formato correcto es: (AAAA######AAAAAA##).";
    }
    
    return "";
}

/*
 * Funcion para verificar un NSS regresando el string del error en caso de ser requerido
 * "cadenaNSS" --> String con el NSS a validar.
 */
function validaNSS(cadenaNSS)
{
    //alert(cadenaNSS);
    var regExpNSS = new RegExp(/^([0-9]{11})$/);
    if (!cadenaNSS.match(regExpNSS))
    {
        return "El formato del NSS es incorrecto. \nEl formato correcto es: (###########).";
    }
    
    return "";
}



/*
 * Funcion q obtiene un el control deseado con parte del Id del mismo
 * parent--> padre del elemento q se busca.
 * identificador --> parte del Id q se esta buscando, debe ser unico para obtener un solo elemento.
 * tagname --> nombre del tipo de etiqueta a la cual pertenece el control q se busca.
 * INVOCACION --> var control = getControl(document, "btnX", "input");
 */
function getControl(parent,identificador, tagname)
{

    //Expresión regular q termina con el nombre del identificador especificado
    var regexCtrl = new RegExp('('+ identificador +'){1,}');
    //alert(regexCtrl);
    //obtencion de los elementos segun el tagName especificado
    var elemsTagName = parent.getElementsByTagName(tagname);
    //alert(elemsTagName);
    var i;
    for(i = 0; i< elemsTagName.length; i++)
    {
        //alert(elemsTagName[i].id);
        if(regexCtrl.test(elemsTagName[i].id))
        {
            //alert("ID DEL ELEMENTO BUSCADO \n" + elemsTagName[i].id);
            return elemsTagName[i];
        }
    }
    return null;
}



//Funcion auxiliar para comprar fechas
function ComparaFechas(FInicio, FFin)
{
    var resultado = "";
    if((FInicio != '-1') && (FFin != '-1'))
    {
        var numFI = parseInt(FInicio.substring(6).toString() + FInicio.substring(3,5).toString() + FInicio.substring(0,2).toString());
        var numFF = parseInt(FFin.substring(6).toString() + FFin.substring(3,5).toString() + FFin.substring(0,2).toString());
        
        if (numFI >= numFF)
            resultado = "La fecha incio no puede ser mayor o igual a la fecha fin.";        
    }
    else
    {
        if((FInicio == '-1') && (FFin != '-1'))
            resultado = "La fecha incio no puede permanecer sin selección.";
        if((FInicio != '-1') && (FFin == '-1'))
            resultado = "La fecha fin no puede permanecer sin selección.";
        if((FInicio == '-1') && (FFin == '-1'))
            resultado = "";        
    }
    return resultado;
}


/*
 * Funcion que selecciona el contenido de una caja de texto al obtener el foco
 * campo --> Control sobre el q se quiere hacer la seleccion 
 * INVOCACION --> onfocus="SeleccionarTexto(this);"
 */


function SeleccionarTexto(campo)
{
    campo.select()
}

/*
 * Funcion que cambia el contenido de una caja de texto a mayusculas
 * campo --> Control sobre el q se quiere hacer laconversion 
 * INVOCACION --> onKeyUp="CambiaMayusculas(this);"
 */
function CambiaMayusculas(campo)
{
 campo.value = campo.value.toUpperCase(); 
} 