using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Data.Models.Dtos
{
    public class ExpenseCreateDto
    {
        [Required]
        public string ExpenseFrom { get; set; }

        [Required]
        public double Value { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        public string UserId { get; set; }
    }
}
