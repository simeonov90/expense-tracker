using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Data.Models
{
    public class Expense
    {
        public Expense()
        {
            DateTime = DateTime.Now;
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string ExpenseFrom { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        
    }
}
