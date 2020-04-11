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
    public partial class Principal : ContentPage
    {
        //Declaración de "conexion" 
        private SQLiteAsyncConnection conexion;
        public Principal()
        {
            InitializeComponent();
            //Coneccción a la base de datos por medio de "conexion""
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            //Inicializacón de botones
            btnRegistrar.Clicked += BtnRegistrar_Clicked;
            btnBuscar.Clicked += BtnBuscar_Clicked;
        }

        //Al presionar el boton "Buscar"
        private void BtnBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                //asignacion de dirección a la variable "rutaBD"
                var rutaBD = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BaseAutos.db3");
                // creación del objeto db para conectar con la base
                var db = new SQLiteConnection(rutaBD);
                db.CreateTable<T_Autos>();
                //IEnumerable para el recorrido en la base de datos llamandolo "resultado"
                //SELECT_WHERE para la consulta enviando el texto que el usuario ingreso en el txtNombre
                IEnumerable<T_Autos> resultado = SELECT_WHERE(db, txtNombre.Text);
                if (resultado.Count() > 0)
                {
                    //Si encuentra similitudes muestra el siguiente mensaje
                    //y manda a la página "Consulta"
                    Navigation.PushAsync(new Consulta());
                    DisplayAlert("Aviso","Existen autos de esa marca","OK");
                }
                else
                {
                    //Si no encuentra similitudes muestra el siguiente mensaje
                    DisplayAlert("Aviso", "No existen autos de esa marca", "OK");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Clase para ejecutar la consulta
        public static IEnumerable<T_Autos>SELECT_WHERE(SQLiteConnection db, string nombre)
        {
          //  Manda la sentencia a la base para ser ejecutada y devuelve el resultado del recorrido
            return db.Query<T_Autos>("SELECT*FROM T_Autos WHERE Marca=?", nombre);
        }

        private void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            //Muestra la página para registar un nuevo auto
            Navigation.PushAsync(new Registro());
        }
    }
}