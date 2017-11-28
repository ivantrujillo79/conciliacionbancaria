CREATE PROCEDURE [dbo].[spCBExtractorRobo] --1,1,'01-02-2014','01-02-2014','2080010861'        
    @Corporativo AS TINYINT ,  
    @Sucursal AS TINYINT ,  
    @FechaIni AS DATETIME ,  
    @FechaFin AS DATETIME ,  
    @CuentaBanco AS VARCHAR(20) = ''  
AS  
    SET NOCOUNT ON;              
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;         
    SET @FechaIni = CAST(FLOOR(CAST(@FechaIni AS FLOAT)) AS DATETIME);             
    SET @FechaFin = CAST(FLOOR(CAST(@FechaFin AS FLOAT)) AS DATETIME);         
  
    SELECT  TAI.CuentaContable AS CuentaBancaria ,  
            CCTFA.FOperacion AS Foperacion ,  
           CCTFA.FOperacion AS Fmovimiento ,      
           -- fd.Descripcion ,      
            TAI.Descripcion Descripcion ,  
            'CAJA ' + CAST(TAI.Caja AS VARCHAR) Referencia ,  
            CCTFA.Consecutivo Transaccion ,  
            CCTFA.Total Deposito ,  
            0 Retiro ,  
            0 SaldoInicial ,  
            0 SaldoFinal ,  
            CCTFA.Observaciones Movimiento ,  
            0 CuentaTercero ,  
            0 Cheque ,  
            0 RFCTercero ,  
            '' NombreTercero ,  
        --    '' NombreTercero ,      
            '' ClabeTercero ,  
            'CAJA ' + CAST(TAI.Caja AS VARCHAR) Concepto ,  
            '' Poliza ,  
            TAI.Caja IdCaja ,  
            3 AS IdstatusConcepto ,  
            'CONCILIACION ABIERTA' AS IdStatusConciliacion ,  
            0 AS IdConceptoBanco ,  
            0 AS IdMotivoNoConciliado  
   FROM    CorteCajaTipoFichaAplicacion CCTFA
            JOIN dbo.TipoAplicacionIngreso TAI ON TAI.TipoAplicacionIngreso = CCTFA.TipoAplicacionIngreso
			JOIN dbo.Caja C ON C.Caja = TAI.Caja
    WHERE   TAI.TipoAplicacionIngreso = 54
            AND CCTFA.Status = 'AUTORIZADO'
            AND  CCTFA.FOperacion BETWEEN @FechaIni AND @FechaFin