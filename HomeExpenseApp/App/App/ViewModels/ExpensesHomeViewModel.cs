using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App.ViewModels
{
    public class ExpensesHomeViewModel : BaseViewModel
    {
        public ExpensesHomeViewModel()
        {
            Title = "My Personal Expenses";
            OpenWebCommand = new Command(async () => await Task.FromResult(1));
        }

        public ICommand OpenWebCommand { get; }
    }
}