using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseTracker.Data.Models;
using ExpenseTracker.Data.Models.Dtos;
using ExpenseTracker.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseRepository expenseRepository;
        private readonly IMapper mapper;
        public ExpenseController(IExpenseRepository expenseRepository, IMapper mapper)
        {
            this.expenseRepository = expenseRepository;
            this.mapper = mapper;
        }

        
        public IActionResult CreateExpense()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateExpense(ExpenseCreateDto expenseCreateDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier).ToString();
            
            if (expenseCreateDto == null)
            {
                return BadRequest(ModelState);
            }

            var expenseObj = this.mapper.Map<Expense>(expenseCreateDto);
            expenseObj.UserId = userId;
            if (!this.expenseRepository.CreateExpense(expenseObj))
            {
                ModelState.AddModelError("", "Something went wrong with saving date.");
                return StatusCode(500, ModelState);
            }
            return View();
        }

        [HttpGet("{expenseId:int}")]
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
    }
}
