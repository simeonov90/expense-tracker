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
            CreateMap<Expense, ExpenseDailyDto>().ReverseMap();
            CreateMap<Expense, HistoryAllDto>().ReverseMap();
            CreateMap<Expense, HistoryDailyDto>().ReverseMap();
            CreateMap<Expense, HistoryByDateDto>().ReverseMap();
            CreateMap<Income, IncomeCreateDto>().ReverseMap();
            CreateMap<Income, IncomeDto>().ReverseMap();
            CreateMap<Income, IncomeDailyDto>().ReverseMap();
            CreateMap<Income, IncomeAllDto>().ReverseMap();
            CreateMap<Income, HistoryAllDto>().ReverseMap();
            CreateMap<Income, HistoryDailyDto>().ReverseMap();
            CreateMap<Income, HistoryByDateDto>().ReverseMap();
        }
    }
}
