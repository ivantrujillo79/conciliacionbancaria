using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public class ImportacionAplicacion : EmisorMensajes
    {
        int identificador;
        string descripcion;
        short tipofuenteinformacion;
        string procedimiento;
        string servidor;
        string basededatos;
        string usuarioconsulta;
        string pass;

        public int Identificador
        {
            get { return identificador; }
        }

        public string Descripcion
        {
            get { return descripcion; }
        }

        public short TipoFuenteInformacion
        {
            get { return tipofuenteinformacion; }
        }

        public string Procedimiento
        {
            get { return procedimiento; }
        }

        public string Servidor
        {
            get { return servidor; }
        }

        public string BaseDeDatos
        {
            get { return basededatos; }
        }

        public string UsuarioConsulta
        {
            get { return usuarioconsulta; }
        }

        public string Pass
        {
            get { return pass; }
        }

        public ImportacionAplicacion(int identificador, string descripcion, short tipofuenteinformacion, 
            string procedimiento, string servidor, string basededatos, string usuarioconsulta, string pass)
        {
            this.identificador = identificador;
            this.descripcion = descripcion;
            this.tipofuenteinformacion = tipofuenteinformacion;
            this.procedimiento = procedimiento;
            this.servidor = servidor;
            this.basededatos = basededatos;
            this.usuarioconsulta = usuarioconsulta;
            this.pass = pass;
        }
    }
}
