using AutoMapper;
using ExpenseTracker.Data.Models;
using ExpenseTracker.Data.Models.Dtos;
using ExpenseTracker.Repository;
using ExpenseTracker.Repository.IRepository;
using ExpenseTracker.Services.Contacts;
using ExpenseTracker.ViewModels.Expense;
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

        public async Task<ICollection<DailyExpensesViewModel>> DailyExpenses(string userId)
        {
            var currDate = DateTime.Today;
            var expensesObj = await this.expenseRepository.GetAllExpenses(userId);

            var dailyExpenses = expensesObj.Where(s => s.UserId == userId && s.DateTime.Date == currDate)
                .Select(s => new DailyExpensesViewModel
                {
                    ExpenseFrom = s.ExpenseFrom,
                    Value = s.Value,
                    DateTime = s.DateTime
                })
                .ToList();

            return dailyExpenses;
        }
    }
}
