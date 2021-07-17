using ExpenseTracker.Infrastructure.Claims;
using ExpenseTracker.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExpenseTracker.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IHistoryService historyService;

        public HistoryController(IHistoryService historyService)
        {
            this.historyService = historyService;
        }

        public async Task<IActionResult> GetAll()
        {
            return Ok(await this.historyService.GetAll(this.User.GetUserId()));
        }
    }
}
