using App.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App.ViewModels
{
    public class ManageExpensesViewModel : BaseViewModel
    {
        public ManageExpensesViewModel()
        {
            Title = "Manage Expenses";
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }  
        
    }
}