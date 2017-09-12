var procesando = false;

/*************** BLOQUEO PAGINA EN ENVIO ***************/
function ValidaEnvio(validaDatos)
{
	var res = !procesando;
	if(validaDatos == null)
		validaDatos = true;
	if (typeof(Page_ClientValidate) == 'function' && validaDatos)
		return Page_ClientValidate();
	if (procesando)
		alert("Su solicitud esta siendo procesada." + String.fromCharCode(13) + "Por favor espere.");
	procesando = true;
	return res;	
}

/*************** INTERFACE USUARIO ***************/
function ChangeColor(controlID, Color)
{
	document.getElementById(controlID).style.backgroundColor = Color;
}	



/*************** VALIDACION DE ENTRADAS DE USUARIO ***************/
function IsDecimal(sender, evt)	
{
	var key;
	var keychar;
	var e = window.event;
	if (e.keyCode) 
	{
		key = e.keyCode;	
		keychar = String.fromCharCode(key);
	}
	return (key == 13 || key == 8 || (isDigit(keychar) && (keychar != '.' || sender.value.indexOf('.') == -1)));
}

function isDigit(num) 
{
	if (num.length>1)
		return false;
	var string="1234567890.";
	return (string.indexOf(num)!= -1);	
}


function IsDecimalNegativo(sender, evt)	
{
	var key;
	var keychar;
	var e = window.event;
	if (e.keyCode) 
	{
		key = e.keyCode;	
		keychar = String.fromCharCode(key);
	}
	return (key == 13 || key == 8 || (isDigitNegativo(keychar) && (keychar != '.' || sender.value.indexOf('.') == -1)&& (keychar != '-' || sender.value.indexOf('-') <= -1)));
}

function isDigitNegativo(num) 
{
	if (num.length>1)
		return false;
	var string="1234567890.-";
	return (string.indexOf(num)!= -1);	
}


function IniciaComboBox(ComboBox)
{
	document.getElementById(ComboBox).selectedIndex = -1;
}

function EliminaEspacios(string) {
	var tstring = "";
	string = '' + string;
	splitstring = string.split(" ");
	for(i = 0; i < splitstring.length; i++)
	tstring += splitstring[i];
	return tstring;
}

//Funciones para limistar la cadena de texto en un objeto de tipo TextArea
function SetMaxLength(sender, ml)	
{
	var key;
    var e = window.event;
	if (e.keyCode) 
		key = e.keyCode;	
	return (key != 13) && sender.value.length < ml;
}

function CheckLength(sender, ml)	
{
			sender.value = sender.value.substring(0,ml);
}
//Funciones para limistar la cadena de texto en un objeto de texto


/****************** FUNCION PARA INVOCAR NUEVA VENTANA **********************/
function ShowWindow(pagina, titulo, ancho, alto, x, y)
{	
	var wnd;
	if(ancho == 0)
		ancho = screen.width - 12;
	if(alto == 0)
		alto = screen.height - 70;
	//if(x == 0)
	//	if (screen.width - ancho > 0)
	//		x = (screen.width - ancho) / 2;
	//	else
	//		x = 0;
	//if(y == 0)
	//	if (screen.height - alto > 0)
	//		y = (screen.height - alto) / 2;
	//	else
	//		y = 0;

	wnd = window.open(pagina,titulo,'status=no,toolbar=no,menubar=no,titlebar=no,location=no,resizable=yes,scrollbars=yes,width='+ancho+',height='+alto+',left='+x+',top='+y);
	wnd.focus();
	return false;
}

/****************** FUNCION PARA INVOCAR NUEVA VENTANA **********************/
function getImage(pImageURL) {
    var img=new Image();
    img.src=pImageURL;
    document.getElementById('ctl00_contenidoPrincipal_imgFotoEmpleado').src=img.src;
    return false;
}

function LoadImage(imgEmpleado, fileImagen)
{
   //document.getElementById(imgEmpleado).src = null;
   //url = document.getElementById(fileImagen).value;
   url = document.getElementById(fileImagen).value;
   url = "C:\\Documents and Settings\\BECORU\\Mis documentos\\Mis imágenes\\DelfinQuispe.jpg";
   alert(url);
   //url.replace(':','$');
   document.getElementById(imgEmpleado).src=url;      
   alert(url);
}  

function LoadImage2(objetoImagen, trober)
{
    url = document.getElementById(objetoImagen).value;
    alert(trober);
    alert(document.getElementById(trober).innerText);
    //alert(url);
    alert(objetoImagen);
}