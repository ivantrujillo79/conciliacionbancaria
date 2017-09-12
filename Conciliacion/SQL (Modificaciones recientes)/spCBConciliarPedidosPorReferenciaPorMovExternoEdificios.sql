/********************************************************
Realizo: Christian Daniel Castellanos Mtz
Fecha: 28/05/2014
Descripcion: Busca los registros en pedido cuyo deposito 
sea igual a un registro externo, compara referencia=cliente y deposito= saldo 

*********************************************************/

CREATE PROCEDURE [dbo].[spCBConciliarPedidosPorReferenciaPorMovExternoEdificios]

@Corporativo SmallInt,
@SucursalConciliacion SmallInt,
@AñoConciliacion SmallInt,
@MesConciliacion SmallInt,
@FolioConciliacion SmallInt,

@SucursalExterno SmallInt,
@AñoExterno SmallInt,
@FolioExterno SmallInt,
@SecuenciaExterno SmallInt,

@Centavos as Decimal (10,2),
@StatusConcepto as Int, 
@Cadena as Varchar(1000),

@CampoExterno as Varchar(50),
@CampoPedido as Varchar(50)


AS  
SET NOCOUNT ON 

DECLARE @Query as nVarchar(2000)
DECLARE @DepositoExterno DECIMAL(10,2)
DECLARE @ValorCampoExterno NVARCHAR(50)
DECLARE @Parm NVARCHAR(500)

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SET @DepositoExterno=(SELECT TDD.Deposito from TablaDestinoDetalle TDD where TDD.Folio=@FolioExterno and TDD.Secuencia=@SecuenciaExterno and TDD.Corporativo=@Corporativo
and TDD.Sucursal=@SucursalExterno and TDD.Año=@AñoExterno)

SET @Query = N'SELECT @ValorOUT = ISNULL(TDD.'+@CampoExterno+','''')
FROM TablaDestinoDetalle TDD where TDD.Corporativo=@CorporativoEx and TDD.Sucursal=@Sucursal and TDD.Año=@Año 
and TDD.Secuencia=@Secuencia and TDD.Folio=@Folio';

SET @Parm = N'@Folio int,@CorporativoEx int,@Sucursal smallint,@Año int,@Secuencia int,@ValorOUT varchar(50) OUTPUT';

EXECUTE sp_executesql @Query, @Parm, @Folio = @FolioExterno,@CorporativoEx = @Corporativo,
					  @Sucursal= @SucursalExterno,@Año = @AñoExterno,@Secuencia = @SecuenciaExterno,
					  @ValorOUT=@ValorCampoExterno OUTPUT;--, 

if @ValorCampoExterno <>'' and @CampoPedido <>''
BEGIN 

IF NOT EXISTS (SELECT * FROM ConciliacionReferencia CR where CR.CorporativoExterno=@Corporativo AND CR.SucursalExterno=@SucursalExterno 
			   AND CR.AñoExterno=@AñoExterno AND CR.FolioExterno=@FolioExterno AND CR.SecuenciaExterno=@SecuenciaExterno AND CR.CorporativoConciliacion=@Corporativo
			   AND CR.SucursalConciliacion=@SucursalConciliacion AND CR.MesConciliacion=@MesConciliacion AND CR.AñoConciliacion=@AñoConciliacion 
			   AND CR.FolioConciliacion=@FolioConciliacion) 
AND NOT EXISTS (SELECT * FROM ConciliacionPedido CP where CP.CorporativoExterno=@Corporativo AND CP.SucursalExterno=@SucursalExterno 
                AND CP.AñoExterno=@AñoExterno AND CP.FolioExterno=@FolioExterno AND CP.SecuenciaExterno=@SecuenciaExterno AND CP.CorporativoConciliacion=@Corporativo
				AND CP.SucursalConciliacion=@SucursalConciliacion AND CP.MesConciliacion=@MesConciliacion AND CP.AñoConciliacion=@AñoConciliacion 
				AND CP.FolioConciliacion=@FolioConciliacion)
BEGIN

SET @Query=''

SET @Query='
SELECT  I.Celula as CelulaPedido,I.AñoPed as AñoPedido,I.Pedido,I.Sucursal as SucursalPedido,SI.Descripcion SucursalPedidoDes,I.TipoCargo as ConceptoPedido,ISNULL(I.Remision,0) RemisionPedido,ISNULL(I.SerieRemision,'''') SeriePedido,I.Folio as FolioSat,I.

Serie as SerieSat,I.Saldo as Total,I.Saldo as MontoConciliado,I.Cliente,I.Nombre,I.PedidoReferencia 
FROM vwCBPedidosPorAbonarEdificiosAdministrados I INNER JOIN Sucursal SI ON SI.Sucursal=I.Sucursal WHERE I.'+Convert(varchar(50),@CampoPedido)+' = '''+@ValorCampoExterno+'''
AND ((I.Saldo + ISNULL(I.SaldoPC, 0)) = '''+Convert(varchar(20),@DepositoExterno)+''')'
select   @Query
END

END
