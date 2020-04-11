using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Automoviles.Tablas;
using System.Collections.ObjectModel;
using System.IO;
using Automoviles.Datos;

namespace Automoviles.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaCliente : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        private ObservableCollection<T_Clientes> TablaCliente;//ObservableCollection nos permite crear 
                                                                   //una colección de los datos almacenados a través de una consulta
        public ConsultaCliente()
        {
            InitializeComponent();
            //Obtenemos la conexión 
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            //Para crear una lista con los resultados que se obtendran
            ListaClientes.ItemSelected += ListaClientes_ItemSelected;
        }

        private void ListaClientes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //A través del objeto llamamos a los datos de cada campo para mostrarlos
            var Obj = (T_Clientes)e.SelectedItem;
            var item = Obj.Id.ToString();
            var nom = Obj.Nombre;
            var ape = Obj.Apellido;
            var alias = Obj.Alias;
            var numtel = Obj.NumeroTelefonico;
            var corr = Obj.Correo;
            int ID = Convert.ToInt32(item);
            try
            {
                //Cuando es seleccionada alguna consulta de la lista se muestra la página "DetalleCliente"
                //Para ver todos los datos que se tienen del cliente
                Navigation.PushAsync(new DetalleCliente(ID, nom, ape,alias,numtel,corr));
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected async override void OnAppearing()
        {
            //Clase para recuperar los registros en una lista
            var ResulRegistros = await conexion.Table<T_Clientes>().ToListAsync();
            TablaCliente = new ObservableCollection<T_Clientes>(ResulRegistros);
            ListaClientes.ItemsSource = TablaCliente;
            base.OnAppearing();
        }
    }
}