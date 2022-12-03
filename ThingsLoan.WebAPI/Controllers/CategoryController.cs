using DB.DataAccess;
using DB.DTO;
using DB.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ThingsLoan.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork uow;

        public CategoryController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        [HttpGet]
        [Authorize(Roles = "Admin",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetCategories()
        {
            uow.CategoryRepository.GetAll();
            return Ok(uow.CategoryRepository.GetAll());
        }
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] CategoryDto category)
        {
            if (uow.CategoryRepository.CheckDuplicate(category.Desc))
                return BadRequest("Category already exists.");
            uow.CategoryRepository.Add(new Category
            {
                Desc = category.Desc,
                Id = 0,
                CreationDate= DateTime.UtcNow
            });
            uow.Complete();
            return StatusCode(201);
        }
    }
}
