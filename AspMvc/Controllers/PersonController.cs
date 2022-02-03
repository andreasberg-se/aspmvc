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
            return View(_personService.GetList());
        }

        [HttpPost]
        public IActionResult Index(string searchString)
        {
            ViewBag.SearchString = searchString;
            return View(_personService.SearchAND(searchString));
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

        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.Message = "Failed to delete person!";
            if (_personService.Delete(id))
                ViewBag.Message = "The person was deleted successfully!";

            return View();
        }
    }

}
