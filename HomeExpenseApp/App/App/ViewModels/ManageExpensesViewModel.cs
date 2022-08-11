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
using System.Windows.Input;

namespace App.ViewModels
{
    public class ManageExpensesViewModel : BaseViewModel
    {
        private readonly IExpenseRepository expenseRepository;
        public List<EntrySummaryItemModel> SummaryList { get; set; } = new List<EntrySummaryItemModel>();
        public ICommand PullLatestEntries { get; set; }
        public string PullEntriesInput { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public ICommand PullByMonthCommand { get; set; }
        public ICommand CloseSelectedRegionCommand { get; set; }
        public bool IsItemSelected { get; set; } = false;
        public bool IsItemNotSelected { get; set; } = true;

        private object summaryItemSelected;

        public object SummaryItemSelected
        {
            get { return summaryItemSelected; }
            set
            {
                summaryItemSelected = value;
                if (summaryItemSelected != null && summaryItemSelected is EntrySummaryItemModel)
                {
                    OnItemSelected(summaryItemSelected as EntrySummaryItemModel);
                }
            }
        }


        public ManageExpensesViewModel()
        {
            Title = "Manage Expenses";
            expenseRepository = DependencyService.Get<IExpenseRepository>();

            PullLatestEntries = new Command(() =>
            {
                int.TryParse(PullEntriesInput, out int pullCount);
                SummaryList = expenseRepository.GetAllExpenseEntries(pullCount).ToList();
                OnPropertyChanged("SummaryList");
            });

            PullByMonthCommand = new Command(() =>
            {
                DateTime.TryParse($"{Month}-01-{Year}", out DateTime pullMonthDate);
                SummaryList = expenseRepository.GetAllExpenseEntries(pullMonthDate).ToList();
                OnPropertyChanged("SummaryList");
            });

            CloseSelectedRegionCommand = new Command(() =>
            {
                ShowSelectionWindow(false);
            });
        }

        public void OnAppearing()
        {
            SummaryList = expenseRepository.GetAllExpenseEntries(DateTime.Now).ToList();
            OnPropertyChanged("SummaryList");
        }

        public void OnItemSelected(EntrySummaryItemModel itemModel)
        {
            ShowSelectionWindow(true);
            var selectedItem = expenseRepository.GetEntryItemDetails(itemModel.Id);
        }

        private void ShowSelectionWindow(bool canShow)
        {
            IsItemNotSelected = !canShow;
            IsItemSelected = canShow;
            OnPropertyChanged("IsItemNotSelected");
            OnPropertyChanged("IsItemSelected");

        }

    }
}