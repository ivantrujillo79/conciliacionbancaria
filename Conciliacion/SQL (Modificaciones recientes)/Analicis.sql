CREATE PROCEDURE [dbo].[spCBConsultaGrupoConciliacion]  
  
@Configuracion as SmallInt,  
@GrupoConciliacion as SmallInt=0  
  
AS  
  
SET NOCOUNT ON      
  
  
IF @Configuracion = 0 -- Consulta todos los grupos activos  
BEGIN   
   
 SELECT GC.GrupoConciliacion as GrupoConciliacionId ,GC.Descripcion, GC.Usuario, GC.Status, GC.Falta , 
 GC.DiferenciaDiasDefault, GC.DiferenciaDiasMaxima,GC.DiferenciaDiasMinima,
 GC.DiferenciaCentavosDefault, GC.DiferenciaCentavosMaxima, GC.DiferenciaCentavosMinima 
 FROM GrupoConciliacion GC   
 WHERE GC.Status = 'ACTIVO'  
    
END   
  
IF @Configuracion = 1 -- Consulta todos los grupos  
BEGIN   
   
 SELECT GC.GrupoConciliacion as GrupoConciliacionId ,GC.Descripcion, GC.Usuario, GC.Status, GC.Falta , 
 GC.DiferenciaDiasDefault, GC.DiferenciaDiasMaxima,GC.DiferenciaDiasMinima,
 GC.DiferenciaCentavosDefault, GC.DiferenciaCentavosMaxima, GC.DiferenciaCentavosMinima 
 FROM GrupoConciliacion GC   
    
END   
  
IF @Configuracion = 2 -- Consulta  un grupo especifico  
BEGIN   
  
 SELECT GC.GrupoConciliacion as GrupoConciliacionId ,GC.Descripcion, GC.Usuario, GC.Status, GC.Falta , 
 GC.DiferenciaDiasDefault, GC.DiferenciaDiasMaxima,GC.DiferenciaDiasMinima,
 GC.DiferenciaCentavosDefault, GC.DiferenciaCentavosMaxima, GC.DiferenciaCentavosMinima 
 FROM GrupoConciliacion GC   
 WHERE GC.Grupoconciliacion = @GrupoConciliacion   
   
END   
IF @Configuracion = 3 -- Consulta  un grupo especifico  y envia los parametros
BEGIN   
  
 SELECT GC.GrupoConciliacion, GC.DiferenciaDiasDefault, GC.DiferenciaDiasMaxima,GC.DiferenciaDiasMinima,
 GC.DiferenciaCentavosDefault, GC.DiferenciaCentavosMaxima, GC.DiferenciaCentavosMinima
 FROM GrupoConciliacion GC   
 WHERE GC.Grupoconciliacion = @GrupoConciliacion   
   
END


/*********************************************************    
Programo: Claudia García    
Fecha: 04/04/2013    
Descripcion: Carga el combo sucursales    
***********************************************************/    
    
CREATE PROCEDURE dbo.spCBCargarComboSucursal    
@Configuracion as SmallInt,    
@Corporativo TinyInt    
    
AS    
    
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED     
--Todas con el iden 0    
if @Configuracion = 0    
begin     
     SELECT DISTINCT(Sucursal.Sucursal) As Identificador,Sucursal.Descripcion, Sucursal.Siglas    
     FROM Sucursal    
     INNER JOIN Celula ON Sucursal.Sucursal = Celula.Sucursal    
     WHERE Sucursal.Corporativo = @Corporativo    
     UNION     
     SELECT 0 As Identificador,'TODAS' As Descripcion, 'TODAS 'As Siglas    
     UNION    
     SELECT Sucursal.Sucursal  As Identificador,Sucursal.Descripcion, Sucursal.Siglas    
     FROM Sucursal WHERE Sucursal.Sucursal=5 --Se agrego para la isntancia de hidro temporalmente    
end    
--Todas sin el iden 0    
if @Configuracion = 1    
begin     
     SELECT DISTINCT(Sucursal.Sucursal) As Identificador,Sucursal.Descripcion, Sucursal.Siglas    
     FROM Sucursal    
     INNER JOIN Celula ON Sucursal.Sucursal = Celula.Sucursal    
     WHERE Sucursal.Corporativo = @Corporativo    
     UNION    
     SELECT Sucursal.Sucursal  As Identificador,Sucursal.Descripcion, Sucursal.Siglas    
     FROM Sucursal WHERE Sucursal.Sucursal=5 --Se agrego para la isntancia de hidro temporalmente    
end    
SET TRANSACTION ISOLATION LEVEL READ COMMITTED 

SP_HELPTEXT SpCBCargaComboStatusConcepto 

/*******************************************  
Autor: Gabina Le�n V.   
Fecha: 29/05/2013  
Cargar el combo de StatusConcepto  
********************************************/ 
/*
se comento linea de configuracion 1 para mostrar todos los status concepto ID 20170806001
Fecha Modificacion 08/06/2017
by Aker
*/ 
  
CREATE PROCEDURE dbo.spCBCargaComboStatusConcepto  
@Configuracion as SmallInt,  
@GrupoConciliacion as Int=0  
AS    
  
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED           
  
-- TODOS  
if @Configuracion = 0  
begin  
     SELECT SC.StatusConcepto As Identificador, SC.Descripcion, SC.Status,SC.Usuario,SC.FAlta  
  FROM StatusConcepto SC  
end  
-- LOS QUE TIENEN ETIQUETAS RELACIONADAS   
if @Configuracion = 1  
begin  
     SELECT DISTINCT(SC.StatusConcepto) As Identificador, SC.Descripcion,SC.Status,SC.Usuario,SC.FAlta  
     FROM StatusConcepto SC
	 WHERE SC.Status = 'ACTIVO'  
   --  INNER JOIN StatusConceptoEtiqueta SCE ON SCE.StatusConcepto = SC.StatusConcepto   20170806001
  --   INNER JOIN Etiqueta E ON E.Etiqueta = SCE.Etiqueta        20170806001
end  
--Aquellos que se muestran como Ajustables (ConciliacionCompartida)  
--if @Configuracion = 2  
--begin  
--     SELECT 0 AS Identificador, 'NINGUNO' as Descripcion, 'ACTIVO' as Status,dbo.NombreUsuario() as Usuario,GetDate() as FAlta  
-- UNION  
--     SELECT SC.StatusConcepto As Identificador, SC.Descripcion, SC.Status,SC.Usuario,SC.FAlta  
--  FROM StatusConcepto SC inner join GrupoConciliacionStatusConcepto GS on   
--  SC.StatusConcepto=GS.StatusConcepto and GS.GrupoConciliacion=@GrupoConciliacion where SC.PermiteCaptura=1  
--  
--    
--end  
SET TRANSACTION ISOLATION LEVEL READ COMMITTED 

/*********************************************************
Programo: Gabina León
Fecha: 04/04/2013
Descripcion: Carga el combo formaconciliacion
***********************************************************/

CREATE PROCEDURE dbo.spCBCargarComboFormaConciliacion

@Configuracion as SmallInt 

AS

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED 

IF @Configuracion <> 2 

	 SELECT FormaConciliacion.FormaConciliacion As Identificador,FormaConciliacion.Descripcion
	 FROM FormaConciliacion
	 WHERE FormaConciliacion.FormaConciliacion <>4 -- NO MANDA COPIA CONCILIACION  
	ORDER BY FormaConciliacion.Descripcion
IF @Configuracion = 2 

	 SELECT FormaConciliacion.FormaConciliacion As Identificador,FormaConciliacion.Descripcion
	 FROM FormaConciliacion
	 WHERE FormaConciliacion.FormaConciliacion not in (1,4,5) -- NO MANDA CONCILIACION COPIA Y MANUAL 
	 --WHERE FormaConciliacion.FormaConciliacion <> 4
	 ORDER BY FormaConciliacion.Descripcion

SET TRANSACTION ISOLATION LEVEL READ COMMITTED 



SELECT*FROM FormaConciliacion


SELECT*FROM CANTIDAD Y REFERENCIA CONCUERDAN



/*********************************************************  
Programo: Claudia Garcísa  
Fecha: 02/05/2013  
Descripcion: Carga campos destino de la información  
***********************************************************/  
  
CREATE PROCEDURE dbo.spCBCargarComboDestino  
@Configuracion as SmallInt,  
@TipoConciliacion As SmallInt,  
@CampoExterno As VarChar(50)  
  
AS  
  
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED   
--Regresa todos los campos externos que coincidan por tipo de conciliacion  
if @Configuracion = 0  
begin  
     SELECT ROW_NUMBER() OVER(ORDER BY ColumnaDestinoExt DESC) AS Identificador, ColumnaDestinoExt As Descripcion, TablaDestinoExt As Campo1  
     FROM dbo.ReferenciaAComparar  
     WHERE TipoConciliacion = 1 and Status= 'ACTIVO'  
     GROUP BY ColumnaDestinoExt, TablaDestinoExt  
end  
--Regresa todos los campos internos que coincidan por tipo de conciliacion y el campo externo  
if @Configuracion = 1  
begin  
     SELECT ROW_NUMBER() OVER(ORDER BY ColumnaDestinoInt DESC) AS Identificador, ColumnaDestinoInt As Descripcion, TablaDestinoInt As Campo1  
     FROM dbo.ReferenciaAComparar  
     WHERE TipoConciliacion = @TipoConciliacion  
     AND ColumnaDestinoExt = @CampoExterno  
  and Status= 'ACTIVO'  
     GROUP BY ColumnaDestinoInt, TablaDestinoInt  
end  
--Regresa todos los campos de la tabla destino  
if @Configuracion = 2  
begin  
     SELECT ROW_NUMBER() OVER(ORDER BY ColumnaDestino DESC) AS Identificador, ColumnaDestino As Descripcion, TipoDato As Campo1  
     FROM dbo.Destino  
     WHERE TablaDestino ='TablaDestinoDetalle'  
     AND ColumnaDestino  in ('Cheque','Concepto','Referencia','Descripcion','FMovimiento','FOperacion','NombreTercero','RFCTercero','Deposito','Retiro','ClabeTercero')  
end  
--Regresa todos los campos de la tabla destino Pedido  
if @Configuracion = 3  
begin  
     SELECT ROW_NUMBER() OVER(ORDER BY ColumnaDestino DESC) AS Identificador, ColumnaDestino As Descripcion, TipoDato As Campo1  
     FROM dbo.Destino  
     WHERE TablaDestino ='Pedido'  
     AND ColumnaDestino  in ('Cliente', 'PedidoReferencia', 'Total','Nombre','AñoPed','ConceptoPedido','Pedido')  
end  
--Regresa los campo para el filtro de la vista de conciliacion compartida (EXTERNO)
if @Configuracion = 4
begin  

	 SELECT ROW_NUMBER() OVER(ORDER BY ColumnaDestino DESC) AS Identificador,ColumnaDestino As Descripcion, TipoDato As Campo1  
     FROM dbo.Destino  
     WHERE TablaDestino ='TablaDestinoDetalle'  and
     ColumnaDestino  in      ('StatusConciliacion','Descripcion','Deposito','FOperacion','Caja','Referencia','Retiro','SaldoFinal','SucursalBancaria',
	 						 'StatusConcepto','FolioTraspaso','MontoTraspaso')  

end  
--Regresa los campo para el filtro de la vista de conciliacion compartida (INTERNO)
if @Configuracion = 5
begin  

	 SELECT ROW_NUMBER() OVER(ORDER BY ColumnaDestino DESC) AS Identificador,ColumnaDestino As Descripcion, TipoDato As Campo1  
     FROM dbo.Destino  
     WHERE  TablaDestino ='TablaDestinoDetalle'  and
     ColumnaDestino  in ('Cliente','DescripcionInterno','ConceptoInterno','MotivoNoConciliado','ComentarioNoConciliado')  

end  
   --exec spCBCargarComboDestino 5,0,''
SET TRANSACTION ISOLATION LEVEL READ COMMITTED   



/*********************************************************  
Programo: Claudia Garcísa  
Fecha: 02/05/2013  
Descripcion: Carga campos destino de la información  
***********************************************************/  
  
CREATE PROCEDURE dbo.spCBCargarComboDestino  
@Configuracion as SmallInt,  
@TipoConciliacion As SmallInt,  
@CampoExterno As VarChar(50)  
  
AS  
  
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED   
--Regresa todos los campos externos que coincidan por tipo de conciliacion  
if @Configuracion = 0  
begin  
     SELECT ROW_NUMBER() OVER(ORDER BY ColumnaDestinoExt DESC) AS Identificador, ColumnaDestinoExt As Descripcion, TablaDestinoExt As Campo1  
     FROM dbo.ReferenciaAComparar  
     WHERE TipoConciliacion = @TipoConciliacion and Status= 'ACTIVO'  
     GROUP BY ColumnaDestinoExt, TablaDestinoExt  
end  
--Regresa todos los campos internos que coincidan por tipo de conciliacion y el campo externo  
if @Configuracion = 1  
begin  
     SELECT ROW_NUMBER() OVER(ORDER BY ColumnaDestinoInt DESC) AS Identificador, ColumnaDestinoInt As Descripcion, TablaDestinoInt As Campo1  
     FROM dbo.ReferenciaAComparar  
     WHERE TipoConciliacion = 1  
     AND ColumnaDestinoExt = 'RFCTercero'  
  and Status= 'ACTIVO'  
     GROUP BY ColumnaDestinoInt, TablaDestinoInt  
end  
--Regresa todos los campos de la tabla destino  
if @Configuracion = 2  
begin  
     SELECT ROW_NUMBER() OVER(ORDER BY ColumnaDestino DESC) AS Identificador, ColumnaDestino As Descripcion, TipoDato As Campo1  
     FROM dbo.Destino  
     WHERE TablaDestino ='TablaDestinoDetalle'  
     AND ColumnaDestino  in ('Cheque','Concepto','Referencia','Descripcion','FMovimiento','FOperacion','NombreTercero','RFCTercero','Deposito','Retiro','ClabeTercero')  
end  
--Regresa todos los campos de la tabla destino Pedido  
if @Configuracion = 3  
begin  
     SELECT ROW_NUMBER() OVER(ORDER BY ColumnaDestino DESC) AS Identificador, ColumnaDestino As Descripcion, TipoDato As Campo1  
     FROM dbo.Destino  
     WHERE TablaDestino ='Pedido'  
     AND ColumnaDestino  in ('Cliente', 'PedidoReferencia', 'Total','Nombre','AñoPed','ConceptoPedido','Pedido')  
end  
--Regresa los campo para el filtro de la vista de conciliacion compartida (EXTERNO)
if @Configuracion = 4
begin  

	 SELECT ROW_NUMBER() OVER(ORDER BY ColumnaDestino DESC) AS Identificador,ColumnaDestino As Descripcion, TipoDato As Campo1  
     FROM dbo.Destino  
     WHERE TablaDestino ='TablaDestinoDetalle'  and
     ColumnaDestino  in      ('StatusConciliacion','Descripcion','Deposito','FOperacion','Caja','Referencia','Retiro','SaldoFinal','SucursalBancaria',
	 						 'StatusConcepto','FolioTraspaso','MontoTraspaso')  

end  
--Regresa los campo para el filtro de la vista de conciliacion compartida (INTERNO)
if @Configuracion = 5
begin  

	 SELECT ROW_NUMBER() OVER(ORDER BY ColumnaDestino DESC) AS Identificador,ColumnaDestino As Descripcion, TipoDato As Campo1  
     FROM dbo.Destino  
     WHERE  TablaDestino ='TablaDestinoDetalle'  and
     ColumnaDestino  in ('Cliente','DescripcionInterno','ConceptoInterno','MotivoNoConciliado','ComentarioNoConciliado')  

end  
   --exec spCBCargarComboDestino 5,0,''
SET TRANSACTION ISOLATION LEVEL READ COMMITTED   




/********************************************************
Realizo: Gabina Leon Velasco 
Fecha: 24/05/2013
Descripcion: Devuelve la informacion de una conciliacion especifica 
exec spCBConsultaConciliacionDetalle 2,1,2013,4,19;
Modifico: Carlos Nirari Santiago Mendoza
Fecha: 12/02/2014
Descripcion:Se agredo campo CorporativoDes para obtener nombre del coporativo 
Modifico: Carlos Nirari Santiago Mendoza
Fecha: 11/03/2014
Descripcion:Se agredo campo Banco para obtener ID del banco
*********************************************************/

CREATE PROCEDURE [dbo].[spCBConsultaConciliacionDetalle]
@Corporativo as Int, 
@Sucursal as Int, 
@AñoConciliacion as Int, 
@MesConciliacion as SmallInt, 
@FolioConciliacion as Int 

AS  
  
SET NOCOUNT ON  

BEGIN

DECLARE @Servidor As VarChar(100)    
DECLARE @BD As VarChar(100)    
DECLARE @Financiero VarChar(1000)  
DECLARE @Cadena VarChar(8000)  

SET @Servidor = (SELECT Valor FROM Parametro WHERE Modulo=30 AND Parametro ='ServidorFinanciero' AND Corporativo=@Corporativo)        
SET @BD = (SELECT Valor FROM Parametro WHERE Modulo=30 AND Parametro ='BaseDatosFinanciero' AND Corporativo=@Corporativo)        
IF @Servidor <>''    
     SET @Servidor = '['+@Servidor+'].'    
    
SET @Financiero= @Servidor+@BD+'.dbo.'   


SET @Cadena = 'SELECT C.CorporativoConciliacion, Corp.Nombre CorporativoDes ,C.SucursalConciliacion,S.Descripcion SucursalDes,C.AñoConciliacion,C.MesConciliacion,C.FolioConciliacion,
				GC.Descripcion GrupoConciliacionStr,TC.Descripcion TipoConciliacionStr,SC.StatusConciliacion,
				dbo.fnCBTransacciones ('+CAST(@Corporativo As Varchar)+','+ CAST(@Sucursal As Varchar)+','+ CAST(@AñoConciliacion As Varchar)+','+CAST(@MesConciliacion As Varchar)+','+ CAST(@FolioConciliacion As Varchar)+',''INTERNO'',0) TransaccionesInternas,
				dbo.fnCBTransacciones ('+CAST(@Corporativo As Varchar)+','+ CAST(@Sucursal As Varchar)+','+ CAST(@AñoConciliacion As Varchar)+','+CAST(@MesConciliacion As Varchar)+','+ CAST(@FolioConciliacion As Varchar)+',''INTERNO'',1) ConciliadasInternas,
				dbo.fnCBTransacciones ('+CAST(@Corporativo As Varchar)+','+ CAST(@Sucursal As Varchar)+','+ CAST(@AñoConciliacion As Varchar)+','+CAST(@MesConciliacion As Varchar)+','+ CAST(@FolioConciliacion As Varchar)+',''EXTERNO'',0) TransaccionesExternas,
				dbo.fnCBTransacciones ('+CAST(@Corporativo As Varchar)+','+ CAST(@Sucursal As Varchar)+','+ CAST(@AñoConciliacion As Varchar)+','+CAST(@MesConciliacion As Varchar)+','+ CAST(@FolioConciliacion As Varchar)+',''EXTERNO'',1) ConciliadasExternas,
				ISNULL(dbo.fnCBMontoTotalTransacciones ('+CAST(@Corporativo As Varchar)+','+ CAST(@Sucursal As Varchar)+','+ CAST(@AñoConciliacion As Varchar)+','+CAST(@MesConciliacion As Varchar)+','+ CAST(@FolioConciliacion As Varchar)+',''EXTERNO''),0) MontoTotalE
xterno,
				ISNULL(dbo.fnCBMontoTotalTransacciones ('+CAST(@Corporativo As Varchar)+','+ CAST(@Sucursal As Varchar)+','+ CAST(@AñoConciliacion As Varchar)+','+CAST(@MesConciliacion As Varchar)+','+ CAST(@FolioConciliacion As Varchar)+',''INTERNO''),0) MontoTotalI
nterno,
				C.CuentaBancoFinanciero,B.Banco,B.Descripcion BancoStr,SC.UbicacionIcono 
				FROM Conciliacion C 
				INNER JOIN Corporativo Corp ON Corp.Corporativo=C.CorporativoConciliacion
				INNER JOIN Sucursal S ON S.Sucursal=C.SucursalConciliacion 
				INNER JOIN GrupoConciliacion GC ON C.GrupoConciliacion=GC.GrupoConciliacion 
				INNER JOIN StatusConciliacion SC ON SC.StatusConciliacion=C.StatusConciliacion 
				INNER JOIN TipoConciliacion TC ON TC.TipoConciliacion=C.TipoConciliacion 
				INNER JOIN '+@Financiero+'CuentaContableBanco CCB ON CCB.NumeroCuenta COLLATE Latin1_General_CI_AS=C.CuentaBancoFinanciero COLLATE Latin1_General_CI_AS
				INNER JOIN '+@Financiero+'Banco B ON CCB.Banco=B.Banco
				WHERE C.CorporativoConciliacion='+CAST(@Corporativo as Varchar)+
				' AND C.SucursalConciliacion='+CAST( @Sucursal as Varchar)+
				' AND C.AñoConciliacion='+ CAST(@AñoConciliacion as Varchar)+
				' AND C.MesConciliacion='+ CAST(@MesConciliacion as Varchar) +
				' AND C.FolioConciliacion='+ CAST(@FolioConciliacion as Varchar)
END
--SELECT LEN(@Cadena)
--SELECT (@Cadena) 

EXEC(@Cadena)


sp_helptext spCBConsultaTransaccionesConciliadas


/********************************************************
Realizo: Gabina Leon Velasco 
Fecha: 22/05/2013
Descripcion: Devuelve los regsitros conciliados externo
exec spCBConsultaTransaccionesConciliadas 2,1,2013,4,5,0;
*********************************************************/

CREATE PROCEDURE dbo.spCBConsultaTransaccionesConciliadas 

@CorporativoConciliacion as Int, 
@SucursalConciliacion as Int, 
@AñoConciliacion as Int, 
@MesConciliacion as SmallInt, 
@FolioConciliacion as Int, 
@FormaConciliacion as Int

AS  
  
SET NOCOUNT ON  

IF @FormaConciliacion = 0 -- TODAS LOS REGISTROS CONCILIADOS

		SELECT TDD.Corporativo, C.SucursalConciliacion as Sucursal, Sucursal.Descripcion as SucursalDes, TDD.Año, TDD.Folio, TDD.Secuencia, 
				TDD.Descripcion, TDD.Concepto,  TDD.Deposito, TDD.Retiro, ISNULL(CP.FormaConciliacion, CR.FormaConciliacion) As FormaConciliacion, 
				ISNULL(CP.StatusConcepto,CR.StatusConcepto) AS StatusConcepto,ISNULL(CP.StatusConciliacion, CR.StatusConciliacion) As StatusConciliacion, 
				TDD.FOperacion, TDD.FMovimiento, C.FolioConciliacion, C.MesConciliacion, ISNULL(CR.AñoConciliacion/CR.AñoConciliacion,0) as ConInterno,
				TDD.Cheque,TDD.Referencia,TDD.NombreTercero,TDD.RFCTercero
		FROM TablaDestinoDetalle TDD
		INNER JOIN TablaDestino TD ON TD.Corporativo= TDD.Corporativo 
								  AND TD.Sucursal= TDD.Sucursal 
								  AND TD.Año= TDD.Año 
								  AND TD.Folio = TDD.Folio
		INNER JOIN TipoFuenteInformacion TFI ON TFI.TipoFuenteInformacion = TD.TipoFuenteInformacion
		INNER JOIN TipoFuente TF ON TF.TipoFuente = TFI.TipoFuente AND TF.TipoFuente = 2 -- EXTERNO					 
		INNER JOIN Conciliacion C ON TD.Corporativo= C.CorporativoExterno 
								 AND TD.Sucursal= C.SucursalExterno 
								 AND TD.Año= C.AñoExterno 
								 AND TD.Folio = C.FolioExterno
		LEFT JOIN ConciliacionReferencia CR ON CR.CorporativoExterno= TDD.Corporativo
											   AND CR.SucursalExterno = TDD.Sucursal
											   AND CR.AñoExterno = TDD.Año
											   AND CR.FolioExterno = TDD.Folio
											   AND CR.SecuenciaExterno = TDD.Secuencia	
											   AND C.CorporativoConciliacion = CR.CorporativoConciliacion
											   AND C.SucursalConciliacion= CR.SucursalConciliacion
											   AND C.AñoConciliacion = CR.AñoConciliacion
											   AND C.FolioConciliacion = CR.FolioConciliacion
											   AND CR.StatusConciliacion IN ('CONCILIADA','CONCILIADA S/REFERENCIA')
		LEFT JOIN FormaConciliacion FCR ON FCR.FormaConciliacion= CR.FormaConciliacion	
		LEFT JOIN StatusConcepto SCR ON SCR.StatusConcepto = CR.StatusConcepto								   	
		LEFT JOIN ConciliacionPedido CP ON CP.CorporativoExterno= TDD.Corporativo
											   AND CP.SucursalExterno = TDD.Sucursal
											   AND CP.AñoExterno = TDD.Año
											   AND CP.FolioExterno = TDD.Folio
											   AND CP.SecuenciaExterno = TDD.Secuencia	
											   AND C.CorporativoConciliacion = CP.CorporativoConciliacion
											   AND C.SucursalConciliacion= CP.SucursalConciliacion
											   AND C.AñoConciliacion = CP.AñoConciliacion
											   AND C.FolioConciliacion = CP.FolioConciliacion
											   AND CP.StatusConciliacion IN ('CONCILIADA','CONCILIADA S/REFERENCIA')
		LEFT JOIN FormaConciliacion FCP ON FCP.FormaConciliacion= CR.FormaConciliacion		
		LEFT JOIN StatusConcepto SCP ON SCP.StatusConcepto = CP.StatusConcepto							   									   						 
		INNER JOIN Sucursal ON Sucursal.Sucursal = C.SucursalConciliacion
		WHERE C.CorporativoConciliacion  =@CorporativoConciliacion  
		  AND C.SucursalConciliacion  =@SucursalConciliacion  
		  AND C.AñoConciliacion=@AñoConciliacion 
		  AND C.MesConciliacion = @MesConciliacion
		  AND C.FolioConciliacion = @FolioConciliacion   
		  AND (CR.SecuenciaExterno IS NOT NULL OR CP.SecuenciaExterno IS NOT NULL)
		GROUP BY TDD.Corporativo, C.SucursalConciliacion, Sucursal.Descripcion, TDD.Año, TDD.Folio, TDD.Secuencia, 
				TDD.Descripcion, TDD.Concepto,  TDD.Deposito, TDD.Retiro, CP.FormaConciliacion, CR.FormaConciliacion, 
				CP.StatusConcepto,CR.StatusConcepto,CP.StatusConciliacion, CR.StatusConciliacion, 
				TDD.FOperacion, TDD.FMovimiento, C.FolioConciliacion, C.MesConciliacion, CR.AñoConciliacion,CR.AñoConciliacion,
				TDD.Cheque,TDD.Referencia,TDD.NombreTercero,TDD.RFCTercero

IF @FormaConciliacion >0 -- LOS REGISTROS CONCILIADOS POR UNA FORMA ESPECIFICA  

		SELECT TDD.Corporativo, C.SucursalConciliacion as Sucursal, Sucursal.Descripcion as SucursalDes, TDD.Año, TDD.Folio, TDD.Secuencia, 
				TDD.Descripcion, TDD.Concepto,  TDD.Deposito, TDD.Retiro, ISNULL(CP.FormaConciliacion, CR.FormaConciliacion) As FormaConciliacion, 
				ISNULL(CP.StatusConcepto,CR.StatusConcepto) AS StatusConcepto,ISNULL(CP.StatusConciliacion, CR.StatusConciliacion) As StatusConciliacion, 
				TDD.FOperacion, TDD.FMovimiento, C.FolioConciliacion, C.MesConciliacion, ISNULL(CR.AñoConciliacion/CR.AñoConciliacion,0) as ConInterno,
				TDD.Cheque,TDD.Referencia,TDD.NombreTercero,TDD.RFCTercero
		FROM TablaDestinoDetalle TDD
		INNER JOIN TablaDestino TD ON TD.Corporativo= TDD.Corporativo 
								  AND TD.Sucursal= TDD.Sucursal 
								  AND TD.Año= TDD.Año 
								  AND TD.Folio = TDD.Folio
		INNER JOIN TipoFuenteInformacion TFI ON TFI.TipoFuenteInformacion = TD.TipoFuenteInformacion
		INNER JOIN TipoFuente TF ON TF.TipoFuente = TFI.TipoFuente AND TF.TipoFuente = 2 -- EXTERNO					 
		INNER JOIN Conciliacion C ON TD.Corporativo= C.CorporativoExterno 
								 AND TD.Sucursal= C.SucursalExterno 
								 AND TD.Año= C.AñoExterno 
								 AND TD.Folio = C.FolioExterno
		LEFT JOIN ConciliacionReferencia CR ON CR.CorporativoExterno= TDD.Corporativo
											   AND CR.SucursalExterno = TDD.Sucursal
											   AND CR.AñoExterno = TDD.Año
											   AND CR.FolioExterno = TDD.Folio
											   AND CR.SecuenciaExterno = TDD.Secuencia	
											   AND C.CorporativoConciliacion = CR.CorporativoConciliacion
											   AND C.SucursalConciliacion= CR.SucursalConciliacion
											   AND C.AñoConciliacion = CR.AñoConciliacion
											   AND C.FolioConciliacion = CR.FolioConciliacion
											   AND CR.FormaConciliacion = @FormaConciliacion
											   AND CR.StatusConciliacion IN ('CONCILIADA','CONCILIADA S/REFERENCIA')
		LEFT JOIN FormaConciliacion FCR ON FCR.FormaConciliacion= CR.FormaConciliacion	
		LEFT JOIN StatusConcepto SCR ON SCR.StatusConcepto = CR.StatusConcepto								   	
		LEFT JOIN ConciliacionPedido CP ON CP.CorporativoExterno= TDD.Corporativo
											   AND CP.SucursalExterno = TDD.Sucursal
											   AND CP.AñoExterno = TDD.Año
											   AND CP.FolioExterno = TDD.Folio
											   AND CP.SecuenciaExterno = TDD.Secuencia	
											   AND C.CorporativoConciliacion = CP.CorporativoConciliacion
											   AND C.SucursalConciliacion= CP.SucursalConciliacion
											   AND C.AñoConciliacion = CP.AñoConciliacion
											   AND C.FolioConciliacion = CP.FolioConciliacion
											   AND CP.FormaConciliacion = @FormaConciliacion
											   AND CP.StatusConciliacion IN ('CONCILIADA','CONCILIADA S/REFERENCIA')
		LEFT JOIN FormaConciliacion FCP ON FCP.FormaConciliacion= CR.FormaConciliacion		
		LEFT JOIN StatusConcepto SCP ON SCP.StatusConcepto = CP.StatusConcepto							   									   						 
		INNER JOIN Sucursal ON Sucursal.Sucursal = C.SucursalConciliacion
		WHERE C.CorporativoConciliacion  =@CorporativoConciliacion  
		  AND C.SucursalConciliacion  =@SucursalConciliacion  
		  AND C.AñoConciliacion=@AñoConciliacion 
		  AND C.MesConciliacion = @MesConciliacion
		  AND C.FolioConciliacion = @FolioConciliacion   
		  AND (CR.SecuenciaExterno IS NOT NULL OR CP.SecuenciaExterno IS NOT NULL)
		GROUP BY TDD.Corporativo, C.SucursalConciliacion, Sucursal.Descripcion, TDD.Año, TDD.Folio, TDD.Secuencia, 
				TDD.Descripcion, TDD.Concepto,  TDD.Deposito, TDD.Retiro, CP.FormaConciliacion, CR.FormaConciliacion, 
				CP.StatusConcepto,CR.StatusConcepto,CP.StatusConciliacion, CR.StatusConciliacion, 
				TDD.FOperacion, TDD.FMovimiento, C.FolioConciliacion, C.MesConciliacion, CR.AñoConciliacion,CR.AñoConciliacion,
				TDD.Cheque,TDD.Referencia,TDD.NombreTercero,TDD.RFCTercero

sp_helptext  spCBConsultaEtiquetas 


/*******************************************  
Autor: Gabina Leon 
Fecha: 29/05/2013
Consulta la informacion de las etiquetas 
spCBConsultaEtiquetas 1,2,1,2013,2,1,21
********************************************/  
CREATE PROCEDURE spCBConsultaEtiquetas

@Configuracion TinyInt,   
@CorporativoConciliacion TinyInt, 
@SucursalConciliacion Int,
@AñoConciliacion Int,
@MesConciliacion TinyInt,
@FolioConciliacion Int, 
@StatusConcepto Int

AS    
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED       

DECLARE @Cadena as Varchar(4000)  
DECLARE @Servidor as Varchar(100)
DECLARE @BD as Varchar(100)

SET @Servidor = (SELECT Valor FROM Parametro WHERE Modulo=30 AND Parametro ='ServidorFinanciero' AND Corporativo=@CorporativoConciliacion)      
SET @BD = (SELECT Valor FROM Parametro WHERE Modulo=30 AND Parametro ='BaseDatosFinanciero' AND Corporativo=@CorporativoConciliacion)      

IF @Servidor <>''  
     SET @Servidor = '['+@Servidor+'].'  


IF @Configuracion = 0 -- EXTERNOS

			SET @Cadena = 
			'SELECT E.Etiqueta, E.Decripcion as EtiquetaStr, E.BancoFinanciero as Banco, B.Descripcion as BancoStr, E.TipoFuenteInformacion, TFI.Descripcion as TipoFuenteInformacionStr, E.TipoDato
			FROM Conciliacion C 
			INNER JOIN TablaDestino TD ON C.CorporativoExterno = TD.Corporativo 
									  AND C.SucursalExterno= TD.Sucursal
									  AND C.AñoExterno = TD.Año
									  AND C.FolioExterno = TD.Folio
			INNER JOIN TipoFuenteInformacion TFI ON TD.TipoFuenteInformacion = TFI.TipoFuenteInformacion
			INNER JOIN TipoFuente TF ON TF.TipoFuente = TFI.TipoFuente AND TF.TipoFuente = 2 
			INNER JOIN Etiqueta E ON E.TipoFuenteInformacion = TFI.TipoFuenteInformacion
			INNER JOIN StatusConceptoEtiqueta SCE ON SCE.Etiqueta = E.Etiqueta
			INNER JOIN StatusConcepto SC ON SC.StatusConcepto = SCE.StatusConcepto			
			INNER JOIN '+ @Servidor+@BD+'.dbo.CuentaContableBanco CCB ON CCB.NumeroCuenta COLLATE Latin1_General_CI_AS=C.CuentaBancoFinanciero COLLATE Latin1_General_CI_AS 
			INNER JOIN '+ @Servidor+@BD+'.dbo.Banco B ON CCB.Banco=B.Banco
													AND B.Banco=E.BancoFinanciero
			WHERE C.CorporativoConciliacion='+CAST(@CorporativoConciliacion AS Varchar) +
			' AND C.SucursalConciliacion='+CAST(@SucursalConciliacion AS Varchar)+
			' AND C.AñoConciliacion='+CAST(@AñoConciliacion AS Varchar)+
			' AND C.MesConciliacion='+CAST(@MesConciliacion AS Varchar)+
			' AND C.FolioConciliacion='+CAST(@FolioConciliacion AS Varchar)+
			' AND SC.StatusConcepto='+CAST(@StatusConcepto AS Varchar)+
			' GROUP BY E.Etiqueta, E.Decripcion,E.BancoFinanciero, B.Descripcion, E.TipoFuenteInformacion, TFI.Descripcion, E.TipoDato'

IF @Configuracion = 1 -- INTERNOS

			SET @Cadena = 
			'SELECT E.Etiqueta, E.Decripcion as EtiquetaStr, E.BancoFinanciero as Banco, B.Descripcion as BancoStr, E.TipoFuenteInformacion, TFI.Descripcion as TipoFuenteInformacionStr, E.TipoDato
			FROM Conciliacion C 
			INNER JOIN ConciliacionArchivo CA ON C.CorporativoConciliacion = CA.CorporativoConciliacion
											 AND C.SucursalConciliacion = CA.SucursalConciliacion
											 AND C.AñoConciliacion = CA.AñoConciliacion
											 AND C.MesConciliacion = CA.MesConciliacion
											 AND C.FolioConciliacion = CA.FolioConciliacion
			INNER JOIN TablaDestino TD ON CA.CorporativoInterno = TD.Corporativo 
									  AND CA.SucursalInterno= TD.Sucursal
									  AND CA.AñoInterno = TD.Año
									  AND CA.FolioInterno = TD.Folio
			INNER JOIN TipoFuenteInformacion TFI ON TD.TipoFuenteInformacion = TFI.TipoFuenteInformacion
			INNER JOIN TipoFuente TF ON TF.TipoFuente = TFI.TipoFuente AND TF.TipoFuente = 1 
			INNER JOIN Etiqueta E ON E.TipoFuenteInformacion = TFI.TipoFuenteInformacion
			INNER JOIN StatusConceptoEtiqueta SCE ON SCE.Etiqueta = E.Etiqueta
			INNER JOIN StatusConcepto SC ON SC.StatusConcepto = SCE.StatusConcepto			
			INNER JOIN '+ @Servidor+@BD+'.dbo.CuentaContableBanco CCB ON CCB.NumeroCuenta COLLATE Latin1_General_CI_AS =C.CuentaBancoFinanciero COLLATE Latin1_General_CI_AS
			INNER JOIN '+ @Servidor+@BD+'.dbo.Banco B ON CCB.Banco=B.Banco
													AND B.Banco = E.BancoFinanciero			
			WHERE C.CorporativoConciliacion='+CAST(@CorporativoConciliacion AS Varchar) +
			' AND C.SucursalConciliacion='+CAST(@SucursalConciliacion AS Varchar)+
			' AND C.AñoConciliacion='+CAST(@AñoConciliacion AS Varchar)+
			' AND C.MesConciliacion='+CAST(@MesConciliacion AS Varchar)+
			' AND C.FolioConciliacion='+CAST(@FolioConciliacion AS Varchar)+
			' AND SC.StatusConcepto='+CAST(@StatusConcepto AS Varchar)+
			
			' UNION ALL 
			
			SELECT E.Etiqueta, E.Decripcion as EtiquetaStr, 0 as Banco, ''SIN BANCO'' as BancoStr, E.TipoFuenteInformacion, TFI.Descripcion as TipoFuenteInformacionStr, E.TipoDato
			FROM Conciliacion C 
			INNER JOIN ConciliacionArchivo CA ON C.CorporativoConciliacion = CA.CorporativoConciliacion
											 AND C.SucursalConciliacion = CA.SucursalConciliacion
											 AND C.AñoConciliacion = CA.AñoConciliacion
											 AND C.MesConciliacion = CA.MesConciliacion
											 AND C.FolioConciliacion = CA.FolioConciliacion
			INNER JOIN TablaDestino TD ON CA.CorporativoInterno = TD.Corporativo 
									  AND CA.SucursalInterno= TD.Sucursal
									  AND CA.AñoInterno = TD.Año
									  AND CA.FolioInterno = TD.Folio
			INNER JOIN TipoFuenteInformacion TFI ON TD.TipoFuenteInformacion = TFI.TipoFuenteInformacion AND TFI.TipoFuenteInformacion =3
			INNER JOIN TipoFuente TF ON TF.TipoFuente = TFI.TipoFuente AND TF.TipoFuente = 1 
			INNER JOIN TablaDestinoDetalle TDD ON TD.Corporativo = TDD.Corporativo
											  AND TD.Sucursal = TDD.Sucursal
											  AND TD.Año = TDD.Año
											  AND TD.Folio = TDD.Folio						  
			INNER JOIN StatusConcepto SC ON SC.StatusConcepto = TDD.StatusConcepto
			INNER JOIN StatusConceptoEtiqueta SCE ON SCE.StatusConcepto = SC.StatusConcepto
			INNER JOIN Etiqueta E ON SCE.Etiqueta = E.Etiqueta 
								 AND E.TipoFuenteInformacion = TFI.TipoFuenteInformacion			
			WHERE C.CorporativoConciliacion='+CAST(@CorporativoConciliacion AS Varchar) +
			' AND C.SucursalConciliacion='+CAST(@SucursalConciliacion AS Varchar)+
			' AND C.AñoConciliacion='+CAST(@AñoConciliacion AS Varchar)+
			' AND C.MesConciliacion='+CAST(@MesConciliacion AS Varchar)+
			' AND C.FolioConciliacion='+CAST(@FolioConciliacion AS Varchar)+
			' AND SC.StatusConcepto='+CAST(@StatusConcepto AS Varchar)+
			' GROUP BY E.Etiqueta,E.Decripcion, E.TipoFuenteInformacion, TFI.Descripcion, E.TipoDato'

--SELECT (@Cadena)
EXEC(@Cadena)  

SET TRANSACTION ISOLATION LEVEL READ COMMITTED   
  

sp_helptext spCBConsultaEtiquetas


/*******************************************  
Autor: Gabina Leon 
Fecha: 29/05/2013
Consulta la informacion de las etiquetas 
spCBConsultaEtiquetas 1,2,1,2013,2,1,21
********************************************/  
CREATE PROCEDURE spCBConsultaEtiquetas

@Configuracion TinyInt,   
@CorporativoConciliacion TinyInt, 
@SucursalConciliacion Int,
@AñoConciliacion Int,
@MesConciliacion TinyInt,
@FolioConciliacion Int, 
@StatusConcepto Int

AS    
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED       

DECLARE @Cadena as Varchar(4000)  
DECLARE @Servidor as Varchar(100)
DECLARE @BD as Varchar(100)

SET @Servidor = (SELECT Valor FROM Parametro WHERE Modulo=30 AND Parametro ='ServidorFinanciero' AND Corporativo=@CorporativoConciliacion)      
SET @BD = (SELECT Valor FROM Parametro WHERE Modulo=30 AND Parametro ='BaseDatosFinanciero' AND Corporativo=@CorporativoConciliacion)      

IF @Servidor <>''  
     SET @Servidor = '['+@Servidor+'].'  


IF @Configuracion = 0 -- EXTERNOS

			SET @Cadena = 
			'SELECT E.Etiqueta, E.Decripcion as EtiquetaStr, E.BancoFinanciero as Banco, B.Descripcion as BancoStr, E.TipoFuenteInformacion, TFI.Descripcion as TipoFuenteInformacionStr, E.TipoDato
			FROM Conciliacion C 
			INNER JOIN TablaDestino TD ON C.CorporativoExterno = TD.Corporativo 
									  AND C.SucursalExterno= TD.Sucursal
									  AND C.AñoExterno = TD.Año
									  AND C.FolioExterno = TD.Folio
			INNER JOIN TipoFuenteInformacion TFI ON TD.TipoFuenteInformacion = TFI.TipoFuenteInformacion
			INNER JOIN TipoFuente TF ON TF.TipoFuente = TFI.TipoFuente AND TF.TipoFuente = 2 
			INNER JOIN Etiqueta E ON E.TipoFuenteInformacion = TFI.TipoFuenteInformacion
			INNER JOIN StatusConceptoEtiqueta SCE ON SCE.Etiqueta = E.Etiqueta
			INNER JOIN StatusConcepto SC ON SC.StatusConcepto = SCE.StatusConcepto			
			INNER JOIN '+ @Servidor+@BD+'.dbo.CuentaContableBanco CCB ON CCB.NumeroCuenta COLLATE Latin1_General_CI_AS=C.CuentaBancoFinanciero COLLATE Latin1_General_CI_AS 
			INNER JOIN '+ @Servidor+@BD+'.dbo.Banco B ON CCB.Banco=B.Banco
													AND B.Banco=E.BancoFinanciero
			WHERE C.CorporativoConciliacion='+CAST(@CorporativoConciliacion AS Varchar) +
			' AND C.SucursalConciliacion='+CAST(@SucursalConciliacion AS Varchar)+
			' AND C.AñoConciliacion='+CAST(@AñoConciliacion AS Varchar)+
			' AND C.MesConciliacion='+CAST(@MesConciliacion AS Varchar)+
			' AND C.FolioConciliacion='+CAST(@FolioConciliacion AS Varchar)+
			' AND SC.StatusConcepto='+CAST(@StatusConcepto AS Varchar)+
			' GROUP BY E.Etiqueta, E.Decripcion,E.BancoFinanciero, B.Descripcion, E.TipoFuenteInformacion, TFI.Descripcion, E.TipoDato'

IF @Configuracion = 1 -- INTERNOS

			SET @Cadena = 
			'SELECT E.Etiqueta, E.Decripcion as EtiquetaStr, E.BancoFinanciero as Banco, B.Descripcion as BancoStr, E.TipoFuenteInformacion, TFI.Descripcion as TipoFuenteInformacionStr, E.TipoDato
			FROM Conciliacion C 
			INNER JOIN ConciliacionArchivo CA ON C.CorporativoConciliacion = CA.CorporativoConciliacion
											 AND C.SucursalConciliacion = CA.SucursalConciliacion
											 AND C.AñoConciliacion = CA.AñoConciliacion
											 AND C.MesConciliacion = CA.MesConciliacion
											 AND C.FolioConciliacion = CA.FolioConciliacion
			INNER JOIN TablaDestino TD ON CA.CorporativoInterno = TD.Corporativo 
									  AND CA.SucursalInterno= TD.Sucursal
									  AND CA.AñoInterno = TD.Año
									  AND CA.FolioInterno = TD.Folio
			INNER JOIN TipoFuenteInformacion TFI ON TD.TipoFuenteInformacion = TFI.TipoFuenteInformacion
			INNER JOIN TipoFuente TF ON TF.TipoFuente = TFI.TipoFuente AND TF.TipoFuente = 1 
			INNER JOIN Etiqueta E ON E.TipoFuenteInformacion = TFI.TipoFuenteInformacion
			INNER JOIN StatusConceptoEtiqueta SCE ON SCE.Etiqueta = E.Etiqueta
			INNER JOIN StatusConcepto SC ON SC.StatusConcepto = SCE.StatusConcepto			
			INNER JOIN '+ @Servidor+@BD+'.dbo.CuentaContableBanco CCB ON CCB.NumeroCuenta COLLATE Latin1_General_CI_AS =C.CuentaBancoFinanciero COLLATE Latin1_General_CI_AS
			INNER JOIN '+ @Servidor+@BD+'.dbo.Banco B ON CCB.Banco=B.Banco
													AND B.Banco = E.BancoFinanciero			
			WHERE C.CorporativoConciliacion='+CAST(@CorporativoConciliacion AS Varchar) +
			' AND C.SucursalConciliacion='+CAST(@SucursalConciliacion AS Varchar)+
			' AND C.AñoConciliacion='+CAST(@AñoConciliacion AS Varchar)+
			' AND C.MesConciliacion='+CAST(@MesConciliacion AS Varchar)+
			' AND C.FolioConciliacion='+CAST(@FolioConciliacion AS Varchar)+
			' AND SC.StatusConcepto='+CAST(@StatusConcepto AS Varchar)+
			
			' UNION ALL 
			
			SELECT E.Etiqueta, E.Decripcion as EtiquetaStr, 0 as Banco, ''SIN BANCO'' as BancoStr, E.TipoFuenteInformacion, TFI.Descripcion as TipoFuenteInformacionStr, E.TipoDato
			FROM Conciliacion C 
			INNER JOIN ConciliacionArchivo CA ON C.CorporativoConciliacion = CA.CorporativoConciliacion
											 AND C.SucursalConciliacion = CA.SucursalConciliacion
											 AND C.AñoConciliacion = CA.AñoConciliacion
											 AND C.MesConciliacion = CA.MesConciliacion
											 AND C.FolioConciliacion = CA.FolioConciliacion
			INNER JOIN TablaDestino TD ON CA.CorporativoInterno = TD.Corporativo 
									  AND CA.SucursalInterno= TD.Sucursal
									  AND CA.AñoInterno = TD.Año
									  AND CA.FolioInterno = TD.Folio
			INNER JOIN TipoFuenteInformacion TFI ON TD.TipoFuenteInformacion = TFI.TipoFuenteInformacion AND TFI.TipoFuenteInformacion =3
			INNER JOIN TipoFuente TF ON TF.TipoFuente = TFI.TipoFuente AND TF.TipoFuente = 1 
			INNER JOIN TablaDestinoDetalle TDD ON TD.Corporativo = TDD.Corporativo
											  AND TD.Sucursal = TDD.Sucursal
											  AND TD.Año = TDD.Año
											  AND TD.Folio = TDD.Folio						  
			INNER JOIN StatusConcepto SC ON SC.StatusConcepto = TDD.StatusConcepto
			INNER JOIN StatusConceptoEtiqueta SCE ON SCE.StatusConcepto = SC.StatusConcepto
			INNER JOIN Etiqueta E ON SCE.Etiqueta = E.Etiqueta 
								 AND E.TipoFuenteInformacion = TFI.TipoFuenteInformacion			
			WHERE C.CorporativoConciliacion='+CAST(@CorporativoConciliacion AS Varchar) +
			' AND C.SucursalConciliacion='+CAST(@SucursalConciliacion AS Varchar)+
			' AND C.AñoConciliacion='+CAST(@AñoConciliacion AS Varchar)+
			' AND C.MesConciliacion='+CAST(@MesConciliacion AS Varchar)+
			' AND C.FolioConciliacion='+CAST(@FolioConciliacion AS Varchar)+
			' AND SC.StatusConcepto='+CAST(@StatusConcepto AS Varchar)+
			' GROUP BY E.Etiqueta,E.Decripcion, E.TipoFuenteInformacion, TFI.Descripcion, E.TipoDato'

--SELECT (@Cadena)
EXEC(@Cadena)  

SET TRANSACTION ISOLATION LEVEL READ COMMITTED   
  
sp_helptext  spCBConciliarArchivosPorReferencia 


/********************************************************
Realizo: Gabina Leon Velasco 
Fecha: 23/04/2013
Descripcion: Busca coincidentes para conciliar los archivos 
por referencia 
Prueba:
spCBConciliarArchivosPorReferencia 2,1,2013,4,5,1,.99,'caja','caja',1,'',''
*********************************************************/

CREATE PROCEDURE spCBConciliarArchivosPorReferencia 

@CorporativoConciliacion as SmallInt, 
@SucursalConciliacion as SmallInt, 
@AñoConciliacion as SmallInt, 
@MesConciliacion as SmallInt, 
@FolioConciliacion as Int,
@Dias as SmallInt,
@Centavos as Decimal(10,2),
@CampoExterno as Varchar(50),
@CampoInterno as Varchar(50),
@StatusConcepto as Int, 
@CadenaExterno as Varchar(1000),
@CadenaInterno as Varchar(1000)

AS  

SET NOCOUNT ON  

CREATE TABLE #CE
(CorporativoConciliacion Smallint,
SucursalConciliacion Smallint,
AñoConciliacion	int,
MesConciliacion Smallint,
FolioConciliacion int,
Corporativo	Smallint,
Sucursal Smallint,	
Año	int,
Folio int,
Secuencia Int,
CuentaBancaria Varchar(20),
FOperacion DateTime,
FMovimiento DateTime,
Referencia Varchar(500),
Descripcion Varchar(500),
Transaccion Varchar(500),
SucursalBancaria Varchar(20), 
Deposito Decimal(10,2),
Retiro Decimal(10,2),
SaldoInicial Decimal(10,2),
SaldoFinal Decimal(10,2),
Movimiento Varchar(500),
CuentaTercero Varchar(20),
Cheque Varchar(20),
RFCTercero Varchar(20), 
NombreTercero Varchar(100),
ClabeTercero Varchar(20),
Concepto Varchar(500),
Poliza Varchar(20),
Caja Smallint,
StatusConcepto Smallint,
ConceptoBanco SmallInt,
MotivoNoConciliado SmallInt)

CREATE TABLE #CI
(Corporativo Smallint,
Sucursal Smallint,	
Año	int,
Folio int,
Secuencia Int,
CuentaBancaria Varchar(20),
FOperacion DateTime,
FMovimiento DateTime,
Referencia Varchar(500),
Descripcion Varchar(500),
Transaccion Varchar(500),
SucursalBancaria Varchar(20), 
Deposito Decimal(10,2),
Retiro Decimal(10,2),
SaldoInicial Decimal(10,2),
SaldoFinal Decimal(10,2),
Movimiento Varchar(500),
CuentaTercero Varchar(20),
Cheque Varchar(20),
RFCTercero Varchar(20), 
NombreTercero Varchar(100),
ClabeTercero Varchar(20),
Concepto Varchar(500),
Poliza Varchar(20),
Caja Smallint,
StatusConcepto Smallint,
ConceptoBanco SmallInt,
MotivoNoConciliado SmallInt)


CREATE TABLE #C
(CorporativoConciliacion SmallInt, 
SucursalConciliacion SmallInt, 
AñoConciliacion SmallInt,
MesConciliacion SmallInt,  
FolioConciliacion SmallInt,
SucursalInterno SmallInt, 
FolioInterno SmallInt,
SecuenciaInterno Int, 
MontoInterno Decimal(10,2),
ConceptoInterno Varchar(500),
FOperacionInt DateTime,
FMovimientoInt DateTime,
SucursalExt SmallInt, 
FolioExt SmallInt,
SecuenciaExt Int, 	
ConceptoExt Varchar(500),
FOperacionExt DateTime,
FMovimientoExt DateTime,	
MontoConciliado Decimal(10,2),
Diferencia Decimal(10,2),
FormaConciliacion SmallInt, 
StatusConcepto SmallInt,
StatusConciliacion Varchar(100),
ChequeInt Varchar(100),
ReferenciaInt Varchar(500),
DescripcionInt Varchar(500),
NombreTerceroInt Varchar(100),
RFCTerceroInt Varchar(20),
DepositoInt decimal (10,2),
RetiroInt decimal (10,2),
ChequeExt Varchar(100),
ReferenciaExt Varchar(500),
DescripcionExt Varchar(500),
NombreTerceroExt Varchar(100),
RFCTerceroExt Varchar(20),
DepositoExt decimal (10,2),
RetiroExt decimal (10,2))

DECLARE @Cadena as Varchar(8000)

SET @Cadena = 
'INSERT INTO #CE
SELECT C.CorporativoConciliacion,C.SucursalConciliacion,C.AñoConciliacion,C.MesConciliacion,C.FolioConciliacion,
TDD.Corporativo,TDD.Sucursal,TDD.Año,TDD.Folio,TDD.Secuencia,TDD.CuentaBancaria,TDD.FOperacion,TDD.FMovimiento,TDD.Referencia,TDD.Descripcion,
TDD.Transaccion,TDD.SucursalBancaria,TDD.Deposito,TDD.Retiro,TDD.SaldoInicial,TDD.SaldoFinal,TDD.Movimiento,TDD.CuentaTercero,TDD.Cheque,TDD.RFCTercero,
TDD.NombreTercero,TDD.ClabeTercero,TDD.Concepto,TDD.Poliza,TDD.Caja, ISNULL(TDD.StatusConcepto,TFI.StatusConcepto),TDD.ConceptoBanco,TDD.MotivoNoConciliado
FROM TablaDestinoDetalle TDD
INNER JOIN TablaDestino TD ON TD.Corporativo=TDD.Corporativo AND TD.Sucursal=TDD.Sucursal AND TD.Año=TDD.Año AND TD.Folio=TDD.Folio
INNER JOIN TipoFuenteInformacion TFI ON TFI.TipoFuenteInformacion=TD.TipoFuenteInformacion
INNER JOIN TipoFuente TF ON TF.TipoFuente=TFI.TipoFuente AND TF.TipoFuente=2
INNER JOIN Conciliacion C ON TD.Corporativo=C.CorporativoExterno AND TD.Sucursal=C.SucursalExterno AND TD.Año=C.AñoExterno AND TD.Folio=C.FolioExterno
LEFT JOIN ConciliacionReferencia CR ON CR.CorporativoExterno=TDD.Corporativo AND CR.SucursalExterno=TDD.Sucursal AND CR.AñoExterno=TDD.Año AND CR.FolioExterno=TDD.Folio AND CR.SecuenciaExterno=TDD.Secuencia AND C.CorporativoConciliacion=CR.CorporativoConc
iliacion AND C.SucursalConciliacion=CR.SucursalConciliacion AND C.AñoConciliacion=CR.AñoConciliacion AND C.FolioConciliacion=CR.FolioConciliacion AND CR.StatusConciliacion=''CONCILIADA''
LEFT JOIN ConciliacionPedido CP ON CP.CorporativoExterno=TDD.Corporativo AND CP.SucursalExterno=TDD.Sucursal AND CP.AñoExterno=TDD.Año AND CP.FolioExterno=TDD.Folio AND CP.SecuenciaExterno=TDD.Secuencia AND C.CorporativoConciliacion=CP.CorporativoConcilia
cion AND C.SucursalConciliacion=CP.SucursalConciliacion AND C.AñoConciliacion=CP.AñoConciliacion AND C.FolioConciliacion=CP.FolioConciliacion AND CP.StatusConciliacion=''CONCILIADA''
WHERE C.CorporativoConciliacion='+CAST(@CorporativoConciliacion AS Varchar)+
   ' AND C.SucursalConciliacion='+CAST(@SucursalConciliacion AS Varchar)  +
   ' AND C.AñoConciliacion='+CAST(@AñoConciliacion AS Varchar) +
   ' AND C.MesConciliacion='+CAST(@MesConciliacion AS Varchar) + 
   ' AND C.FolioConciliacion='+CAST(@FolioConciliacion AS Varchar) + 
   ' AND CR.SecuenciaExterno IS NULL AND CP.SecuenciaExterno IS NULL'

IF @StatusConcepto > 0 
SET @Cadena = @Cadena + --' AND TDD.StatusConcepto= ' + CAST(@StatusConcepto as Varchar)+' '+
						@CadenaExterno

SET @Cadena = @Cadena +' INSERT INTO #CI
SELECT TDD.Corporativo,TDD.Sucursal,TDD.Año, TDD.Folio, TDD.Secuencia,TDD.CuentaBancaria,TDD.FOperacion,TDD.FMovimiento,TDD.Referencia,TDD.Descripcion,
TDD.Transaccion,TDD.SucursalBancaria,TDD.Deposito,TDD.Retiro,TDD.SaldoInicial,TDD.SaldoFinal,TDD.Movimiento,TDD.CuentaTercero,TDD.Cheque,TDD.RFCTercero,
TDD.NombreTercero,TDD.ClabeTercero,TDD.Concepto,TDD.Poliza,TDD.Caja,ISNULL(TDD.StatusConcepto,TFI.StatusConcepto),TDD.ConceptoBanco,TDD.MotivoNoConciliado
FROM TablaDestinoDetalle TDD
INNER JOIN TablaDestino TD ON TD.Corporativo=TDD.Corporativo AND TD.Sucursal=TDD.Sucursal AND TD.Año=TDD.Año AND TD.Folio=TDD.Folio
INNER JOIN TipoFuenteInformacion TFI ON TFI.TipoFuenteInformacion=TD.TipoFuenteInformacion
INNER JOIN TipoFuente TF ON TF.TipoFuente=TFI.TipoFuente AND TF.TipoFuente=1
INNER JOIN ConciliacionArchivo CA ON CA.CorporativoInterno=TD.Corporativo AND CA.SucursalInterno=TD.Sucursal AND CA.AñoInterno=TD.Año AND CA.FolioInterno=TD.Folio
INNER JOIN Conciliacion C ON CA.CorporativoConciliacion=C.CorporativoConciliacion AND CA.SucursalConciliacion= C.SucursalConciliacion AND CA.AñoConciliacion=C.AñoConciliacion AND CA.MesConciliacion=C.MesConciliacion AND CA.FolioConciliacion=C.FolioConcili
acion
LEFT JOIN ConciliacionReferencia CR ON CR.CorporativoInterno=TDD.Corporativo AND CR.SucursalInterno=TDD.Sucursal AND CR.AñoInterno=TDD.Año AND CR.FolioInterno=TDD.Folio AND CR.SecuenciaInterno=TDD.Secuencia AND C.CorporativoConciliacion=CR.CorporativoConc
iliacion AND C.SucursalConciliacion=CR.SucursalConciliacion AND C.AñoConciliacion=CR.AñoConciliacion AND C.FolioConciliacion=CR.FolioConciliacion AND CR.StatusConciliacion=''CONCILIADA''
WHERE C.CorporativoConciliacion='+CAST (@CorporativoConciliacion AS Varchar)+
   ' AND C.SucursalConciliacion='+CAST(@SucursalConciliacion AS Varchar)  +
   ' AND C.AñoConciliacion='+CAST(@AñoConciliacion AS Varchar) +
   ' AND C.MesConciliacion='+CAST(@MesConciliacion AS Varchar) + 
   ' AND C.FolioConciliacion='+CAST(@FolioConciliacion AS Varchar) + 
   ' AND CR.SecuenciaInterno IS NULL '
IF @StatusConcepto > 0 
SET @Cadena = @Cadena +-- ' AND TDD.StatusConcepto= ' + CAST(@StatusConcepto as Varchar)+' '+
						@CadenaInterno

SET @Cadena = @Cadena +'
DECLARE @P Int 
DECLARE @U Int
SET @P=(SELECT MIN(Secuencia)FROM #CE)
SET @U=(SELECT MAX(Secuencia)FROM #CE)
WHILE @P<=@U
BEGIN
IF ((SELECT COUNT(*)FROM #CI I INNER JOIN #CE E ON (E.FOperacion BETWEEN I.FOperacion-'+CAST(@Dias as Varchar)+' AND I.FOperacion+'+CAST(@Dias as Varchar)+
	' OR E.FMovimiento BETWEEN I.FMovimiento-'+CAST(@Dias as Varchar)+' AND I.FMovimiento+'+CAST(@Dias as Varchar)+			
	') AND E.Deposito BETWEEN I.Deposito-'+CAST(@Centavos AS varchar)+' AND I.Deposito+'+CAST(@Centavos as Varchar)+
	' AND E.Retiro BETWEEN I.Retiro-'+CAST(@Centavos AS varchar)+' AND I.Retiro+'+CAST(@Centavos as Varchar)+
	--') AND (E.Retiro+E.Deposito) BETWEEN (I.Retiro+I.Deposito)-'+CAST(@Centavos AS varchar)+'AND (I.Retiro+I.Deposito)+ '+CAST(@Centavos AS varchar)+
	' AND E.'+@CampoExterno+ '=I.'+@CampoInterno+' WHERE E.Secuencia= @P 
	AND I.Secuencia NOT IN(SELECT SecuenciaInterno FROM #C))=1)
BEGIN 
INSERT INTO #C
SELECT E.CorporativoConciliacion,E.SucursalConciliacion,E.AñoConciliacion,E.MesConciliacion,E.FolioConciliacion,I.Sucursal,I.Folio,I.Secuencia,
I.Deposito+I.Retiro,I.Concepto,I.FOperacion,I.FMovimiento,E.Sucursal,E.Folio,E.Secuencia,E.Concepto,E.Foperacion,E.FMovimiento,(E.Deposito+E.Retiro),
((E.Deposito+E.Retiro )-(I.Deposito+I.Retiro)),2,E.StatusConcepto,''EN PROCESO DE CONCILIACION'',
I.Cheque,I.Referencia,I.Descripcion,I.NombreTercero,I.RFCTercero ,I.Deposito,I.Retiro,E.Cheque,E.Referencia, E.Descripcion,E.NombreTercero,E.RFCTercero,E.Deposito,E.Retiro
FROM #CI I INNER JOIN #CE E ON (E.FOperacion BETWEEN I.FOperacion-'+CAST(@Dias as Varchar)+' AND I.FOperacion+'+CAST(@Dias AS Varchar)+
' OR E.FMovimiento BETWEEN I.FMovimiento-'+CAST(@Dias as Varchar)+' AND I.FMovimiento+'+CAST(@Dias as Varchar)+	
') AND E.Deposito BETWEEN I.Deposito-'+CAST(@Centavos AS varchar)+' AND I.Deposito+'+CAST(@Centavos as Varchar)+
' AND E.Retiro BETWEEN I.Retiro-'+CAST(@Centavos AS varchar)+' AND I.Retiro+'+CAST(@Centavos as Varchar)+
--') AND (E.Retiro+E.Deposito) BETWEEN (I.Retiro+I.Deposito)-'+CAST(@Centavos AS varchar)+'AND (I.Retiro+I.Deposito)+ '+CAST(@Centavos AS varchar)+
' AND E.'+@CampoExterno+'=I.'+@CampoInterno+
' WHERE E.Secuencia=@P AND I.Secuencia NOT IN(SELECT SecuenciaInterno FROM #C)
END SET @P=@P+1 END'

EXEC (@Cadena)

SELECT C.CorporativoConciliacion,C.SucursalConciliacion,C.AñoConciliacion,C.MesConciliacion,C.FolioConciliacion,SucursalExt,SE.Descripcion SucursalExtDes,C.FolioExt,
C.SecuenciaExt,C.ConceptoExt,C.MontoConciliado,C.Diferencia,C.FormaConciliacion,C.StatusConcepto,C.StatusConciliacion,C.SucursalInterno,SI.Descripcion SucursalIntDes,
C.FolioInterno,C.SecuenciaInterno,C.ConceptoInterno,C.MontoInterno,C.FMovimientoInt,C.FOperacionInt,C.FMovimientoExt,C.FOperacionExt,C.ChequeInt ,C.ReferenciaInt,C.DescripcionInt,
C.NombreTerceroInt,C.RFCTerceroInt,C.DepositoInt,C.RetiroInt,C.ChequeExt,C.ReferenciaExt,C.DescripcionExt,C.NombreTerceroExt,C.RFCTerceroExt,C.DepositoExt,C.RetiroExt
FROM #C C
INNER JOIN Sucursal SI ON SI.Sucursal=C.SucursalInterno
INNER JOIN Sucursal SE ON SE.Sucursal=C.SucursalExt

--SELECT @Cadena
--SELECT LEN(@Cadena)
