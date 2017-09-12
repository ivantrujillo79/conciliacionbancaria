using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Conciliacion.RunTime.ReglasDeNegocio
{
    public class ListaCombo : EmisorMensajes
    {
        int identificador;
        string descripcion;
        string campo1;
        string campo2;
        string campo3;
        string campo4;

        public int Identificador
        {
            get { return identificador; }            
        }

        public string Descripcion
        {
            get { return descripcion; }
        }

        public string Campo1
        {
            get { return campo1; }
        }

        public string Campo2
        {
            get { return campo2; }
        }

        public string Campo3
        {
            get { return campo3; }
        }

        public string Campo4
        {
            get { return campo4; }
        }

        public ListaCombo(int identificador, string descripcion)
        {
            this.identificador = identificador;
            this.descripcion = descripcion;
        }

        public ListaCombo(int identificador, string descripcion, string campo1)
        {
            this.identificador = identificador;
            this.descripcion = descripcion;
            this.campo1 = campo1;
        }

        public ListaCombo(int identificador, string descripcion, string campo1, string campo2)
        {
            this.identificador = identificador;
            this.descripcion = descripcion;
            this.campo1 = campo1;
            this.campo2 = campo2;
        }

        public ListaCombo(int identificador, string descripcion, string campo1, string campo2, string campo3, string campo4)
        {
            this.identificador = identificador;
            this.descripcion = descripcion;
            this.campo1 = campo1;
            this.campo2 = campo2;
            this.campo3 = campo3;
            this.campo4 = campo4;
        }
    }   
}
