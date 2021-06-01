using ExpenseTracker.Data;
using ExpenseTracker.Data.Models;
using ExpenseTracker.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Repository
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext db;
        public ExpenseRepository(ApplicationDbContext db)
        {
            this.db = db;
        }


        public bool CreateExpense(Expense expense)
        {
            this.db.Expenses.Add(expense);
            return Save();
        }

        public bool DeleteExpense(Expense expense)
        {
            this.db.Expenses.Remove(expense);
            return Save();
        }

        public bool ExpenseExists(int expenseId)
        {
            return this.db.Expenses.Any(a => a.Id == expenseId);
        }

        public ICollection<Expense> GetAllExpenses()
        {
            return this.db.Expenses.OrderBy(a => a.DateTime).ToList();
        }

        public Expense GetExpense(int expenseId)
        {
            return this.db.Expenses.FirstOrDefault(e => e.Id == expenseId);
        }

        public bool Save()
        {
            return this.db.SaveChanges() >= 0 ? true : false;
        }
    }
}
