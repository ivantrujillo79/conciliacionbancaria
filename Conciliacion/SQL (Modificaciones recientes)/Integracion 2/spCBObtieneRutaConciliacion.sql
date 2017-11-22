/*********************************************************
Programó: Iván Trujillo
Fecha: 13/11/2017
Descripcion: Consulta el destino de redirección para un 
tipo y forma de conciliación dado
Modificado: 22/11/2017
Descripción: Soportar la recuperación de URL por defecto,
dicho caso se utilizará en la pantalla principal del sistema
durante la elección de una conciliación determinada por los
filtros proporcionados por el usuario
***********************************************************/

alter PROCEDURE dbo.spCBObtieneRutaConciliacion

@TipoConciliacion as SmallInt,
@FormaConciliacion AS SmallInt

AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED 
 
IF @FormaConciliacion <> 0
BEGIN
	SELECT *
	FROM TipoFormaConciliacion tfc
	WHERE tfc.TipoConciliacion = @TipoConciliacion AND tfc.FormaConciliacion = @FormaConciliacion
END
ELSE
BEGIN
	SELECT *
	FROM TipoFormaConciliacion T
	INNER JOIN (SELECT MIN(FormaConciliacion) AS FormaConciliacion
				FROM TipoFormaConciliacion tfc
				WHERE tfc.TipoConciliacion = @TipoConciliacion) A ON(T.TipoConciliacion = @TipoConciliacion AND T.FormaConciliacion = A.FormaConciliacion)
END

SET TRANSACTION ISOLATION LEVEL READ COMMITTED 
