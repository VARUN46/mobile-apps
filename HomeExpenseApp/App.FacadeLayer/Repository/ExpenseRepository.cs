using App.Entities.Core;
using App.Entities.Models;
using App.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace App.FacadeLayer.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private static readonly List<ExpenseEntry> entries = new List<ExpenseEntry>();
        public bool AddExpenseEntry(AddEntryModel addEntryModel)
        {
            bool isAdded = false;
            try
            {
                var amount = Convert.ToDecimal(addEntryModel.Amount);
                entries.Add(new ExpenseEntry
                {
                    Id = Guid.NewGuid(),
                    Amount = Convert.ToDecimal(addEntryModel.Amount),
                    Note = addEntryModel.Note,
                    NotedDate = addEntryModel.IsPastExpense ? DateTime.Now.AddMonths(-1) : DateTime.Now,
                    UpdatedDate = DateTime.Now,
                });
                isAdded = true;
            }
            catch (Exception)
            {

            }
            return isAdded;
        }

        public IEnumerable<EntrySummaryItemModel> GetAllExpenseEntries(int byLatestCount)
        {
            var data = new List<EntrySummaryItemModel>();
            try
            {
                data = (from entry in entries
                            orderby entry.NotedDate descending
                            select new EntrySummaryItemModel
                            {
                                Amount = entry.Amount,
                                Date = entry.NotedDate.ToString("ddd,MMM yyyy"),
                                Id = entry.Id
                            }).Take(byLatestCount).ToList();

            }
            catch (Exception)
            {

            }
            return data;
        }

        public IEnumerable<EntrySummaryItemModel> GetAllExpenseEntries(DateTime month)
        {
            var data = new List<EntrySummaryItemModel>();
            try
            {
                data = (from entry in entries
                        where entry.NotedDate.Month == month.Month
                        orderby entry.NotedDate descending
                        select new EntrySummaryItemModel
                        {
                            Amount = entry.Amount,
                            Date = entry.NotedDate.ToString("ddd, d MMM yyyy"),
                            Id = entry.Id,
                            SummaryText = string.Format("{0} spent on {1}", entry.Amount, entry.NotedDate.ToString("ddd, d MMM yyyy")),
                            Note = entry.Note
                        }).ToList();
            }
            catch (Exception)
            {

            }
            return data;
        }

        public EntryItemDetailsModel GetEntryItemDetails(Guid id)
        {
            var entry = new EntryItemDetailsModel();
            try
            {
                var entryData = entries.FirstOrDefault(c => c.Id == id);
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

        public ExpenseSummaryModel GetExpenseSummary()
        {
            var summary = new ExpenseSummaryModel();
            try
            {
                var currentMonthAmountSummary = entries.Where(c=>c.NotedDate.Month == DateTime.Now.Month).Sum(c => c.Amount);
                summary.CurrentMonthSpent = string.Format("This month expense is {0}", currentMonthAmountSummary);
                var pastMonthAmountSummary = entries.Where(c => c.NotedDate.Month == DateTime.Now.Month - 1).Sum(c => c.Amount);
                summary.PreviousMonthSpent = string.Format("Previous month expense is {0}", pastMonthAmountSummary);
            }
            catch (Exception)
            {

            }
            return summary;
        }
    }
}
