/*
Descripcion:Consulta de la Vista de los pagares en Conciliacion Bancaria.
Fecha de Creacion: 23/1/2017
AKER
*/

ALTER PROCEDURE spCBConsultaPagare --'01/11/2017','23/11/2017',1
    @FechaIni DATETIME = NULL ,
    @FechaFin DATETIME = NULL ,
    @Todos BIT = NULL
AS
    SET TRAN ISOLATION LEVEL READ UNCOMMITTED;
    SET NOCOUNT ON;		

    IF @Todos = 0
        BEGIN	
            SET @Todos = NULL;
        END;	


    SET @FechaFin = @FechaFin + '23:59:59';


    SELECT  CCTFA.Consecutivo AS FolioCorte ,
            CCTFA.FOperacion ,
            CCTFA.Caja ,
            CCTFA.Consecutivo ,
            TAI.Descripcion ,
            CCTFA.Total ,
            CCTFA.Observaciones
    FROM    CorteCajaTipoFichaAplicacion CCTFA
            JOIN dbo.TipoAplicacionIngreso TAI ON TAI.TipoAplicacionIngreso = CCTFA.TipoAplicacionIngreso
    WHERE   TAI.TipoAplicacionIngreso = 54
            AND CCTFA.Status = 'AUTORIZADO'
            AND ( ( @Todos IS NOT NULL )
                  OR CCTFA.FOperacion BETWEEN @FechaIni AND @FechaFin
                );


				