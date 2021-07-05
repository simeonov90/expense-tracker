﻿using ExpenseTracker.Data.Models.Dtos;
using ExpenseTracker.ViewModels.Income;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Services.Contacts
{
    public interface IIncomeService
    {
        Task<bool> CreateIncome(IncomeCreateDto incomeCreateDto, string userId);
        Task<ICollection<IncomeDayliDto>> DailyIncomes(string userId);
    }
}
