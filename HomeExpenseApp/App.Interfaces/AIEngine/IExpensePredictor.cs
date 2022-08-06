using App.Entities.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Interfaces.AIEngine
{
    public interface IExpensePredictor
    {
        bool TrainModel(IEnumerable<ExpenseEntry> expenseEntries);
        string PredictValue(int weekNumber);
    }
}
