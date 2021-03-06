﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Automoviles.Tablas;
using Automoviles.Datos;
using SQLite;
using System.IO;

namespace Automoviles.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleAccesorio : ContentPage
    {
        public int IdSeleccionado;
        //variables para los datos seleccionados
        public string NomSelec,CatSelec,PieSelec, ColSelec, ProSelec;
        private SQLiteAsyncConnection conexion;
        //Para aplicar las sentencias de eliminar y modificar
        IEnumerable<T_Accesorios> ResultadoDelete;
        IEnumerable<T_Accesorios> ResultadoUpdate;

        public DetalleAccesorio(int id, string nom, string cat, string pie,string col,string pro)
        {
            InitializeComponent();
            //Se obtiene nuevamente la conexión
            conexion = DependencyService.Get<ISQLiteDB>().GetConnection();
            //Se asignan valores seleccionados
            IdSeleccionado = id;
            NomSelec = nom;
            CatSelec = cat;
            PieSelec = pie;
            ColSelec = col;
            ProSelec = pro;
            //Eventos de los botones
            btnActu.Clicked += BtnActu_Clicked;
            btnEliminar.Clicked += BtnEliminar_Clicked;
           
        }

        

        protected override void OnAppearing()
        {
            //Se asignan los valores a mostrar
            base.OnAppearing();
            lblMensaje.Text = " ID: " + IdSeleccionado;
            txtNombre.Text = NomSelec;
            txtCat.Text = CatSelec;
            txtPie.Text = PieSelec;
            txtColor.Text = ColSelec;
            txtPro.Text = ProSelec;
        }


        private void BtnEliminar_Clicked(object sender, EventArgs e)
        {
            //Se elimina el registro apartir del ID que se seleccionó en la pagina "ConsultaAccesorio"
            var rutaBD = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BaseAutos.db3");
            var db = new SQLiteConnection(rutaBD);
            ResultadoDelete = Delete(db, IdSeleccionado);
            //Se muestra mensaje del correcto DELETE
            DisplayAlert("Confirmación", "El artículo se eliminó correctamente", "OK");
            
            //Se limpia formulario
            Limpiar();
        }

        private void BtnActu_Clicked(object sender, EventArgs e)
        {
            var rutaBD = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "BaseAutos.db3");
            var db = new SQLiteConnection(rutaBD);
            //Datos que se encuentran en los txt para enviados a la base y hacer la modificación con el UPDATE
            ResultadoUpdate = Update(db, txtNombre.Text, txtCat.Text, txtPie.Text,txtColor.Text,txtPro.Text, IdSeleccionado);
            //Mensaje de la correcta actualización
            DisplayAlert("Confirmación", "El articulo se actualizó correctamente", "Ok");
          
        }

        public static IEnumerable<T_Accesorios> Delete(SQLiteConnection db, int id)
        {
            //Aplicación de la sentencia DELET con el id seleccionado
            return db.Query<T_Accesorios>("DELETE FROM T_Accesorios where id=?", id);
        }
        public static IEnumerable<T_Accesorios> Update(SQLiteConnection db, string nom, string cat, string pie, string col, string pro, int id)
        //UPDATE del registro recibiendo los nuevos datos que se muestran en "Detalle"
        {
            return db.Query<T_Accesorios>("UPDATE T_Accesorios SET Nombre=?,Categoria=?,Piezas=?,Color=?,Proveedor=? where id=?", nom, cat,pie,col,pro, id);

        }
        public void Limpiar()
        {
            //Manda valores nulos para vaciar formulario
            lblMensaje.Text = "";
            txtNombre.Text = "";
            txtColor.Text = "";
            txtCat.Text = "";
            txtPie.Text = "";
            txtPro.Text = "";
        }
    }
}