using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseTracker.Data.Models;
using ExpenseTracker.Data.Models.Dtos;
using ExpenseTracker.Infrastructure.Claims;
using ExpenseTracker.Repository.IRepository;
using ExpenseTracker.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    [Authorize]
    public class IncomeController : Controller
    {
        private readonly IIncomeRepository incomeRepository;
        private readonly IIncomeService incomeService;
        private readonly IMapper mapper;

        public IncomeController(IIncomeRepository incomeRepository, IIncomeService incomeService, IMapper mapper)
        {
            this.incomeRepository = incomeRepository;
            this.incomeService = incomeService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncome([FromBody]IncomeCreateDto incomeCreateDto)
        {
           if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await this.incomeService.CreateIncome(incomeCreateDto, this.User.GetUserId()))
            {
                ModelState.AddModelError("", "Something went wrong with saving date.");
                return StatusCode(500, ModelState);
            }

            return Json("Ok");
        }

        [HttpGet("{incomeId:int}", Name = "GetIncome")]
        public async Task<IActionResult> GetIncome(int incomeId)
        {
            var obj = await this.incomeRepository.GetIncome(incomeId);
            if (obj == null)
            {
                return NotFound();
            }

            var objDto = this.mapper.Map<IncomeDto>(obj);
            return Ok(objDto);
        }

        [HttpGet("[controller]/[action]/{page}/{itemsPerPage}")]
        public async Task<IActionResult> GetAllIncomes(int page, int itemsPerPage)
        {
            return Ok(await this.incomeService.AllIncomes(this.User.GetUserId(), page, itemsPerPage));
        }

        public async Task<IActionResult> GetDailyIncomes()
        {
            return Ok(await this.incomeService.DailyIncomes(this.User.GetUserId()));
        }

        [HttpDelete("{incomeId:int}", Name = "DeleteIncome")]
        public async Task<IActionResult> DeleteIncome(int incomeId)
        {
            if (!await this.incomeRepository.IncomeExists(incomeId))
            {
                NotFound();
            }

            var incomeObj = await this.incomeRepository.GetIncome(incomeId);
            if (!await this.incomeRepository.DeleteIncome(incomeObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {incomeObj.From}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        public async Task<IActionResult> SumFromIncomes()
        {
            return Ok(await this.incomeService.SumFromIncomes(this.User.GetUserId()));
        }
    }
}
