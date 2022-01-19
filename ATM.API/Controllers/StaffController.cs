﻿using ATM.API.Models;
using ATM.Models;
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
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;
        private readonly ICommonService _commonService;
        private readonly ILogger<StaffController> _logger;
        public StaffController(IStaffService staffService, ICommonService commonService, ILogger<StaffController> logger)
        {
            _staffService = staffService;
            _logger = logger;
            _commonService = commonService;
        }

        [HttpGet("{id}/{pass}")]
        public IActionResult GetStaffByIdPass(string id, string pass)
        {
            try
            {
                _logger.Log(LogLevel.Information, message: $"Fetching Staff data");
                return Ok(_commonService.UserLogin(id, pass, "1"));
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, message: ex.Message);
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult CreateStaff(NewAccount newAccount)
        {
            //(string bankId, string name, string password, long phoneNumber, string gender, int choice)
            string staffId = _staffService.CreateAccount(newAccount.BankId,newAccount.Name,newAccount.Password,newAccount.PhoneNumber,newAccount.Gender,1);
            _logger.Log(LogLevel.Information, message: "New Staff created Successfully");
            return Created($"{Request.Path}/{staffId}", newAccount);
        }

        //[HttpPost]
        //public IActionResult CreateAccount(string bankId, string name, string password, long phoneNumber, string gender, int choice)
        //{
        //    string id= _staffService.CreateAccount(bankId, name, password, phoneNumber, gender, choice);
        //    _logger.Log(LogLevel.Information, message: "New Account created Successfully");
        //    return Created($"{Request.Path}/id/{id}",bankId);

        //}
    }
}