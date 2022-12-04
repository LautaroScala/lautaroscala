using DB.DataAccess;
using DB.DTO;
using DB.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ThingsLoan.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThingsController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        public ThingsController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetListOfThings()
        {
            var dbThings = uow.ThingsRepository.GetAll();
            return Ok(dbThings);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateNewThing([FromBody] ThingDto thing)
        {
            var dbThings = uow.ThingsRepository.Add(
                new Things
                {
                    Id = 0,
                    Desc = thing.Desc,
                    CategoryId = thing.Category.Id,
                    CreationDate = DateTime.UtcNow,
                    Available = true

                });
            uow.Complete();
            return StatusCode(201);
        }
    }
}
