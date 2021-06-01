using ExpenseTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Repository.IRepository
{
    public interface IExpenseRepository
    {
        ICollection<Expense> GetAllExpenses();
        Expense GetExpense(int expenseId);
        bool CreateExpense(Expense expense);
        bool DeleteExpense(Expense expense);
        bool ExpenseExists(int expenseId);
    }
}
