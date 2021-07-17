using ExpenseTracker.Infrastructure.Claims;
using ExpenseTracker.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ExpenseTracker.Controllers
{
    [Authorize]
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

        public async Task<IActionResult> GetDaily()
        {
            return Ok(await this.historyService.GetDaily(this.User.GetUserId()));
        }
    }
}
