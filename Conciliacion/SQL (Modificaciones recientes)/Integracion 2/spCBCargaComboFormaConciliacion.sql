/*********************************************************
Programó: Iván Trujillo
Fecha: 13/11/2017
Descripcion: Carga el combo formaconciliacion considerando 
las reglas establecidas para la versión 2.4 de Conciliación 
Bancaria
***********************************************************/

CREATE PROCEDURE dbo.spCBCargaComboFormaConciliacion

@TipoConciliacion as SmallInt 

AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED 
 
SELECT fc.FormaConciliacion AS Identificador, fc.Descripcion
FROM TipoConciliacion tc
INNER JOIN TipoFormaConciliacion tfc ON(tfc.tipoconciliacion = tc.TipoConciliacion)
INNER JOIN FormaConciliacion fc ON(tfc.FormaConciliacion = fc.FormaConciliacion)
WHERE tc.TipoConciliacion = @TipoConciliacion

SET TRANSACTION ISOLATION LEVEL READ COMMITTED 

