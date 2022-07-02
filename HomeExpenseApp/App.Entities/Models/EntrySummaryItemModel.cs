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
    }
}
