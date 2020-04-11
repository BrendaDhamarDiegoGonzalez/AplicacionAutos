using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Automoviles.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrincipalApp : ContentPage
    {
        public PrincipalApp()

        {
            //Se inicializan botones
            InitializeComponent();
            btnAutos.Clicked += BtnAutos_Clicked;
            btnAccesorios.Clicked += BtnAccesorios_Clicked;
            btnClientes.Clicked += BtnClientes_Clicked;
        }

        //Direccionan a la página correspondiente
        private void BtnClientes_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PrincipalCliente ());
        }

        private void BtnAccesorios_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PrincipalAccesorios());
        }

        private void BtnAutos_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Principal());
        }
    }
}