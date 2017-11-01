/*******************************************    
Autor: Claudia García    
Fecha: 13/08/2013    
Cargar el combo de ImportacionAplicacion    
********************************************/    
CREATE PROCEDURE [dbo].[spCBImportacionAplicacionConfiguracion]
    @Configuracion AS SMALLINT ,
    @Sucursal AS TINYINT = 1,
    @CuentaBanco VARCHAR(20)
AS
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;             
--ImportacionAplicacion    
    IF @Configuracion = 0
        BEGIN    
            SELECT  ImportacionAplicacion.ImportacionAplicacion ,
                    ImportacionAplicacion.Descripcion ,
                    ImportacionAplicacion.TipoFuenteInformacion ,
                    ImportacionAplicacion.Procedimiento ,
                    ImportacionAplicacionOrigen.Servidor ,
                    ImportacionAplicacionOrigen.BaseDeDatos ,
                    Usuario = CASE ImportacionAplicacionOrigen.Servidor
                                WHEN '192.168.1.11' THEN 'ROPIMA'
                                ELSE Usuario.Usuario
                              END ,
                    ClaveEncriptada = CASE ImportacionAplicacionOrigen.Servidor
                                        WHEN '192.168.1.11'
                                        THEN ( SELECT   ClaveEncriptada
                                               FROM     Usuario
                                               WHERE    Usuario = 'ROPIMA'
                                             )
                                        ELSE Usuario.ClaveEncriptada
                                      END
            FROM    ImportacionAplicacion
                    INNER JOIN ImportacionAplicacionOrigen ON ImportacionAplicacion.ImportacionAplicacion = ImportacionAplicacionOrigen.ImportacionAplicacion
                    INNER JOIN dbo.ImportacionAplicacionCuenta IAC ON IAC.ImportacionAplicacion = ImportacionAplicacion.ImportacionAplicacion
                    INNER JOIN Usuario ON Usuario.Usuario = 'ROPIMA'
            WHERE   IAC.CuentaBancoFinanciero = @CuentaBanco
            AND ImportacionAplicacionOrigen.Sucursal = @Sucursal
                    AND ImportacionAplicacion.Status = 'ACTIVO';  
        END;    
    
    SET TRANSACTION ISOLATION LEVEL READ COMMITTED; 