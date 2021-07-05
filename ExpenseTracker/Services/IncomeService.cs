using AutoMapper;
using ExpenseTracker.Data.Models;
using ExpenseTracker.Data.Models.Dtos;
using ExpenseTracker.Repository.IRepository;
using ExpenseTracker.Services.Contacts;
using ExpenseTracker.ViewModels.Income;
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

        public async Task<ICollection<IncomeDayliDto>> DailyIncomes(string userId)
        {         
            var currDate = DateTime.Today;
            var incomesObj = await this.incomeRepository.GetAllIncome(userId);
            var dailyObj = incomesObj.Where(s => s.UserId == userId && s.DateTime.Date == currDate).ToList();
            var objDto = new List<IncomeDayliDto>();

            foreach (var obj in dailyObj)
            {
                objDto.Add(this.mapper.Map<IncomeDayliDto>(obj));
            }
           
            return objDto;
        }
    }
}
