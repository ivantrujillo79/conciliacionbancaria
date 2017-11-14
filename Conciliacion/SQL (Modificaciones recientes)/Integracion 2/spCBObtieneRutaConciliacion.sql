/*********************************************************
Programó: Iván Trujillo
Fecha: 13/11/2017
Descripcion: Consulta el destino de redirección para un 
tipo y forma de conciliación dado
***********************************************************/

CREATE PROCEDURE dbo.spCBObtieneRutaConciliacion

@TipoConciliacion as SmallInt,
@FormaConciliacion AS SmallInt

AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED 
 
SELECT *
FROM TipoFormaConciliacion tfc
WHERE tfc.TipoConciliacion = @TipoConciliacion AND tfc.FormaConciliacion = @FormaConciliacion

SET TRANSACTION ISOLATION LEVEL READ COMMITTED 
