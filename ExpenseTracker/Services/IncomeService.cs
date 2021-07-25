using AutoMapper;
using ExpenseTracker.Data.Models;
using ExpenseTracker.Data.Models.Dtos;
using ExpenseTracker.Repository.IRepository;
using ExpenseTracker.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository incomeRepository;
        private readonly IMapper mapper;

        public IncomeService(IIncomeRepository incomeRepository, IMapper mapper)
        {
            this.incomeRepository = incomeRepository;
            this.mapper = mapper;
        }

        public async Task<bool> CreateIncome(IncomeCreateDto incomeCreateDto, string userId)
        {
            var incomeObj = this.mapper.Map<Income>(incomeCreateDto);
            incomeObj.UserId = userId;
            var obj = await this.incomeRepository.CreateIncome(incomeObj);

            return obj;
        }

        public async Task<ICollection<IncomeDailyDto>> DailyIncomes(string userId)
        {         
            var currDate = DateTime.Today;
            var incomesObj = await this.incomeRepository.GetAllIncomes(userId);
            var dailyObj = incomesObj.Where(s => s.UserId == userId && s.DateTime.Date == currDate).ToList();
            var objDto = new List<IncomeDailyDto>();

            foreach (var obj in dailyObj)
            {
                objDto.Add(this.mapper.Map<IncomeDailyDto>(obj));
            }
           
            return objDto;
        }

        public async Task<double> SumFromIncomes(string userId)
        {
            var incomes = await this.incomeRepository.GetAllIncomes(userId);
            var sum = incomes.Select(a => a.Value).ToList();
            return sum.Sum();
        }
    }
}
