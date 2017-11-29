
CREATE PROCEDURE spCCLConsultaVwDatosClienteReferencia
@Cliente varchar(50)
      
AS      

 DECLARE @CONVIERTE INT

 SET @CONVIERTE  =
 CASE
    WHEN ISNUMERIC(@Cliente) = 0     THEN 0
    WHEN @Cliente LIKE '%\[^-+ 0-9\]%' THEN 0
    WHEN CAST(@Cliente AS NUMERIC(38, 0))
  NOT BETWEEN -2147483648. AND 2147483647. THEN 0
    ELSE 1
  END

      
 SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED      
      
IF @CONVIERTE = 1
BEGIN
	 SELECT Cliente, Nombre, isnull(RazonSocial, '-') as RazonSocial, Celula, Ruta, 
		 isnull(Programacion,0) as Programacion,
		 isnull(TelCasa, '-') as TelCasa, isnull(TelAlterno1, '-') as TelAlterno1, 
		 isnull(TelAlterno2, '-') as TelAlterno2, isnull(Saldo,0) as Saldo, isnull(Email, '-') as EMail, 
		 dbo.Trim(isnull(DireccionCompleta, '-')) + ' entre ' + dbo.Trim(EntreCalle1Nombre) + ' y ' +  dbo.Trim(EntreCalle2Nombre) as Direccion
	 FROM  vwDatosCliente      
	 WHERE Cliente = CONVERT(INTEGER, @Cliente ) 
 END
 ELSE
 BEGIN
 SELECT Cliente, Nombre, isnull(RazonSocial, '-') as RazonSocial, Celula, Ruta, 
		 isnull(Programacion,0) as Programacion,
		 isnull(TelCasa, '-') as TelCasa, isnull(TelAlterno1, '-') as TelAlterno1, 
		 isnull(TelAlterno2, '-') as TelAlterno2, isnull(Saldo,0) as Saldo, isnull(Email, '-') as EMail, 
		 dbo.Trim(isnull(DireccionCompleta, '-')) + ' entre ' + dbo.Trim(EntreCalle1Nombre) + ' y ' +  dbo.Trim(EntreCalle2Nombre) as Direccion
	 FROM  vwDatosCliente      
	 WHERE CliReferencia = @Cliente
 END
      
 SET TRANSACTION ISOLATION LEVEL READ COMMITTED
