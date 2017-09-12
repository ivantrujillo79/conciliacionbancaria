SELECT*FROM dbo.Parametro WHERE Modulo = 30
SELECT*FROM dbo.Modulo

INSERT INTO dbo.Parametro 
        ( Modulo ,
          Parametro ,
          Valor ,
          Observaciones ,
          FAlta ,
          Corporativo ,
          Sucursal
        )
VALUES  ( 30 , -- Modulo - smallint
          'AplicaCobranza' , -- Parametro - char(25)
          '1' , -- Valor - varchar(250)
          'Permite definir si se imprimira el reporte de relación de cobranza' , -- Observaciones - varchar(250)
          GETDATE() , -- FAlta - datetime
          4 , -- Corporativo - tinyint
          1  -- Sucursal - tinyint
        )