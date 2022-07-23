using System;

namespace App.Entities.Core
{
    public class ExpenseEntry
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime NotedDate { get; set; }
        public int NotedMonth { get; set; }
        public int NotedYear { get; set; }
        public string GeoLocation { get; set; }
    }
}
