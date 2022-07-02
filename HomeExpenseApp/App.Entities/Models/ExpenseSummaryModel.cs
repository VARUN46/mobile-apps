using System;
using System.Collections.Generic;
using System.Text;

namespace App.Entities.Models
{
    public class ExpenseSummaryModel
    {
        public decimal CurrentMonthSpent { get; set; }
        public decimal PreviousMonthSpent { get; set; }
        public decimal FutureMonthForecast { get; set; }
    }
}
