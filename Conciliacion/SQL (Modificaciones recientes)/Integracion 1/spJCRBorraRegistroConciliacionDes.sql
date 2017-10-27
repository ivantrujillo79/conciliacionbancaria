CREATE PROCEDURE spJCRBorraRegistroConciliacionDes  
  
  
  
 @CorporativoConciliacion INT ,  
    @SucursalConciliacion INT ,  
    @Añoconciliacion INT ,  
    @Mesconciliacion INT ,  
    @FolioConciliacion INT  
  
  
 AS  
 SET NOCOUNT ON   
   
      
  
SELECT  tdd.* INTO TmpConciliacionReferencia  
FROM    dbo.TablaDestinoDetalle cf  
        JOIN dbo.ConciliacionReferencia tdd ON tdd.CorporativoExterno = cf.Corporativo  
                                               AND tdd.SucursalExterno = cf.Sucursal  
                                               AND tdd.AñoExterno = cf.Año  
                                               AND tdd.FolioExterno = cf.Folio  
                                               AND tdd.SecuenciaExterno = cf.Secuencia  
WHERE   tdd.CorporativoConciliacion = @CorporativoConciliacion  
        AND tdd.SucursalConciliacion = @SucursalConciliacion  
        AND tdd.AñoConciliacion = @Añoconciliacion  
        AND tdd.MesConciliacion = @Mesconciliacion  
        AND tdd.FolioConciliacion = @FolioConciliacion;       
  
  
DELETE tdd  
FROM    dbo.TablaDestinoDetalle cf  
        JOIN dbo.ConciliacionReferencia tdd ON tdd.CorporativoExterno = cf.Corporativo  
                                               AND tdd.SucursalExterno = cf.Sucursal  
                                               AND tdd.AñoExterno = cf.Año  
                                               AND tdd.FolioExterno = cf.Folio  
                                               AND tdd.SecuenciaExterno = cf.Secuencia  
WHERE   tdd.CorporativoConciliacion = @CorporativoConciliacion  
        AND tdd.SucursalConciliacion = @SucursalConciliacion  
        AND tdd.AñoConciliacion = @Añoconciliacion  
        AND tdd.MesConciliacion = @Mesconciliacion  
        AND tdd.FolioConciliacion = @FolioConciliacion;       
  
  
  
  
UPDATE cf  
 SET cf.StatusConciliacion = 'EN PROCESO DE CONCILIACION',cf.MotivoNoConciliado=NULL    
FROM    dbo.TablaDestinoDetalle cf  
        JOIN dbo.TmpConciliacionReferencia tdd ON tdd.CorporativoExterno = cf.Corporativo  
                                               AND tdd.SucursalExterno = cf.Sucursal  
                                               AND tdd.AñoExterno = cf.Año  
                                               AND tdd.FolioExterno = cf.Folio  
                                               AND tdd.SecuenciaExterno = cf.Secuencia  
WHERE   tdd.CorporativoConciliacion = @CorporativoConciliacion  
        AND tdd.SucursalConciliacion = @SucursalConciliacion  
        AND tdd.AñoConciliacion = @Añoconciliacion  
        AND tdd.MesConciliacion = @Mesconciliacion  
        AND tdd.FolioConciliacion = @FolioConciliacion;       
  
  
  DROP TABLE TmpConciliacionReferencia  
  
  
update dbo.Conciliacion SET statusconciliacion = 'CONCILIACION ABIERTA' WHERE  CorporativoConciliacion = @CorporativoConciliacion  
        AND SucursalConciliacion = @SucursalConciliacion  
        AND AñoConciliacion = @Añoconciliacion  
        AND MesConciliacion = @Mesconciliacion  
        AND FolioConciliacion = @FolioConciliacion; 