-- =============================================  
-- Author:  <AKER>  
-- Create date: <04/10/2017>  
-- Description: <Muestra las Facturas sin un pedido relacionado.>  
-- spCBConsultaFacturaSinPedido '10-06-2017',null,null,null  
-- =============================================  
CREATE PROCEDURE [dbo].[spCBConsultaFacturaSinPedido]  
 -- Add the parameters for the stored procedure here  
  --Fecha y factura, cliente padre o NORMAL  
    @Cliente INT ,
    @FolioFactura VARCHAR(50) = NULL ,
    @Fechaini DATETIME = NULL ,
    @Fechafin DATETIME = NULL ,
    @ClientePadre BIT = 0
AS
    BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
        SET NOCOUNT ON;  
        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;    
  
  
    -- Insert statements for procedure here  
        IF @ClientePadre = 0
            BEGIN   
                SELECT  dbo.GetFecha(F.FFactura) AS 'FFactura' ,
                        CAST(F.Folio AS VARCHAR(15)) + F.Serie AS 'FolioFactura' ,
                        C.Cliente ,
                        C.Nombre AS 'Nombre.Cliente' ,
                        CP.Descripcion AS Concepto ,
                        F.Total AS Monto
                FROM    Factura F
                        JOIN dbo.ConceptoFactura CP ON CP.ConceptoFactura = F.ConceptoFactura
                        JOIN Cliente C ON C.Cliente = F.Cliente
                WHERE   ( @Cliente IS NULL
                          OR C.Cliente = @Cliente
                        )
                        AND ( F.FFactura BETWEEN @Fechaini AND @Fechafin
                              OR @Fechaini IS NULL
                              AND @Fechafin IS NULL
                            )
                        AND ( @FolioFactura = ''
                              OR CONVERT(VARCHAR(20), F.Folio) + F.Serie = @FolioFactura
                            )
                        AND CP.CapturarPedido = 0
                        AND CP.CapturarRemision = 0
                        AND CP.CapturarSinReferencia = 0;  
            END;  
        ELSE
            BEGIN  
                SELECT  dbo.GetFecha(F.FFactura) AS 'FFactura' ,
                        CAST(F.Folio AS VARCHAR(15)) + F.Serie AS 'FolioFactura' ,
                        C.Cliente ,
                        C.Nombre AS 'Nombre.Cliente' ,
                        CP.Descripcion AS Concepto ,
                        F.Total AS Monto
                FROM    Factura F
                        JOIN dbo.ConceptoFactura CP ON CP.ConceptoFactura = F.ConceptoFactura
                        JOIN Cliente C ON C.Cliente = F.Cliente
                        JOIN Cliente Padre ON C.Cliente = Padre.Cliente
                WHERE   ( @Cliente IS NULL
                          OR C.Cliente = @Cliente
                        )
                        AND ( F.FFactura BETWEEN @Fechaini AND @Fechafin
                              OR @Fechaini IS NULL
                              AND @Fechafin IS NULL
                            )
                        AND ( @FolioFactura = ''
                              OR CONVERT(VARCHAR(20), F.Folio) + F.Serie = @FolioFactura
                            )
                        AND CP.CapturarPedido = 0
                        AND CP.CapturarRemision = 0
                        AND CP.CapturarSinReferencia = 0;  
            END;   
    END;   
    SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

