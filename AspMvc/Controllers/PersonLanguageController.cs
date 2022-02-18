using Microsoft.AspNetCore.Mvc;
using AspMvc.Models;
using AspMvc.Models.ViewModels;
using AspMvc.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AspMvc.Controllers
{

    public class PersonLanguageController : Controller
    {
        private readonly AspMvcDbContext _aspMvcDbContext;

        public PersonLanguageController(AspMvcDbContext aspMvcDbContext)
        {
            _aspMvcDbContext = aspMvcDbContext;
        }

        public IActionResult Index()
        {
            ViewData["PeopleList"] = new SelectList(_aspMvcDbContext.People, "PersonId", "FirstName");
            ViewData["LanguageList"] = new SelectList(_aspMvcDbContext.Languages, "LanguageId", "LanguageName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(PersonLanguage personLanguage)
        {
            ViewData["PeopleList"] = new SelectList(_aspMvcDbContext.People, "PersonId", "FirstName");
            ViewData["LanguageList"] = new SelectList(_aspMvcDbContext.Languages, "LanguageId", "LanguageName");
            if (ModelState.IsValid)
            {
                try
                {
                    _aspMvcDbContext.PersonLanguages.Add(personLanguage);
                    _aspMvcDbContext.SaveChanges();
                    return RedirectToAction(nameof(Index), "Person");
                }
                catch
                {
                    ViewData["Message"] = "Failed to add language to person!";
                }
            }
            return View(personLanguage);
        }

        [HttpGet]
        public IActionResult Show()
        {
            PersonLanguageViewModel personLanguageViewModel = new PersonLanguageViewModel();
            personLanguageViewModel.People = _aspMvcDbContext.People.ToList();
            personLanguageViewModel.Languages = _aspMvcDbContext.Languages.ToList();
            personLanguageViewModel.PersonLanguages = _aspMvcDbContext.PersonLanguages.ToList();
            return View(personLanguageViewModel);
        }

        [HttpGet("{controller}/{action}/{pid}/{lid}")]
        public IActionResult Show(int pid, int lid)
        {
            PersonLanguageViewModel personLanguageViewModel = new PersonLanguageViewModel();
            personLanguageViewModel.People = _aspMvcDbContext.People.ToList();
            personLanguageViewModel.Languages = _aspMvcDbContext.Languages.ToList();
            personLanguageViewModel.PersonLanguages = _aspMvcDbContext.PersonLanguages.ToList();

            var deletePersonLanguage = _aspMvcDbContext.PersonLanguages.FirstOrDefault(pl => pl.PersonId == pid && pl.LanguageId == lid);
            if (deletePersonLanguage == null)
            {
                ViewData["Message"] = "Failed to delete spoken language (not found)!";
                return View(personLanguageViewModel);
            }

            try
            {
                _aspMvcDbContext.PersonLanguages.Remove(deletePersonLanguage);
                _aspMvcDbContext.SaveChanges();
                return RedirectToAction(nameof(Index), "Person");
            }
            catch
            {
                ViewData["Message"] = "Failed to delete spoken language!";
            }
            return View(personLanguageViewModel);
        }
    }

}