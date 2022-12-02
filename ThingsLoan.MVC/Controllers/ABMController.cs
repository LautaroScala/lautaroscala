using Microsoft.AspNetCore.Mvc;
using ThingsLoan.MVC.ExtMethods;
using ThingsLoan.MVC.Models;
using ThingsLoan.WebAPI.DataAccess;

namespace ThingsLoan.MVC.Controllers
{
    public class ABMController : Controller
    {
        private readonly IUnitOfWork uow;

        public ABMController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        public IActionResult Index(string search)
        {
            var dbThings = uow.ThingsRepository.GetAll()
                .Where(x => x.Desc == search)
                .ToList();
            var viewmodels = dbThings.ToViewModels();
            return View(viewmodels);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ABMViewModel abmViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", abmViewModel);
            }
            var list = uow.ThingsRepository.GetThingsByDesc(abmViewModel.Desc);
            if (list is null)
            {
                var entity = abmViewModel.ToEntity();
                uow.ThingsRepository.Add(entity);
                uow.Complete();
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(String.Empty, "Ya existe una cosa con esa descripcion");
            return View(abmViewModel);
        }
    }
}
