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
    public partial class DetalleCliente : ContentPage
    {

        public int IdSeleccionado;
        //variables para los datos seleccionados
        public string NomSelec, ApeSelec,AliasSelec,NumTelSelec,CorreoSelec;
        private SQLiteAsyncConnection conexion;
        //Para aplicar las sentencias de eliminar y modificar
        IEnumerable<T_Clientes> ResultadoDelete;
        IEnumerable<T_Clientes> ResultadoUpdate;

        public DetalleCliente(int id, string nom, string ape, string ali, string numtel, string corr)
        {
            InitializeComponent();
            //Se obtiene nuevamente la conexión
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            //Se asignan valores seleccionados
            IdSeleccionado = id;
            NomSelec = nom;
            ApeSelec = ape;
            AliasSelec = ali;
            NumTelSelec = numtel;
            CorreoSelec = corr;
            //Eventos de los botones
            btnActu.Clicked += BtnActu_Clicked;
            btnEliminar.Clicked += BtnEliminar_Clicked;
        
        }

        

        protected override void OnAppearing()
        {
            //Se asignan los valores a mostrar
            base.OnAppearing();
            lblMensaje.Text = " ID: " + IdSeleccionado;
            txtNombre.Text = NomSelec;
            txtApellido.Text = ApeSelec;
            txtAlias.Text = AliasSelec;
            txtNumTel.Text = NumTelSelec;
            txtCorreo.Text = CorreoSelec;
        }


        private void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            //Se elimina el registro apartir del ID que se seleccionó en la pagina "ConsultaCliente"
            var rutaBD = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BaseAutos.db3");
            var db = new SQLiteConnection(rutaBD);
            ResultadoDelete = Delete(db, IdSeleccionado);
            //Se muestra mensaje del correcto DELETE
            DisplayAlert("Confirmación", "El contacto se eliminó correctamente", "OK");
            //Se limpia formulario
            Limpiar();
            Navigation.PushAsync(new ConsultaCliente());
        }

        private void BtnActu_Clicked(object sender, EventArgs e)
        {
            var rutaBD = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BaseAutos.db3");
            var db = new SQLiteConnection(rutaBD);
            //Datos que se encuentran en los txt para enviados a la base y hacer la modificación con el UPDATE
            ResultadoUpdate = Update(db, txtNombre.Text, txtApellido.Text,txtAlias.Text,txtNumTel.Text,txtCorreo.Text, IdSeleccionado);
            //Mensaje de la correcta actualización
            DisplayAlert("Confirmación", "El contacto se actualizó correctamente", "Ok");
            Navigation.PushAsync(new ConsultaCliente());

        }

        public static IEnumerable<T_Clientes> Delete(SQLiteConnection db, int id)
        {
            //Aplicación de la sentencia DELET con el id seleccionado
            return db.Query<T_Clientes>("DELETE FROM T_Clientes where id=?", id);
        }
        public static IEnumerable<T_Clientes> Update(SQLiteConnection db, string nom, string ape, string ali, string numtel, string corr, int id)
        //UPDATE del registro recibiendo los nuevos datos que se muestran en "DetalleCliente"
        {
            return db.Query<T_Clientes>("UPDATE T_Clientes SET Nombre=?,Apellido=?,Alias=?,NumeroTelefonico=?,Correo=? where id=?", nom, ape,ali,numtel,corr, id);

        }
        public void Limpiar()
        {
            //Manda valores nulos para vaciar formulario
            lblMensaje.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtAlias.Text = "";
            txtNumTel.Text = "";
            txtCorreo.Text = "";
        }
    }
}