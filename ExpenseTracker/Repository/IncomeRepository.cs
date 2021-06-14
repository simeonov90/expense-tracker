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

        public Task<ICollection<Income>> GetAllIncome()
        {
            throw new NotImplementedException();
        }

        public Task<Income> GetIncome(int incomeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IncomeExists(int incomeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }
    }
}
