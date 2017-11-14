CREATE PROCEDURE spJCREliminaEdoCuentaConciliacion
    @FolioExt INT ,
    @AñoExt INT ,
    @AñoConciliacion INT ,
    @MesConciliacion INT ,
    @FolioConciliacion INT
AS
    SET NOCOUNT ON; 

    SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
    DELETE  cp
    FROM    dbo.TablaDestino td
            JOIN dbo.TablaDestinoDetalle tdd ON tdd.Corporativo = td.Corporativo
                                                AND tdd.Sucursal = td.Sucursal
                                                AND tdd.Año = td.Año
                                                AND tdd.Folio = td.Folio
            JOIN dbo.ConciliacionPedido cp ON cp.CorporativoExterno = tdd.Corporativo
                                              AND cp.SucursalExterno = tdd.Sucursal
                                              AND cp.AñoExterno = tdd.Año
                                              AND cp.FolioExterno = tdd.Folio
                                              AND cp.SecuenciaExterno = tdd.Secuencia
    WHERE   td.Folio = @FolioExt
            AND td.Año = @AñoExt;



    DELETE  tdd
    FROM    dbo.TablaDestino td
            JOIN dbo.TablaDestinoDetalle tdd ON tdd.Corporativo = td.Corporativo
                                                AND tdd.Sucursal = td.Sucursal
                                                AND tdd.Año = td.Año
                                                AND tdd.Folio = td.Folio
    WHERE   td.Folio = @FolioExt
            AND td.Año = @AñoExt;


    DELETE  FROM dbo.ConciliacionReferencia
    WHERE   FolioConciliacion = @FolioConciliacion
            AND MesConciliacion = @MesConciliacion
            AND AñoConciliacion = @AñoConciliacion;

    DELETE  FROM dbo.Conciliacion
    WHERE   FolioConciliacion = @FolioConciliacion
            AND MesConciliacion = @MesConciliacion
            AND AñoConciliacion = @AñoConciliacion;



    DELETE  tdd
    FROM    dbo.TablaDestino td
    WHERE   td.Folio = @FolioExt
            AND td.Año = @AñoExt;