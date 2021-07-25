using ExpenseTracker.Infrastructure.Claims;
using ExpenseTracker.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet("[controller]/[action]/{page}/{itemsPerPage}")]
        public async Task<IActionResult> GetAll(int page, int itemsPerPage)
        {
            return Ok(await this.historyService.GetAll(this.User.GetUserId(), page, itemsPerPage));
        }

        [HttpGet("[controller]/[action]/{from}/{to}")]
        public async Task<IActionResult> GetByDate(DateTime from, DateTime to)
        {
            return Ok(await this.historyService.GetByDate(this.User.GetUserId(), from, to));
        }

        [HttpGet("[controller]/[action]/{from}/{to}/{page}/{itemsPerPage}")]
        public async Task<IActionResult> GetByDate(DateTime from, DateTime to, int page, int itemsPerPage)
        {
            return Ok(await this.historyService.GetByDate(this.User.GetUserId(), from, to, page, itemsPerPage));
        }

        public async Task<IActionResult> GetDaily()
        {
            return Ok(await this.historyService.GetDaily(this.User.GetUserId()));
        }
    }
}
