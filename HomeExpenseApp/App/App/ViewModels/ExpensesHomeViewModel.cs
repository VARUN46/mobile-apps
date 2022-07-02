using App.Entities.Models;
using App.Interfaces.Repository;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App.ViewModels
{
    public class ExpensesHomeViewModel : BaseViewModel
    {
        public AddEntryModel EntryModel { get; set; }
        public ExpenseSummaryModel SummaryModel { get; set; }
        public ICommand AddPositiveExpense { get; set; }
        public ICommand AddNegativeExpense { get; set; }

        private IExpenseRepository expenseRepository;

        public ExpensesHomeViewModel()
        {
            Title = "My Personal Expenses";
            EntryModel = new AddEntryModel();
            SummaryModel = new ExpenseSummaryModel();
            expenseRepository = DependencyService.Get<IExpenseRepository>();
            AddPositiveExpense = new Command(() =>
            {
                expenseRepository.AddExpenseEntry(EntryModel);
                SummaryModel = expenseRepository.GetExpenseSummary();
                OnPropertyChanged("SummaryModel");
                EntryModel = new AddEntryModel();
                OnPropertyChanged("EntryModel");
            });
            AddNegativeExpense = new Command(() =>
            {
                var amount = Convert.ToDecimal(EntryModel.Amount);
                var finalAmount = amount > 0 ? -amount : amount;
                EntryModel.Amount = Convert.ToString(finalAmount);
                expenseRepository.AddExpenseEntry(EntryModel);
                SummaryModel = expenseRepository.GetExpenseSummary();
                OnPropertyChanged("SummaryModel");
                EntryModel = new AddEntryModel();
                OnPropertyChanged("EntryModel");
            });
        }

    }
}