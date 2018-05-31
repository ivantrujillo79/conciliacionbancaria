using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.IO;
using System.Collections;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Web;
using System.Drawing.Printing;

/// <summary>
/// Descripción breve de ClaseReporte
/// </summary>
public class ClaseReporte
{
#region "Variables de la clase"
    public CrystalDecisions.CrystalReports.Engine.ReportDocument RepDoc = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    private string _strReporte = "";
    private ArrayList _arrPar = new ArrayList();
    private string _strServidor = "";
    private string _strBase = "";
    private string _strUsuario = "";
    private string _strPW = "";

    private string _strError = "";
#endregion

#region "Constructor de la clase"
    /// <summary>
    /// Constructor de la clase
    /// </summary>
    /// <param name="Reporte">Ruta y Nombre del reporte</param>
    /// <param name="Parametros">Parámetros que requiere el reporte ejemplo: clave=1</param>
    /// <param name="Servidor">Servidor donde se encuentra la base de datos</param>
    /// <param name="Base">Nombre de la Base de Datos</param>
    /// <param name="Usuario">Nombre de usuario con permiso de acceso</param>
    /// <param name="PW">Contraseña o Password del usuario</param>
    public ClaseReporte(string Reporte, ArrayList Parametros, string Servidor, string Base, string Usuario, string PW)
    {
        if (File.Exists(Reporte))
        {
            try
            {
                this._strReporte = Reporte;
                this._arrPar = (ArrayList)Parametros.Clone();
                this._strServidor = Servidor;
                this._strBase = Base;
                this._strUsuario = Usuario;
                this._strPW = PW;

                try
                {
                    RepDoc.FileName = Reporte;
                }
                catch
                {
                }
                RepDoc.Load(Reporte);

                //Variables
                TableLogOnInfo _LogonInfo;
                ParameterFieldDefinitions crParameterFieldDefinitions;
                ParameterFieldDefinition crParameterFieldDefinition;
                CrystalDecisions.Shared.ParameterValues crParametervalues;
                CrystalDecisions.Shared.ParameterDiscreteValue crParameterDiscretValue;
                string TablaNombre = "";
                string strValor = "";

                //Pasa los datos de la conexion al reporte principal
                RepDoc.SetDatabaseLogon(Usuario, PW, Servidor, Base);
                foreach (CrystalDecisions.CrystalReports.Engine.Table _TablaReporte in RepDoc.Database.Tables)
                {
                    _LogonInfo = _TablaReporte.LogOnInfo;
                    _LogonInfo.ConnectionInfo.ServerName = Servidor;
                    _LogonInfo.ConnectionInfo.DatabaseName = Base;
                    _LogonInfo.ConnectionInfo.UserID = Usuario;
                    _LogonInfo.ConnectionInfo.Password = PW;
                    try
                    {
                        _TablaReporte.ApplyLogOnInfo(_LogonInfo);
                    }
                    catch (Exception ex)
                    {
                        this._strError = ex.ToString();
                    }

                    //pasa un datatable al reporte
                    TablaNombre = "";
                    if (_TablaReporte.Name.IndexOf(";") > 0)
                        TablaNombre = _TablaReporte.Name.Substring(0, _TablaReporte.Name.IndexOf(";"));
                    else
                        TablaNombre = _TablaReporte.Name;
                }

                //Pasa los valores a los parametros
                System.Data.SqlClient.SqlCommand cmdRep = SeguridadCB.Seguridad.Conexion.CreateCommand();
                cmdRep.CommandType = CommandType.StoredProcedure;
                cmdRep.CommandText = TablaNombre;
                crParameterFieldDefinitions = RepDoc.DataDefinition.ParameterFields;
                foreach (ParameterFieldDefinition par in RepDoc.DataDefinition.ParameterFields)
                {
                    try
                    {
                        if (Existe_Parametro(Parametros, par.Name))
                        {
                            crParameterFieldDefinition = crParameterFieldDefinitions[par.Name];
                            crParametervalues = crParameterFieldDefinition.CurrentValues;
                            crParameterDiscretValue = new CrystalDecisions.Shared.ParameterDiscreteValue();
                            strValor = Leer_Valor_Parametro(Parametros, par.Name);
                            crParameterDiscretValue.Value = strValor;
                            crParametervalues.Add(crParameterDiscretValue);
                            crParameterFieldDefinition.ApplyCurrentValues(crParametervalues);

                            System.Data.SqlClient.SqlParameter parNuevo = new System.Data.SqlClient.SqlParameter();
                            parNuevo.ParameterName = par.Name;
                            parNuevo.Value = strValor;
                            if (!cmdRep.Parameters.Contains(par.Name)) cmdRep.Parameters.Add(parNuevo);
                        }
                    }
                    catch (Exception ex)
                    {
                        this._strError = ex.ToString();
                    }
                }
                System.Data.SqlClient.SqlDataAdapter daRep = new System.Data.SqlClient.SqlDataAdapter();
                daRep.SelectCommand = cmdRep;
                System.Data.DataTable dtRep = new DataTable(TablaNombre);
                daRep.Fill(dtRep);
                RepDoc.SetDataSource(dtRep);
                
                //Pasa los parametros a los subreportes
                foreach (CrystalDecisions.CrystalReports.Engine.ReportDocument lRepDoc in RepDoc.Subreports)
                {
                    if (lRepDoc != null)
                    {
                        lRepDoc.SetDatabaseLogon(Usuario, PW, Servidor, Base);
                        foreach (CrystalDecisions.CrystalReports.Engine.Table _TablasReporte in lRepDoc.Database.Tables)
                        {
                            _LogonInfo = _TablasReporte.LogOnInfo;
                            _LogonInfo.ConnectionInfo.ServerName = Servidor;
                            _LogonInfo.ConnectionInfo.DatabaseName = Base;
                            _LogonInfo.ConnectionInfo.UserID = Usuario;
                            _LogonInfo.ConnectionInfo.Password = PW;
                            try
                            {
                                _TablasReporte.ApplyLogOnInfo(_LogonInfo);
                            }
                            catch (Exception ex)
                            {
                                this._strError = ex.ToString();
                            }

                            //pasa un datatable al reporte
                            TablaNombre = "";
                            if (_TablasReporte.Name.IndexOf(";") > 0)
                                TablaNombre = _TablasReporte.Name.Substring(0, _TablasReporte.Name.IndexOf(";"));
                            else
                                TablaNombre = _TablasReporte.Name;
                        }

                        //Pasa los valores a los parametros
                        System.Data.SqlClient.SqlCommand cmdsRep = SeguridadCB.Seguridad.Conexion.CreateCommand();
                        cmdsRep.CommandType = CommandType.StoredProcedure;
                        cmdsRep.CommandText = TablaNombre;
                        crParameterFieldDefinitions = lRepDoc.DataDefinition.ParameterFields;
                        foreach (ParameterFieldDefinition par in lRepDoc.DataDefinition.ParameterFields)
                        {
                            try
                            {
                                if (Existe_Parametro(Parametros, par.Name))
                                {
                                    crParameterFieldDefinition = crParameterFieldDefinitions[par.Name];
                                    crParametervalues = crParameterFieldDefinition.CurrentValues;
                                    crParameterDiscretValue = new CrystalDecisions.Shared.ParameterDiscreteValue();
                                    strValor = Leer_Valor_Parametro(Parametros, par.Name);
                                    crParameterDiscretValue.Value = strValor;
                                    crParametervalues.Add(crParameterDiscretValue);
                                    crParameterFieldDefinition.ApplyCurrentValues(crParametervalues);
                                    System.Data.SqlClient.SqlParameter parNuevo = new System.Data.SqlClient.SqlParameter();
                                    parNuevo.ParameterName = par.Name;
                                    parNuevo.Value = strValor;
                                    if (!cmdsRep.Parameters.Contains(par.Name)) cmdsRep.Parameters.Add(parNuevo);
                                }
                            }
                            catch (Exception ex)
                            {
                                this._strError = ex.ToString();
                            }
                        }

                        System.Data.SqlClient.SqlDataAdapter dasRep = new System.Data.SqlClient.SqlDataAdapter();
                        cmdsRep.CommandType = CommandType.StoredProcedure;
                        dasRep.SelectCommand = cmdsRep;
                        System.Data.DataTable dtsRep = new DataTable(TablaNombre);
                        dasRep.Fill(dtsRep);
                        try
                        {
                            lRepDoc.SetDataSource(dtsRep);
                        }
                        catch (Exception exs)
                        {
                            this._strError = exs.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this._strError = ex.ToString();
            }
        }
        else
        {
            this._strError = "No existe el reporte en la ruta especificada";
        }
    }
#endregion

#region "Funciones Publicas"
    /// <summary>
    /// Envía a impresora el reporte especificado
    /// </summary>
    public void Imprimir_Reporte()
    {
        try
        {
            //PrinterSettings PS = new PrinterSettings();
            //string predeterminada = "";
            //foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            //{
            //    PS.PrinterName = printer;
            //    if (PS.IsDefaultPrinter) predeterminada = printer;
            //}

            //this.RepDoc.PrintOptions.PrinterName = predeterminada;
            this.RepDoc.PrintToPrinter(1, true, 0, 0);
        }
        catch (Exception ex)
        {
            this._strError = ex.ToString();
        }
    }

    /// <summary>
    /// Verifica si se produjo un error devuelve un verdadero o falso
    /// </summary>
    public Boolean Hay_Error
    {
        get
        {
            if (this._strError.Length > 0)
                return true;
            else
                return false;
        }

    }

    /// <summary>
    /// Devuelve el error producido o vacio si no lo hubo
    /// </summary>
    public string Error
    {
        get
        {
            return this._strError;
        }
    }
#endregion

#region "Funciones privadas"
    private string Leer_Valor_Parametro(ArrayList Par, string Nombre)
    {
        try
        {
            Nombre = Nombre.ToUpper();
            Boolean blnEncontrado = false;
            int intp = 0;
            string strDato = "";
            string strNombre = "";
            string strValor = "";
            for (int inti = 0; inti < Par.Count && !blnEncontrado; inti++)
            {
                strDato = Par[inti].ToString();
                if (strDato.Trim().Length > 0)
                {
                    intp = strDato.LastIndexOf("=");
                    if (intp > 0)
                    {
                        strNombre = strDato.Substring(0, intp).ToUpper();
                        if (strNombre == Nombre)
                        {
                            strValor = strDato.Substring(intp + 1);
                            blnEncontrado = true;
                        }
                    }
                }
            }
            return strValor;
        }
        catch
        {
            return "";
        }
    }

    private string Leer_Nombre_Parametro(string par)
    {
        try
        {
            int intp = 0;
            string strNombre = "";

            if (par.Trim().Length > 0)
            {
                intp = par.LastIndexOf("=");
                if (intp > 0)
                {
                    strNombre = par.Substring(0, intp).ToUpper();
                    return strNombre;
                }
                else return "";
            }
            else return "";
        }
        catch
        {
            return "";
        }
    }

    private string Leer_Valor_Parametro(string par)
    {
        try
        {
            int intp = 0;
            string strValor = "";

            if (par.Trim().Length > 0)
            {
                intp = par.LastIndexOf("=");
                if (intp > 0)
                {
                    strValor = par.Substring(intp + 1);
                    return strValor;
                }
                else return "";
            }
            else return "";
        }
        catch
        {
            return "";
        }
    }

    private Boolean Existe_Parametro(ArrayList Par, string Nombre)
    {
        try
        {
            Nombre = Nombre.ToUpper();
            Boolean blnEncontrado = false;
            int intp = 0;
            string strDato = "";
            string strNombre = "";
            for (int inti = 0; inti < Par.Count && !blnEncontrado; inti++)
            {
                strDato = Par[inti].ToString();
                if (strDato.Trim().Length > 0)
                {
                    intp = strDato.LastIndexOf("=");
                    if (intp > 0)
                    {
                        strNombre = strDato.Substring(0, intp).ToUpper();
                        if (strNombre == Nombre) blnEncontrado = true;
                    }
                }
            }
            return blnEncontrado;
        }
        catch
        {
            return false;
        }
    }

    private ReportDocument OpenSubreport(ReportDocument Reporte, string reportObjectName)
    {
        string subreportName;
        SubreportObject subreportObject;
        ReportDocument subreport = new ReportDocument();

        subreportObject = Reporte.ReportDefinition.ReportObjects [reportObjectName] as SubreportObject;
        if (subreportObject != null)
        {
            subreportName = subreportObject.SubreportName;
            subreport = Reporte.OpenSubreport(subreportName);
            return subreport;
        }
        return null;
    } 
#endregion
}