using ExpenseTracker.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Repository.IRepository
{
    public interface IIncomeRepository
    {
        Task<ICollection<Income>> GetAllIncome();
        Task<Income> GetIncome(int incomeId);
        Task<bool> CreateIncome(Income income);
        Task<bool> DeleteIncome(Income income);
        Task<bool> IncomeExists(int incomeId);
        Task<bool> Save();
    }
}
