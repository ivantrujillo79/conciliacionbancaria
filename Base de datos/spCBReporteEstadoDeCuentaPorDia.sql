alter PROCEDURE spCBReporteEstadoDeCuentaPorDia-- '01/01/2017','01/12/2017'
    @FechaIni DATETIME ,
    @FechaFin DATETIME ,
    @Banco VARCHAR(50) = '' ,
    @CuentaBanco VARCHAR(50) = ''
AS
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
    SET NOCOUNT ON;


    SELECT   Co.Nombre AS Corporativo ,
             S.Descripcion AS Sucursal ,
             Td.CuentaBancoFinanciero ,
             TDD.FOperacion AS Fecha ,
             TDD.Retiro ,
             TDD.Deposito AS Depositos ,
             TDD.SaldoFinal
    FROM     dbo.Conciliacion C
             JOIN dbo.TablaDestino Td ON Td.Corporativo = C.CorporativoExterno
                                         AND Td.Sucursal = C.SucursalExterno
                                         AND Td.Año = C.AñoExterno
                                         AND Td.Folio = C.FolioExterno
             JOIN dbo.TablaDestinoDetalle TDD ON TDD.Corporativo = Td.Corporativo
                                                 AND TDD.Sucursal = Td.Sucursal
                                                 AND TDD.Año = Td.Año
                                                 AND TDD.Folio = Td.Folio
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
             JOIN dbo.Corporativo Co ON Co.Corporativo = C.CorporativoConciliacion
             JOIN dbo.Sucursal S ON S.Sucursal = C.SucursalConciliacion
    WHERE    dbo.GetFecha(TDD.FOperacion)
             BETWEEN dbo.GetFecha(@FechaIni) AND dbo.GetFecha(@FechaFin)
             AND (   @Banco = 'TODOS'
                     OR @Banco = ''
                     OR B.Descripcion = LTRIM(RTRIM(@Banco)))
             AND (   @CuentaBanco = 'TODAS'
                     OR @CuentaBanco = ''
                     OR CCB.NumeroCuenta = LTRIM(RTRIM(@CuentaBanco)))
    ORDER BY Td.CuentaBancoFinanciero ,
             TDD.FOperacion ,
             TDD.Secuencia;

