using App.Entities.Core;
using App.Entities.Models;
using App.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SQLite;
using Xamarin.Forms;

namespace App.FacadeLayer.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private SQLiteConnection databaseConnection;
        public static object lockObj = new object(); 

        public ExpenseRepository()
        {
            databaseConnection = DependencyService.Get<IDatabaseConnection>().GetConnection();
        }
        
        public bool AddExpenseEntry(AddEntryModel addEntryModel)
        {
            lock (lockObj)
            {
                bool isAdded = false;
                try
                {
                    var amount = Convert.ToDecimal(addEntryModel.Amount);
                    databaseConnection.CreateTable<ExpenseEntry>();
                    var notedDate = addEntryModel.IsPastExpense ? DateTime.Now.AddMonths(-1) : DateTime.Now;
                    databaseConnection.Insert(new ExpenseEntry
                    {
                        Id = Guid.NewGuid(),
                        Amount = Convert.ToDecimal(addEntryModel.Amount),
                        Note = addEntryModel.Note,
                        NotedDate = notedDate,
                        NotedMonth = notedDate.Month,
                        NotedYear = notedDate.Year,
                        UpdatedDate = DateTime.Now,
                        GeoLocation = addEntryModel.GeoLocation
                    });
                    isAdded = true;
                }
                catch (Exception)
                {

                }
                return isAdded;
            }
        }

        public IEnumerable<EntrySummaryItemModel> GetAllExpenseEntries(int byLatestCount)
        {
            lock (lockObj)
            {
                var data = new List<EntrySummaryItemModel>();
                try
                {
                    data = (from entry in databaseConnection.Table<ExpenseEntry>().OrderByDescending(c=>c.NotedDate)
                            .Take(byLatestCount).ToList()
                            select new EntrySummaryItemModel
                            {
                                Amount = entry.Amount,
                                Date = entry.NotedDate.ToString("ddd,MMM yyyy"),
                                Id = entry.Id
                            }).ToList();

                }
                catch (Exception)
                {

                }
                return data;
            }
        }

        public IEnumerable<EntrySummaryItemModel> GetAllExpenseEntries(DateTime month)
        {
            lock (lockObj)
            {

                var data = new List<EntrySummaryItemModel>();
                try
                {
                    var qyear = month.Year;
                    var qmonth = month.Month;
                    data = (from entry in databaseConnection.Table<ExpenseEntry>()
                            .Where(c => c.NotedMonth == qmonth && c.NotedYear == qyear)
                            .OrderByDescending(c => c.NotedDate)
                            .ToList()
                            select new EntrySummaryItemModel
                            {
                                Amount = entry.Amount,
                                Date = entry.NotedDate.ToString("ddd, d MMM yyyy"),
                                Id = entry.Id,
                                SummaryText = string.Format("{0} spent on {1}", entry.Amount, entry.NotedDate.ToString("ddd, d MMM yyyy")),
                                Note = entry.Note,
                                GeoLocation = $"( Marked from {entry.GeoLocation} )"
                            }).ToList();
                }
                catch (Exception ex)
                {

                }
                return data;
            }
        }

        public EntryItemDetailsModel GetEntryItemDetails(Guid id)
        {
            lock (lockObj)
            {

                var entry = new EntryItemDetailsModel();
                try
                {
                    var entryData = databaseConnection.Table<ExpenseEntry>().FirstOrDefault(c => c.Id == id);
                    entry = new EntryItemDetailsModel
                    {
                        Id = entryData.Id,
                        Amount = entryData.Amount,
                        Note = entryData.Note,
                        NotedDate = entryData.NotedDate,
                        UpdatedDate = entryData.UpdatedDate
                    };
                }
                catch (Exception)
                {

                }
                return entry;
            }
        }

        public ExpenseSummaryModel GetExpenseSummary()
        {
            lock (lockObj)
            {

                var summary = new ExpenseSummaryModel();
                try
                {
                    var currentMonthAmountSummary = databaseConnection.Table<ExpenseEntry>().Where(c => c.NotedMonth == DateTime.Now.Month).Sum(c => c.Amount);
                    summary.CurrentMonthSpent = string.Format("This month expense is {0}", currentMonthAmountSummary);
                    var pastMonthAmountSummary = databaseConnection.Table<ExpenseEntry>().Where(c => c.NotedMonth == DateTime.Now.Month - 1).Sum(c => c.Amount);
                    summary.PreviousMonthSpent = string.Format("Previous month expense is {0}", pastMonthAmountSummary);
                }
                catch (Exception)
                {

                }
                return summary;
            }
        }
    }
}
