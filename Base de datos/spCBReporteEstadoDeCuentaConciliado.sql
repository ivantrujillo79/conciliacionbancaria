ALTER PROCEDURE spCBReporteEstadoDeCuentaConciliado --'01/01/2017','01/02/2017'
    @FechaIni DATETIME ,
    @FechaFin DATETIME ,
    @Banco VARCHAR(150) = '' ,
    @CuentaBanco VARCHAR(150) = '' ,
    @Status VARCHAR(150) = '' ,
    @StatusConcepto VARCHAR(150) = ''
AS
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
    SET NOCOUNT ON;

    SELECT   CO.Nombre AS Corporativo ,
             S.Descripcion AS Sucursal ,
             YEAR(TDD.FOperacion) AS Año ,
             MONTH(TDD.FOperacion) AS Mes ,
             Td.CuentaBancoFinanciero ,
             TDD.ConsecutivoFlujo ,
             TDD.FOperacion AS Fecha ,
             TDD.Referencia ,
             TDD.Concepto ,
             TDD.Retiro AS Retiros ,
             TDD.Deposito AS Depositos ,
             TDD.SaldoFinal ,
             CASE WHEN COR.TipoCobro = 3 THEN CL.Nombre
                  WHEN TDD.MotivoNoConciliado IS NOT NULL THEN
                      CONVERT(
                          VARCHAR(150) ,
                          MNC.Descripcion + CR.ComentarioNoConciliado)
                  ELSE TDD.Concepto
             END AS ConceptoConciliado ,
             CASE WHEN COR.TipoCobro = 3 THEN CONVERT(VARCHAR(50), CL.Cliente)
                  WHEN TDD.MotivoNoConciliado IS NOT NULL THEN
                      CONVERT(
                          VARCHAR(150) ,
                          MNC.Descripcion + CR.ComentarioNoConciliado)
                  ELSE TDD.Referencia
             END AS DocumentoConciliado
    FROM     dbo.Conciliacion C
             JOIN dbo.TablaDestino Td ON Td.Corporativo = C.CorporativoExterno
                                         AND Td.Sucursal = C.SucursalExterno
                                         AND Td.Año = C.AñoExterno
                                         AND Td.Folio = C.FolioExterno
             JOIN dbo.TablaDestinoDetalle TDD ON TDD.Corporativo = Td.Corporativo
                                                 AND TDD.Sucursal = Td.Sucursal
                                                 AND TDD.Año = Td.Año
                                                 AND TDD.Folio = Td.Folio
             LEFT JOIN dbo.ConciliacionReferencia CR ON CR.CorporativoConciliacion = C.CorporativoConciliacion
                                                        AND CR.SucursalConciliacion = C.SucursalConciliacion
                                                        AND CR.AñoConciliacion = C.AñoConciliacion
                                                        AND CR.MesConciliacion = C.MesConciliacion
                                                        AND CR.FolioConciliacion = C.FolioConciliacion
                                                        AND CR.CorporativoExterno = TDD.Corporativo
                                                        AND CR.SucursalExterno = TDD.Sucursal
                                                        AND CR.AñoExterno = TDD.Año
                                                        AND CR.FolioExterno = TDD.Folio
                                                        AND CR.SecuenciaExterno = TDD.Secuencia
             LEFT JOIN dbo.StatusConcepto SC ON SC.StatusConcepto = CR.StatusConcepto
             JOIN dbo.TipoFuenteInformacion TFI ON TFI.TipoFuenteInformacion = Td.TipoFuenteInformacion
             JOIN dbo.TipoFuente TF ON TF.TipoFuente = TFI.TipoFuente
             JOIN [192.168.1.11].SigametFinancieroCorporativo.dbo.CuentaContableBanco CCB ON dbo.fnCBCharIntoTrim(
                                                                                                 '' ,
                                                                                                 ( dbo.fnCBCharIzqTrim(
                                                                                                       '0' ,
                                                                                                       CCB.NumeroCuenta))) = dbo.fnCBCharIntoTrim(
                                                                                                                                 '' ,
                                                                                                                                 ( dbo.fnCBCharIzqTrim(
                                                                                                                                       '0' ,
                                                                                                                                       Td.CuentaBancoFinanciero)))
             JOIN [192.168.1.11].SigametFinancieroCorporativo.dbo.Banco B ON B.Banco = CCB.Banco
             JOIN dbo.Corporativo CO ON CO.Corporativo = C.CorporativoConciliacion
             JOIN dbo.Sucursal S ON S.Sucursal = C.SucursalConciliacion
             LEFT JOIN dbo.MovimientoCajaFichaDeposito MCFD ON CONVERT(
                                                                   VARCHAR(150) ,
                                                                   MCFD.Folio) = CONVERT(
                                                                                     VARCHAR(150) ,
                                                                                     TDD.Referencia)
             LEFT JOIN Cobro COR ON COR.AñoCobro = MCFD.AñoCobro
                                    AND COR.Cobro = MCFD.Cobro
             LEFT JOIN Cliente CL ON CL.Cliente = COR.Cliente
             LEFT JOIN dbo.MotivoNoConciliado MNC ON MNC.MotivoNoConciliado = CR.MotivoNoConciliado
             LEFT JOIN dbo.ConciliacionFacturaManual CFM ON CFM.CorporativoConciliacion = C.CorporativoConciliacion
                                                            AND CFM.SucursalConciliacion = C.SucursalConciliacion
                                                            AND CFM.AñoConciliacion = C.AñoConciliacion
                                                            AND CFM.MesConciliacion = C.MesConciliacion
                                                            AND CFM.FolioConciliacion = C.FolioConciliacion
                                                            AND CFM.CorporativoExterno = TDD.Corporativo
                                                            AND CFM.SucursalExterno = TDD.Sucursal
                                                            AND CFM.AñoExterno = TDD.Año
                                                            AND CFM.FolioExterno = TDD.Folio
                                                            AND CFM.SecuenciaExterno = TDD.Secuencia
    WHERE    dbo.GetFecha(TDD.FOperacion)
             BETWEEN dbo.GetFecha(@FechaIni) AND dbo.GetFecha(@FechaFin)
             AND (   @Banco = 'TODOS'
                     OR @Banco = ''
                     OR B.Descripcion = LTRIM(RTRIM(@Banco)))
             AND (   @CuentaBanco = 'TODAS'
                     OR @CuentaBanco = ''
                     OR CCB.NumeroCuenta = CONVERT(
                                               VARCHAR(50) ,
                                               LTRIM(RTRIM(@CuentaBanco))))
             AND (   @Status = 'TODOS'
                     OR @Status = ''
                     OR CR.StatusConciliacion = @Status )
             AND (   @StatusConcepto = 'TODOS'
                     OR @StatusConcepto = ''
                     OR CR.StatusConcepto = @StatusConcepto )
    ORDER BY TDD.FOperacion ,
             TDD.Secuencia;