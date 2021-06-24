using ExpenseTracker.Data;
using ExpenseTracker.Data.Models;
using ExpenseTracker.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
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


        public async Task<bool> CreateExpense(Expense expense)
        {
            await this.db.Expenses.AddAsync(expense);
            return await Save();
        }

        public async Task<bool> DeleteExpense(Expense expense)
        {
            this.db.Expenses.Remove(expense);
            return await Save();
        }

        public async Task<bool> ExpenseExists(int expenseId)
        {
            return await this.db.Expenses.AnyAsync(a => a.Id == expenseId);
        }

        public async Task<ICollection<Expense>> GetAllExpenses(string userId)
        { 
            return await this.db.Expenses.Where(a => a.UserId == userId).OrderByDescending(a => a.DateTime).ToListAsync();
        }

        public async Task<Expense> GetExpense(int expenseId)
        {
            return await this.db.Expenses.FirstOrDefaultAsync(e => e.Id == expenseId);
        }

        public async Task<bool> Save()
        {
            return await this.db.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
