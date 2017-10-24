USE SIGAMET
GO
/********************************************************
Realizó: Iván Trujillo
Fecha: 5/10/2017
Descripción: Verifica si un pedido referencia existe o no
 spCBVerificaPedidoReferenciaExiste 201781234567
*********************************************************/

CREATE PROCEDURE spCBVerificaPedidoReferenciaExiste
@PedidoReferencia varchar(20)
AS
DECLARE @TotalRegistros INT;
SET @TotalRegistros = 0;

SELECT @TotalRegistros = COUNT(*)
FROM Pedido 
WHERE PedidoReferencia = @PedidoReferencia and saldo > 0.01;

SELECT @TotalRegistros as TotalRegistros

