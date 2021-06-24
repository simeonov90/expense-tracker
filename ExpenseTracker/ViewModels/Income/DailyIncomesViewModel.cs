using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.ViewModels.Income
{
    public class DailyIncomesViewModel
    {
        [Required]
        public string IncomeFrom { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
    }
}
