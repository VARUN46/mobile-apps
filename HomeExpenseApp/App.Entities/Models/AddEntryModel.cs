using System;
using System.Collections.Generic;
using System.Text;

namespace App.Entities.Models
{
    public class AddEntryModel
    {
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public bool IsPastExpense { get; set; }
    }
}
