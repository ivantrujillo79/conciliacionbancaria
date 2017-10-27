   
CREATE PROCEDURE spCBFamiliaresCliente   

   
--DECLARE @Cliente INT=110002685   --padre
 @cliente INT  --= 23868    -- hijo
AS

SET NOCOUNT ON;
      
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;    
  
DECLARE @T TABLE
    (
      ClientePadre INT ,
      Cliente INT ,
      EsPadre BIT
    );   
   
   
INSERT  INTO @T
        SELECT  CP.Cliente AS ClientePadre ,
                DCH.Cliente ,
                0
        FROM    vwClientesPadreCyC AS CP
                JOIN vwDatosCliente AS DCP ON CP.Cliente = DCP.Cliente
                JOIN vwDatosCliente AS DCH ON CP.Cliente = DCH.ClientePadre
        WHERE   CP.Cliente <> DCH.Cliente
                AND CP.Cliente = @cliente;


--SELECT COUNT(*) FROM @t
IF ( SELECT COUNT(*)
     FROM   @T
   ) > 0
    INSERT  INTO @T
            SELECT  ClientePadre ,
                    Cliente ,
                    1
            FROM    Cliente
            WHERE   Cliente = @cliente;

IF @@ROWCOUNT = 0
    BEGIN	

        DECLARE @Clientepadre INT;

        SET @Clientepadre = ( SELECT    ClientePadre
                              FROM      dbo.Cliente
                              WHERE     Cliente = @cliente
                            );

        INSERT  INTO @T
                SELECT  ClientePadre ,
                        Cliente ,
                        CASE WHEN Cliente = ClientePadre THEN 1
                             ELSE 0
                        END AS Tipo
                FROM    dbo.Cliente
                WHERE   ClientePadre = @Clientepadre;
    END;	

SELECT  *
FROM    @T
ORDER BY EsPadre DESC;