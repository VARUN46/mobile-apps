﻿using App.Entities.Models;
using App.Interfaces.Repository;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App.ViewModels
{
    public class ExpensesHomeViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public AddEntryModel EntryModel { get; set; }
        public ExpenseSummaryModel SummaryModel { get; set; }
        public ICommand AddPositiveExpense { get; set; }
        public ICommand AddNegativeExpense { get; set; }

        private IExpenseRepository expenseRepository;

        public ExpensesHomeViewModel()
        {
            EntryModel = new AddEntryModel();
            SummaryModel = new ExpenseSummaryModel();
            expenseRepository = DependencyService.Get<IExpenseRepository>();
            Title = "My Personal Expenses";
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}