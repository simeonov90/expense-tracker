using AutoMapper;
using ExpenseTracker.Data.Models.Dtos;
using ExpenseTracker.Repository.IRepository;
using ExpenseTracker.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IIncomeRepository incomeRepository;
        private readonly IExpenseRepository expenseRepository;
        private readonly IMapper mapper;

        public HistoryService(IIncomeRepository incomeRepository, IExpenseRepository expenseRepository, IMapper mapper)
        {
            this.incomeRepository = incomeRepository;
            this.expenseRepository = expenseRepository;
            this.mapper = mapper;
        }
        public async Task<ICollection<HistoryAllDto>> GetAll(string userId)
        {
            var allIncomes = await this.incomeRepository.GetAllIncome(userId);
            var allExpenses = await this.expenseRepository.GetAllExpenses(userId);

            var allIncomesObj = allIncomes.Where(a => a.UserId == userId).ToList();
            var allExpensesObj = allExpenses.Where(a => a.UserId == userId).ToList();

            var allHistory = new List<HistoryAllDto>();    

            foreach (var obj in allIncomesObj)
            {
                allHistory.Add(this.mapper.Map<HistoryAllDto>(obj));
            }

            foreach (var obj in allExpensesObj)
            {
                allHistory.Add(this.mapper.Map<HistoryAllDto>(obj));
            }

            HashSet<HistoryAllDto> sortedObj = allHistory.OrderByDescending(a => a.DateTime).ToHashSet();

            return sortedObj;
        }
    }
}
