using App.Entities.Models;
using App.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.FacadeLayer.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        public bool AddExpenseEntry(AddEntryModel addEntryModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntrySummaryItemModel> GetAllExpenseEntries(int byLatestCount)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EntrySummaryItemModel> GetAllExpenseEntries(DateTime month)
        {
            throw new NotImplementedException();
        }

        public EntryItemDetailsModel GetEntryItemDetails(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
