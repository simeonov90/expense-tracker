using ExpenseTracker.Data.Models.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Services.Contracts
{
    public interface IHistoryService
    {
        Task<ICollection<HistoryAllDto>> GetAll(string userId);
        Task<ICollection<HistoryAllDto>> GetAll(string userId, int page, int itemsPerPage);
        Task<ICollection<HistoryDailyDto>> GetDaily(string userId);
        Task<ICollection<HistoryByDateDto>> GetByDate(string userId, DateTime from, DateTime to);
        Task<ICollection<HistoryByDateDto>> GetByDate(string userId, DateTime from, DateTime to, int page, int itemsPerPage);
    }
}
