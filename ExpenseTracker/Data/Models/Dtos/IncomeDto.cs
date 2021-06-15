using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Data.Models.Dtos
{
    public class IncomeDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string IncomeFrom { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
