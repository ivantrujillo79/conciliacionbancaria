USE [Sigamet]
GO
/********************************************************
Realizó: Iván Trujillo
Fecha: 5/10/2017
Descripción: Verifica si un pedido referencia existe o no
 spCBVerificaPedidoReferenciaExiste 201781234567
Actualizó: Ricardo Rojas - 23/10/2017
*********************************************************/

ALTER PROCEDURE [dbo].[spCBVerificaPedidoReferenciaExiste]
@PedidoReferencia varchar(20)
AS

DECLARE @TotalRegistros INT;

SET @TotalRegistros = 0;

SELECT @TotalRegistros = COUNT(*)
FROM Pedido 
WHERE PedidoReferencia = @PedidoReferencia;

SELECT @TotalRegistros as TotalRegistros

