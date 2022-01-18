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
        private readonly ICommonService _commonService;
        private readonly ILogger<BankController> _logger;
        public AccountController(ICustomerService customerService, ICommonService commonService, ILogger<BankController> logger)
        {
            _customerService = customerService;
            _commonService = commonService;
            _logger = logger;
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
    }
}