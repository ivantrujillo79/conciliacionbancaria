-- =============================================
-- Author: Jose Carlos Reyes  Ramos  
-- Create date: 22/10/2017
-- Description: Elimina en la tabla  ImportacionAplicacionCuenta
-- =============================================
CREATE PROCEDURE spCBEliminaImportacionAplicacionCuenta
    @ImportacionAplicacion SMALLINT ,
    @CuentaBanco VARCHAR(20)
AS
    SET XACT_ABORT, NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        DELETE  FROM dbo.ImportacionAplicacionCuenta
        WHERE   ImportacionAplicacion = @ImportacionAplicacion
                AND CuentaBancoFinanciero = @CuentaBanco;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@trancount > 0
            ROLLBACK TRANSACTION;
        DECLARE @msg NVARCHAR(2048) = 'Error al Eliminar el Registro. ';  
        RAISERROR ( @msg, 16, 1);
        RETURN 1;
    END CATCH;