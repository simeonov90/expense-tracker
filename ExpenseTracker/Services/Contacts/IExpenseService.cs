using ExpenseTracker.Data.Models.Dtos;
using ExpenseTracker.ViewModels.Expense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Services.Contacts
{
    public interface IExpenseService
    {
        Task<bool> CreateExpense(ExpenseCreateDto expenseCreateDto, string userId);
        Task<ICollection<DailyExpensesViewModel>> DailyExpenses(string userId);
    }
}
