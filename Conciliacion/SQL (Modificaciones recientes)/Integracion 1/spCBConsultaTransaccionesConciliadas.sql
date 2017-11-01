/********************************************************  
Realizo: Gabina Leon Velasco   
Fecha: 22/05/2013  
Descripcion: Devuelve los regsitros conciliados externo  
exec spCBConsultaTransaccionesConciliadas 2,1,2013,4,5,0;
*********************************************************/  
/*
Se agregaron los Campos
a)serie factura
b)factura
c)cliente referencia
Fecha: 01/11/2017
por  Jose Carlos Reyes Ramos.
*/
  
CREATE PROCEDURE dbo.spCBConsultaTransaccionesConciliadas
    @CorporativoConciliacion AS INT ,
    @SucursalConciliacion AS INT ,
    @AñoConciliacion AS INT ,
    @MesConciliacion AS SMALLINT ,
    @FolioConciliacion AS INT ,
    @FormaConciliacion AS INT
AS
    SET NOCOUNT ON;    
  
    IF @FormaConciliacion = 0 -- TODAS LOS REGISTROS CONCILIADOS  
        SELECT  TDD.Corporativo ,
                C.SucursalConciliacion AS Sucursal ,
                Sucursal.Descripcion AS SucursalDes ,
                TDD.Año ,
                TDD.Folio ,
                TDD.Secuencia ,
                TDD.Descripcion ,
                TDD.Concepto ,
                TDD.Deposito ,
                TDD.Retiro ,
                ISNULL(CP.FormaConciliacion, CR.FormaConciliacion) AS FormaConciliacion ,
                ISNULL(CP.StatusConcepto, CR.StatusConcepto) AS StatusConcepto ,
                ISNULL(CP.StatusConciliacion, CR.StatusConciliacion) AS StatusConciliacion ,
                TDD.FOperacion ,
                TDD.FMovimiento ,
                C.FolioConciliacion ,
                C.MesConciliacion ,
                ISNULL(CR.AñoConciliacion / CR.AñoConciliacion, 0) AS ConInterno ,
                TDD.Cheque ,
                TDD.Referencia ,
                TDD.NombreTercero ,
                TDD.RFCTercero ,
                p.SerieFactura ,
                p.Factura ,
                CONVERT(VARCHAR, CL.Cliente)
                + CONVERT(VARCHAR, dbo.DigitoVerificador(CL.Cliente)) AS CliReferencia
        FROM    TablaDestinoDetalle TDD
                INNER JOIN TablaDestino TD ON TD.Corporativo = TDD.Corporativo
                                              AND TD.Sucursal = TDD.Sucursal
                                              AND TD.Año = TDD.Año
                                              AND TD.Folio = TDD.Folio
                INNER JOIN TipoFuenteInformacion TFI ON TFI.TipoFuenteInformacion = TD.TipoFuenteInformacion
                INNER JOIN TipoFuente TF ON TF.TipoFuente = TFI.TipoFuente
                                            AND TF.TipoFuente = 2 -- EXTERNO        
                INNER JOIN Conciliacion C ON TD.Corporativo = C.CorporativoExterno
                                             AND TD.Sucursal = C.SucursalExterno
                                             AND TD.Año = C.AñoExterno
                                             AND TD.Folio = C.FolioExterno
                LEFT JOIN ConciliacionReferencia CR ON CR.CorporativoExterno = TDD.Corporativo
                                                       AND CR.SucursalExterno = TDD.Sucursal
                                                       AND CR.AñoExterno = TDD.Año
                                                       AND CR.FolioExterno = TDD.Folio
                                                       AND CR.SecuenciaExterno = TDD.Secuencia
                                                       AND C.CorporativoConciliacion = CR.CorporativoConciliacion
                                                       AND C.SucursalConciliacion = CR.SucursalConciliacion
                                                       AND C.AñoConciliacion = CR.AñoConciliacion
                                                       AND C.FolioConciliacion = CR.FolioConciliacion
                                                       AND CR.StatusConciliacion IN (
                                                       'CONCILIADA',
         'CONCILIADA S/REFERENCIA' )
                LEFT JOIN FormaConciliacion FCR ON FCR.FormaConciliacion = CR.FormaConciliacion
                LEFT JOIN StatusConcepto SCR ON SCR.StatusConcepto = CR.StatusConcepto
                LEFT JOIN ConciliacionPedido CP ON CP.CorporativoExterno = TDD.Corporativo
                                                   AND CP.SucursalExterno = TDD.Sucursal
                                                   AND CP.AñoExterno = TDD.Año
                                                   AND CP.FolioExterno = TDD.Folio
                                                   AND CP.SecuenciaExterno = TDD.Secuencia
                                                   AND C.CorporativoConciliacion = CP.CorporativoConciliacion
                                                   AND C.SucursalConciliacion = CP.SucursalConciliacion
                                                   AND C.AñoConciliacion = CP.AñoConciliacion
                                                   AND C.FolioConciliacion = CP.FolioConciliacion
                                                   AND CP.StatusConciliacion IN (
                                                   'CONCILIADA',
                                                   'CONCILIADA S/REFERENCIA' )
			  --20171101001
                LEFT	 JOIN dbo.Pedido p ON p.Celula = CP.Celula
                                              AND p.AñoPed = CP.AñoPed
                                              AND p.Pedido = CP.Pedido
                LEFT JOIN dbo.Cliente CL ON CL.Cliente = p.Cliente  
			--20171101001
                LEFT JOIN FormaConciliacion FCP ON FCP.FormaConciliacion = CR.FormaConciliacion
                LEFT JOIN StatusConcepto SCP ON SCP.StatusConcepto = CP.StatusConcepto
                INNER JOIN Sucursal ON Sucursal.Sucursal = C.SucursalConciliacion
        WHERE   C.CorporativoConciliacion = @CorporativoConciliacion
                AND C.SucursalConciliacion = @SucursalConciliacion
                AND C.AñoConciliacion = @AñoConciliacion
                AND C.MesConciliacion = @MesConciliacion
                AND C.FolioConciliacion = @FolioConciliacion
                AND ( CR.SecuenciaExterno IS NOT NULL
                      OR CP.SecuenciaExterno IS NOT NULL
                    )
        GROUP BY TDD.Corporativo ,
                C.SucursalConciliacion ,
                Sucursal.Descripcion ,
                TDD.Año ,
                TDD.Folio ,
                TDD.Secuencia ,
                TDD.Descripcion ,
                TDD.Concepto ,
                TDD.Deposito ,
                TDD.Retiro ,
                CP.FormaConciliacion ,
                CR.FormaConciliacion ,
                CP.StatusConcepto ,
                CR.StatusConcepto ,
                CP.StatusConciliacion ,
                CR.StatusConciliacion ,
                TDD.FOperacion ,
                TDD.FMovimiento ,
                C.FolioConciliacion ,
                C.MesConciliacion ,
                CR.AñoConciliacion ,
                CR.AñoConciliacion ,
                TDD.Cheque ,
                TDD.Referencia ,
                TDD.NombreTercero ,
                TDD.RFCTercero,
				p.SerieFactura,
				p.Factura,
				CL.Cliente
				  
  
    IF @FormaConciliacion > 0 -- LOS REGISTROS CONCILIADOS POR UNA FORMA ESPECIFICA    
        SELECT  TDD.Corporativo ,
                C.SucursalConciliacion AS Sucursal ,
                Sucursal.Descripcion AS SucursalDes ,
                TDD.Año ,
                TDD.Folio ,
                TDD.Secuencia ,
                TDD.Descripcion ,
                TDD.Concepto ,
                TDD.Deposito ,
                TDD.Retiro ,
                ISNULL(CP.FormaConciliacion, CR.FormaConciliacion) AS FormaConciliacion ,
                ISNULL(CP.StatusConcepto, CR.StatusConcepto) AS StatusConcepto ,
                ISNULL(CP.StatusConciliacion, CR.StatusConciliacion) AS StatusConciliacion ,
                TDD.FOperacion ,
                TDD.FMovimiento ,
                C.FolioConciliacion ,
                C.MesConciliacion ,
                ISNULL(CR.AñoConciliacion / CR.AñoConciliacion, 0) AS ConInterno ,
                TDD.Cheque ,
                TDD.Referencia ,
                TDD.NombreTercero ,
                TDD.RFCTercero ,
                p.SerieFactura ,
                p.Factura ,
                CONVERT(VARCHAR, CL.Cliente)
                + CONVERT(VARCHAR, dbo.DigitoVerificador(CL.Cliente)) AS CliReferencia
        FROM    TablaDestinoDetalle TDD
                INNER JOIN TablaDestino TD ON TD.Corporativo = TDD.Corporativo
                                              AND TD.Sucursal = TDD.Sucursal
                                              AND TD.Año = TDD.Año
                                              AND TD.Folio = TDD.Folio
                INNER JOIN TipoFuenteInformacion TFI ON TFI.TipoFuenteInformacion = TD.TipoFuenteInformacion
                INNER JOIN TipoFuente TF ON TF.TipoFuente = TFI.TipoFuente
                                            AND TF.TipoFuente = 2 -- EXTERNO        
                INNER JOIN Conciliacion C ON TD.Corporativo = C.CorporativoExterno
                                             AND TD.Sucursal = C.SucursalExterno
                                             AND TD.Año = C.AñoExterno
                                             AND TD.Folio = C.FolioExterno
                LEFT JOIN ConciliacionReferencia CR ON CR.CorporativoExterno = TDD.Corporativo
                                                       AND CR.SucursalExterno = TDD.Sucursal
                                                       AND CR.AñoExterno = TDD.Año
                                                       AND CR.FolioExterno = TDD.Folio
                                                       AND CR.SecuenciaExterno = TDD.Secuencia
                                                       AND C.CorporativoConciliacion = CR.CorporativoConciliacion
                                                       AND C.SucursalConciliacion = CR.SucursalConciliacion
                                                       AND C.AñoConciliacion = CR.AñoConciliacion
                                                       AND C.FolioConciliacion = CR.FolioConciliacion
                                                       AND CR.FormaConciliacion = @FormaConciliacion
                                                       AND CR.StatusConciliacion IN (
                                                       'CONCILIADA',
                                                       'CONCILIADA S/REFERENCIA' )
                LEFT JOIN FormaConciliacion FCR ON FCR.FormaConciliacion = CR.FormaConciliacion
                LEFT JOIN StatusConcepto SCR ON SCR.StatusConcepto = CR.StatusConcepto
                LEFT JOIN ConciliacionPedido CP ON CP.CorporativoExterno = TDD.Corporativo
                                                   AND CP.SucursalExterno = TDD.Sucursal
                                                   AND CP.AñoExterno = TDD.Año
                                                   AND CP.FolioExterno = TDD.Folio
                                                   AND CP.SecuenciaExterno = TDD.Secuencia
                                                   AND C.CorporativoConciliacion = CP.CorporativoConciliacion
                                                   AND C.SucursalConciliacion = CP.SucursalConciliacion
                                                   AND C.AñoConciliacion = CP.AñoConciliacion
                                                   AND C.FolioConciliacion = CP.FolioConciliacion
                                                   AND CP.FormaConciliacion = @FormaConciliacion
                                                   AND CP.StatusConciliacion IN (
                         'CONCILIADA',
                                                   'CONCILIADA S/REFERENCIA' ) 
			   --20171101001
                LEFT	 JOIN dbo.Pedido p ON p.Celula = CP.Celula
                                              AND p.AñoPed = CP.AñoPed
                                              AND p.Pedido = CP.Pedido
                LEFT JOIN dbo.Cliente CL ON CL.Cliente = p.Cliente  
			--20171101001 
                LEFT JOIN FormaConciliacion FCP ON FCP.FormaConciliacion = CR.FormaConciliacion
                LEFT JOIN StatusConcepto SCP ON SCP.StatusConcepto = CP.StatusConcepto
                INNER JOIN Sucursal ON Sucursal.Sucursal = C.SucursalConciliacion
        WHERE   C.CorporativoConciliacion = @CorporativoConciliacion
                AND C.SucursalConciliacion = @SucursalConciliacion
                AND C.AñoConciliacion = @AñoConciliacion
                AND C.MesConciliacion = @MesConciliacion
                AND C.FolioConciliacion = @FolioConciliacion
                AND ( CR.SecuenciaExterno IS NOT NULL
                      OR CP.SecuenciaExterno IS NOT NULL
                    )
        GROUP BY TDD.Corporativo ,
                C.SucursalConciliacion ,
                Sucursal.Descripcion ,
                TDD.Año ,
                TDD.Folio ,
                TDD.Secuencia ,
                TDD.Descripcion ,
                TDD.Concepto ,
                TDD.Deposito ,
                TDD.Retiro ,
                CP.FormaConciliacion ,
                CR.FormaConciliacion ,
                CP.StatusConcepto ,
                CR.StatusConcepto ,
                CP.StatusConciliacion ,
                CR.StatusConciliacion ,
                TDD.FOperacion ,
                TDD.FMovimiento ,
                C.FolioConciliacion ,
                C.MesConciliacion ,
                CR.AñoConciliacion ,
                CR.AñoConciliacion ,
                TDD.Cheque ,
                TDD.Referencia ,
                TDD.NombreTercero ,
                TDD.RFCTercero,
				p.SerieFactura,
				p.Factura,
				CL.Cliente

