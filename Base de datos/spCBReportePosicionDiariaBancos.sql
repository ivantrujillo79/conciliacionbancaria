ALTER PROCEDURE spCBReportePosicionDiariaBancos --'01/03/2018','01/03/2018'  ,7      
    --  spCBReportePosicionDiariaBancos '01/01/2017','31/01/2017'  ,null      
    @FechaIni DATETIME ,
    @FechaFin DATETIME ,
    @Caja TINYINT = NULL
AS
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
    SET NOCOUNT ON;

    DECLARE @FactorDensidad DECIMAL;
    SET @FactorDensidad = (   SELECT Valor
                              FROM   dbo.Parametro
                              WHERE  Parametro = 'FactorDensidad'
                                     AND Modulo = 16 );
    DECLARE @Tabla TABLE
        (
            ID INT ,
            Sucursal VARCHAR(50) ,
            Concepto VARCHAR(100) ,
            Fecha DATETIME NULL ,
            Caja TINYINT NULL ,
            Kilos DECIMAL(15, 2) ,
            Total MONEY
        );

    INSERT INTO @Tabla
                SELECT   1 ,
                         s.Descripcion ,
                         'Portátil' ,
                         dbo.GetFecha(FTerminoRuta) ,
                         MC.Caja ,
                         SUM(p.kilos) - SUM(ISNULL(tc.KilosAplicacion, 0)) ,
                         SUM(p.Total) - SUM(ISNULL(tc.MontoAplicacion, 0))
                FROM     dbo.AutotanqueTurno att
                         JOIN dbo.MovimientoCaja MC ON MC.AñoAtt = att.AñoAtt
                                                       AND MC.FolioAtt = att.Folio
                         JOIN dbo.AlmacenGas AG ON AG.AlmacenGas = att.AlmacenGas
                         JOIN dbo.Pedido p ON att.Folio = p.Folio
                                              AND att.AñoAtt = p.AñoAtt
                         INNER JOIN Celula CL ON AG.Celula = CL.Celula
                         JOIN dbo.Sucursal s ON s.Sucursal = CL.Sucursal
                         JOIN dbo.TipoCobro TCO ON TCO.TipoCobro = p.TipoCobro
                         JOIN dbo.TipoPago TPP ON TPP.TipoPago = TCO.TipoPago
                         LEFT JOIN dbo.TipoCuenta tc ON tc.AñoPed = p.AñoPed
                                                        AND tc.Celula = p.Celula
                                                        AND tc.Pedido = p.Pedido
                                                        AND tc.AñoAtt = att.AñoAtt
                                                        AND tc.Folio = att.Folio
                WHERE    dbo.GetFecha(FTerminoRuta)
                         BETWEEN dbo.GetFecha(@FechaIni) AND dbo.GetFecha(
                                                                 @FechaFin)
                         AND att.TipoProducto = 5
                         AND p.TipoCobro IN ( 5 )
                         AND p.Status = 'SURTIDO'
                         AND att.StatusLogistica = 'liqcaja'
                         AND (   @Caja IS NULL
                                 OR MC.Caja = @Caja )
                GROUP BY s.Descripcion ,
                         FTerminoRuta ,
                         MC.Caja
                UNION ALL
                SELECT   2 ,
                         s.Descripcion ,
                         'Estacionario' ,
                         dbo.GetFecha(FTerminoRuta) ,
                         MC.Caja ,
                         ( SUM(p.Litros) - ISNULL(SUM(tc.LitrosAplicacion), 0))
                         * @FactorDensidad ,
                         SUM(p.Total) - SUM(ISNULL(tc.MontoAplicacion, 0))
                FROM     dbo.AutotanqueTurno att
                         JOIN dbo.MovimientoCaja MC ON MC.AñoAtt = att.AñoAtt
                                                       AND MC.FolioAtt = att.Folio
                         JOIN Ruta r ON r.Ruta = att.Ruta
                         JOIN dbo.Pedido p ON att.Folio = p.Folio
                                              AND att.AñoAtt = p.AñoAtt
                         INNER JOIN Celula CL ON r.Celula = CL.Celula
                         JOIN dbo.Sucursal s ON s.Sucursal = CL.Sucursal
                         LEFT JOIN dbo.TipoCuenta tc ON tc.AñoPed = p.AñoPed
                                                        AND tc.Celula = p.Celula
                                                        AND tc.Pedido = p.Pedido
                                                        AND tc.AñoAtt = att.AñoAtt
                                                        AND tc.Folio = att.Folio
                WHERE    dbo.GetFecha(FTerminoRuta)
                         BETWEEN dbo.GetFecha(@FechaIni) AND dbo.GetFecha(
                                                                 @FechaFin)
                         AND att.TipoProducto = 1
                         AND p.Status = 'SURTIDO'
                         AND p.TipoPedido IN ( 1, 2, 3 )
                         AND p.TipoCargo = 1
                         AND p.TipoCobro = 5
                         AND att.StatusLogistica = 'liqcaja'
                         AND p.Cliente NOT IN (   SELECT Cliente
                                                  FROM   dbo.ClienteAutotanque )
                         AND (   @Caja IS NULL
                                 OR MC.Caja = @Caja )
                GROUP BY s.Descripcion ,
                         FTerminoRuta ,
                         MC.Caja
                --Edificios
                UNION ALL
                SELECT   3 ,
                         s.Descripcion ,
                         'Edificios' ,
                         dbo.GetFecha(FTerminoRuta) ,
                         MC.Caja ,
                         ( SUM(p.Litros) - ISNULL(SUM(tc.LitrosAplicacion), 0))
                         * @FactorDensidad ,
                         SUM(p.Total) - SUM(ISNULL(tc.MontoAplicacion, 0))
                FROM     dbo.AutotanqueTurno att
                         JOIN dbo.MovimientoCaja MC ON MC.AñoAtt = att.AñoAtt
                                                       AND MC.FolioAtt = att.Folio
                         JOIN Ruta r ON r.Ruta = att.Ruta
                         JOIN dbo.Pedido p ON att.Folio = p.Folio
                                              AND att.AñoAtt = p.AñoAtt
                         INNER JOIN Celula CL ON r.Celula = CL.Celula
                         JOIN dbo.Sucursal s ON s.Sucursal = CL.Sucursal
                         LEFT JOIN dbo.TipoCuenta tc ON tc.AñoPed = p.AñoPed
                                                        AND tc.Celula = p.Celula
                                                        AND tc.Pedido = p.Pedido
                                                        AND tc.AñoAtt = att.AñoAtt
                                                        AND tc.Folio = att.Folio
                WHERE    dbo.GetFecha(FTerminoRuta)
                         BETWEEN dbo.GetFecha(@FechaIni) AND dbo.GetFecha(
                                                                 @FechaFin)
                         AND p.Status = 'SURTIDO'
                         AND att.StatusLogistica = 'liqcaja'
                         AND TipoCobro IN ( 6, 8, 9 )
                         AND p.Cliente IN (   SELECT Cliente
                                              FROM   vwCCEdificiosAdministrados )
                         AND CyC IS NULL
                         AND (   @Caja IS NULL
                                 OR MC.Caja = @Caja )
                GROUP BY s.Descripcion ,
                         FTerminoRuta ,
                         MC.Caja
                UNION ALL
                SELECT   4 ,
                         s.Descripcion ,
                         'Servicios Tecnicos' ,
                         dbo.GetFecha(FTerminoRuta) ,
                         MC.Caja ,
                         ISNULL(
                             ( SUM(p.Litros)
                               - ISNULL(SUM(tc.LitrosAplicacion), 0))
                             * @FactorDensidad ,
                             0) ,
                         SUM(p.Total) - SUM(ISNULL(tc.MontoAplicacion, 0))
                FROM     Pedido p
                         JOIN dbo.AutotanqueTurno ATT ON ATT.Folio = p.Folio
                                                         AND ATT.AñoAtt = p.AñoAtt
                         JOIN dbo.MovimientoCaja MC ON MC.AñoAtt = ATT.AñoAtt
                                                       AND MC.FolioAtt = ATT.Folio
                         JOIN Ruta r ON r.Ruta = ATT.Ruta
                         INNER JOIN Celula CL ON r.Celula = CL.Celula
                         JOIN dbo.Sucursal s ON s.Sucursal = CL.Sucursal
                         INNER JOIN ServicioTecnico st ON p.Celula = st.Celula
                                                          AND p.AñoPed = st.AñoPed
                                                          AND p.Pedido = st.Pedido
                         INNER JOIN TipoPedido tp ON p.TipoPedido = tp.TipoPedido
                         INNER JOIN TipoServicio ts ON st.TipoServicio = ts.TipoServicio
                         INNER JOIN Cliente c ON c.Cliente = p.Cliente
                         LEFT JOIN dbo.TipoCuenta tc ON tc.AñoPed = p.AñoPed
                                                        AND tc.Celula = p.Celula
                                                        AND tc.Pedido = p.Pedido
                                                        AND tc.AñoAtt = ATT.AñoAtt
                                                        AND tc.Folio = ATT.Folio
                WHERE    p.Status = 'SURTIDO'
                         AND st.StatusServicioTecnico = 'ATENDIDO'
                         AND dbo.GetFecha(FTerminoRuta)
                         BETWEEN dbo.GetFecha(@FechaIni) AND dbo.GetFecha(
                                                                 @FechaFin)
                         AND ATT.StatusLogistica = 'liqcaja'
                         AND p.TipoPedido IN ( 7, 8, 9 )
                         AND (   @Caja IS NULL
                                 OR MC.Caja = @Caja )
                GROUP BY s.Descripcion ,
                         FTerminoRuta ,
                         MC.Caja
                UNION ALL
                SELECT   5 ,
                         s.Descripcion ,
                         'Crédito portátil' ,
                         dbo.GetFecha(FTerminoRuta) ,
                         MC.Caja ,
                         SUM(p.kilos) - SUM(ISNULL(tc.KilosAplicacion, 0)) ,
                         SUM(p.Total) - SUM(ISNULL(tc.MontoAplicacion, 0))
                FROM     dbo.AutotanqueTurno att
                         JOIN dbo.MovimientoCaja MC ON MC.AñoAtt = att.AñoAtt
                                                       AND MC.FolioAtt = att.Folio
                         JOIN dbo.AlmacenGas AG ON AG.AlmacenGas = att.AlmacenGas
                         JOIN dbo.Pedido p ON att.Folio = p.Folio
                                              AND att.AñoAtt = p.AñoAtt
                         INNER JOIN Celula CL ON AG.Celula = CL.Celula
                         JOIN dbo.Sucursal s ON s.Sucursal = CL.Sucursal
                         JOIN dbo.TipoCobro TCO ON TCO.TipoCobro = p.TipoCobro
                         JOIN dbo.TipoPago TPP ON TPP.TipoPago = TCO.TipoPago
                         LEFT JOIN dbo.TipoCuenta tc ON tc.AñoPed = p.AñoPed
                                                        AND tc.Celula = p.Celula
                                                        AND tc.Pedido = p.Pedido
                                                        AND tc.AñoAtt = att.AñoAtt
                                                        AND tc.Folio = att.Folio
                WHERE    dbo.GetFecha(FTerminoRuta)
                         BETWEEN dbo.GetFecha(@FechaIni) AND dbo.GetFecha(
                                                                 @FechaFin)
                         AND att.TipoProducto = 5
                         AND p.TipoCobro IN ( 18 )
                         AND p.Status = 'SURTIDO'
                         AND att.StatusLogistica = 'liqcaja'
                         AND (   @Caja IS NULL
                                 OR MC.Caja = @Caja )
                GROUP BY s.Descripcion ,
                         FTerminoRuta ,
                         MC.Caja
                UNION ALL
                SELECT   6 ,
                         s.Descripcion ,
                         'Credito Estacionario' ,
                         dbo.GetFecha(FTerminoRuta) ,
                         MC.Caja ,
                         ( SUM(p.Litros) - ISNULL(SUM(tc.LitrosAplicacion), 0))
                         * @FactorDensidad ,
                         SUM(p.Total) - SUM(ISNULL(tc.MontoAplicacion, 0))
                FROM     dbo.AutotanqueTurno att
                         JOIN dbo.MovimientoCaja MC ON MC.AñoAtt = att.AñoAtt
                                                       AND MC.FolioAtt = att.Folio
                         JOIN Ruta r ON r.Ruta = att.Ruta
                         JOIN dbo.Pedido p ON att.Folio = p.Folio
                                              AND att.AñoAtt = p.AñoAtt
                         INNER JOIN Celula CL ON r.Celula = CL.Celula
                         JOIN dbo.Sucursal s ON s.Sucursal = CL.Sucursal
                         LEFT JOIN dbo.TipoCuenta tc ON tc.AñoPed = p.AñoPed
                                                        AND tc.Celula = p.Celula
                                                        AND tc.Pedido = p.Pedido
                                                        AND tc.AñoAtt = att.AñoAtt
                                                        AND tc.Folio = att.Folio
                WHERE    dbo.GetFecha(FTerminoRuta)
                         BETWEEN dbo.GetFecha(@FechaIni) AND dbo.GetFecha(
                                                                 @FechaFin)
                         AND att.TipoProducto = 1
                         AND p.Status = 'SURTIDO'
                         AND p.TipoPedido IN ( 1, 2, 3 )
                         AND p.TipoCargo = 1
                         AND p.TipoCobro IN ( 6, 8, 9 )
                         AND att.StatusLogistica = 'liqcaja'
                         AND p.Cliente NOT IN (   SELECT Cliente
                                                  FROM   dbo.ClienteAutotanque )
                         AND (   @Caja IS NULL
                                 OR MC.Caja = @Caja )
                GROUP BY s.Descripcion ,
                         FTerminoRuta ,
                         MC.Caja
                UNION ALL
                SELECT   7 ,
                         s.Descripcion ,
                         'Crédito Edificios' ,
                         dbo.GetFecha(FTerminoRuta) ,
                         MC.Caja ,
                         ( SUM(p.Litros) - ISNULL(SUM(tc.LitrosAplicacion), 0))
                         * @FactorDensidad ,
                         ISNULL(SUM(p.Total), 0)
                FROM     dbo.AutotanqueTurno att
                         JOIN dbo.MovimientoCaja MC ON MC.AñoAtt = att.AñoAtt
                                                       AND MC.FolioAtt = att.Folio
                         JOIN Ruta r ON r.Ruta = att.Ruta
                         JOIN dbo.Pedido p ON att.Folio = p.Folio
                                              AND att.AñoAtt = p.AñoAtt
                         JOIN dbo.LecturaMedidor LM ON LM.AñoPed = p.AñoPed
                                                       AND LM.Celula = p.Celula
                                                       AND LM.Pedido = p.Pedido
                         JOIN dbo.Lectura L ON L.Lectura = LM.Lectura
                         INNER JOIN Celula CL ON r.Celula = CL.Celula
                         JOIN dbo.Sucursal s ON s.Sucursal = CL.Sucursal
                         LEFT JOIN dbo.TipoCuenta tc ON tc.AñoPed = p.AñoPed
                                                        AND tc.Celula = p.Celula
                                                        AND tc.Pedido = p.Pedido
                                                        AND tc.AñoAtt = att.AñoAtt
                                                        AND tc.Folio = att.Folio
                         LEFT JOIN (   SELECT p.AñoPed ,
                                              p.Celula ,
                                              p.Pedido ,
                                              Litros ,
                                              Total
                                       FROM   Pedido AS p
                                       WHERE  TipoCobro IN ( 6, 8, 9 )
                                              AND Status = 'SURTIDO'
                                              AND Cliente IN (   SELECT Cliente
                                                                 FROM   vwCCEdificiosAdministrados )
                                              AND CyC IS NULL ) AS CelConsignacion ON p.AñoPed = CelConsignacion.AñoPed
                                                                                      AND p.Celula = CelConsignacion.Celula
                                                                                      AND p.Pedido = CelConsignacion.Pedido
                WHERE    dbo.GetFecha(FTerminoRuta)
                         BETWEEN dbo.GetFecha(@FechaIni) AND dbo.GetFecha(
                                                                 @FechaFin)
                         AND p.Status = 'SURTIDO'
                         AND att.StatusLogistica = 'liqcaja'
                         AND TipoCobro IN ( 6, 8, 9 )
                         AND p.TipoPedido = 11
                         AND ISNULL(CelConsignacion.Litros, 0) = 0
                         AND (   @Caja IS NULL
                                 OR MC.Caja = @Caja )
                GROUP BY s.Descripcion ,
                         FTerminoRuta ,
                         MC.Caja
                UNION ALL
                SELECT   7 ,
                         s.Descripcion ,
                         'Crédito Edificios' ,
                         dbo.GetFecha(FTerminoRuta) ,
                         MC.Caja ,
                         ( SUM(p.Litros) - ISNULL(SUM(tc.LitrosAplicacion), 0))
                         * @FactorDensidad ,
                         ISNULL(SUM(p.Total), 0)
                FROM     dbo.AutotanqueTurno att
                         JOIN dbo.MovimientoCaja MC ON MC.AñoAtt = att.AñoAtt
                                                       AND MC.FolioAtt = att.Folio
                         JOIN Ruta r ON r.Ruta = att.Ruta
                         JOIN dbo.Pedido p ON att.Folio = p.Folio
                                              AND att.AñoAtt = p.AñoAtt
                         JOIN dbo.LecturaMedidor LM ON LM.CelulaCargo = p.Celula
                                                       AND LM.AñoPedCargo = p.AñoPed
                                                       AND LM.PedidoCargo = p.Pedido
                         JOIN dbo.Lectura L ON L.Lectura = LM.Lectura
                         INNER JOIN Celula CL ON r.Celula = CL.Celula
                         JOIN dbo.Sucursal s ON s.Sucursal = CL.Sucursal
                         LEFT JOIN dbo.TipoCuenta tc ON tc.AñoPed = p.AñoPed
                                                        AND tc.Celula = p.Celula
                                                        AND tc.Pedido = p.Pedido
                                                        AND tc.AñoAtt = att.AñoAtt
                                                        AND tc.Folio = att.Folio
                         LEFT JOIN (   SELECT p.AñoPed ,
                                              p.Celula ,
                                              p.Pedido ,
                                              Litros ,
                                              Total
                                       FROM   Pedido AS p
                                       WHERE  TipoCobro IN ( 6, 8, 9 )
                                              AND Status = 'SURTIDO'
                                              AND Cliente IN (   SELECT Cliente
                                                                 FROM   vwCCEdificiosAdministrados )
                                              AND CyC IS NULL ) AS CelConsignacion ON p.AñoPed = CelConsignacion.AñoPed
                                                                                      AND p.Celula = CelConsignacion.Celula
                                                                                      AND p.Pedido = CelConsignacion.Pedido
                WHERE    dbo.GetFecha(FTerminoRuta)
                         BETWEEN dbo.GetFecha(@FechaIni) AND dbo.GetFecha(
                                                                 @FechaFin)
                         AND p.Status = 'SURTIDO'
                         AND att.StatusLogistica = 'liqcaja'
                         AND TipoCobro IN ( 6, 8, 9 )
                         AND p.TipoCargo = 9
                         AND p.TipoPedido IS NULL
                         AND ISNULL(CelConsignacion.Litros, 0) = 0
                         AND CyC IS NULL
                         AND (   @Caja IS NULL
                                 OR MC.Caja = @Caja )
                GROUP BY s.Descripcion ,
                         FTerminoRuta ,
                         MC.Caja
                UNION ALL
                SELECT   8 ,
                         s.Descripcion ,
                         'Crédito Servicios Tecnicos' ,
                         dbo.GetFecha(FTerminoRuta) ,
                         MC.Caja ,
                         ISNULL(
                             ( SUM(p.Litros)
                               - ISNULL(SUM(tc.LitrosAplicacion), 0))
                             * @FactorDensidad ,
                             0) ,
                         SUM(p.Total) - SUM(ISNULL(tc.MontoAplicacion, 0))
                FROM     Pedido p
                         JOIN dbo.AutotanqueTurno ATT ON ATT.Folio = p.Folio
                                                         AND ATT.AñoAtt = p.AñoAtt
                         JOIN dbo.MovimientoCaja MC ON MC.AñoAtt = ATT.AñoAtt
                                                       AND MC.FolioAtt = ATT.Folio
                         JOIN Ruta r ON r.Ruta = ATT.Ruta
                         INNER JOIN Celula CL ON r.Celula = CL.Celula
                         JOIN dbo.Sucursal s ON s.Sucursal = CL.Sucursal
                         INNER JOIN ServicioTecnico st ON p.Celula = st.Celula
                                                          AND p.AñoPed = st.AñoPed
                                                          AND p.Pedido = st.Pedido
                         INNER JOIN TipoPedido tp ON p.TipoPedido = tp.TipoPedido
                         INNER JOIN TipoServicio ts ON st.TipoServicio = ts.TipoServicio
                         INNER JOIN Cliente c ON c.Cliente = p.Cliente
                         LEFT JOIN dbo.TipoCuenta tc ON tc.AñoPed = p.AñoPed
                                                        AND tc.Celula = p.Celula
                                                        AND tc.Pedido = p.Pedido
                                                        AND tc.AñoAtt = ATT.AñoAtt
                                                        AND tc.Folio = ATT.Folio
                WHERE    p.Status = 'SURTIDO'
                         AND st.StatusServicioTecnico = 'ATENDIDO'
                         AND dbo.GetFecha(FTerminoRuta)
                         BETWEEN dbo.GetFecha(@FechaIni) AND dbo.GetFecha(
                                                                 @FechaFin)
                         AND ATT.StatusLogistica = 'liqcaja'
                         AND p.TipoPedido IN ( 10 )
                         AND (   @Caja IS NULL
                                 OR MC.Caja = @Caja )
                GROUP BY s.Descripcion ,
                         FTerminoRuta ,
                         MC.Caja
                UNION ALL

                /*COBRANZA*/
                SELECT   9 ,
                         s.Descripcion ,
                         'Cobranza' ,
                         dbo.GetFecha(FTerminoRuta) ,
                         MC.Caja ,
                         ( SUM(p.Litros) - ISNULL(SUM(tc.LitrosAplicacion), 0))
                         * @FactorDensidad ,
                         SUM(CP.Total) - SUM(ISNULL(tc.MontoAplicacion, 0))
                FROM     dbo.AutotanqueTurno att
                         JOIN Ruta r ON r.Ruta = att.Ruta
                         JOIN dbo.Pedido p ON att.Folio = p.Folio
                                              AND att.AñoAtt = p.AñoAtt
                         INNER JOIN CobroPedido CP ON p.Celula = CP.Celula
                                                      AND p.AñoPed = CP.AñoPed
                                                      AND p.Pedido = CP.Pedido
                         INNER JOIN Cobro C ON C.AñoCobro = CP.AñoCobro
                                               AND C.Cobro = CP.Cobro
                         INNER JOIN MovimientoCajaCobro MCC ON MCC.AñoCobro = C.AñoCobro
                                                               AND MCC.Cobro = C.Cobro
                         INNER JOIN MovimientoCaja MC ON MC.Caja = MCC.Caja
                                                         AND MC.FOperacion = MCC.FOperacion
                                                         AND MC.Consecutivo = MCC.Consecutivo
                                                         AND MC.Folio = MCC.Folio
                                                         AND MC.Status = 'VALIDADO'
                         INNER JOIN Celula CL ON r.Celula = CL.Celula
                         JOIN dbo.Sucursal s ON s.Sucursal = CL.Sucursal
                         LEFT JOIN dbo.TipoCuenta tc ON tc.AñoPed = p.AñoPed
                                                        AND tc.Celula = p.Celula
                                                        AND tc.Pedido = p.Pedido
                                                        AND tc.AñoAtt = att.AñoAtt
                                                        AND tc.Folio = att.Folio
                WHERE    dbo.GetFecha(FTerminoRuta)
                         BETWEEN dbo.GetFecha(@FechaIni) AND dbo.GetFecha(
                                                                 @FechaFin)
                         AND p.Status = 'SURTIDO'
                         AND att.StatusLogistica = 'liqcaja'
                         AND (   @Caja IS NULL
                                 OR MC.Caja = @Caja )
                         AND MC.TipoMovimientoCaja IN ( 1, 3 )
                GROUP BY s.Descripcion ,
                         FTerminoRuta ,
                         MC.Caja

                /* cobranza filiales*/
                UNION ALL
                SELECT 10 ,
                       '' ,
                       'Cobranza Filial' ,
                       '' ,
                       '' ,
                       0 ,
                       0
                UNION ALL
                SELECT   11 ,
                         S.Descripcion ,
                         'Otros Ingresos' ,
                         dbo.GetFecha(mc.FOperacion) ,
                         MC.Caja ,
                         0 AS Kilos,
                         SUM(C.Total) AS Total
                FROM     MovimientoCaja MC
                         JOIN dbo.TipoMovimientoCaja tmc ON tmc.TipoMovimientoCaja = MC.TipoMovimientoCaja
                         INNER JOIN dbo.Ruta R ON R.Ruta = MC.Ruta
                         JOIN Celula cE ON R.Celula = cE.Celula
                         JOIN dbo.Sucursal S ON S.Sucursal = cE.Sucursal
                         INNER JOIN MovimientoCajaCobro MCC ON MC.Caja = MCC.Caja
                                                               AND MC.FOperacion = MCC.FOperacion
                                                               AND MC.Consecutivo = MCC.Consecutivo
                                                               AND MC.Folio = MCC.Folio
                         INNER JOIN Cobro C ON MCC.AñoCobro = C.AñoCobro
                                               AND MCC.Cobro = C.Cobro
                WHERE    dbo.GetFecha(MC.FOperacion)
                         BETWEEN dbo.GetFecha(@FechaIni) AND dbo.GetFecha(
                                                                 @FechaFin)
                         AND tmc.NotaIngreso = 1
                         AND (   @Caja IS NULL
                                 OR MC.Caja = @Caja )
                         AND MC.Status = 'VALIDADO'
                GROUP BY S.Descripcion ,
                         MC.FOperacion ,
                         MC.Caja;



    INSERT INTO @Tabla
                SELECT   12 ,
                         Sucursal ,
                         'Total ' + Concepto ,
                         NULL AS Fecha ,
                         Caja ,
                         SUM(Kilos) ,
                         SUM(Total) AS Total
                FROM     @Tabla
                GROUP BY ID ,
                         Caja ,
                         [@Tabla].Sucursal ,
                         [@Tabla].Concepto;


    SELECT   Concepto ,
             Fecha ,
             Caja ,
             ISNULL(SUM(Kilos), 0) AS Kilos ,
             ISNULL(SUM(Total), 0) AS Importe
    FROM     @Tabla
    GROUP BY ID ,
             Fecha ,
             Concepto ,
             Caja
    ORDER BY ID ,
             Concepto ,
             Fecha;