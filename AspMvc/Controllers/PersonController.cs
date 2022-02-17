using System;
using Microsoft.AspNetCore.Mvc;
using AspMvc.Models;
using AspMvc.Models.ViewModels;
using AspMvc.Models.Services;
using AspMvc.Data;
using System.Linq;

namespace AspMvc.Controllers
{
    public class PersonController : Controller
    {
        private readonly AspMvcDbContext _aspMvcDbContext;

        public PersonController(AspMvcDbContext aspMvcDbContext)
        {
            _aspMvcDbContext = aspMvcDbContext;
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
                Person person = new Person();
                person.FirstName = createPersonViewModel.FirstName;
                person.LastName = createPersonViewModel.LastName;
                person.City = createPersonViewModel.City;
                person.Phone = createPersonViewModel.Phone;

                _aspMvcDbContext.People.Add(person);
                _aspMvcDbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(createPersonViewModel);
        }

        [HttpPost]
        public IActionResult Search(string searchString)
        {
            var result = from person in _aspMvcDbContext.People
                where person.FirstName.Contains(searchString.Trim())
                    || person.LastName.Contains(searchString.Trim())
                    || person.City.Contains(searchString.Trim()) 
                select person;

            try
            {
                return PartialView("~/Views/Person/Shared/_ListPartial.cshtml", result.ToList());
            }
            catch
            {
                return PartialView("~/Views/Person/Shared/_ListPartial.cshtml", _aspMvcDbContext.People.ToList());
            }    
        }

        [HttpGet]
        public IActionResult ListPeople()
        {
            return PartialView("~/Views/Person/Shared/_ListPartial.cshtml", _aspMvcDbContext.People.ToList());
        }

        [HttpGet]
        public IActionResult ShowPerson(int personId)
        {
            return PartialView("~/Views/Person/Shared/_DetailPartial.cshtml", _aspMvcDbContext.People.Find(personId));
        }

        [HttpGet]
        public IActionResult DeletePerson(int personId)
        {
            var deletePerson = _aspMvcDbContext.People.Find(personId);

            ViewBag.Message = "Failed to delete person!";
            if (deletePerson != null)
            {
                _aspMvcDbContext.People.Remove(deletePerson);
                _aspMvcDbContext.SaveChanges();
                ViewBag.Message = "The person was deleted successfully!";
            }     

            return PartialView("~/Views/Person/Shared/_DeletePartial.cshtml");
        }
    }

}
