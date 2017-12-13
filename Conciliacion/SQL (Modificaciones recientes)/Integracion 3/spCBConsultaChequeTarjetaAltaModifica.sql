alter	 PROCEDURE [dbo].[spCBConsultaChequeTarjetaAltaModifica]
@Corporativo as Int, 
@Sucursal as Int, 
@Año as Int, 
@Mes as SmallInt, 
@Folio as Int 
----exec spCBConsultaChequeTarjetaAltaModifica 1,1,2014,5,154
--SELECT dbo.fnCBTotalMovimientoCaja (1,1,2014,5,154,'CONCILIADO',502313472)
--SELECT dbo.fnCBTotalMovimientoCaja (1,1,2014,5,154,'EXTERNO',502313472)
----select Cliente,* from Pedido where PedidoReferencia='20148147039'
AS  
SET NOCOUNT ON  
			
BEGIN

DECLARE @Servidor As VarChar(100)    
DECLARE @BD As VarChar(100)    
DECLARE @Financiero VarChar(1000)  
DECLARE @Cadena VarChar(8000)  
DECLARE @SigConsecutivo Int

SET @SigConsecutivo = (SELECT ISNULL(MAX(Cobro),0)+1 FROM Cobro WHERE AñoCobro = YEAR(GETDATE()))  
        
SET @Servidor = (SELECT Valor FROM Parametro WHERE Modulo=30 AND Parametro ='ServidorFinanciero' AND Corporativo=@Corporativo)        
SET @BD = (SELECT Valor FROM Parametro WHERE Modulo=30 AND Parametro ='BaseDatosFinanciero' AND Corporativo=@Corporativo)        
IF @Servidor <>''    
     SET @Servidor = '['+@Servidor+'].'    
   
SET @Financiero= @Servidor+@BD+'.dbo.'   

/* Modificacion:
Ses cambio "CAST(TDD.Cheque as Varchar(20))" por Consecutivo CAST((ROW_NUMBER() OVER(ORDER BY TDD.Cheque)) as Varchar(20))*/


SET @Cadena= 'SELECT YEAR(GETDATE()) AñoCobro,0 Cobro, 
TDD.Corporativo,TDD.Sucursal,TDD.Año,TDD.Folio,TDD.Secuencia,TDD.Deposito,TDD.Retiro,
CAST((ROW_NUMBER() OVER(ORDER BY TDD.Cheque)) as Varchar(20)) as NumeroCheque,
--dbo.fnCBTotalMovimientoCajaExterno2('+CAST(@Corporativo as Varchar(15))+','+CAST(@Sucursal as Varchar(15))+','+CAST(@Año as Varchar(15))+','+CAST(@Mes as Varchar(15))+','+CAST(@Folio as Varchar(15))+',''EXTERNO'',Cliente.Cliente,,TDD.CorporativoTDD.Sucursal,TDD.Año,TDD.Folio,TDD.Secuencia) AS Total, 
(TDD.Deposito+TDD.Retiro) as Total,
--dbo.fnCBTotalMovimientoCajaExterno2('+CAST(@Corporativo as Varchar(15))+','+CAST(@Sucursal as Varchar(15))+','+CAST(@Año as Varchar(15))+','+CAST(@Mes as Varchar(15))+','+CAST(@Folio as Varchar(15))+',''INTERNO'',Cliente.Cliente,TDD.Corporativo,TDD.Sucursal,TDD.Año,TDD.Folio,TDD.Secuencia) as MontoInterno,
--(dbo.fnCBTotalMovimientoCajaExterno2('+CAST(@Corporativo as Varchar(15))+','+CAST(@Sucursal as Varchar(15))+','+CAST(@Año as Varchar(15))+','+CAST(@Mes as Varchar(15))+','+CAST(@Folio as Varchar(15))+',''EXTERNO'',Cliente.Cliente,TDD.Corporativo,TDD.Sucursal,TDD.Año,TDD.Folio,TDD.Secuencia)
--(TDD.Deposito+TDD.Retiro)-dbo.fnCBTotalMovimientoCajaExterno2('+CAST(@Corporativo as Varchar(15))+','+CAST(@Sucursal as Varchar(15))+','+CAST(@Año as Varchar(15))+','+CAST(@Mes as Varchar(15))+','+CAST(@Folio as Varchar(15))+',''CONCILIADO'',Cliente.Cliente,TDD.Corporativo,TDD.Sucursal,TDD.Año,TDD.Folio,TDD.Secuencia) as Saldo,
 (CASE WHEN (TDD.Deposito+TDD.Retiro)>dbo.fnCBTotalMovimientoCajaExterno2('+CAST(@Corporativo as Varchar(15))+','+CAST(@Sucursal as Varchar(15))+','+CAST(@Año as Varchar(15))+','+CAST(@Mes as Varchar(15))+','+CAST(@Folio as Varchar(15))+',''INTERNO'',Cliente.Cliente,TDD.Corporativo,TDD.Sucursal,TDD.Año,TDD.Folio,TDD.Secuencia) THEN 
 (TDD.Deposito+TDD.Retiro)-dbo.fnCBTotalMovimientoCajaExterno2('+CAST(@Corporativo as Varchar(15))+','+CAST(@Sucursal as Varchar(15))+','+CAST(@Año as Varchar(15))+','+CAST(@Mes as Varchar(15))+','+CAST(@Folio as Varchar(15))+',''INTERNO'',Cliente.Cliente,TDD.Corporativo,TDD.Sucursal,TDD.Año,TDD.Folio,TDD.Secuencia) 
 ELSE (TDD.Deposito+TDD.Retiro)-dbo.fnCBTotalMovimientoCajaExterno2('+CAST(@Corporativo as Varchar(15))+','+CAST(@Sucursal as Varchar(15))+','+CAST(@Año as Varchar(15))+','+CAST(@Mes as Varchar(15))+','+CAST(@Folio as Varchar(15))+',''CONCILIADO'',Cliente.Cliente,TDD.Corporativo,TDD.Sucursal,TDD.Año,TDD.Folio,TDD.Secuencia)
 END) AS Saldo, TDD.CuentaTercero	AS NumeroCuenta,TD.CuentaBancoFinanciero AS  NumeroCuentaDestino, MIN(TDD.FMovimiento) as FCheque,Cliente.Cliente,  B.Banco ,  0 BancoOrigen,'''' Observaciones,''EMITIDO'' Status, 7 as TipoCobro, 1 as Alta, dbo.NombreUsuario() AS Usuario, 
 --( dbo.fnCBTotalMovimientoCajaExterno2('+CAST(@Corporativo as Varchar(15))+','+CAST(@Sucursal as Varchar(15))+','+CAST(@Año as Varchar(15))+','+CAST(@Mes as Varchar(15))+','+CAST(@Folio as Varchar(15))+',''EXTERNO'',Cliente.Cliente,TDD.Corporativo,TDD.Sucursal,TDD.Año,TDD.Folio,TDD.Secuencia)
 (TDD.Deposito+TDD.Retiro) -dbo.fnCBTotalMovimientoCajaExterno2('+CAST(@Corporativo as Varchar(15))+','+CAST(@Sucursal as Varchar(15))+','+CAST(@Año as Varchar(15))+','+CAST(@Mes as Varchar(15))+','+CAST(@Folio as Varchar(15))+',''INTERNO'',Cliente.Cliente,TDD.Corporativo,TDD.Sucursal,TDD.Año,TDD.Folio,TDD.Secuencia)  SaldoAFavor,
 ISNULL((CASE WHEN ISNUMERIC(TDD.SucursalBancaria)=1 THEN CONVERT(INT,SucursalBancaria) ELSE 0 END),0) AS SucursalBancaria,TDD.Descripcion,isnull(TDD.ClientePago,0) as ClientePago
FROM ConciliacionPedido CP
INNER JOIN Conciliacion C ON C.CorporativoConciliacion = CP.CorporativoConciliacion
						AND C.SucursalConciliacion= CP.SucursalConciliacion
						AND C.AñoConciliacion= CP.AñoConciliacion
						AND C.MesConciliacion = CP.MesConciliacion
						AND C.FolioConciliacion = CP.FolioConciliacion
INNER JOIN TablaDestinoDetalle TDD ON TDD.Corporativo = CP.CorporativoExterno
								AND TDD.Sucursal = CP.SucursalExterno
								AND TDD.Año = CP.AñoExterno
								AND TDD.Folio= CP.FolioExterno
								AND TDD.Secuencia = CP.SecuenciaExterno	
INNER JOIN TablaDestino TD ON TD.Corporativo = TDD.Corporativo
							AND TD.Sucursal = TDD.Sucursal
							AND TD.Año = TDD.Año
							AND TD.Folio= TDD.Folio	
INNER JOIN Pedido P ON P.Celula = CP.Celula
					AND P.AñoPed = CP.AñoPed
					AND P.Pedido= CP.Pedido
INNER JOIN Cliente ON Cliente.Cliente= dbo.fnCBMuestraClientePadre(P.Cliente)
INNER JOIN '+@Financiero+'CuentaContableBanco CCB ON CCB.NumeroCuenta COLLATE Latin1_General_CI_AS =C.CuentaBancoFinanciero COLLATE Latin1_General_CI_AS
INNER JOIN '+@Financiero+'Banco B ON CCB.Banco=B.Banco
WHERE C.CorporativoConciliacion =' +CAST(@Corporativo AS Varchar)+
'AND C.SucursalConciliacion='+ CAST(@Sucursal AS Varchar)+
'AND C.AñoConciliacion ='+CAST( @Año AS Varchar)+
'AND C.MesConciliacion ='+CAST( @Mes AS Varchar)+
'AND C.FolioConciliacion ='+CAST( @Folio AS Varchar)+
'AND CP.StatusConciliacion = ''CONCILIADA''
--AND CP.StatusMovimiento = ''PENDIENTE''
--AND C.StatusConciliacion = ''CONCILIACION CERRADA''
GROUP BY  
TDD.Corporativo,TDD.Sucursal,TDD.Año,TDD.Folio,TDD.Secuencia,TDD.Deposito,TDD.Retiro,
TDD.Cheque , TDD.CuentaTercero,TD.CuentaBancoFinanciero , Cliente.Cliente, B.Banco,  Cliente.TipoCobro,TDD.SucursalBancaria,TDD.Descripcion,ClientePago'

EXEC(@Cadena)
--select(@Cadena)

END



