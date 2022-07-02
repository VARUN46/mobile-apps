using System;
using System.Collections.Generic;
using System.Text;

namespace App.Entities.Models
{
    public class ExpenseSummaryModel
    {
        public string CurrentMonthSpent { get; set; }
        public string PreviousMonthSpent { get; set; }
        public string FutureMonthForecast { get; set; }

        public ExpenseSummaryModel()
        {
            CurrentMonthSpent = "Current month spent is to be calculated";

            PreviousMonthSpent = "Past month spent is to be calculated";

            FutureMonthForecast = "Future month spent is to be calculated";
        }
    }
}
