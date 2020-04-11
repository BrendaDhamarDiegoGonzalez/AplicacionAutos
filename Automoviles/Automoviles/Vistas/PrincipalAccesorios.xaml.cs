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
    public partial class PrincipalAccesorios : ContentPage
    {
        //Declaración de "conexion" 
        private SQLiteAsyncConnection conexiona;
        public PrincipalAccesorios()
        {
            InitializeComponent();
            //Coneccción a la base de datos por medio de "conexion""
            conexiona = DependencyService.Get<ISQLiteDB>().GetConnection();
            //Inicializacón de botones
            btnRegistrar.Clicked += BtnRegistrar_Clicked;
            btnBuscar.Clicked += BtnBuscar_Clicked;
        }
        private void BtnBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                //asignacion de dirección a la variable "rutaBD"
                var rutaBD = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BaseAutos.db3");
                // creación del objeto db para conectar con la base
                var db = new SQLiteConnection(rutaBD);
                db.CreateTable<T_Accesorios>();
                //IEnumerable para el recorrido en la base de datos llamandolo "resultado"
                //SELECT_WHERE para la consulta enviando el texto que el usuario ingreso en el txtAccesorio
                IEnumerable<T_Accesorios> resultado = SELECT_WHERE(db, txtAccesorio.Text);
                if (resultado.Count() > 0)
                {
                    //Si encuentra similitudes muestra el siguiente mensaje
                    //y manda a la página "ConsultaAccesorio"
                    Navigation.PushAsync(new ConsultaAccesorio());
                    DisplayAlert("Aviso", "Se encontraron coincidencias", "OK");
                }
                else
                {
                    //Si no encuentra similitudes muestra el siguiente mensaje
                    DisplayAlert("Aviso", "Articulo no registrado", "OK");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Clase para ejecutar la consulta
        public static IEnumerable<T_Accesorios> SELECT_WHERE(SQLiteConnection db, string nombre)
        {
            //  Manda la sentencia a la base para ser ejecutada y devuelve el resultado del recorrido
            return db.Query<T_Accesorios>("SELECT*FROM T_Accesorios WHERE Nombre=?", nombre);
        }

        private void BtnRegistrar_Clicked(object sender, EventArgs e)
        {
            //Muestra la página para registar un nuevo accesorio
            Navigation.PushAsync(new RegistroAccesorio());
        }

    }
}