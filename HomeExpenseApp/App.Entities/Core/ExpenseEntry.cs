using System;

namespace App.Entities.Core
{
    public class ExpenseEntry
    {
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime NotedDate { get; set; }
    }
}
