/*
Procedimiento para dar de alta los datos de una cobranza.
Autor: Santiago Mendoza Carlos Nirari
Fecha: 11/07/2017
*/
ALTER  PROCEDURE spCBGuardarCobranza
    @FCobranza DATETIME ,
    @UsuarioCaptura CHAR(15) ,
    @Total MONEY
AS
    SET NOCOUNT ON

    DECLARE @TipoCobranza TINYINT ,
        @Empleado INT ,
        @Observaciones VARCHAR(100) ,
        @Status CHAR(10) ,
        @SigCobranza INT


    SET @SigCobranza = ( SELECT ISNULL(MAX(Cobranza), 0) + 1
                         FROM   Cobranza
                       )
	
    SET @TipoCobranza = 10 
    SET @Observaciones = 'PROCESO REALIZADO DESDE EL MODULO DE CONCILIACION BANCARIA'
    SET @Empleado = ( SELECT    Empleado
                      FROM      dbo.Usuario
                      WHERE     Usuario = @UsuarioCaptura
                    )

    IF ( SELECT RTRIM(CAST(Tipo AS VARCHAR))
         FROM   TipoCobranza
         WHERE  TipoCobranza = @TipoCobranza
       ) = 'RESGUARDO'
        SET @Status = 'CERRADO'
    ELSE
        SET @Status = 'ABIERTO'

    INSERT  INTO Cobranza
            ( Cobranza ,
              TipoCobranza ,
              FCobranza ,
              UsuarioCaptura ,
              Empleado ,
              Total ,
              FAlta ,
              FActualizacion ,
              Status ,
              Observaciones ,
              CobranzaOrigen ,
              UsuarioEntrega ,
              FEntrega ,
              StatusEntrega
            )
    VALUES  ( @SigCobranza ,
              @TipoCobranza ,
              @FCobranza ,
              @UsuarioCaptura ,
              @Empleado ,
              @Total ,
              GETDATE() ,
              GETDATE() ,
              @Status ,
              @Observaciones ,
              NULL ,
              NULL ,
              NULL ,
              NULL
            )
		
    SELECT  @SigCobranza AS SigCobranza

