﻿using AutoMapper;
using ExpenseTracker.Data.Models;
using ExpenseTracker.Data.Models.Dtos;
using ExpenseTracker.Repository.IRepository;
using ExpenseTracker.Services.Contacts;
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
    }
}