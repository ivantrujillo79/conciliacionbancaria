/*
Procedimiento para dar de alta los datos de una cobranza.
Autor: Santiago Mendoza Carlos Nirari
Fecha: 11/07/2017
*/

CREATE PROCEDURE spCBGuardarPedidoCobranza
    @AñoPed SMALLINT ,
    @Celula TINYINT ,
    @Pedido INT ,
    @Cobranza INT ,
    @Saldo MONEY ,
    @GestionInicial TINYINT = 1
AS
    SET NOCOUNT ON


    INSERT  INTO PedidoCobranza
            ( AñoPed ,
              Celula ,
              Pedido ,
              Cobranza ,
              Saldo ,
              GestionInicial
            )
    VALUES  ( @AñoPed ,
              @Celula ,
              @Pedido ,
              @Cobranza ,
              @Saldo ,
              @GestionInicial
            )
