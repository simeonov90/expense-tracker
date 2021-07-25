using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class ExpenseController : Controller
    {
        private readonly IExpenseRepository expenseRepository;
        private readonly IMapper mapper;
        private readonly IExpenseService expenseService;

        public ExpenseController(IExpenseRepository expenseRepository, IMapper mapper, IExpenseService expenseService)
        {
            this.expenseRepository = expenseRepository;
            this.mapper = mapper;
            this.expenseService = expenseService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody]ExpenseCreateDto expenseCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (!await this.expenseService.CreateExpense(expenseCreateDto, this.User.GetUserId()))
            {
                ModelState.AddModelError("", "Something went wrong with saving date.");
                return StatusCode(500, ModelState);
            }

            return Json("Ok");
        }

        [HttpGet("{expenseId:int}", Name = "GetExpense")]
        public async Task<IActionResult> GetExpense(int expenseId)
        {
            var obj = await this.expenseRepository.GetExpense(expenseId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = this.mapper.Map<ExpenseDto>(obj);

            return Ok(objDto);
        }

        public async Task<IActionResult> GetAllExpenses()
        {
            var objList = await this.expenseRepository.GetAllExpenses(this.User.GetUserId());
            var objDto = new List<ExpenseDto>();
            foreach (var obj in objList)
            {
                objDto.Add(this.mapper.Map<ExpenseDto>(obj));
            }

            return Ok(objDto);
        }

        public async Task<IActionResult> GetDailyExpenses()
        {
            return Ok(await this.expenseService.DailyExpenses(this.User.GetUserId()));
        }

        [HttpDelete("{expenseId:int}", Name = "DeleteExpense")]
        public async Task<IActionResult> DeleteExpense(int expenseId)
        {
            if (!await this.expenseRepository.ExpenseExists(expenseId))
            {
                return NotFound();
            }

            var expenseObj = await this.expenseRepository.GetExpense(expenseId);
            if (!await this.expenseRepository.DeleteExpense(expenseObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {expenseObj.From}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        public async Task<IActionResult> SumFromExpenses()
        {
            return Ok(await this.expenseService.SumFromExpenses(this.User.GetUserId()));
        }
    }
}
