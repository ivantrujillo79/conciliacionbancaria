
namespace Conciliacion.RunTime.ReglasDeNegocio {
	public struct Caja {

		private int id;
		private string descripcion;
        
        public Caja(int id, string descripcion)
        {
            this.id = id;
            this.descripcion = descripcion;
        }

        #region Propiedades

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        #endregion

    }//end Caja

}//end namespace ReglasDeNegocio