/*
SELECT * FROM vwCBPedidosPorAbonar
*/
ALTER VIEW dbo.vwCBPedidosPorAbonar  
  
AS  
  
   
SELECT C.Cliente, C.Nombre, ISNULL(C.Referencia,'') Referencia,C.ClientePadre,P.Celula, P.AñoPed, P.Pedido, P.Total, P.Saldo,P.FSuministro, P.TipoCargo, P.Remision, P.SerieRemision ,P.PedidoReferencia , TCa.Descripcion as TipoCargoDescripcion, Celula.Sucursal, ISNULL(Factura.Folio,0) Folio, ISNULL(Factura.Serie,'')Serie, 0 as SaldoPC , ISNULL((replace(Empresa.RFC,' ', '')),'') RFC, ---ISNULL(PC.Saldo,0)
Factura.FFactura  
 FROM   Cliente C JOIN Pedido P                          
                    ON  P.Cliente = C.Cliente                          
        /*     LEFT JOIN LecturaMedidor LM                      
                    ON  P.Celula = LM.Celula                      
                    AND P.AñoPed = LM.AñoPed                      
                    AND P.Pedido = LM.Pedido                      
             LEFT JOIN Pedido PC                      
      ON  LM.CelulaCargo = PC.Celula                      
                    AND LM.AñoPedCargo = PC.AñoPed                      
                    AND LM.PedidoCargo = PC.Pedido*/  
             INNER JOIN Celula ON Celula.Celula=P.Celula  
    INNER JOIN TipoCargo TCa ON TCa.TipoCargo=P.TipoCargo  
    LEFT JOIN (SELECT Celula, AñoPed, Pedido, Factura.Serie,Factura.Folio,Factura.FFactura
               FROM FacturaPedido FP 
               INNER JOIN Factura ON FP.Factura=Factura.Factura 
               WHERE Factura.Status <>'CANCELADO'  AND Factura.Status  IS NOT NULL) As Factura
               ON Factura.Pedido=P.Pedido AND Factura.Celula=P.Celula AND Factura.AñoPed=P.AñoPed                               
    LEFT JOIN ConciliacionPedido CP ON CP.AñoPed=P.AñoPed AND CP.Celula=P.Celula AND CP.Pedido=P.Pedido AND CP.StatusConciliacion = 'CONCILIADA'--AND CP.MontoExterno=P.Saldo  
    LEFT JOIN Empresa ON Empresa.Empresa= C.Cliente  
 WHERE  P.TipoCargo = 1                      
        --AND C.Referencia = @Cliente                          
        AND P.CyC = 1                      
        AND P.Status = 'SURTIDO'                      
        AND P.Saldo > 0.01                       
        --AND ((P.Saldo + ISNULL(PC.Saldo, 0)) = @Saldo OR ISNULL(@Saldo, 0) = 0)                     
        AND CP.Pedido IS NULL  
        AND P.FSuministro >= DATEADD (MONTH , -5 ,getdate())               
          
 UNION ALL                     
                        
SELECT C.Cliente, C.Nombre, ISNULL(C.Referencia,'') Referencia,C.ClientePadre, P.Celula, P.AñoPed, P.Pedido, P.Total, P.Saldo,P.FSuministro, P.TipoCargo, P.Remision, P.SerieRemision ,P.PedidoReferencia , TCa.Descripcion as TipoCargoDescripcion, Celula.Sucursal, ISNULL(Factura.Folio,0) Folio, ISNULL(Factura.Serie,'')Serie, 0 as SaldoPC , ISNULL((replace(Empresa.RFC,' ', '')),'') RFC--ISNULL(PC.Saldo,0)  
,Factura.FFactura
 FROM   Cliente C JOIN Pedido P                          
                    ON  P.Cliente = C.Cliente                          
           /*  LEFT JOIN LecturaMedidor LM                      
                    ON  P.Celula = LM.CelulaCargo                      
                    AND P.AñoPed = LM.AñoPedCargo                      
                    AND P.Pedido = LM.PedidoCargo                      
             LEFT JOIN Pedido PC                      
                    ON  LM.Celula = PC.Celula                      
                    AND LM.AñoPed = PC.AñoPed                      
                    AND LM.Pedido = PC.Pedido   */  
             INNER JOIN Celula ON Celula.Celula=P.Celula  
    INNER JOIN TipoCargo TCa ON TCa.TipoCargo=P.TipoCargo  
    LEFT JOIN (SELECT Celula, AñoPed, Pedido, Factura.Serie,Factura.Folio,Factura.FFactura
               FROM FacturaPedido FP 
               INNER JOIN Factura ON FP.Factura=Factura.Factura 
               WHERE Factura.Status <>'CANCELADO'  AND Factura.Status  IS NOT NULL) As Factura
               ON Factura.Pedido=P.Pedido AND Factura.Celula=P.Celula AND Factura.AñoPed=P.AñoPed                    
LEFT JOIN ConciliacionPedido CP ON CP.AñoPed=P.AñoPed AND CP.Celula=P.Celula AND CP.Pedido=P.Pedido AND CP.StatusConciliacion = 'CONCILIADA'--AND CP.MontoExterno=P.Saldo  
    LEFT JOIN Empresa ON Empresa.Empresa= C.Cliente  
 WHERE  P.TipoCargo = 9                      
        --AND C.Referencia = @Cliente                          
        AND P.CyC = 1                      
        AND P.Status = 'SURTIDO'                      
        AND P.Saldo > 0.01       
        --AND ((P.Saldo + ISNULL(PC.Saldo, 0)) = @Saldo OR ISNULL(@Saldo, 0) = 0)                          
  AND CP.Pedido IS NULL  
        AND P.FSuministro >= DATEADD (MONTH , -5 ,getdate())


