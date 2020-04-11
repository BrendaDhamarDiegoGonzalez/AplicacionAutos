using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using SQLite;
using System.IO;
using Automoviles.Droid;
using Automoviles.Datos;
using Xamarin.Forms;

[assembly:Dependency(typeof(SQLiteDB))] // el atributo assembly:Dependency se usa 
                                        //para poder realizar la resolución de la 
                                        //implementación con DependencyService.
                                        //En el caso de Android.

namespace Automoviles.Droid
{
    public class SQLiteDB : ISQLiteDB 
    {
        public SQLiteAsyncConnection GetConnection() //Clase donde se crea la base de datos
        {
            //Dirección donde se almacenará la base
            var ruta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            //Nombre de la bese de datos 
            var path = Path.Combine(ruta, "BaseAutos.db3");
            return new SQLiteAsyncConnection(path);
        }
    }
}