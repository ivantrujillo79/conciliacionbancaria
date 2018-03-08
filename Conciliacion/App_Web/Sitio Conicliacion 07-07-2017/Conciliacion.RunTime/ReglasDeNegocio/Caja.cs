
namespace Conciliacion.RunTime.ReglasDeNegocio {
	public struct Caja {

		private int id;
		private string descripcion;

		//public Caja(){

		//}

		//~Caja(){

		//}

		//public virtual void Dispose(){

		//}

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
        
	}//end Caja

}//end namespace ReglasDeNegocio