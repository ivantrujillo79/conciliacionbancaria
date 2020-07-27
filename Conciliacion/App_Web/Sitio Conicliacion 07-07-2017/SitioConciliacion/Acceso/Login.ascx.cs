using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using SeguridadCB;
using SeguridadCB.Public;
using SeguridadCB.DataLayer;
using Conciliacion.RunTime;
using CatalogoConciliacion;
using Conciliacion.Migracion.Runtime;
public partial class Acceso_Login : System.Web.UI.UserControl
{
    Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Conciliacion.RunTime.App.ImplementadorMensajes.ContenedorActual = this;
        objApp.ImplementadorMensajes.ContenedorActual = this;
        Page.ClientScript.RegisterOnSubmitStatement(this.GetType(), "ValidaEnvio", "return ValidaEnvio();");
        
    }
    #region "Variables globales"
    string servidor = string.Empty, baseDatos = string.Empty, modulo = string.Empty;
    SeguridadCB.Public.Modulos modulos;
    SeguridadCB.Public.Operaciones operaciones;
    SeguridadCB.Public.Parametros parametros;
    SeguridadCB.Public.Usuario usuario;
    SqlConnection cn = new SqlConnection();
    SeguridadCB.Seguridad.TipoSeguridad seguridad = SeguridadCB.Seguridad.TipoSeguridad.SQL;
    #endregion

    public void CargaScriptsIniciales()
    {
        if (!Page.IsPostBack)
            txtUsuario.Focus();
        
        txtUsuario.Attributes.Add("onFocus", "ChangeColor('" + txtUsuario.ClientID + "', '#FFFDE8');");
        txtUsuario.Attributes.Add("onBlur", "ChangeColor('" + txtUsuario.ClientID + "', '#FFFFFF');");
        txtClave.Attributes.Add("onFocus", "ChangeColor('" + txtClave.ClientID + "', '#FFFDE8');");
        txtClave.Attributes.Add("onBlur", "ChangeColor('" + txtClave.ClientID + "', '#FFFFFF');");
        btnEntrar.Attributes.Add("onClick", "return ValidaEnvio();");
    }

    #region "Rutinas de validacion"
    protected void btnEntrar_Click1(object sender, EventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            System.Threading.Thread.Sleep(2000);
            ConfiguraConexion();
            Session["AppCadenaConexion"] = cn.ConnectionString;
            SeguridadCB.Seguridad seguridad = new SeguridadCB.Seguridad();
            //seguridad.Conexion = cn;
            try
            {
                if (seguridad.ExisteUsuarioActivo(txtUsuario.Text.Trim()))
                {
                    this.usuario = seguridad.DatosUsuario(txtUsuario.Text.Trim());
                    if (seguridad.ComparaClaves(txtClave.Text.Trim().ToUpper(), usuario))
                    {
                        AppSettingsReader settings = new AppSettingsReader();
                        this.modulo = settings.GetValue("Modulo", typeof(string)).ToString();

                        this.modulos = seguridad.Modulos(this.usuario.IdUsuario);
                        this.operaciones = seguridad.Operaciones(modulo, this.usuario.IdUsuario);
                        if (this.operaciones.TieneAcceso)
                        {
                            this.parametros = seguridad.Parametros(modulo, this.usuario.Corporativo, this.usuario.Sucursal);
                            Session.Add("Operaciones", this.operaciones);
                            Session.Add("Usuario", this.usuario);
                            Session.Add("Conexion", this.cn);
                            Session.Add("Parametros", this.parametros);
                            Session.Add("PiePagina", this.usuario.Nombre.Trim() + " (" + this.usuario.IdUsuario + ") conectado a [" + this.cn.DataSource + "]." + this.cn.Database);
                            Response.Redirect("~/Inicio.aspx", true);
                        }
                        else
                        {
                            Mensaje("Usted no tiene acceso al módulo.");
                            txtUsuario.Focus();
                        }
                    }
                    else
                    {
                        Mensaje("La clave es incorrecta, verifique.");
                        txtClave.Focus();
                    }
                }
                else
                {
                    Mensaje("El usuario no existe o se encuentra inactivo.");
                    txtUsuario.Focus();
                }
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 18452:
                    case 18456:
                        Mensaje("No se ha logrado abrir la conexión, revise el nombre de usuario y la contraseña.");
                        txtUsuario.Focus();
                        break;
                    case 4060:
                        Mensaje("La base de datos no está disponible, comuníquelo al área de sistemas.");
                        break;
                    case 17:
                        Mensaje("El servidor no está disponible, comuníquelo al área de sistemas.");
                        break;
                    default:
                        Mensaje("Ha ocurrido el siguiente error: " + ex.Message);
                        break;
                }
            }
            catch (Exception ex)
            {
                Mensaje("Ha ocurrido el siguiente error: " + ex.Message);
            }
        }
    }
    #endregion

    #region "Manejo de la conexion"



    private void ConfiguraConexion()
    {
        Conciliacion.RunTime.App objApp = new Conciliacion.RunTime.App();

        Conciliacion.Migracion.Runtime.App.UsuarioActual = txtUsuario.Text.Trim();

        AppSettingsReader settings = new AppSettingsReader();
        this.servidor = settings.GetValue("Servidor", typeof(string)).ToString();
        this.baseDatos = settings.GetValue("Base", typeof(string)).ToString();

        if (settings.GetValue("Seguridad", typeof(string)).ToString() == "NT")
            this.seguridad = SeguridadCB.Seguridad.TipoSeguridad.NT;
        else
            this.seguridad = SeguridadCB.Seguridad.TipoSeguridad.SQL;
        if (seguridad == SeguridadCB.Seguridad.TipoSeguridad.NT)
            cn.ConnectionString = "Application Name = Conciliacion Bancaria" + " v.1.0.0.0" + "; Data Source = " + this.servidor + "; Initial Catalog = " +
            this.baseDatos + "; User ID = " + this.txtUsuario.Text.Trim() + "; Integrated Security = SSPI";
        else
            cn.ConnectionString = "Application Name = Conciliacion Bancaria" + "; Data Source = " + this.servidor + "; Initial Catalog = " +
            this.baseDatos + "; User ID = " + this.txtUsuario.Text.Trim() + "; Password = " + this.txtClave.Text.Trim();

        Conciliacion.Migracion.Runtime.App.CadenaConexion = cn.ConnectionString;
        objApp.CadenaConexion = cn.ConnectionString;
        objApp.CadenaConexion = cn.ConnectionString;
        
    }

    #endregion

    private void Mensaje(string mensaje)
    {
        mensaje = mensaje.Replace((char)10, (char)32);
        mensaje = mensaje.Replace((char)13, (char)39);
        mensaje = mensaje.Replace("'", "");
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "MensajeError", "alert('" + mensaje + "'); ", true);

        ScriptManager.RegisterClientScriptBlock(Page,
                                                this.GetType(),
                                              "MensajeError", "alert('" + mensaje + "')",
                                              true);
    }
    private void Mensaje(Exception ex)
    {
        string mensaje = ex.Message;
        mensaje = mensaje.Replace((char)10, (char)32);
        mensaje = mensaje.Replace((char)13, (char)39);
        mensaje = mensaje.Replace("'", "");
       //Page.ClientScript.RegisterStartupScript(this.GetType(), "MensajeError", "alert('" + mensaje + "'); ", true);
       ScriptManager.RegisterClientScriptBlock(Page,this.GetType(),
                                                   "MensajeError", "alert('" + mensaje + "')",
                                                   true);
    }

    protected void txtUsuario_TextChanged(object sender, EventArgs e)
    {

    }
}