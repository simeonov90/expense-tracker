using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Data.Models.Dtos
{
    public class HistoryDailyDto
    {
        public string Type { get; set; }
        public string From { get; set; }
        public double Value { get; set; }
        public DateTime DateTime { get; set; }
    }
}
