using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Data.Models
{
    public class Income
    {
        private string type;

        [Key]
        public int Id { get; set; }
        public string Type
        {
            get
            {
                return this.type;
            }
            private set
            {
                this.type = "Income";
            }
        }
        [Required]
        public string From { get; set; }
        [Required]
        public double Value { get; set; }      
        [Required]        
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Income()
        {
            Type = this.type;
            DateTime = DateTime.Now;
        }
    }
}
