using Microsoft.AspNetCore.Mvc;
using AspMvc.Models;
using AspMvc.Models.ViewModels;
using AspMvc.Models.Services;

namespace AspMvc.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController()
        {
            _personService = new PersonService();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatePersonViewModel createPersonViewModel)
        {
            if (!createPersonViewModel.IsValidForm())
                return View(createPersonViewModel);

            if (ModelState.IsValid)
            {
                Person person = _personService.Add(createPersonViewModel);
                if (person != null)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError("Error", "Failed to add person!");
            }
            return View(createPersonViewModel);
        }

        [HttpPost]
        public IActionResult Search(string searchString)
        {
            ViewData["s"] = searchString;
            return PartialView("~/Views/Person/Shared/_ListPartial.cshtml", _personService.SearchAND(searchString));
        }

        [HttpGet]
        public IActionResult ListPeople()
        {
            return PartialView("~/Views/Person/Shared/_ListPartial.cshtml", _personService.GetList());
        }

        [HttpGet]
        public IActionResult ShowPerson(int personId)
        {
            return PartialView("~/Views/Person/Shared/_DetailPartial.cshtml", _personService.GetById(personId));
        }

        [HttpGet]
        public IActionResult DeletePerson(int personId)
        {
            ViewBag.Message = "Failed to delete person!";
            if (_personService.Delete(personId))
                ViewBag.Message = "The person was deleted successfully!";

            return PartialView("~/Views/Person/Shared/_DeletePartial.cshtml");
        }
    }

}
