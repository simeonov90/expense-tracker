using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExpenseTracker.Data.Models.Dtos;
using ExpenseTracker.Infrastructure.Claims;
using ExpenseTracker.Repository.IRepository;
using ExpenseTracker.Services.Contacts;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
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
            if (incomeCreateDto == null)
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
    }
}
