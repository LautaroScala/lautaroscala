﻿using DB.DataAccess;
using DB.Entities;
using DB.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ThingsLoan.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly IUnitOfWork uow;

        public LoanController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllLoans()
        {
            return Ok(uow.LoanRepository.GetAll());
        }
        [HttpGet("person/{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult GetPersonLoans(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var allLoans = uow.LoanRepository.GetAllLoansById(id);
            if (allLoans.Count == 0)
            {
                return NotFound();
            }
            return Ok(allLoans);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public IActionResult MakeLoan([FromBody] LoanDto loaninfo)
        {
            if (loaninfo is null)
            {
                return BadRequest();
            }
            var PersonGet = uow.PersonRepository.GetEntById(loaninfo.PersonId);
            if (PersonGet is null)
            {
                return NotFound();
            }
            var ThingGet = uow.ThingsRepository.GetEntById(loaninfo.ThingId);
            if (ThingGet is null || !ThingGet.Available)
            {
                return NotFound();
            }
            var newloan = new Loan
            {
                LoanDate = DateTime.Now,
                PersonId = PersonGet.Id,
                Returned = false,
                ThingId = loaninfo.ThingId
            };
            ThingGet.Available = false;
            uow.LoanRepository.Add(newloan);
            uow.ThingsRepository.Update(ThingGet);
            uow.Complete();
            return StatusCode(201);
        }
        [HttpPost("return/{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult ReturnThing([FromBody] LoanDto loaninfo, int id)
        {
            if (loaninfo is null)
            {
                return BadRequest();
            }
            var thingGet = uow.ThingsRepository.GetEntById(loaninfo.ThingId);
            if (thingGet is null)
            {
                return NotFound();
            }
            else if (thingGet.Available)
            {
                return BadRequest();
            }
            var personGet = uow.PersonRepository.GetEntById(loaninfo.PersonId);
            if (personGet is null)
            {
                return NotFound();
            }
            var loanGet = uow.LoanRepository.GetEntById(id);
            if (loanGet is null)
            {
                return NotFound();
            }
            loanGet.Returned = true;
            loanGet.ReturnDate = DateTime.UtcNow;
            thingGet.Available = true;
            uow.LoanRepository.Update(loanGet);
            uow.ThingsRepository.Update(thingGet);
            uow.Complete();

            return Ok();
        }
    }
}
