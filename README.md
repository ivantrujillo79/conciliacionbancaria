# conciliacionbancaria
Durante la fase de despliegue se requiere de la instalación de los siguientes componentes:

## Microsoft Access Database Engine:

https://www.microsoft.com/es-mx/download/details.aspx?id=48145

https://www.microsoft.com/en-us/download/details.aspx?id=54920

https://www.microsoft.com/es-es/download/details.aspx?id=23734

## Crystal reports para el ambiente productivo


- Desinstalar todas las versiones de crystal reports para Visual Studio
- Desinstalar todas las versiones del runtime de crystal reports 
- Si usan visual studio 2017 y 2015 (no importa la edición) descargar el archivo de la siguiente URL
http://downloads.businessobjects.com/akdlm/cr4vs2010/CRforVS_13_0_21.exe

#### Resolución de problemas con Crystal reports
https://www.tektutorialshub.com/how-to-download-and-install-crystal-report-runtime/

- Si necesitan el de alguna otra versión de Visual studio referirse a la siguiente página web
https://wiki.scn.sap.com/wiki/display/BOBJ/Crystal+Reports%2C+Developer+for+Visual+Studio+Downloads



- **Reiniciar el equipo**
- Abrir el proyecto o solución con Visual Studio en la que se encuentre el reporte que desean editar mismo que deberá mostrarse con el ícono de cristal reports como se muestra a continuación
 
- Hacer doble clic y comenzar a editar:
 

NOTAS IMPORTANTES
- Es probable que para el proyecto de conciliación bancaria se genere el error siguiente que impide compilar el proyecto:

 


- Para corregir el error primero haga clic derecho en el mensaje de error y seleccione la opción de copiar, pegue el texto en un editor y tome la ruta en la que se encuentra el archivo preview.aspx:
 

- Abrir el archivo preview.aspx de la ruta obtenida en el paso anterior con un editor de texto en modo de administrador y modifique las líneas tres a ocho con la versión que el sistema requiere que es la 13.0.2000.0, guardar el archivo , recargar la solución y recompilar:
 
## Visual Studio 2010 Tools for Office Runtime
https://help.salesforce.com/articleView?id=How-to-install-Primary-Interop-Assemblies-PIA-and-Office-Visual-Studio-Runtime-manually&language=en_US&type=1

## Procedimiento antiguo (descontinuado)

https://drive.google.com/open?id=19L4uulKQls6-6CXReprs9FTnkUTbgcD_

https://drive.google.com/open?id=1ImtLhzP253bFkdmXqHd3QOpo0ercfMJY


Crystal Reports 13 para el ambiente de desarrollo-
Procedimiento de instalación:
https://www.tektutorialshub.com/download-crystal-reports-for-visual-studio/
Descarga de esta pagina Crystal Reports Developer Edition for Visual Studio:
https://www.tektutorialshub.com/download-crystal-reports-for-visual-studio/#Crystal-Reports-For-Visual-Studio-2015


