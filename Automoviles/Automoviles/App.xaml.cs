﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Automoviles
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Vistas.PrincipalApp());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}