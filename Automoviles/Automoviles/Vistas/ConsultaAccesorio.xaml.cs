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
    public partial class ConsultaAccesorio : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        private ObservableCollection<T_Accesorios> TablaAccesorios;//ObservableCollection nos permite crear 
                                                         //una colección de los datos almacenados a través de la consulta
        public ConsultaAccesorio()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            //Para crear una lista con las consultas
            ListaAcce.ItemSelected += ListaAcce_ItemSelected;
        }

        private void ListaAcce_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //A través del objeto llamamos a los datos de cada campo para mosrarlos
            var Obj = (T_Accesorios)e.SelectedItem;
            var item = Obj.Id.ToString();
            var nom = Obj.Nombre;
            var cat = Obj.Categoria;
            var pie = Obj.Piezas;
            var col = Obj.Color;
            var pro = Obj.Proveedor;
            int ID = Convert.ToInt32(item);
            try
            {
                //Cuando es seleccionada alguna consulta de la lista se muestra la página "DetalleAccesorio"
                Navigation.PushAsync(new DetalleAccesorio(ID, nom,cat,pie, col, pro));
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected async override void OnAppearing()
        {
            //Clase para recuperar los registros en una lista
            var ResulRegistros = await conexion.Table<T_Accesorios>().ToListAsync();
            TablaAccesorios = new ObservableCollection<T_Accesorios>(ResulRegistros);
            ListaAcce.ItemsSource = TablaAccesorios;
            base.OnAppearing();
        }
    }
}