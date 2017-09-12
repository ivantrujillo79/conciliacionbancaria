-- =============================================
-- Author:		<AKER>
-- Create date: <15/06/2017>
-- Description:	<Muestra las Facturas sin un pedido relacionado.>
-- spCBConsultaFacturaSinPedido '10-06-2017',null,null,null
-- spCBConsultaFacturaSinPedido @Cliente='424',@ClientePadre=0,@FolioFactura=NULL,@Fecha=NULL
-- =============================================
CREATE PROCEDURE [spCBConsultaFacturaSinPedido]
	-- Add the parameters for the stored procedure here
	 --Fecha y factura, cliente padre o NORMAL
    @Fecha DATETIME ,
    @FolioFactura INT = NULL ,
    @Cliente INT = NULL ,
    @ClientePadre BIT = 0
AS
    BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
        SET NOCOUNT ON;
        SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;  

		-- SET @Cliente=502731900

    -- Insert statements for procedure here
        IF @ClientePadre = 0
            BEGIN	
                SELECT  dbo.GetFecha(F.FFactura) AS 'FechaFactura' ,
                        C.Cliente ,
                        C.Nombre AS 'NombreCliente' ,
                        F.Serie + ' ' + CAST(F.Folio AS VARCHAR) AS 'FolioFactura' ,
                        F.Serie ,
                        F.Folio
                FROM    Factura F
                        JOIN dbo.ConceptoFactura CP ON CP.ConceptoFactura = F.ConceptoFactura
                        JOIN Cliente C ON C.Cliente = F.Cliente
                WHERE   (@Fecha IS NULL OR dbo.GetFecha(F.FFactura) = @Fecha)
                        AND ( @Cliente IS NULL
                              OR C.Cliente = @Cliente
                            )
                        AND ( @FolioFactura IS NULL
                              OR F.Factura = @FolioFactura
                            )
                        AND CP.CapturarPedido = 0
                        AND CP.CapturarRemision = 0
                        AND CP.CapturarSinReferencia = 0;
            END;
        ELSE
            BEGIN
                SELECT  dbo.GetFecha(F.FFactura) AS 'FechaFactura' ,
                        C.Cliente ,
                        C.Nombre AS 'NombreCliente' ,
                        F.Serie + ' ' + CAST(F.Folio AS VARCHAR) AS 'FolioFactura' ,
                        F.Serie ,
                        F.Folio
                FROM    Factura F
                        JOIN dbo.ConceptoFactura CP ON CP.ConceptoFactura = F.ConceptoFactura
                        JOIN Cliente C ON C.Cliente = F.Cliente
                        JOIN Cliente Padre ON C.Cliente = Padre.Cliente
                WHERE   (@Fecha IS NULL OR dbo.GetFecha(F.FFactura) = @Fecha)
                        AND ( @Cliente IS NULL
                              OR Padre.ClientePadre = @Cliente
                            )
                        AND ( @FolioFactura IS NULL
                              OR F.Factura = @FolioFactura
                            )
                        AND CP.CapturarPedido = 0
                        AND CP.CapturarRemision = 0
                        AND CP.CapturarSinReferencia = 0;
            END;	
    END;	
    SET TRANSACTION ISOLATION LEVEL READ COMMITTED;  