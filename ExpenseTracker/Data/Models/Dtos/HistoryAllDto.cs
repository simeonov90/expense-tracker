﻿using System;

namespace ExpenseTracker.Data.Models.Dtos
{
    public class HistoryAllDto
    {
        public string Type { get; set; }
        public string From { get; set; }
        public double Value { get; set; }
        public DateTime DateTime { get; set; }
    }
}
