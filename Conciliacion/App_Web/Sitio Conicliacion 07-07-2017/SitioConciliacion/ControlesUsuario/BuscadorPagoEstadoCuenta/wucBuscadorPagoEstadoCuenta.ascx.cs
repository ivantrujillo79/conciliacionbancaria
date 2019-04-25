﻿using Conciliacion.RunTime.ReglasDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ControlesUsuario_ClientePago_wucBuscadorPagoEstadoCuenta : System.Web.UI.UserControl
{
    private int corporativo;
    private int sucursal;
    private int año;
    private int mes;
    private int folio;
    //public ListaPago;
    private bool activaEstaConciliacion;

    private int Corporativo;
    private int Sucursal;
    private int Año;
    private int Mes;
    private int Folio;
    //public ListaPago;
    public bool ActivaEstaConciliacion
    {
        get { return activaEstaConciliacion; }
        set { activaEstaConciliacion = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarGrid();
            txtFinicio.Text = (DateTime.Today.AddMonths(-1)).ToShortDateString();
            txtFfinal.Text = DateTime.Today.ToShortDateString();
        }
        chkBuscarEnEsta.Enabled = ActivaEstaConciliacion;
    }

    public void CargarGrid()
    {

    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {

    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        List<EstadoDeCuenta> listaEstadoCuenta = Conciliacion.RunTime.App.Consultas.BuscarPagoEstadoCuenta(DateTime.Parse(txtFinicio.Text), DateTime.Parse(txtFfinal.Text),decimal.Parse(txtMonto.Text),chkBuscaEnRetiros.Checked,chkBuscarEnDepositos.Checked);
        grvPagoEstadoCuenta.DataSource = listaEstadoCuenta;
        if (listaEstadoCuenta.Count > 0)
            grvPagoEstadoCuenta.DataBind();

    } 
}
