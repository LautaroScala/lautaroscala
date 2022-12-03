using Microsoft.AspNetCore.Mvc;
using ThingsLoan.MVC.ExtMethods;
using ThingsLoan.MVC.Models;
using DB.DataAccess;
using DB.Entities;

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
            var dbThings = uow.ThingsRepository.FilterByDesc(search);
            var viewmodels = dbThings.ToViewModels();
            return View(viewmodels);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thing = uow.ThingsRepository.GetEntById(id.Value);
            if (thing == null)
            {
                return NotFound();
            }
            var category = uow.CategoryRepository.GetEntById(thing.CategoryId);

            var cosa = thing.ToViewModel();
            cosa.Categories = category.Desc;

            return View(cosa);
        }
        public IActionResult Create()
        {
            var list = uow.CategoryRepository.GetAll();

            ViewBag.CategoryList = list;
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
                var catExist = uow.CategoryRepository.GetByDescription(abmViewModel.Categories);
                if (catExist is null)
                {
                    ModelState.AddModelError(String.Empty, "Esa categoria es inexistente");
                    return RedirectToAction(nameof(Index));
                }
                var entity = abmViewModel.ToEntity();
                uow.ThingsRepository.Add(new Things
                {
                    Id = 0,
                    Desc = abmViewModel.Desc,
                    CategoryId = catExist.Id,
                    CreationDate= DateTime.UtcNow,
                    Available = true
                });
                uow.Complete();
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(String.Empty, "Ya existe una cosa con esa descripcion");
            return View(abmViewModel);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thing = uow.ThingsRepository.GetEntById(id.Value);
            if (thing == null)
            {
                return NotFound();
            }
            var category = uow.CategoryRepository.GetEntById(thing.CategoryId);

            var cosa = thing.ToViewModel();
            cosa.Categories = category.Desc;
            var list = uow.CategoryRepository.GetAll();

            ViewBag.CategoryList = list;
            return View(cosa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ABMViewModel abmViewModel)
        {
            if (id != abmViewModel.Id)
            {
                return NotFound();
            }
            var categoryCkeck = uow.CategoryRepository.CheckDuplicate(abmViewModel.Categories);
            if (!categoryCkeck)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                uow.ThingsRepository.Update(abmViewModel.ToEntity());

                return RedirectToAction(nameof(Index));
            }
            return View(abmViewModel);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var thing = uow.ThingsRepository.GetEntById(id.Value);
            if (thing == null)
            {
                return NotFound();
            }
            var category = uow.CategoryRepository.GetEntById(thing.CategoryId);

            var cosa = thing.ToViewModel();
            cosa.Categories = category.Desc;
            return View(cosa);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thing = uow.ThingsRepository.GetEntById(id);
            if (thing is null)
            {
                return NotFound();
            }

            uow.ThingsRepository.Delete(thing.Id);
            uow.Complete();
            return RedirectToAction(nameof(Index));
        }
    }
}
