using App.Entities.Models;
using App.Interfaces.Repository;
using System;
using System.ComponentModel;
using System.Linq;
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
        public string ErrorMessage { get; set; }
        public bool HasError { get; set; }

        private IExpenseRepository expenseRepository;

        public ExpensesHomeViewModel()
        {
            Title = "My Personal Expenses";
            EntryModel = new AddEntryModel();
            SummaryModel = new ExpenseSummaryModel();
            expenseRepository = DependencyService.Get<IExpenseRepository>();
            AddPositiveExpense = new Command(async () =>
            {
                if (CanSubmitData())
                {
                    EntryModel.GeoLocation = await GetFormattedLocationAsync();
                    expenseRepository.AddExpenseEntry(EntryModel);
                    SummaryModel = expenseRepository.GetExpenseSummary();
                    OnPropertyChanged("SummaryModel");
                    EntryModel = new AddEntryModel();
                    OnPropertyChanged("EntryModel");
                    ErrorMessagePresenter();
                }
                else
                {
                    ErrorMessagePresenter("Invalid Input");
                }

            });

            AddNegativeExpense = new Command(async () =>
            {
                if (CanSubmitData())
                {

                    EntryModel.GeoLocation = await GetFormattedLocationAsync();
                    var amount = Convert.ToDecimal(EntryModel.Amount);
                    var finalAmount = amount > 0 ? -amount : amount;
                    EntryModel.Amount = Convert.ToString(finalAmount);
                    expenseRepository.AddExpenseEntry(EntryModel);
                    SummaryModel = expenseRepository.GetExpenseSummary();
                    OnPropertyChanged("SummaryModel");
                    EntryModel = new AddEntryModel();
                    OnPropertyChanged("EntryModel");
                    ErrorMessagePresenter();
                }
                else
                {
                    ErrorMessagePresenter("Invalid Input");
                }
            });
        }

        public void ErrorMessagePresenter(string text = "")
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                HasError = false;
                ErrorMessage = string.Empty;
            }
            else
            {
                HasError = true;
                ErrorMessage = text;
            }
            OnPropertyChanged("ErrorMessage");
            OnPropertyChanged("HasError");
        }

        public bool CanSubmitData()
        {
            bool canSubmit;
            try
            {
                canSubmit = !string.IsNullOrWhiteSpace(EntryModel.Amount)
                    && !string.IsNullOrWhiteSpace(EntryModel.Note)
                    && int.TryParse(EntryModel.Amount, out int intAmount);
            }
            catch (Exception)
            {
                canSubmit = false;
            }
            return canSubmit;
        }

        public async Task<string> GetFormattedLocationAsync()
        {
            string locationFormatted = string.Empty;
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location != null)
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                    locationFormatted = $"{placemark.SubLocality}, {placemark.Locality}";
            }
            return locationFormatted;
        }

    }

}
