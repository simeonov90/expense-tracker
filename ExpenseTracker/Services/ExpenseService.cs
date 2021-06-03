using AutoMapper;
using ExpenseTracker.Data.Models;
using ExpenseTracker.Data.Models.Dtos;
using ExpenseTracker.Repository;
using ExpenseTracker.Repository.IRepository;
using ExpenseTracker.Services.Contacts;
using ExpenseTracker.ViewModels;
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

        public bool CreateExpense(ExpenseCreateDto expenseCreateDto, string userId)
        {
            var expenseObj = this.mapper.Map<Expense>(expenseCreateDto);
            expenseObj.UserId = userId;

            var obj = this.expenseRepository.CreateExpense(expenseObj);

            return obj;  
        }

        public IEnumerable<GetAllExpensesViewModel> GetAllExpenses(string userId)
        {
            var obj = this.expenseRepository.GetAllExpenses()
                .Where(x => x.UserId == userId)
                .Select(x => new GetAllExpensesViewModel
                {
                    ExpenseFrom = x.ExpenseFrom,
                    Value = x.Value,
                    DateTime = x.DateTime
                })
                .ToList();

            return obj;
        }
    }
}
