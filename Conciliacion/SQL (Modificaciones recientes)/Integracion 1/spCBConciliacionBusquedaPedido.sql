/********************************************************          
Realizo: Gabina Leon Velasco           
Fecha: 21/05/2013          
Descripcion: Busqueda de registros en pedido          
spCBConciliacionBusquedaPedido 2,1,1,2013,9,14,40,1,0;          
*********************************************************/          
          
alter PROCEDURE dbo.spCBConciliacionBusquedaPedidoPorFactura    
    @Configuracion AS SMALLINT ,    
    @CorporativoConciliacion AS TINYINT ,    
    @SucursalConciliacion AS TINYINT ,    
    @AñoConciliacion AS INT ,    
    @MesConciliacion AS SMALLINT ,    
    @FolioConciliacion AS INT ,    
    @Folio AS INT ,    
    @Secuencia AS INT ,    
    @Celula AS SMALLINT ,    
    @ClienteSeleccion AS INT ,    
    @ClientePadre AS BIT ,  
 @Factura AS VARCHAR(50) = NULL,  
 @FFactura AS DateTime = NULL,  
 @Referencia AS VARCHAR(100)=NULL  
AS    
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;              
    SET NOCOUNT ON;            
    DECLARE @Mensaje VARCHAR(100);  
          
--SET @ClienteSeleccion = substring(@ClienteSeleccion,0,len(@ClienteSeleccion));          
          
--IF ((EXISTS (SELECT COUNT(CLIENTE) FROM Cliente WHERE CLIENTE=@ClienteSeleccion)) and (ISNUMERIC(@ClienteSeleccion)=1))          
--BEGIN          
          
    DECLARE @Cadena VARCHAR(8000);          
          
    CREATE TABLE #Pedidos    
        (    
          Corporativo SMALLINT ,    
          Sucursal SMALLINT ,    
          SucursalDes VARCHAR(100) ,    
          Celula INT ,    
          AñoPed INT ,    
          Pedido INT ,    
          Cliente INT ,    
          Nombre VARCHAR(500) ,    
          RemisionPedido INT ,    
          SeriePedido VARCHAR(10) ,    
          FolioSat INT ,    
          SerieSat VARCHAR(10) ,    
          Concepto VARCHAR(500) ,    
          Monto DECIMAL(10, 2) ,    
          FormaConciliacion SMALLINT ,    
          StatusConcepto SMALLINT ,    
          StatusConciliacion VARCHAR(200) ,    
          FOperacion DATETIME ,    
          FMovimiento DATETIME ,    
          PedidoReferencia VARCHAR(50) ,    
          Total DECIMAL(10, 2) ,    
          Referencia VARCHAR(100)    
        );           
          
    CREATE TABLE #Externo    
        (    
          StatusConcepto INT ,    
          Foperacion DATETIME ,    
          FMovimiento DATETIME ,    
          Referencia VARCHAR(500) ,    
          Deposito DECIMAL(10, 2)    
        );          
          
    INSERT  INTO #Externo    
            SELECT  ISNULL(TDD.StatusConcepto, TFI.StatusConcepto) AS 'StatusConcepto' ,    
                    TDD.FOperacion ,    
                    TDD.FMovimiento ,    
                    TDD.Referencia ,    
                    TDD.Deposito    
            FROM    TablaDestinoDetalle TDD    
                    INNER JOIN TablaDestino TD ON TD.Corporativo = TDD.Corporativo    
                                                  AND TD.Sucursal = TDD.Sucursal    
                                                  AND TD.Año = TDD.Año    
                                                  AND TD.Folio = TDD.Folio    
                    INNER JOIN TipoFuenteInformacion TFI ON TFI.TipoFuenteInformacion = TD.TipoFuenteInformacion    
                    INNER JOIN TipoFuente TF ON TF.TipoFuente = TFI.TipoFuente    
                                                AND TF.TipoFuente = 2    
                    INNER JOIN Conciliacion C ON TD.Corporativo = C.CorporativoExterno    
                                                 AND TD.Sucursal = C.SucursalExterno    
                                                 AND TD.Año = C.AñoExterno    
                                                 AND TD.Folio = C.FolioExterno    
            WHERE   TDD.Corporativo = @CorporativoConciliacion    
                    AND TDD.Sucursal = @SucursalConciliacion    
            AND TDD.Año = @AñoConciliacion    
                    AND TDD.Folio = @Folio    
                    AND TDD.Secuencia = @Secuencia    
                    AND C.CorporativoConciliacion = @CorporativoConciliacion    
                    AND C.SucursalConciliacion = @SucursalConciliacion    
                    AND C.AñoConciliacion = @AñoConciliacion    
                    AND C.MesConciliacion = @MesConciliacion    
                    AND C.FolioConciliacion = @FolioConciliacion;          
                 
                 
                 
                
    IF @Celula >= 0           
   -- IF @Celula > 0        
        BEGIN          
            INSERT  INTO #Pedidos    
                    SELECT  Celula.Corporativo ,    
                            Celula.Sucursal ,    
                            Sucursal.Descripcion AS 'SucursalDes' ,    
                            P.Celula ,    
                            P.AñoPed ,    
                            P.Pedido ,    
                            P.Cliente ,    
                            P.Nombre ,    
                            ISNULL(P.Remision, 0) AS 'RemisionPedido' ,    
                            ISNULL(P.SerieRemision, '') AS 'SeriePedido' ,    
                            ISNULL(P.Folio, 0) AS 'FolioSat' ,    
                            ISNULL(P.Serie, '') AS 'SerieSat' ,    
                            P.TipoCargoDescripcion AS 'Concepto' ,    
                            P.Saldo AS 'Monto' ,    
                            3 AS 'FormaConciliacion' ,    
                            2 AS 'StatusConcepto' ,    
                            'EN PROCESO DE CONCILIACION' AS 'StatusConciliacion' ,    
                            P.FSuministro AS 'FOperacion' ,    
                            P.FSuministro AS 'FMovimiento' ,    
                            P.PedidoReferencia ,    
                            P.Total ,    
                            P.Referencia    
                    FROM    vwCBPedidosPorAbonar P    
                            INNER JOIN Sucursal ON P.Sucursal = Sucursal.Sucursal    
                            INNER JOIN Celula ON P.Celula = Celula.Celula    
                    WHERE   Sucursal.Sucursal = @SucursalConciliacion    
                            AND Celula.Corporativo = @CorporativoConciliacion    
                            AND Celula.Celula = @Celula    
       AND (P.Folio = @Factura OR @Factura IS NULL)  
--     AND (dbo.GetFecha(P.FFactura) = dbo.GetFecha(@FFactura) OR @FFactura IS NULL)  
       AND (P.Referencia = @Referencia OR @Referencia IS NULL)  
                            AND   
       (  
       (@ClienteSeleccion = '-' OR @ClienteSeleccion='')  
       OR  
       @ClienteSeleccion = CASE WHEN @ClientePadre = 1    
                                                         THEN P.ClientePadre --Nota: Se agrega para filtrar unicamente los Pedidos del Cliente/Cliente Padre          
                                                         ELSE P.Cliente      --      segun el parametro @ClientePadre Recibido          
                                                    END  
       )        
                 
                
          
        END;           
          
    IF @Celula < 0           
  --  IF @Celula <= 0        
        BEGIN          
            INSERT  INTO #Pedidos    
                    SELECT  Celula.Corporativo ,    
                            Celula.Sucursal ,    
                            Sucursal.Descripcion AS 'SucursalDes' ,    
                            P.Celula ,    
                            P.AñoPed ,    
                            P.Pedido ,    
                            P.Cliente ,    
                            P.Nombre ,    
                            ISNULL(P.Remision, 0) AS 'RemisionPedido' ,    
                            ISNULL(P.SerieRemision, '') AS 'SeriePedido' ,    
                            ISNULL(P.Folio, 0) AS 'FolioSat' ,    
                            ISNULL(P.Serie, '') AS 'SerieSat' ,    
                            P.TipoCargoDescripcion AS 'Concepto' ,    
                            P.Saldo AS 'Monto' ,    
                            3 AS 'FormaConciliacion' ,    
             2 AS 'StatusConcepto' ,    
                            'EN PROCESO DE CONCILIACION' AS 'StatusConciliacion' ,    
                            P.FSuministro AS 'FOperacion' ,    
                            P.FSuministro AS 'FMovimiento' ,    
                            P.PedidoReferencia ,    
                            P.Total ,    
                            P.Referencia    
         FROM    vwCBPedidosPorAbonar P    
                            INNER JOIN Sucursal ON P.Sucursal = Sucursal.Sucursal    
                            INNER JOIN Celula ON P.Celula = Celula.Celula    
                    WHERE   Sucursal.Sucursal = @SucursalConciliacion    
                            AND Celula.Corporativo = @CorporativoConciliacion     
       AND (P.Folio = @Factura OR @Factura IS NULL)  
       AND (P.Referencia = @Referencia OR @Referencia IS NULL)  
 --    AND (dbo.GetFecha(P.FFactura) = dbo.GetFecha(@FFactura) OR @FFactura IS NULL)   
       AND   
       (  
       (@ClienteSeleccion = '-' OR @ClienteSeleccion='')  
       OR  
       @ClienteSeleccion = CASE WHEN @ClientePadre = 1    
                                                         THEN P.ClientePadre --Nota: Se agrega para filtrar unicamente los Pedidos del Cliente/Cliente Padre          
                                                         ELSE P.Cliente      --      segun el parametro @ClientePadre Recibido          
                                                    END  
       )     
          
        END;           
           
          
    IF @Configuracion = 0 -- TODOS LOS PEDIDOS CON SALDO           
        BEGIN          
            SELECT P.Corporativo, P.Sucursal, P.SucursalDes,          
       P.Celula,P.AñoPed,P.Pedido,P.Cliente,P.Nombre,P.RemisionPedido ,P.SeriePedido, P.FolioSat,          
       P.SerieSat,P.Concepto,P.Monto,P.FormaConciliacion, P.StatusConcepto, P.StatusConciliacion,           
       P.FOperacion,P.FMovimiento,P.PedidoReferencia, P.Total FROM #Pedidos P;             
        END;          
          
    IF @Configuracion = 1   -- MISMA REFERENCIA e importe del pedido menor al deposito del registro externo          
        BEGIN           
            SELECT P.Corporativo, P.Sucursal, P.SucursalDes,          
       P.Celula,P.AñoPed,P.Pedido,P.Cliente,P.Nombre,P.RemisionPedido ,P.SeriePedido, P.FolioSat,          
       P.SerieSat,P.Concepto,P.Monto,P.FormaConciliacion, P.StatusConcepto, P.StatusConciliacion,           
       P.FOperacion,P.FMovimiento,P.PedidoReferencia, P.Total FROM #Pedidos P           
              INNER JOIN #Externo Externo ON Externo.Referencia = P.Referencia AND P.Total< Externo.Deposito;        
        END;               
          
    IF @Configuracion = 2 -- Importe del pedido menor al deposito del registro externo (sin importar la referencia)          
        BEGIN            
            SELECT P.Corporativo, P.Sucursal, P.SucursalDes,          
       P.Celula,P.AñoPed,P.Pedido,P.Cliente,P.Nombre,P.RemisionPedido ,P.SeriePedido, P.FolioSat,          
       P.SerieSat,P.Concepto,P.Monto,P.FormaConciliacion, P.StatusConcepto, P.StatusConciliacion,           
       P.FOperacion,P.FMovimiento,P.PedidoReferencia, P.Total FROM #Pedidos P           
              INNER JOIN #Externo Externo ON P.Total<= Externo.Deposito ;         
        END;          
          
    IF @Configuracion = 3 -- MISMA REFERENCIA SIN IMPORTE IMPORTE MAYOR O MENOR           
        BEGIN          
            SELECT P.Corporativo, P.Sucursal, P.SucursalDes,          
       P.Celula,P.AñoPed,P.Pedido,P.Cliente,P.Nombre,P.RemisionPedido ,P.SeriePedido, P.FolioSat,          
       P.SerieSat,P.Concepto,P.Monto,P.FormaConciliacion, P.StatusConcepto, P.StatusConciliacion,           
       P.FOperacion,P.FMovimiento,P.PedidoReferencia, P.Total FROM #Pedidos P           
              INNER JOIN #Externo Externo ON Externo.Referencia = P.Referencia ;          
        END;                 
  
    EXEC(@Cadena);          
  
  
 DROP TABLE #Externo  
 DROP TABLE #Pedidos  
          
--END          
--ELSE          
--BEGIN          
--  SET @Mensaje = 'EL CLIENTE NO ES VALIDO, TENDRA QUE AGREGAR EL PEDIDO DIRECTAMENTE'           
--  RAISERROR(@Mensaje, 16, 1)           
--END          
    SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

