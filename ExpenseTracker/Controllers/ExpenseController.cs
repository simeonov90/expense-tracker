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
using ExpenseTracker.Services.Contacts;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
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


        public IActionResult CreateExpense()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateExpense([FromBody]ExpenseCreateDto expenseCreateDto)
        {
            if (expenseCreateDto == null)
            {
                return BadRequest(ModelState);
            }

            
            if (!this.expenseService.CreateExpense(expenseCreateDto, this.User.GetUserId()))
            {
                ModelState.AddModelError("", "Something went wrong with saving date.");
                return StatusCode(500, ModelState);
            }

            return Json("Ok");
        }

        [HttpGet("{expenseId:int}", Name = "GetExpense")]
        public IActionResult GetExpense(int expenseId)
        {
            var obj = this.expenseRepository.GetExpense(expenseId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = this.mapper.Map<ExpenseDto>(obj);

            return Ok(objDto);
        }

        public IActionResult GetAllExpenses()
        {
            var objList = this.expenseRepository.GetAllExpenses();
            var objDto = new List<ExpenseDto>();
            foreach (var obj in objList)
            {
                objDto.Add(this.mapper.Map<ExpenseDto>(obj));
            }

            return Ok(objDto);
        }

        [HttpDelete("{expenseId:int}", Name = "DeleteExpense")]
        public IActionResult DeleteExpense(int expenseId)
        {
            if (!this.expenseRepository.ExpenseExists(expenseId))
            {
                return NotFound();
            }

            var expenseObj = this.expenseRepository.GetExpense(expenseId);
            if (!this.expenseRepository.DeleteExpense(expenseObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {expenseObj.ExpenseFrom}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        public IActionResult AllExpense()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(this.expenseService.GetAllExpenses(userId));
        }
    }
}
