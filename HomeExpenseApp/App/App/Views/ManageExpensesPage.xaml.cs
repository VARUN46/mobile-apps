using App.Models;
using App.ViewModels;
using App.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Views
{
    public partial class ManageExpensesPage : ContentPage
    {
        ManageExpensesViewModel _viewModel;

        public ManageExpensesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ManageExpensesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}