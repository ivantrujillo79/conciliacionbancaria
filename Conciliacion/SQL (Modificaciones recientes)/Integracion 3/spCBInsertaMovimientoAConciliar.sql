SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
-- =============================================
-- Author: Jose Carlos Reyes  Ramos  
-- Create date: 12/12/2017
-- Description: Inserta en la tabla  MovimientoAConciliar
-- =============================================
CREATE PROCEDURE [dbo].[spCBInsertaMovimientoAConciliar]
    @FolioMovimiento INT ,
    @AñoMovimiento INT ,
    @TipoMovimientoAConciliar SMALLINT ,
    @EmpresaContable INT ,
    @Caja TINYINT ,
    @FOperacion DATETIME ,
    @TipoFicha INT ,
    @Consecutivo INT ,
    @TipoAplicacionIngreso TINYINT ,
    @ConsecutivoTipoAplicacion INT ,
    @Factura INT ,
    @AñoCobro SMALLINT ,
    @Cobro INT ,
    @Monto INT ,
    @StatusMovimiento VARCHAR(20) ,
    @FMovimiento DATETIME ,
    @StatusConciliacion VARCHAR(20) ,
    @FConciliacion DATETIME ,
    @CorporativoConciliacion TINYINT ,
    @SucursalConciliacion TINYINT ,
    @AñoConciliacion INT ,
    @MesConciliacion SMALLINT ,
    @FolioConciliacion INT ,
    @CorporativoExterno TINYINT ,
    @SucursalExterno TINYINT ,
    @AñoExterno INT ,
    @FolioExterno INT ,
    @SecuenciaExterno INT
AS
    SET XACT_ABORT, NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        INSERT  INTO dbo.MovimientoAConciliar
                ( FolioMovimiento ,
                  AñoMovimiento ,
                  TipoMovimientoAConciliar ,
                  EmpresaContable ,
                  Caja ,
                  FOperacion ,
                  TipoFicha ,
                  Consecutivo ,
                  TipoAplicacionIngreso ,
                  ConsecutivoTipoAplicacion ,
                  Factura ,
                  AñoCobro ,
                  Cobro ,
                  Monto ,
                  StatusMovimiento ,
                  FMovimiento ,
                  StatusConciliacion ,
                  FConciliacion ,
                  CorporativoConciliacion ,
                  SucursalConciliacion ,
                  AñoConciliacion ,
                  MesConciliacion ,
                  FolioConciliacion ,
                  CorporativoExterno ,
                  SucursalExterno ,
                  AñoExterno ,
                  FolioExterno ,
                  SecuenciaExterno
                )
        VALUES  ( @FolioMovimiento ,
                  @AñoMovimiento ,
                  @TipoMovimientoAConciliar ,
                  @EmpresaContable ,
                  @Caja ,
                  @FOperacion ,
                  @TipoFicha ,
                  @Consecutivo ,
                  @TipoAplicacionIngreso ,
                  @ConsecutivoTipoAplicacion ,
                  @Factura ,
                  @AñoCobro ,
                  @Cobro ,
                  @Monto ,
                  @StatusMovimiento ,
                  @FMovimiento ,
                  @StatusConciliacion ,
                  @FConciliacion ,
                  @CorporativoConciliacion ,
                  @SucursalConciliacion ,
                  @AñoConciliacion ,
                  @MesConciliacion ,
                  @FolioConciliacion ,
                  @CorporativoExterno ,
                  @SucursalExterno ,
                  @AñoExterno ,
                  @FolioExterno ,
                  @SecuenciaExterno
                );
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@trancount > 0
            ROLLBACK TRANSACTION;
        DECLARE @msg NVARCHAR(2048) = 'Error al  Insertar el Registro. '
            + ERROR_MESSAGE();  
        RAISERROR ( @msg, 16, 1);
        RETURN 1;
    END CATCH;

