using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Automoviles.Tablas;
using Automoviles.Datos;
using SQLite;
using System.IO;
namespace Automoviles.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Detalle : ContentPage
    {
        public int IdSeleccionado;
        //variables para los datos seleccionados
        public string MarSelec, ColSelec, AnSelec;
        private SQLiteAsyncConnection conexion;
        //Para aplicar las sentencias de eliminar y modificar
        IEnumerable<T_Autos> ResultadoDelete;
        IEnumerable<T_Autos> ResultadoUpdate;

        public Detalle(int id,string mar,string col,string an)
        {
            InitializeComponent();
            //Se obtiene nuevamente la conexión
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            //Se asignan valores seleccionados
            IdSeleccionado = id;
            MarSelec = mar;
            ColSelec = col;
            AnSelec = an;
            //Eventos de los botones
            btnActu.Clicked += BtnActu_Clicked;
            btnEliminar.Clicked += BtnEliminar_Clicked;
          
        }

       

        protected override void OnAppearing()
        {
            //Se asignan los vaalores a mostrar
            base.OnAppearing();
            lblMensaje.Text = " ID: " + IdSeleccionado;
            txtMarca.Text = MarSelec;
            txtColor.Text = ColSelec;
            txtAnio.Text = AnSelec;
        }


        private void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            //Se elimina el registro apartir del ID que se seleccionó en la pagina "Consulta"
            var rutaBD = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BaseAutos.db3");
            var db = new SQLiteConnection(rutaBD);
            ResultadoDelete = Delete(db, IdSeleccionado);
            //Se muestra mensaje del correcto DELETE
            DisplayAlert("Confirmación", "El auto se eliminó correctamente", "OK");
            //Se limpia formulario
            Limpiar();
            //Manda a la lista de autos
            
        }

        private void BtnActu_Clicked(object sender, EventArgs e)
        {
            var rutaBD = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BaseAutos.db3");
            var db = new SQLiteConnection(rutaBD);
            //Datos que se encuentran en los txt para enviados a la base y hacer la modificación con el UPDATE
            ResultadoUpdate = Update(db, txtMarca.Text, txtColor.Text, txtAnio.Text, IdSeleccionado);
            //Mensaje de la correcta actualización
            DisplayAlert("Confirmación", "El auto se actualizó correctamente", "Ok");
            
        }

        public static IEnumerable<T_Autos> Delete(SQLiteConnection db,int id)
        {
            //Aplicación de la sentencia DELET con el id seleccionado
            return db.Query<T_Autos>("DELETE FROM T_Autos where id=?", id);
        }
        public static IEnumerable<T_Autos>Update(SQLiteConnection db,string marca, string color, string anio,int id)
            //UPDATE del registro recibiendo los nuevos datos que se muestran en "Detalle"
        {
            return db.Query<T_Autos>("UPDATE T_Autos SET Marca=?,Color=?,Anio=? where id=?", marca, color, anio, id);

        }
        public void Limpiar()
        {
            //Manda valores nulos para vaciar formulario
            lblMensaje.Text = "";
            txtMarca.Text = "";
            txtColor.Text = "";
            txtAnio.Text = "";
        }
    }
}