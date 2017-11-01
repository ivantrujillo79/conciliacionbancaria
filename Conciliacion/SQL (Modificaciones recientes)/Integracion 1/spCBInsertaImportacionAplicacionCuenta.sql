-- =============================================
-- Author: Jose Carlos Reyes  Ramos  
-- Create date: 22/10/2017
-- Description: Inserta en la tabla  ImportacionAplicacionCuenta
-- =============================================
CREATE PROCEDURE spCBInsertaImportacionAplicacionCuenta
    @ImportacionAplicacion SMALLINT ,
    @CuentaBanco VARCHAR(20)
AS
    SET XACT_ABORT, NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        INSERT  INTO dbo.ImportacionAplicacionCuenta
                ( ImportacionAplicacion ,
                  CuentaBancoFinanciero
                )
        VALUES  ( @ImportacionAplicacion , -- ImportacionAplicacion - smallint
                  @CuentaBanco  -- CuentaBancoFinanciero - varchar(20)
                );
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@trancount > 0
            ROLLBACK TRANSACTION;
        DECLARE @msg NVARCHAR(2048) = 'Error al  Insertar el Registro. ';  
        RAISERROR ( @msg, 16, 1);
        RETURN 1;
    END CATCH;