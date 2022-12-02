using Microsoft.AspNetCore.Mvc;
using ThingsLoan.API.Classes;
using ThingsLoan.WebAPI.DataAccess;

namespace ThingsLoan.API.Controllers
{
    public class ThingsController : Controller
    {
        public IUnitOfWork uow;
        public ThingsController(IUnitOfWork uow) 
        {
            this.uow = uow;
        }
        public IActionResult Index(string search)
        {
            var dbThings = uow.ThingsRepository.GetAll();
            var viewmodel = dbThings.ToViewModels();
            return View(viewmodel);
        }
    }
}
