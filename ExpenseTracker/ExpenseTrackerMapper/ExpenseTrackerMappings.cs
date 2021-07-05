using AutoMapper;
using ExpenseTracker.Data.Models;
using ExpenseTracker.Data.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.ExpenseTrackerMapper
{
    public class ExpenseTrackerMappings : Profile
    {
        public ExpenseTrackerMappings()
        {
            CreateMap<Expense, ExpenseCreateDto>().ReverseMap();
            CreateMap<Expense, ExpenseDto>().ReverseMap();
            CreateMap<Income, IncomeCreateDto>().ReverseMap();
            CreateMap<Income, IncomeDto>().ReverseMap();
            CreateMap<Income, IncomeDayliDto>().ReverseMap();
        }
    }
}
