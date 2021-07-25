using ExpenseTracker.Data.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Services.Contracts
{
    public interface IExpenseService
    {
        Task<bool> CreateExpense(ExpenseCreateDto expenseCreateDto, string userId);
        Task<ICollection<ExpenseDailyDto>> DailyExpenses(string userId);
        Task<double> SumFromExpenses(string userId);
    }
}
