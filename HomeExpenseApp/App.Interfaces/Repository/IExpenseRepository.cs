using App.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Interfaces.Repository
{
    public interface IExpenseRepository
    {
        bool AddExpenseEntry(AddEntryModel addEntryModel);
        IEnumerable<EntrySummaryItemModel> GetAllExpenseEntries(int byLatestCount);
        IEnumerable<EntrySummaryItemModel> GetAllExpenseEntries(DateTime month);
        EntryItemDetailsModel GetEntryItemDetails(Guid id);
    }
}
