using ExpenseTracker.Data.Models.Dtos;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Services.Contracts
{
    public interface IHistoryService
    {
        Task<ICollection<HistoryAllDto>> GetAll(string userId);
        Task<ICollection<HistoryDailyDto>> GetDaily(string userId);
    }
}
