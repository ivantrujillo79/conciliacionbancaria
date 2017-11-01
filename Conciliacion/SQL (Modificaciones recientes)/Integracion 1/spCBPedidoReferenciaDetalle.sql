/********************************************************
Realizó: Iván Trujillo
Fecha: 31/10/2017
Descripción: Verifica si un pedido referencia existe o no
 spCBPedidoReferenciaDetalle 201781234567
*********************************************************/

alter PROCEDURE spCBPedidoReferenciaDetalle
@PedidoReferencia varchar(20)
AS

SELECT PedidoReferencia,Cliente,Saldo
FROM Pedido 
WHERE PedidoReferencia = @PedidoReferencia and saldo > 0.01;



