using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Data.Models.Dtos
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        [Required]
        public string From { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
