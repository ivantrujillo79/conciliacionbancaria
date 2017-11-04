/*********************************************************
Programó: Claudia García
Fecha: 02/05/2013
Descripción: Actualiza la informacion de la tabla conciliacion referencia
***********************************************************/

alter Procedure dbo.spCBActualizaConciliacionReferencia
@Configuracion as SmallInt,
@Corporativo as TinyInt,
@Sucursal as TinyInt,
@AñoConciliacion As Int,
@MesConciliacion As SmallInt,
@FolioConciliacion As Int,
@SecuenciaRelacion as Int=0,

@SucursalInterno As TinyInt,
@AñoInterno as Int,
@FolioInterno As Int,
@SecuenciaInterno As Int,
@AñoExterno as Int,
@FolioExterno As Int,
@SecuenciaExterno As Int,
@Concepto As VarChar(500),
@MontoConciliado As Money,
@Diferencia As Money,
@MontoExterno As Money,
@MontoInterno As Money,
@FormaConciliacion SmallInt,
@StatusConcepto as SmallInt,
@StatusConciliacion As VarChar(100),
@MotivoNoConciliado as SmallInt,
@ComentarioNoConciliado  as Varchar(250),
@Descripcion as Varchar(1000) =''    
AS  

SET NOCOUNT ON  

--DECLARE @SecuenciaRelacion Int

DECLARE @UsuarioAlta Varchar(15)
SET @UsuarioAlta= dbo.NombreUsuario()

IF @SecuenciaRelacion IS NULL or @SecuenciaRelacion=0
BEGIN
	SET @SecuenciaRelacion = (SELECT ISNULL(MAX(SecuenciaRelacion),1)+1 FROM ConciliacionReferencia WHERE CorporativoConciliacion = 
	@Corporativo AND SucursalConciliacion = @Sucursal AND AñoConciliacion = @AñoConciliacion AND FolioConciliacion = @FolioConciliacion
	AND MesConciliacion= @MesConciliacion)
END

IF @Configuracion = 0  --Registra conciliacion CONCILIADA
BEGIN  
     BEGIN TRANSACTION  
	 	      --BORRAR AQUELLOS MOVIMIENTOS QUE SE CONCILIARON MANUALMENTE COMO :" CONCILIACION SIN REFERENCIA "
		  DELETE FROM ConciliacionReferencia where 
		  CorporativoConciliacion = @Corporativo AND
          SucursalConciliacion = @Sucursal AND
          AñoConciliacion = @AñoConciliacion  AND
          MesConciliacion = @MesConciliacion AND
          FolioConciliacion = @FolioConciliacion AND
          AñoExterno = @AñoExterno AND
          FolioExterno = @FolioExterno AND
          SecuenciaExterno= @SecuenciaExterno AND
		  StatusConciliacion='CONCILIADA S/REFERENCIA'

		  DELETE FROM ConciliacionPedido where 
		  CorporativoConciliacion = @Corporativo AND
          SucursalConciliacion = @Sucursal AND
          AñoConciliacion = @AñoConciliacion  AND
          MesConciliacion = @MesConciliacion AND
          FolioConciliacion = @FolioConciliacion AND
          AñoExterno = @AñoExterno AND
          FolioExterno = @FolioExterno AND
          SecuenciaExterno= @SecuenciaExterno AND
		  StatusConciliacion='CONCILIADA S/REFERENCIA'

		  IF NOT EXISTS(SELECT * 
						FROM TablaDestinoDetalle 
						WHERE Corporativo = @Corporativo
						AND Sucursal=@SucursalInterno
						AND Año = @AñoInterno
						AND Folio = @FolioInterno
						AND Secuencia= @SecuenciaInterno)
		  BEGIN

				IF NOT EXISTS(SELECT * 
						FROM TablaDestino
						WHERE Corporativo = @Corporativo
						AND Sucursal=@SucursalInterno
						AND Año = @AñoInterno
						AND Folio = @FolioInterno)
				BEGIN
					INSERT INTO TablaDestino(Corporativo, Sucursal, Año, Folio,FAlta,CuentaBancoFinanciero,TipoFuenteInformacion,Frecuencia,FInicial,FFinal,StatusConciliacion,Usuario)
					VALUES(@Corporativo, @SucursalInterno, @AñoInterno, @FolioInterno,GETDATE(),'',1,1,GETDATE(),GETDATE(),'CONCILIADA',@UsuarioAlta)
				END
                
			  INSERT INTO TablaDestinoDetalle (Corporativo,Sucursal,Año,Folio,Secuencia,CuentaBancaria,FOperacion,FMovimiento)
			  VALUES(@Corporativo, @SucursalInterno, @AñoInterno, @FolioInterno, @SecuenciaInterno,'CUENTABANACARIA',GETDATE(),GETDATE());
			  --SELECT @Corporativo, @Sucursal, @AñoConciliacion, @FolioConciliacion, @SecuenciaRelacion,'CUENTABANACARIA',GETDATE(),GETDATE()
		  END

          INSERT INTO dbo.ConciliacionReferencia
				 (CorporativoConciliacion, SucursalConciliacion, AñoConciliacion, MesConciliacion,FolioConciliacion, SecuenciaRelacion,
                      CorporativoInterno, SucursalInterno, AñoInterno, FolioInterno, SecuenciaInterno,
                      CorporativoExterno, SucursalExterno, AñoExterno, FolioExterno, SecuenciaExterno,
                      Concepto, MontoConciliado, Diferencia, MontoExterno, MontoInterno, 
                      FormaConciliacion, StatusConcepto, StatusConciliacion, MotivoNoConciliado, ComentarioNoConciliado, Usuario, FAlta )
          VALUES (@Corporativo, @Sucursal, @AñoConciliacion, @MesConciliacion, @FolioConciliacion, @SecuenciaRelacion,
                  @Corporativo, @SucursalInterno, @AñoInterno, @FolioInterno, @SecuenciaInterno,
                  @Corporativo, @Sucursal, @AñoExterno, @FolioExterno, @SecuenciaExterno,
                  @Concepto, @MontoConciliado, @Diferencia, @MontoExterno, @MontoInterno,
                  @FormaConciliacion, @StatusConcepto, 'CONCILIADA', 0,'', @UsuarioAlta, GETDATE())
          
        UPDATE TablaDestinoDetalle SET StatusConciliacion = 'CONCILIADA'
          WHERE Corporativo=@Corporativo
          AND Sucursal= @Sucursal
          AND Año= @AñoConciliacion
          AND Folio = @FolioInterno
          AND Secuencia= @SecuenciaInterno

          UPDATE TablaDestinoDetalle SET StatusConciliacion = 'CONCILIADA'
          WHERE Corporativo=@Corporativo
          AND Sucursal= @Sucursal
          AND Año= @AñoConciliacion
          AND Folio = @FolioExterno
          AND Secuencia= @SecuenciaExterno

          SELECT @FolioConciliacion As Folio

          IF @@ERROR <> 0  
          BEGIN  
               ROLLBACK TRANSACTION  
               RAISERROR('Ha ocurrido un error al dar de alta el registro. R01',16,1)  
               RETURN 0
          END  
                 
     COMMIT TRANSACTION    
         
END  


IF @Configuracion = 1  --desconcilia el registro por el valor de externo
BEGIN  
     BEGIN TRANSACTION  

         DELETE FROM dbo.ConciliacionReferencia
          WHERE  CorporativoConciliacion = @Corporativo AND
          SucursalConciliacion = @Sucursal AND
          AñoConciliacion = @AñoConciliacion  AND
          MesConciliacion = @MesConciliacion AND
          FolioConciliacion = @FolioConciliacion AND
          AñoExterno= @AñoExterno AND
          FolioExterno = @FolioExterno AND
          SecuenciaExterno= @SecuenciaExterno         
          
          UPDATE TablaDestinoDetalle SET StatusConciliacion = 'EN PROCESO DE CONCILIACION',MotivoNoConciliado=NULL
          WHERE Corporativo=@Corporativo
          AND Sucursal= @Sucursal
          AND Año= @AñoExterno
          AND Folio = @FolioExterno
          AND Secuencia= @SecuenciaExterno
          
          SELECT @FolioConciliacion As Folio

          IF @@ERROR <> 0  
          BEGIN  
               ROLLBACK TRANSACTION  
               RAISERROR('Ha ocurrido un error al dar de alta el registro. R01',16,1)  
               RETURN 0
          END  
                 
     COMMIT TRANSACTION   
            
END  

IF @Configuracion = 2  --Registra conciliacion CANCELADA EXTERNO
BEGIN  
     BEGIN TRANSACTION  

          INSERT INTO dbo.ConciliacionReferencia(CorporativoConciliacion, SucursalConciliacion, AñoConciliacion, MesConciliacion,FolioConciliacion, SecuenciaRelacion,
                      CorporativoInterno, SucursalInterno, AñoInterno, FolioInterno, SecuenciaInterno,
                      CorporativoExterno, SucursalExterno, AñoExterno, FolioExterno, SecuenciaExterno,
                      Concepto, MontoConciliado, Diferencia, MontoExterno, MontoInterno, 
                      FormaConciliacion, StatusConcepto, StatusConciliacion, MotivoNoConciliado, ComentarioNoConciliado, Usuario, FAlta )
          VALUES (@Corporativo, @Sucursal, @AñoConciliacion, @MesConciliacion, @FolioConciliacion, @SecuenciaRelacion,
                  null, null, null, null, null,
                  @Corporativo, @Sucursal, @AñoExterno, @FolioExterno, @SecuenciaExterno,
                  @Concepto, 0, 0, @MontoExterno, null,
                  @FormaConciliacion, @StatusConcepto, 'CONCILIACION CANCELADA', @MotivoNoConciliado, @ComentarioNoConciliado, @UsuarioAlta, GETDATE())
          
          UPDATE TablaDestinoDetalle SET StatusConciliacion = 'CONCILIACION CANCELADA',MotivoNoConciliado=@MotivoNoConciliado
          WHERE Corporativo=@Corporativo
          AND Sucursal= @Sucursal
     AND Año= @AñoExterno
          AND Folio = @FolioExterno
          AND Secuencia= @SecuenciaExterno

          SELECT @FolioConciliacion As Folio

          IF @@ERROR <> 0  
          BEGIN  
               ROLLBACK TRANSACTION  
               RAISERROR('Ha ocurrido un error al dar de alta el registro. R01',16,1)  
               RETURN 0
          END  
                 
     COMMIT TRANSACTION    
            
END  


IF @Configuracion = 3 --Registra conciliacion CANCELADA INTERNO
BEGIN  
     BEGIN TRANSACTION  

          INSERT INTO dbo.ConciliacionReferencia(CorporativoConciliacion, SucursalConciliacion, AñoConciliacion, MesConciliacion,FolioConciliacion, SecuenciaRelacion,
                      CorporativoInterno, SucursalInterno, AñoInterno, FolioInterno, SecuenciaInterno,
                      CorporativoExterno, SucursalExterno, AñoExterno, FolioExterno, SecuenciaExterno,
                      Concepto, MontoConciliado, Diferencia, MontoExterno, MontoInterno, 
                      FormaConciliacion, StatusConcepto, StatusConciliacion, MotivoNoConciliado, ComentarioNoConciliado, Usuario, FAlta )
          VALUES (@Corporativo, @Sucursal, @AñoConciliacion, @MesConciliacion, @FolioConciliacion, @SecuenciaRelacion,
                  @Corporativo, @SucursalInterno, @AñoInterno, @FolioInterno, @SecuenciaInterno,
                  null, null, null, null, null,
                  @Concepto, 0, 0, null, @MontoInterno,
                  @FormaConciliacion, @StatusConcepto, 'CONCILIACION CANCELADA',@MotivoNoConciliado, @ComentarioNoConciliado, @UsuarioAlta, GETDATE())
          
          UPDATE TablaDestinoDetalle SET StatusConciliacion = 'CONCILIACION CANCELADA'
          WHERE Corporativo=@Corporativo
          AND Sucursal= @Sucursal
          AND Año= @AñoInterno
          AND Folio = @FolioInterno
          AND Secuencia= @SecuenciaInterno

          SELECT @FolioConciliacion As Folio

          IF @@ERROR <> 0  
          BEGIN  
               ROLLBACK TRANSACTION  
               RAISERROR('Ha ocurrido un error al dar de alta el registro. R01',16,1)  
               RETURN 0
          END  
                 
     COMMIT TRANSACTION    
            
END  


IF @Configuracion = 4 --desconcilia el registro por el valor de interno
BEGIN  
     BEGIN TRANSACTION  

		  DELETE FROM dbo.ConciliacionReferencia
          WHERE  CorporativoConciliacion = @Corporativo AND
          SucursalConciliacion = @Sucursal AND
          AñoConciliacion = @AñoConciliacion  AND
          MesConciliacion = @MesConciliacion AND
          FolioConciliacion = @FolioConciliacion AND
          AñoInterno= @AñoInterno AND
          FolioInterno = @FolioInterno AND
          SecuenciaInterno= @SecuenciaInterno

          UPDATE TablaDestinoDetalle SET StatusConciliacion = 'EN PROCESO DE CONCILIACION'
          WHERE Corporativo=@Corporativo
          AND Sucursal= @Sucursal
          AND Año= @AñoInterno
          AND Folio = @FolioInterno
          AND Secuencia= @SecuenciaInterno
          
          SELECT @FolioConciliacion As Folio

          IF @@ERROR <> 0  
          BEGIN  
               ROLLBACK TRANSACTION  
               RAISERROR('Ha ocurrido un error al dar de alta el registro. R01',16,1)  
               RETURN 0
          END  
                 
     COMMIT TRANSACTION   
            
END  

IF @Configuracion = 5 --Actualiza StatusConcepto y/o Descripcion
BEGIN  
     BEGIN TRANSACTION 

	  IF NOT EXISTS(SELECT * 
						FROM TablaDestinoDetalle 
						WHERE Corporativo = @Corporativo
						AND Sucursal=@Sucursal
						AND Año = @AñoConciliacion
						AND Folio = @FolioConciliacion
						AND Secuencia= @SecuenciaRelacion)
		  BEGIN
			  INSERT INTO TablaDestinoDetalle (Corporativo,Sucursal,Año,Folio,Secuencia,CuentaBancaria,FOperacion,FMovimiento)
			  VALUES(@Corporativo, @Sucursal, @AñoConciliacion, @FolioConciliacion, @SecuenciaRelacion,'CUENTABANACARIA',GETDATE(),GETDATE())
		  END
	 
	 IF NOT EXISTS (SELECT SecuenciaRelacion FROM ConciliacionReferencia  WHERE CorporativoConciliacion = @Corporativo AND
																		  SucursalConciliacion = @Sucursal AND
																		  AñoConciliacion = @AñoConciliacion  AND
																		  MesConciliacion = @MesConciliacion AND
																		  FolioConciliacion = @FolioConciliacion AND
																		  SecuenciaRelacion= @SecuenciaRelacion)
		BEGIN
		INSERT INTO dbo.ConciliacionReferencia(CorporativoConciliacion, SucursalConciliacion, AñoConciliacion, MesConciliacion,FolioConciliacion, SecuenciaRelacion,
                      CorporativoInterno, SucursalInterno, AñoInterno, FolioInterno, SecuenciaInterno,
                      CorporativoExterno, SucursalExterno, AñoExterno, FolioExterno, SecuenciaExterno,
                      Concepto, MontoConciliado, Diferencia, MontoExterno, MontoInterno, 
                      FormaConciliacion, StatusConcepto, StatusConciliacion, MotivoNoConciliado, ComentarioNoConciliado, Usuario, FAlta)
          VALUES (@Corporativo, @Sucursal, @AñoConciliacion, @MesConciliacion, @FolioConciliacion, @SecuenciaRelacion,
                  null, null, null, null, null,
                  @Corporativo, @Sucursal, @AñoExterno, @FolioExterno, @SecuenciaExterno,
                  @Concepto, 0, 0, @MontoExterno, null,
                  @FormaConciliacion, @StatusConcepto, 'CONCILIADA S/REFERENCIA',5,@Descripcion, @UsuarioAlta, GETDATE())

		  


		END
  -- Actualiza StatusConcepto 
	 UPDATE ConciliacionReferencia set 
	 StatusConcepto=@StatusConcepto,FStatusConcepto=GETDATE(),StatusConciliacion=CASE WHEN StatusConciliacion='CONCILIACION CANCELADA'
																												THEN 'CONCILIADA S/REFERENCIA' 
																												ELSE StatusConciliacion
																											END
		  WHERE
	      CorporativoConciliacion = @Corporativo AND
          SucursalConciliacion = @Sucursal AND
          AñoConciliacion = @AñoConciliacion  AND
          MesConciliacion = @MesConciliacion AND
          FolioConciliacion = @FolioConciliacion AND
          --SecuenciaRelacion = @SecuenciaRelacion
		  CorporativoExterno= @Corporativo AND 
		  SucursalExterno= @Sucursal AND 
		  AñoExterno= @AñoExterno AND 
		  FolioExterno=  @FolioExterno AND 
		  SecuenciaExterno= @SecuenciaExterno
		  --Descripcion Interna
		UPDATE ConciliacionReferencia set 
		  Descripcion=@Descripcion
		  WHERE
	      CorporativoConciliacion = @Corporativo AND
          SucursalConciliacion = @Sucursal AND
          AñoConciliacion = @AñoConciliacion  AND
          MesConciliacion = @MesConciliacion AND
          FolioConciliacion = @FolioConciliacion AND
          SecuenciaRelacion = @SecuenciaRelacion AND
		  CorporativoExterno= @Corporativo AND 
		  SucursalExterno= @Sucursal AND 
		  AñoExterno= @AñoExterno AND 
		  FolioExterno=  @FolioExterno AND 
		  SecuenciaExterno= @SecuenciaExterno

	      UPDATE TablaDestinoDetalle SET StatusConciliacion=CASE WHEN StatusConciliacion != 'CONCILIADA'
																 THEN 'CONCILIADA S/REFERENCIA' 
																 ELSE StatusConciliacion
															END
          WHERE Corporativo=@Corporativo
          AND Sucursal= @Sucursal
          AND Año= @AñoExterno
          AND Folio = @FolioExterno
          AND Secuencia= @SecuenciaExterno

	 IF @@ERROR <> 0  
          BEGIN  
               ROLLBACK TRANSACTION  
               RAISERROR('Ha ocurrido un error al dar de alta el registro. R01',16,1)  
               RETURN 0
          END  
                 
     COMMIT TRANSACTION 
END
  