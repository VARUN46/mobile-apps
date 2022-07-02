using App.FacadeLayer.Repository;
using App.Interfaces.Repository;
using App.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //   DependencyService.Register<MockDataStore>();
            DependencyService.Register<IExpenseRepository, ExpenseRepository>();
            MainPage = new AppShell();
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
