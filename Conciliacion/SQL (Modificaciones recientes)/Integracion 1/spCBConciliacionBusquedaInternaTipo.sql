USE [Sigamet]
GO
/****** Object:  StoredProcedure [dbo].[spCBConciliacionBusquedaInternaTipo]    Script Date: 10/24/2017 1:22:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************  
Realizo: Gabina Leon Velasco   
Fecha: 03/05/2013  
Descripcion: Busca los registros internos cuyo deposito/retiro sea menor   
al registro del archivo externo  
  
exec spCBConciliacionBusquedaInternaTipo @Configuracion=0,@CorporativoConciliacion=2,
@SucursalConciliacion=1,@AñoConciliacion=2013,  
@MesConciliacion=1,@FolioConciliacion=1,  
@Dias=5, @Diferencia=5, @SucursalInterno=1,@StatusConcepto=0,  
@Cadena='' , @Monto=287.96,@Deposito=1,@FMinima='01-01-2013',@FMaxima='31-01-2013' 
*********************************************************/  
  
ALTER PROCEDURE [dbo].[spCBConciliacionBusquedaInternaTipo]
  
@Configuracion SmallInt,  
@CorporativoConciliacion SmallInt,  
@SucursalConciliacion SmallInt,  
@AñoConciliacion SmallInt,  
@MesConciliacion SmallInt,  
@FolioConciliacion SmallInt,  
@Dias SmallInt,  
@Diferencia as Decimal (10,2),  
@SucursalInterno SmallInt,  
@StatusConcepto as Int ,  
@Cadena as Varchar (1000),
@Monto As Decimal(10,2),
@Deposito Bit,
@FMinima As DateTime,
@FMaxima As DateTime
  
AS    
SET NOCOUNT ON    
  

DECLARE @Query Varchar (8000)  
  
SET @Query =     
 ' SELECT   
 TDD.Corporativo, TDD.Sucursal, Sucursal.Descripcion as SucursalDes, TDD.Año, TDD.Folio, TDD.Secuencia, TDD.Descripcion,  
 TDD.Concepto, 6 As FormaConciliacion, ISNULL(TDD.StatusConcepto,TFI.StatusConcepto) StatusConcepto,   
 ISNULL(CR.StatusConciliacion, ''EN PROCESO DE CONCILIACION'') StatusConciliacion,   
 TDD.FMovimiento, TDD.FOperacion, TDD.Deposito as Monto,  
 TDD.Cheque,TDD.Referencia,TDD.NombreTercero,TDD.RFCTercero,TDD.Deposito,TDD.Retiro,   
 ISNULL(SCR.UbicacionIcono,''~/App_Themes/GasMetropolitanoSkin/Iconos/StatusConciliacion/ENPROCESO.png'') UbicacionIcono  
 FROM TablaDestinoDetalle TDD  
 INNER JOIN TablaDestino TD ON TD.Corporativo=TDD.Corporativo   
         AND TD.Sucursal=TDD.Sucursal   
         AND TD.Año=TDD.Año   
         AND TD.Folio=TDD.Folio  
 INNER JOIN TipoFuenteInformacion TFI ON TFI.TipoFuenteInformacion=TD.TipoFuenteInformacion  
 INNER JOIN TipoFuente TF ON TF.TipoFuente=TFI.TipoFuente AND TF.TipoFuente =1   
 INNER JOIN ConciliacionArchivo CA ON CA.CorporativoInterno=TD.Corporativo   
          AND CA.SucursalInterno=TD.Sucursal   
          AND CA.AñoInterno=TD.Año   
          AND CA.FolioInterno=TD.Folio  
 INNER JOIN Conciliacion C ON CA.CorporativoConciliacion=C.CorporativoConciliacion   
        AND CA.SucursalConciliacion= C.SucursalConciliacion   
        AND CA.AñoConciliacion=C.AñoConciliacion   
        AND CA.MesConciliacion=C.MesConciliacion   
        AND CA.FolioConciliacion=C.FolioConciliacion  
 LEFT JOIN ConciliacionReferencia CR ON CR.CorporativoInterno= TDD.Corporativo  
            AND CR.SucursalInterno = TDD.Sucursal  
            AND CR.AñoInterno = TDD.Año  
            AND CR.FolioInterno = TDD.Folio  
            AND CR.SecuenciaInterno = TDD.Secuencia  
            AND C.CorporativoConciliacion = CR.CorporativoConciliacion  
            AND C.SucursalConciliacion= CR.SucursalConciliacion  
            AND C.AñoConciliacion = CR.AñoConciliacion  
            AND C.FolioConciliacion = CR.FolioConciliacion              
 LEFT JOIN StatusConciliacion SCR ON SCR.StatusConciliacion = CR.StatusConciliacion             
 INNER JOIN Sucursal ON Sucursal.Sucursal = TDD.Sucursal              
 WHERE C.CorporativoConciliacion='+ CAST(@CorporativoConciliacion as VARCHAR) +  
  ' AND C.SucursalConciliacion='+CAST(@SucursalConciliacion as VARCHAR) +  
  ' AND C.AñoConciliacion='+CAST(@AñoConciliacion as VARCHAR) +  
  ' AND C.MesConciliacion='+CAST(@MesConciliacion as VARCHAR) +  
  ' AND C.FolioConciliacion='+CAST(@FolioConciliacion as VARCHAR) +  
  ' AND (CR.StatusConciliacion <> ''CONCILIADA'' OR CR.StatusConciliacion IS NULL )' +  
  ' AND Sucursal.Sucursal ='+ CAST(@SucursalInterno as VARCHAR) +
  
  ' AND (TDD.FOperacion BETWEEN CAST('''+CONVERT(VarChar(10),@FMinima-@Dias,102) +''' As DateTime) AND CAST('''+CONVERT(VarChar(10),@FMaxima-@Dias,102) +  ''' As DateTime)'+
  ' OR TDD.FMovimiento BETWEEN CAST('''+CONVERT(VarChar(10),@FMinima-@Dias,102) +''' As DateTime) AND CAST('''+CONVERT(VarChar(10),@FMaxima-@Dias,102) +''' As DateTime))'  
    
IF @Deposito=1 
      
  SET @Query = @Query+' AND TDD.Deposito BETWEEN '+CAST(@Monto- @Diferencia as Varchar) + ' AND '+CAST(@Monto+ @Diferencia as Varchar)+' AND TDD.Retiro = 0 '  
  
IF @Deposito= 0   
  
  SET @Query = @Query+' AND TDD.Retiro BETWEEN '+CAST(@Monto- @Diferencia as Varchar) + ' AND '+ CAST(@Monto+@Diferencia as Varchar)+' AND TDD.Deposito = 0 '  
  
IF @StatusConcepto>0   
   SET @Query = @Query+ @Cadena  
  
  
IF @Configuracion = 1 -- ConReferenciaMenores     
  SET @Query = @Query+' AND TDD.Referencia IS NOT NULL AND TDD.Referencia <> '''' '  
  
  
SET @Query = @Query +' ORDER BY TDD.Retiro,TDD.Deposito '  
  EXEC(@Query)  
  
  --SELECT @Query  
