/*          
MODULO DE CONCILIACION          
Procedimiento para el reporte de movimientos de caja detallado          
base:spCBReporteMovimientoCajaDetalle
spCBReporteMovimientoCajaDetallePrueba 4,1,173,'25-05-2017'

*/          
          
ALTER PROCEDURE dbo.spCBReporteMovimientoCajaDetallePrueba      
          
@Consecutivo TINYINT,          
@Folio    INTEGER,          
@Caja    TINYINT,          
@FOperacion DATETIME          
          
AS          
          
SET NOCOUNT ON          
      
          
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED          
      
declare @Empresa as varchar(50)      
      
set @Empresa = dbo.NombreEmpresa()      
 SET @FOperacion= CAST(FLOOR(CAST(@FOperacion AS FLOAT)) AS DATETIME)
             
SELECT  @Empresa as Empresa,mc.Caja, mc.CajaDescripcion, mc.FOperacion, mc.Consecutivo, mc.Folio,          
        mc.FMovimiento, mc.Total as MovimientoCajaTotal, mc.Empleado, mc.EmpleadoNombre, mc.TipoMovimientoCaja,          
        mc.TipoMovimientoCajaDescripcion, mc.TipoMovimientoCajaAplicaVenta, mc.AfectaSaldoCredito, mc.AfectaSaldoContado, mc.MovimientoCajaStatus,          
        mc.Ruta, mc.RutaDescripcion, mc.FAlta, mc.Clave, mc.RutaCelula,          
        mc.Observaciones as MCObservaciones, co.*,          
        co.Total AS CobroTotal, tc.Descripcion AS TipoCobroDescripcion, tc.Descripcion + ' ' + ltrim(rtrim(Isnull(ba.Nombre,''))) + ' ' + ltrim(rtrim(Isnull(co.NumeroCheque,''))) AS DescripcionCompuesta,          
        cp.Celula, cp.AñoPed, cp.Pedido, cp.Total AS TotalAbono, pe.Total AS TotalCargo,          
        pe.Cliente AS PedidoCliente, pe.PedidoReferencia, SUBSTRING(cl.Nombre, 1, 30) Nombre , pe.FSuministro, pe.FCargo,          
        pe.Litros, pe.Saldo AS SaldoPedido, tcar.Descripcion AS TipoCargo, ba.Nombre as BancoNombre, bao.Nombre as BancoOrigenNombre,      
        dbo.CyCMontoSaldoAFavor_OtrosIngresos(co.SaldoAFavor, co.Saldo, 1) AS MontoSaldoAFavor,      
        dbo.CyCMontoSaldoAFavor_OtrosIngresos(co.SaldoAFavor, co.Saldo, 0) AS OtrosIngresos,      
        ISNULL('CUENTA ORIGEN: ' + RTRIM(LTRIM(bao.Nombre)) + ' ' + LTRIM(RTRIM(CO.NumeroCuentaDestino)), '') AS ObservacionesTransferencia,    
        USR.Nombre AS UsuarioCaptura,
		ISNULL(CAST(pe.Factura AS VARCHAR(100)),'SF') FolioFactura
FROM    vwMovimientoCaja1 AS mc JOIN MovimientoCajaCobro AS mcc        
                                  ON  mc.Caja = mcc.Caja          
                                  AND mc.FOperacion = mcc.FOperacion          
                                  AND mc.Consecutivo = mcc.Consecutivo          
                                  AND mc.Folio = mcc.Folio          
                                JOIN Cobro AS co    
                                  ON  mcc.AñoCobro = co.AñoCobro        
                                  AND mcc.Cobro = co.Cobro          
                                JOIN TipoCobro AS tc           
                                  ON  co.TipoCobro = tc.TipoCobro          
                           LEFT JOIN Banco AS ba          
                                  ON  co.Banco = ba.Banco        
                           LEFT JOIN Banco AS bao        
                                  ON  co.BancoOrigen = bao.Banco        
                                JOIN CobroPedido AS cp        
                                  ON  co.AñoCobro = cp.AñoCobro        
                                  AND co.Cobro = cp.Cobro          
                                JOIN Pedido AS pe        
                                  ON  cp.Celula = pe.Celula        
                                  AND cp.AñoPed = pe.AñoPed        
                                  AND cp.Pedido = pe.Pedido
								/*LEFT JOIN vwCFDFacturasEmitidas FAC                        
								  ON  pe.Celula = FAC.Celula                        
								  AND pe.AñoPed = FAC.AñoPed                    
                                  AND pe.Pedido = FAC.Pedido          
                                */
								JOIN Cliente AS cl    
                                  ON  pe.Cliente = cl.Cliente          
                                JOIN TipoCargo AS tcar        
                                  ON  pe.TipoCargo = tcar.TipoCargo    
                           LEFT JOIN Usuario USR    
                                  ON mc.UsuarioCaptura = USR.Usuario    
WHERE   mc.Caja = @Caja          
        AND mc.FOperacion = @FOperacion          
        AND mc.Consecutivo = @Consecutivo          
        AND mc.Folio = @Folio               

