using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.ViewModels
{
    public class GetAllExpensesViewModel
    {
        public string ExpenseFrom { get; set; }
        public double Value { get; set; }
        public DateTime DateTime { get; set; }

    }
}
