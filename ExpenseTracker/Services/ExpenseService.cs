using AutoMapper;
using ExpenseTracker.Data.Models;
using ExpenseTracker.Data.Models.Dtos;
using ExpenseTracker.Repository;
using ExpenseTracker.Repository.IRepository;
using ExpenseTracker.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository expenseRepository;
        private readonly IMapper mapper;
        public ExpenseService(IExpenseRepository expenseRepository, IMapper mapper)
        {
            this.expenseRepository = expenseRepository;
            this.mapper = mapper;
        }

        public async Task<bool> CreateExpense(ExpenseCreateDto expenseCreateDto, string userId)
        {
            var expenseObj = this.mapper.Map<Expense>(expenseCreateDto);
            expenseObj.UserId = userId;

            var obj = await this.expenseRepository.CreateExpense(expenseObj);

            return obj;  
        }

        public async Task<ICollection<ExpenseDailyDto>> DailyExpenses(string userId)
        {
            var currDate = DateTime.Today;
            var expensesObj = await this.expenseRepository.GetAllExpenses(userId);

            var dailyObj = expensesObj.Where(s => s.UserId == userId && s.DateTime.Date == currDate).ToList();
            var objDto = new List<ExpenseDailyDto>();
            foreach (var obj in dailyObj)
            {
                objDto.Add(this.mapper.Map<ExpenseDailyDto>(obj));
            }

            return objDto;
        }

        public async Task<double> SumFromExpenses(string userId)
        {
            var expenses = await this.expenseRepository.GetAllExpenses(userId);
            var sum = expenses.Select(a => a.Value).ToList();
            return sum.Sum();
        }
    }
}
