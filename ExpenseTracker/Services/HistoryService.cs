using AutoMapper;
using ExpenseTracker.Data.Models.Dtos;
using ExpenseTracker.Repository.IRepository;
using ExpenseTracker.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            var allIncomes = await this.incomeRepository.GetAllIncomes(userId);
            var allExpenses = await this.expenseRepository.GetAllExpenses(userId);

            var allHistory = new List<HistoryAllDto>();

            foreach (var obj in allIncomes)
            {
                allHistory.Add(this.mapper.Map<HistoryAllDto>(obj));
            }

            foreach (var obj in allExpenses)
            {
                allHistory.Add(this.mapper.Map<HistoryAllDto>(obj));
            }

            HashSet<HistoryAllDto> sortedHistory = allHistory.OrderByDescending(a => a.DateTime).ToHashSet();

            return sortedHistory;
        }

        public async Task<ICollection<HistoryAllDto>> GetAll(string userId, int page, int itemsPerPage)
        {
            var allHistory = await this.GetAll(userId);

            var pagingHistory = allHistory
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).ToList();

            return pagingHistory;
        }

        public async Task<ICollection<HistoryByDateDto>> GetByDate(string userId, DateTime from, DateTime to)
        {
            var allIncomes = await this.incomeRepository.GetAllIncomes(userId);
            var allExpenses = await this.expenseRepository.GetAllExpenses(userId);

            var byDateHistory = new List<HistoryByDateDto>();

            foreach (var obj in allIncomes)
            {
                byDateHistory.Add(this.mapper.Map<HistoryByDateDto>(obj));
            }

            foreach (var obj in allExpenses)
            {
                byDateHistory.Add(this.mapper.Map<HistoryByDateDto>(obj));
            }

            HashSet<HistoryByDateDto> sortedHistory = byDateHistory.Where(a => a.DateTime.Date >= from && a.DateTime.Date <= to).OrderByDescending(a => a.DateTime).ToHashSet();

            return sortedHistory;
        }

        public async Task<ICollection<HistoryByDateDto>> GetByDate(string userId, DateTime from, DateTime to, int page, int itemsPerPage)
        {
            var byDate = await this.GetByDate(userId, from, to);

            var pagingByDateHistory = byDate
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage).ToList();

            return pagingByDateHistory;
        }

        public async Task<ICollection<HistoryDailyDto>> GetDaily(string userId)
        {
            var dailyIncomes = await this.incomeRepository.GetAllIncomes(userId);
            var dailyExpenses = await this.expenseRepository.GetAllExpenses(userId);

            var currDate = DateTime.Today;
            var dailyIncomesObj = dailyIncomes.Where(a => a.UserId == userId && a.DateTime.Date == currDate).ToList();
            var dailyExpensesObj = dailyExpenses.Where(a => a.UserId == userId && a.DateTime.Date == currDate).ToList();

            var dailyHistory = new List<HistoryDailyDto>();

            foreach (var obj in dailyIncomesObj)
            {
                dailyHistory.Add(this.mapper.Map<HistoryDailyDto>(obj));
            }

            foreach (var obj in dailyExpensesObj)
            {
                dailyHistory.Add(this.mapper.Map<HistoryDailyDto>(obj));
            }

            HashSet<HistoryDailyDto> sortedObj = dailyHistory.OrderByDescending(a => a.DateTime).ToHashSet();

            return sortedObj;
        }
    }
}
