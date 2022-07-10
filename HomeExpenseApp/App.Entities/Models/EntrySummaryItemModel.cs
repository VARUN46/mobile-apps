using System;
using System.Collections.Generic;
using System.Text;

namespace App.Entities.Models
{
    public class EntrySummaryItemModel
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }
        public string SummaryText { get; set; }
        public string Note { get; set; }
        public string GeoLocation { get; set; }
    }
}
