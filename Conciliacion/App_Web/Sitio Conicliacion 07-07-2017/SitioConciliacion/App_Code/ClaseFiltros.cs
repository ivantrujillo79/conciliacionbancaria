using Conciliacion.RunTime;
using Conciliacion.RunTime.ReglasDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ClaseFiltros
/// </summary>
public class ClaseFiltros
{
    private cConciliacion conciliacion;
    public cConciliacion Conciliacion { get; set; }

    private int empresa;
    public int Empresa
    {
        get
        {
            return empresa;
        }
        set
        {
            empresa = value;
        }
    }

    private int sucursal;
    public int Sucursal { get; set; }

    private int grupo;
    public int Grupo { get; set; }

    private int tipoConciliacion;
    public int TipoConciliacion { get; set; }

    private int status;
    public int Status { get; set; }

    private int anio;
    public int Anio { get; set; }

    private int mes;
    public int Mes { get; set; }

    private int folio;
    public int Folio { get; set; }

    public ClaseFiltros()
    {
        empresa = -1;
        sucursal = -1;
        grupo = -1;
        tipoConciliacion = -1;
        status = -1;
        anio = -1;
        mes = -1;
        folio = -1;
        conciliacion = null;
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
}