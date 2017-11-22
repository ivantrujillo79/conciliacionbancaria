/*
Missing Index Details from SQLQuery1.sql - 172.16.50.15,3390.SigametMetro (ROPIMA (51))
The Query Processor estimates that implementing the following index could improve the query cost by 98.8497%.
spCBConsultaFacturaSinPedido
*/


USE [SigametMetro]
GO
CREATE NONCLUSTERED INDEX IX_Factura_Cliente
ON [dbo].[Factura] ([Cliente])
INCLUDE ([FFactura],[Total],[Folio],[Serie],[ConceptoFactura])
GO

