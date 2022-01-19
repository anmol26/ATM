using ATM.API.Models;
using ATM.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IStaffService _staffService;
        private readonly ICommonService _commonService;
        private readonly ILogger<BankController> _logger;
        public AccountController(ICustomerService customerService, ICommonService commonService, ILogger<BankController> logger, IStaffService staffService)
        {
            _customerService = customerService;
            _commonService = commonService;
            _logger = logger;
            _staffService = staffService;
        }
        [HttpGet("{accountId}")]
        public IActionResult GetAccountById(string accountId)
        {
            try
            {
                _logger.Log(LogLevel.Information, message: $"Fetching Account data");
                return Ok(_commonService.FindAccount(accountId));
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, message: ex.Message);
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult CreateAccount(NewAccount newAccount)
        {
            string AccountId = _staffService.CreateAccount(newAccount.BankId, newAccount.Name, newAccount.Password, newAccount.PhoneNumber, newAccount.Gender, 2);
            _logger.Log(LogLevel.Information, message: "New Account created Successfully");
            return Created($"{Request.Path}/{AccountId}", newAccount);
        }
    }
}