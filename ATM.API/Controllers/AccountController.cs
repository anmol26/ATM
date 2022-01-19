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
        private readonly IStaffService _staffService;
        private readonly ICommonService _commonService;
        private readonly ILogger<AccountController> _logger;
        public AccountController(ICommonService commonService, ILogger<AccountController> logger, IStaffService staffService)
        {
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

        [HttpDelete("{accountId}")]
        public IActionResult DeleteAccountById(string accountId)
        {
            try
            {
                _staffService.DeleteAccount(accountId);
                _logger.Log(LogLevel.Information, message: "Account Deleted Sucessfully");
                return Ok("Account Deleted Sucessfully");
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, message: ex.Message);
                return NotFound(ex.Message);
            }
        }
    }
}