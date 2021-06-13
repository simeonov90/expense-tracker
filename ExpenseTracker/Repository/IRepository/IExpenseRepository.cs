using ExpenseTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Repository.IRepository
{
    public interface IExpenseRepository
    {
        Task<ICollection<Expense>> GetAllExpenses();
        Task<Expense> GetExpense(int expenseId);
        Task<bool> CreateExpense(Expense expense);
        Task<bool> DeleteExpense(Expense expense);
        Task<bool> ExpenseExists(int expenseId);
        Task<bool> Save();
    }
}
