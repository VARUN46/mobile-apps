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
        public string Message { get; set; }
        public bool HasError { get; set; }
        public bool IsSuccessMessage { get; set; }

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
                    PresentMessage("Entry Added!", false);
                }
                else
                {
                    PresentMessage("Invalid Input");
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
                    PresentMessage("Negative Entry Added!",false);
                }
                else
                {
                    PresentMessage("Invalid Input");
                }
            });
        }

        public void PresentMessage(string text = "",bool isError = true)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                HasError = false;
                Message = string.Empty;
                IsSuccessMessage = false;
            }
            else
            {
                HasError = isError;
                Message = text;
                IsSuccessMessage = !isError;
            }
            OnPropertyChanged("Message");
            OnPropertyChanged("HasError");
            OnPropertyChanged("IsSuccessMessage");
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
