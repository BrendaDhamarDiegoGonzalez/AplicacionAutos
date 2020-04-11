using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using SQLite;
using Automoviles.Tablas;
using System.IO;
using Automoviles.Datos;

namespace Automoviles.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrincipalCliente : ContentPage
    {
        //Declaración de "conexion" 
        private SQLiteAsyncConnection conexionc;
        public PrincipalCliente()
        {
            InitializeComponent();
            //Coneccción a la base de datos por medio de "conexionc""
            conexionc = DependencyService.Get<ISQLiteDB>().GetConnection();
            //Inicializacón de botones
            btnRegistrar.Clicked += BtnRegistrar_Clicked;
            btnBuscar.Clicked += BtnBuscar_Clicked;
        }
        private void BtnBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                //asignacion de dirección a la variable "rutaBD" para conectar con la base de datos
                var rutaBD = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BaseAutos.db3");
                // creación del objeto db 
                var db = new SQLiteConnection(rutaBD);
                db.CreateTable<T_Clientes>();
                //IEnumerable para el recorrido en la base de datos llamandolo "resultado"
                //SELECT_WHERE para la consulta enviando el texto que el usuario ingreso en el txtCli
                IEnumerable<T_Clientes> resultado = SELECT_WHERE(db, txtCli.Text);
                if (resultado.Count() > 0)
                {
                    //Si encuentra similitudes muestra el siguiente mensaje
                    //y manda a la página "ConsultaCliente"
                    Navigation.PushAsync(new ConsultaCliente());
                    DisplayAlert("Aviso", "Se encontraron coincidencias", "OK");
                }
                else
                {
                    //Si no encuentra similitudes muestra el siguiente mensaje
                    DisplayAlert("Aviso", "El contacto no existe", "OK");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Clase para ejecutar la consulta
        public static IEnumerable<T_Clientes> SELECT_WHERE(SQLiteConnection db, string alias)
        {
            //  Manda la sentencia a la base para ser ejecutada y devuelve el resultado del recorrido
            return db.Query<T_Clientes>("SELECT*FROM T_Clientes WHERE Alias=?", alias);
        }

        private void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            //Muestra la página para registar un nuevo Cliente
            Navigation.PushAsync(new RegistroCliente());
        }
    }
}