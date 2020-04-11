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
    public partial class RegistroCliente : ContentPage
    {
        private SQLiteAsyncConnection conexion;
        public RegistroCliente()
        {
            //Obtenemos la conexión a la base de datos e inicializamos botón
            InitializeComponent();
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            btnGuardar.Clicked += BtnGuardar_Clicked;
        }
        private void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            //Se asignan los valores de los txt a los atributos de la base de datos a través de DatosCliente 
            var DatosCliente = new T_Clientes { Nombre = txtNombre.Text, Apellido = txtApellido.Text, Alias = txtAlias.Text, NumeroTelefonico = txtNumTel.Text, Correo = txtCorreo.Text };
            conexion.InsertAsync(DatosCliente);
            //Llamamos a la clase Limpiar
            Limpiar();
            //Confirmamos el correcto registro
            DisplayAlert("Confirmación", "El registro se ejecutó", "OK");
        }
        void Limpiar()
        {
            //Asigan valores nulos para limpiar las casillas
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtAlias.Text = "";
            txtNumTel.Text = "";
            txtCorreo.Text = "";
        }
    }
}