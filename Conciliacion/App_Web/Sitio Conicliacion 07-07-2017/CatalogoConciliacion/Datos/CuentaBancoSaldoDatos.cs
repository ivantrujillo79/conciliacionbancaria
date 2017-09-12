using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CatalogoConciliacion.ReglasNegocio;
using CatalogoConciliacion.ReglasNegocio;
using System.Data.SqlClient;
using System.Diagnostics;
using Conciliacion.RunTime;

namespace CatalogoConciliacion.Datos
{
  public  class CuentaBancoSaldoDatos:CuentaBancoSaldo
    {
       public CuentaBancoSaldoDatos(IMensajesImplementacion implementadorMensajes)
            : base(implementadorMensajes)
        {
        }


       public CuentaBancoSaldoDatos(int corporativo, int sucursal, int banco, string bancodes, string cuentabancaria,
                                decimal saldoinicialmes, decimal saldofinal, IMensajesImplementacion implemntadorMensajes)
            : base(corporativo,  sucursal,  banco,  bancodes,  cuentabancaria,
                                 saldoinicialmes,  saldofinal,  implemntadorMensajes)
        {
        }
    }
}
