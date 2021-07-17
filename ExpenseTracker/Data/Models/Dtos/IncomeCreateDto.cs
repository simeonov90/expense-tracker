using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Data.Models.Dtos
{
    public class IncomeCreateDto
    {
        [Required]
        public string From { get; set; }
        [Required]
        public double Value { get; set; }
    }
}
