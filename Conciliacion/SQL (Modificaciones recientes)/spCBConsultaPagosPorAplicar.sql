/********************************************************
Realizo: Gabina Leon Velasco 
Fecha: 20/06/2013
Descripcion: Devuelve la informacion los pedidos a abonar en una conciliacion
exec spCBConsultaPagosPorAplicar 0,1,1,2014,10,73,0,0,0,0,0,0;
exec spCBConsultaPagosPorAplicar ,1,1,2014,10,73,0,972,1,1,2014,2;
*********************************************************/

ALTER PROCEDURE spCBConsultaPagosPorAplicar
    @Configuracion SMALLINT ,
    @CorporativoConciliacion AS INT ,
    @SucursalConciliacion AS INT ,
    @AñoConciliacion AS INT ,
    @MesConciliacion AS SMALLINT ,
    @FolioConciliacion AS INT ,
    @Cliente AS INT ,
    @FolioExterno AS INT ,
    @CorporativoExterno AS INT ,
    @SucursalExterno AS INT ,
    @AñoExterno AS INT ,
    @SecuenciaExterno AS INT
AS
    SET NOCOUNT ON  

    IF @Configuracion = 0
        BEGIN
            SELECT  MAX(CP.CorporativoConciliacion) CorporativoConciliacion ,
                    MAX(CP.AñoConciliacion) AñoConciliacion ,
                    MAX(CP.MesConciliacion) MesConciliacion ,
                    MAX(CP.FolioConciliacion) FolioConciliacion ,
                    CP.SucursalExterno SucursalExt ,
                    SE.Descripcion SucursalExtDes ,
                    CP.FolioExterno FolioExt ,
                    CP.SecuenciaExterno SecuenciaExt ,
                    CP.Concepto ConceptoExt ,
                    CP.MontoConciliado ,
                    ( CP.MontoConciliado - CP.MontoInterno ) Diferencia ,
                    CP.FormaConciliacion ,
                    CP.StatusConcepto ,
                    CP.StatusConciliacion ,
                    TDD.FOperacion FOperacionExt ,
                    TDD.FMovimiento FMovimientoExt ,
                    SI.Sucursal SucursalPedido ,
                    SI.Descripcion SucursalPedidoDes ,
                    CP.Celula CelulaPedido ,
                    CP.AñoPed AñoPedido ,
                    CP.Pedido ,
                    CP.RemisionPedido ,
                    CP.SeriePedido ,
                    CP.FolioSat ,
                    CP.SerieSat ,
                    TC.Descripcion ConceptoPedido ,
                    CP.MontoInterno Total ,
                    CP.StatusMovimiento ,
                    P.Cliente ,
                    Cliente.Nombre ,
                    P.PedidoReferencia ,
                    P.Saldo ,
                    TDD.Cheque ,
                    TDD.Referencia ,
                    TDD.NombreTercero ,
                    TDD.RFCTercero ,
                    TDD.Retiro ,
                    TDD.Descripcion ,
                    TDD.Deposito ,
                    TDD.Año AS AñoExterno
            FROM    Conciliacion
                    INNER JOIN ConciliacionPedido CP ON Conciliacion.CorporativoConciliacion = CP.CorporativoConciliacion
                                                        AND Conciliacion.SucursalConciliacion = CP.SucursalConciliacion
                                                        AND Conciliacion.AñoConciliacion = CP.AñoConciliacion
                                                        AND Conciliacion.MesConciliacion = CP.MesConciliacion
                                                        AND Conciliacion.FolioConciliacion = CP.FolioConciliacion
                                                        AND CP.StatusConciliacion = 'CONCILIADA'
										--AND CP.StatusMovimiento ='PENDIENTE'
                    INNER JOIN TablaDestinoDetalle TDD ON TDD.Corporativo = CP.CorporativoExterno
                                                          AND TDD.Sucursal = CP.SucursalExterno
                                                          AND TDD.Año = CP.AñoExterno
                                                          AND TDD.Folio = CP.FolioExterno
                                                          AND TDD.Secuencia = CP.SecuenciaExterno
                    INNER JOIN Sucursal SE ON SE.Sucursal = CP.SucursalExterno
                    INNER JOIN Pedido P ON P.Celula = CP.Celula
                                           AND P.AñoPed = CP.AñoPed
                                           AND P.Pedido = CP.Pedido
                    INNER JOIN Cliente ON Cliente.Cliente = P.Cliente
                    INNER JOIN Celula C ON C.Celula = P.Celula
                    INNER JOIN Sucursal SI ON SI.Sucursal = C.Sucursal
                    INNER JOIN TipoCargo TC ON TC.TipoCargo = P.TipoCargo
            WHERE   Conciliacion.CorporativoConciliacion = @CorporativoConciliacion
                    AND Conciliacion.SucursalConciliacion = @SucursalConciliacion
                    AND Conciliacion.AñoConciliacion = @AñoConciliacion
                    AND Conciliacion.MesConciliacion = @MesConciliacion
                    AND Conciliacion.FolioConciliacion = @FolioConciliacion
                    AND Conciliacion.StatusConciliacion = 'CONCILIACION CERRADA'
            GROUP BY CP.SucursalExterno ,
                    SE.Descripcion ,
                    CP.FolioExterno ,
                    CP.SecuenciaExterno ,
                    CP.Concepto ,
                    CP.MontoConciliado ,
                    CP.MontoConciliado ,
                    CP.MontoInterno ,
                    CP.FormaConciliacion ,
                    CP.StatusConcepto ,
                    CP.StatusConciliacion ,
                    TDD.FOperacion ,
                    TDD.FMovimiento ,
                    SI.Sucursal ,
                    SI.Descripcion ,
                    CP.Celula ,
                    CP.AñoPed ,
                    CP.Pedido ,
                    CP.RemisionPedido ,
                    CP.SeriePedido ,
                    CP.FolioSat ,
                    CP.SerieSat ,
                    TC.Descripcion ,
                    CP.MontoInterno ,
                    CP.StatusMovimiento ,
                    P.Cliente ,
                    Cliente.Nombre ,
                    P.PedidoReferencia ,
					P.Saldo ,
                    TDD.Cheque ,
                    TDD.Referencia ,
                    TDD.NombreTercero ,
                    TDD.RFCTercero ,
                    TDD.Retiro ,
                    TDD.Descripcion ,
                    TDD.Deposito ,
                    TDD.Año
        END

    IF @Configuracion = 1
        BEGIN
            SELECT  MAX(CP.CorporativoConciliacion) CorporativoConciliacion ,
                    MAX(CP.AñoConciliacion) AñoConciliacion ,
                    MAX(CP.MesConciliacion) MesConciliacion ,
                    MAX(CP.FolioConciliacion) FolioConciliacion ,
                    CP.SucursalExterno SucursalExt ,
                    SE.Descripcion SucursalExtDes ,
                    CP.FolioExterno FolioExt ,
                    CP.SecuenciaExterno SecuenciaExt ,
                    CP.Concepto ConceptoExt ,
                    CP.MontoConciliado ,
                    ( CP.MontoConciliado - CP.MontoInterno ) Diferencia ,
                    CP.FormaConciliacion ,
                    CP.StatusConcepto ,
                    CP.StatusConciliacion ,
                    TDD.FOperacion FOperacionExt ,
                    TDD.FMovimiento FMovimientoExt ,
                    SI.Sucursal SucursalPedido ,
                    SI.Descripcion SucursalPedidoDes ,
                    CP.Celula CelulaPedido ,
                    CP.AñoPed AñoPedido ,
                    CP.Pedido ,
                    CP.RemisionPedido ,
                    CP.SeriePedido ,
                    CP.FolioSat ,
                    CP.SerieSat ,
                    TC.Descripcion ConceptoPedido ,
                    CP.MontoInterno Total ,
                    CP.StatusMovimiento ,
                    P.Cliente ,
                    Cliente.Nombre ,
                    P.PedidoReferencia ,
                    TDD.Cheque ,
                    TDD.Referencia ,
                    TDD.NombreTercero ,
                    TDD.RFCTercero ,
                    TDD.Retiro ,
                    TDD.Descripcion ,
                    TDD.Deposito ,
                    TDD.Año AS AñoExterno
            FROM    Conciliacion
                    INNER JOIN ConciliacionPedido CP ON Conciliacion.CorporativoConciliacion = CP.CorporativoConciliacion
                                                        AND Conciliacion.SucursalConciliacion = CP.SucursalConciliacion
                                                        AND Conciliacion.AñoConciliacion = CP.AñoConciliacion
                                                        AND Conciliacion.MesConciliacion = CP.MesConciliacion
                                                        AND Conciliacion.FolioConciliacion = CP.FolioConciliacion
                                                        AND CP.StatusConciliacion = 'CONCILIADA'
										--AND CP.StatusMovimiento='PENDIENTE'
                    INNER JOIN TablaDestinoDetalle TDD ON TDD.Corporativo = CP.CorporativoExterno
                                                          AND TDD.Sucursal = CP.SucursalExterno
                                                          AND TDD.Año = CP.AñoExterno
                                                          AND TDD.Folio = CP.FolioExterno
                                                          AND TDD.Secuencia = CP.SecuenciaExterno
                    INNER JOIN Sucursal SE ON SE.Sucursal = CP.SucursalExterno
                    INNER JOIN Pedido P ON P.Celula = CP.Celula
                                           AND P.AñoPed = CP.AñoPed
                                           AND P.Pedido = CP.Pedido
                    INNER JOIN Cliente ON Cliente.Cliente = P.Cliente
                    INNER JOIN Celula C ON C.Celula = P.Celula
                    INNER JOIN Sucursal SI ON SI.Sucursal = C.Sucursal
                    INNER JOIN TipoCargo TC ON TC.TipoCargo = P.TipoCargo
            WHERE   Conciliacion.CorporativoConciliacion = @CorporativoConciliacion
                    AND Conciliacion.SucursalConciliacion = @SucursalConciliacion
                    AND Conciliacion.AñoConciliacion = @AñoConciliacion
                    AND Conciliacion.MesConciliacion = @MesConciliacion
                    AND Conciliacion.FolioConciliacion = @FolioConciliacion
                    AND Cliente.Cliente = @Cliente
						 ------------------------------------------
                    AND TDD.Folio = @FolioExterno
                    AND TDD.Corporativo = @CorporativoExterno
                    AND TDD.Sucursal = @SucursalExterno
                    AND TDD.Año = @AñoExterno
                    AND TDD.Secuencia = @SecuenciaExterno
						 ------------------------------------------
                    AND Conciliacion.StatusConciliacion = 'CONCILIACION CERRADA'
            GROUP BY CP.SucursalExterno ,
                    SE.Descripcion ,
                    CP.FolioExterno ,
                    CP.SecuenciaExterno ,
                    CP.Concepto ,
                    CP.MontoConciliado ,
                    CP.MontoConciliado ,
                    CP.MontoInterno ,
                    CP.FormaConciliacion ,
                    CP.StatusConcepto ,
                    CP.StatusConciliacion ,
                    TDD.FOperacion ,
                    TDD.FMovimiento ,
                    SI.Sucursal ,
                    SI.Descripcion ,
                    CP.Celula ,
                    CP.AñoPed ,
                    CP.Pedido ,
                    CP.RemisionPedido ,
                    CP.SeriePedido ,
                    CP.FolioSat ,
                    CP.SerieSat ,
                    TC.Descripcion ,
                    CP.MontoInterno ,
                    CP.StatusMovimiento ,
                    P.Cliente ,
                    Cliente.Nombre ,
                    P.PedidoReferencia ,
                    TDD.Cheque ,
                    TDD.Referencia ,
                    TDD.NombreTercero ,
                    TDD.RFCTercero ,
                    TDD.Retiro ,
                    TDD.Descripcion ,
                    TDD.Deposito ,
                    TDD.Año
        END
    IF @Configuracion = 2
        BEGIN
            SELECT  MAX(CP.CorporativoConciliacion) CorporativoConciliacion ,
                    MAX(CP.AñoConciliacion) AñoConciliacion ,
                    MAX(CP.MesConciliacion) MesConciliacion ,
                    MAX(CP.FolioConciliacion) FolioConciliacion ,
                    CP.SucursalExterno SucursalExt ,
                    SE.Descripcion SucursalExtDes ,
                    CP.FolioExterno FolioExt ,
                    CP.SecuenciaExterno SecuenciaExt ,
                    CP.Concepto ConceptoExt ,
                    CP.MontoConciliado ,
                    ( CP.MontoConciliado - CP.MontoInterno ) Diferencia ,
                    CP.FormaConciliacion ,
                    CP.StatusConcepto ,
                    CP.StatusConciliacion ,
                    TDD.FOperacion FOperacionExt ,
                    TDD.FMovimiento FMovimientoExt ,
                    SI.Sucursal SucursalPedido ,
                    SI.Descripcion SucursalPedidoDes ,
                    CP.Celula CelulaPedido ,
                    CP.AñoPed AñoPedido ,
                    CP.Pedido ,
                    CP.RemisionPedido ,
                    CP.SeriePedido ,
                    CP.FolioSat ,
                    CP.SerieSat ,
                    TC.Descripcion ConceptoPedido ,
                    CP.MontoInterno Total ,
                    CP.StatusMovimiento ,
                    P.Cliente ,
                    Cliente.Nombre ,
                    P.PedidoReferencia ,
                    TDD.Cheque ,
                    TDD.Referencia ,
                    TDD.NombreTercero ,
                    TDD.RFCTercero ,
                    TDD.Retiro ,
                    TDD.Descripcion ,
                    TDD.Deposito ,
                    TDD.Año AS AñoExterno
            FROM    Conciliacion
                    INNER JOIN ConciliacionPedido CP ON Conciliacion.CorporativoConciliacion = CP.CorporativoConciliacion
                                                        AND Conciliacion.SucursalConciliacion = CP.SucursalConciliacion
                                                        AND Conciliacion.AñoConciliacion = CP.AñoConciliacion
                                                        AND Conciliacion.MesConciliacion = CP.MesConciliacion
                                                        AND Conciliacion.FolioConciliacion = CP.FolioConciliacion
                                                        AND CP.StatusConciliacion = 'CONCILIADA'
										--AND CP.StatusMovimiento='PENDIENTE'
                    INNER JOIN TablaDestinoDetalle TDD ON TDD.Corporativo = CP.CorporativoExterno
                                                          AND TDD.Sucursal = CP.SucursalExterno
                                                          AND TDD.Año = CP.AñoExterno
                                                          AND TDD.Folio = CP.FolioExterno
                                                          AND TDD.Secuencia = CP.SecuenciaExterno
                    INNER JOIN Sucursal SE ON SE.Sucursal = CP.SucursalExterno
                    INNER JOIN Pedido P ON P.Celula = CP.Celula
                                           AND P.AñoPed = CP.AñoPed
                                           AND P.Pedido = CP.Pedido
                    INNER JOIN Cliente ON Cliente.Cliente = P.Cliente
                    INNER JOIN Celula C ON C.Celula = P.Celula
                    INNER JOIN Sucursal SI ON SI.Sucursal = C.Sucursal
                    INNER JOIN TipoCargo TC ON TC.TipoCargo = P.TipoCargo
            WHERE   Conciliacion.CorporativoConciliacion = @CorporativoConciliacion
                    AND Conciliacion.SucursalConciliacion = @SucursalConciliacion
                    AND Conciliacion.AñoConciliacion = @AñoConciliacion
                    AND Conciliacion.MesConciliacion = @MesConciliacion
                    AND Conciliacion.FolioConciliacion = @FolioConciliacion 
						 --AND Cliente.Cliente= @Cliente
						 ------------------------------------------
                    AND TDD.Folio = @FolioExterno
                    AND TDD.Corporativo = @CorporativoExterno
                    AND TDD.Sucursal = @SucursalExterno
                    AND TDD.Año = @AñoExterno
                    AND TDD.Secuencia = @SecuenciaExterno
						 ------------------------------------------
                    AND Conciliacion.StatusConciliacion = 'CONCILIACION CERRADA'
            GROUP BY CP.SucursalExterno ,
                    SE.Descripcion ,
                    CP.FolioExterno ,
                    CP.SecuenciaExterno ,
                    CP.Concepto ,
                    CP.MontoConciliado ,
                    CP.MontoConciliado ,
                    CP.MontoInterno ,
                    CP.FormaConciliacion ,
                    CP.StatusConcepto ,
                    CP.StatusConciliacion ,
                    TDD.FOperacion ,
                    TDD.FMovimiento ,
                    SI.Sucursal ,
                    SI.Descripcion ,
                    CP.Celula ,
                    CP.AñoPed ,
                    CP.Pedido ,
                    CP.RemisionPedido ,
                    CP.SeriePedido ,
                    CP.FolioSat ,
                    CP.SerieSat ,
                    TC.Descripcion ,
                    CP.MontoInterno ,
                    CP.StatusMovimiento ,
                    P.Cliente ,
                    Cliente.Nombre ,
                    P.PedidoReferencia ,
                    TDD.Cheque ,
                    TDD.Referencia ,
                    TDD.NombreTercero ,
                    TDD.RFCTercero ,
                    TDD.Retiro ,
                    TDD.Descripcion ,
                    TDD.Deposito ,
                    TDD.Año
        END
