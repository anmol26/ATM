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
    public class BankController : Controller
    {
        private readonly ICommonService _commonService;
        private readonly ILogger<BankController> _logger;
        public BankController(ICommonService commonService, ILogger<BankController> logger)
        {
            _commonService = commonService;
            _logger = logger;
        }
        [HttpGet("{bankId}")]
        public IActionResult GetBankById(string bankId)
        {
            try
            {
                _logger.Log(LogLevel.Information, message: $"Fetching Bank data");
                return Ok(_commonService.FindBank(bankId));
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, message: ex.Message);
                return NotFound(ex.Message);
            }
        }
    }
}