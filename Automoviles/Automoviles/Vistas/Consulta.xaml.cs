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
    public partial class Consulta : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        private ObservableCollection<T_Autos> TablaAutos;//ObservableCollectio nos permite crear 
                                                        //una colección de los datos almacenados a través de la consulta
        public Consulta()
        {
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            //Para crear una lista con las consultas
            ListaAutos.ItemSelected += ListaAutos_ItemSelected;
        }

        private void ListaAutos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //A través del objeto llamamos a los datos de cada campo para mosrarlos
            var Obj = (T_Autos)e.SelectedItem;
            var item = Obj.Id.ToString();
            var an = Obj.Anio;
            var col = Obj.Color;
            var mar = Obj.Marca;
            int ID = Convert.ToInt32(item);
            try
            {
                //Cuando es seleccionada alguna consulta de la lista se muestra la página "Detalle"
                Navigation.PushAsync(new Detalle(ID, mar, col, an));
            }
            catch(Exception)
            {
                throw;
            }
        }
        protected async override void OnAppearing()
        {
            //Clase para recuperar los registros en una lista
            var ResulRegistros = await conexion.Table<T_Autos>().ToListAsync();
            TablaAutos = new ObservableCollection<T_Autos>(ResulRegistros);
            ListaAutos.ItemsSource = TablaAutos;
            base.OnAppearing();
        }
    }
}