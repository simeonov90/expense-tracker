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
        IEnumerable<GetAllExpensesViewModel> GetAllExpenses(string userId);
        bool CreateExpense(ExpenseCreateDto expenseCreateDto, string userId);

    }
}
