using ExpenseTracker.Data.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Services.Contracts
{
    public interface IIncomeService
    {
        Task<bool> CreateIncome(IncomeCreateDto incomeCreateDto, string userId);
        Task<ICollection<IncomeDailyDto>> DailyIncomes(string userId);
        Task<ICollection<IncomeAllDto>> AllIncomes(string userId, int page, int itemsPerPage);
        Task<double> SumFromIncomes(string userId);
    }
}
