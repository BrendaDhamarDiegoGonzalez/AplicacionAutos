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
    public partial class RegistroAccesorio : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        public RegistroAccesorio()
        {
            //Al igual que en la pagina principal obtenemos la conexion a la base de datos
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            btnGuardar.Clicked += BtnGuardar_Clicked;
        }
        private void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            //Se asignan los valores de los txt a los atributos de la base de datos a través de DatosAccesorio 
            var DatosAccesorio = new T_Accesorios { Nombre=txtNombre.Text,Categoria=txtCategoria.Text,Piezas=txtPiezas.Text, Color = txtColor.Text, Proveedor=txtProveedor.Text };
            conexion.InsertAsync(DatosAccesorio);
            //Llamamos a la clase Limpiar
            Limpiar();
            //Confirmamos el correcto registro
            DisplayAlert("Confirmación", "El articulo se registro correctamente", "OK");
        }
        void Limpiar()
        {
            //Asigan valores nulos para limpiar las casillas
            txtNombre.Text = "";
            txtColor.Text = "";
            txtCategoria.Text = "";
            txtProveedor.Text = "";
            txtPiezas.Text = "";
        }
    }
}