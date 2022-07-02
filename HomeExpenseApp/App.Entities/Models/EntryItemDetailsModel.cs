using System;
using System.Collections.Generic;
using System.Text;

namespace App.Entities.Models
{
    public class EntryItemDetailsModel
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime NotedDate { get; set; }
    }
}
