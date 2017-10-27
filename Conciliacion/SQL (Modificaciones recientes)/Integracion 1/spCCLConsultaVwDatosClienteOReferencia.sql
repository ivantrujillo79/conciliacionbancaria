CREATE PROCEDURE spCCLConsultaVwDatosClienteOReferencia @Cliente INTEGER
AS
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;      
     
    DECLARE @t TABLE
        (
          Cliente INT ,
          Nombre VARCHAR(100) ,
          Razonsocial VARCHAR(100) ,
          Celula INT ,
          Ruta INT ,
          Programacion BIT ,
          Telcasa VARCHAR(50) ,
          Telalterno1 VARCHAR(50) ,
          Telalterno2 VARCHAR(50) ,
          Saldo MONEY ,
          Email VARCHAR(254) ,
          Direccion VARCHAR(254)
        );          
    INSERT  INTO @t
            SELECT  Cliente ,
                    Nombre ,
                    ISNULL(RazonSocial, '-') AS RazonSocial ,
                    Celula ,
                    Ruta ,
                    ISNULL(Programacion, 0) AS Programacion ,
                    ISNULL(TelCasa, '-') AS TelCasa ,
                    ISNULL(TelAlterno1, '-') AS TelAlterno1 ,
                    ISNULL(TelAlterno2, '-') AS TelAlterno2 ,
                    ISNULL(Saldo, 0) AS Saldo ,
                    ISNULL(Email, '-') AS EMail ,
                    dbo.Trim(ISNULL(DireccionCompleta, '-')) + ' entre '
                    + dbo.Trim(EntreCalle1Nombre) + ' y '
                    + dbo.Trim(EntreCalle2Nombre) AS Direccion
            FROM    vwDatosCliente
            WHERE   NumeroReferencia = CONVERT(VARCHAR(250), @Cliente);    
  
  
    IF NOT EXISTS ( SELECT  *
                    FROM    @t )
        BEGIN   
            SET @Cliente = ( SELECT LEFT(LTRIM(@Cliente),
                                         LEN(RTRIM(@Cliente)) - 1)
                           );  
     
         
     
      
            SELECT  Cliente ,
                    Nombre ,
                    ISNULL(RazonSocial, '-') AS RazonSocial ,
                    Celula ,
                    Ruta ,
                    ISNULL(Programacion, 0) AS Programacion ,
                    ISNULL(TelCasa, '-') AS TelCasa ,
                    ISNULL(TelAlterno1, '-') AS TelAlterno1 ,
                    ISNULL(TelAlterno2, '-') AS TelAlterno2 ,
                    ISNULL(Saldo, 0) AS Saldo ,
                    ISNULL(Email, '-') AS EMail ,
                    dbo.Trim(ISNULL(DireccionCompleta, '-')) + ' entre '
                    + dbo.Trim(EntreCalle1Nombre) + ' y '
                    + dbo.Trim(EntreCalle2Nombre) AS Direccion
            FROM    vwDatosCliente
            WHERE   Cliente = @Cliente;  
        END;   
   
    SET TRANSACTION ISOLATION LEVEL READ COMMITTED;    
  
    SELECT  *
    FROM    @t;