using ExpenseTracker.Data.Models.Dtos;
using ExpenseTracker.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Services.Contacts
{
    public interface IExpenseService
    {
        Task<IEnumerable<GetAllExpensesViewModel>> GetAllExpenses(string userId);
        Task<bool> CreateExpense(ExpenseCreateDto expenseCreateDto, string userId);

    }
}
