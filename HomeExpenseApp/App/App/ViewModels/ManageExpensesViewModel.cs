using App.Entities.Models;
using App.Interfaces.Repository;
using App.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace App.ViewModels
{
    public class ManageExpensesViewModel : BaseViewModel
    {
        private IExpenseRepository expenseRepository;
        public List<EntrySummaryItemModel> SummaryList { get; set; } = new List<EntrySummaryItemModel>();
        public ManageExpensesViewModel()
        {
            Title = "Manage Expenses";
            expenseRepository = DependencyService.Get<IExpenseRepository>();
        }

        public void OnAppearing()
        {
            SummaryList = expenseRepository.GetAllExpenseEntries(DateTime.Now).ToList();
            OnPropertyChanged("SummaryList");
        }

    }
}