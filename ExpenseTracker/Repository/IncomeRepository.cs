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
    public class IncomeRepository : IIncomeRepository
    {
        private readonly ApplicationDbContext db;

        public IncomeRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> CreateIncome(Income income)
        {
            await this.db.Incomes.AddAsync(income);
            return await Save();
        }

        public async Task<bool> DeleteIncome(Income income)
        {
            this.db.Incomes.Remove(income);
            return await Save();
        }

        public async Task<ICollection<Income>> GetAllIncome(string userId)
        {
            return await this.db.Incomes.Where(a => a.UserId == userId).OrderByDescending(a => a.DateTime).ToListAsync();
        }

        public async Task<Income> GetIncome(int incomeId)
        {
            return await this.db.Incomes.FirstOrDefaultAsync(a => a.Id == incomeId);
        }

        public async Task<bool> IncomeExists(int incomeId)
        {
            return await this.db.Incomes.AnyAsync(a => a.Id == incomeId);
        }

        public async Task<bool> Save()
        {
            return await this.db.SaveChangesAsync() >= 0 ? true : false;
        }
    }
}
