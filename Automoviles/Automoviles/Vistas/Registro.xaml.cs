using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using Automoviles.Tablas;
using Automoviles.Datos;

namespace Automoviles.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registro : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        public Registro()
        {
            //Al igual que en la pagina principal obtenemos la conexion a la base de datos
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            btnGuardar.Clicked += BtnGuardar_Clicked;
        }
        private void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            //Se asignan los valores de los txt a los atributos de la base de datos a través de DatosAuto 
            var DatosAuto = new T_Autos { Marca = txtMarca.Text, Color = txtColor.Text, Anio = txtAnio.Text };
            conexion.InsertAsync(DatosAuto);
            //Llamamos a la clase Limpiar
            Limpiar();
            //Confirmamos el correcto registro
            DisplayAlert("Confirmación", "El auto se registro correctamente", "OK");
        }
        void Limpiar()
        {
            //Asigan valores nulos para limpiar las casillas
            txtMarca.Text = "";
            txtColor.Text = "";
            txtAnio.Text = "";
        }
    }
}