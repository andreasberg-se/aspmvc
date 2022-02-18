using Microsoft.AspNetCore.Mvc;
using AspMvc.Models;
using AspMvc.Models.ViewModels;
using AspMvc.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AspMvc.Controllers
{

    public class LanguageController : Controller
    {
        private readonly AspMvcDbContext _aspMvcDbContext;

        public LanguageController(AspMvcDbContext aspMvcDbContext)
        {
            _aspMvcDbContext = aspMvcDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LanguageViewModel languageViewModel)
        {
            if (!languageViewModel.IsValidForm())
                return View(languageViewModel);

            if (ModelState.IsValid)
            {
                Language language = new Language();
                language.LanguageName = languageViewModel.LanguageName;
                
                _aspMvcDbContext.Languages.Add(language);
                _aspMvcDbContext.SaveChanges();
                return RedirectToAction(nameof(Index), "Person");
            }
            return View(languageViewModel);
        }

        [HttpGet]
        public IActionResult Show()
        {
            return View(_aspMvcDbContext.Languages.ToList());
        }

        [HttpGet("{controller}/{action}/{id}")]
        public IActionResult Show(int id)
        {
            var deleteLanguage = _aspMvcDbContext.Languages.FirstOrDefault(l => l.LanguageId == id);
            if (deleteLanguage == null)
            {
                ViewData["Message"] = "Failed to delete language (not found)!";
                return View(_aspMvcDbContext.Languages.ToList());
            }

            try
            {
                _aspMvcDbContext.Languages.Remove(deleteLanguage);
                _aspMvcDbContext.SaveChanges();
                return RedirectToAction(nameof(Index), "Person");
            }
            catch
            {
                ViewData["Message"] = $"Failed to delete {deleteLanguage.LanguageName}!";
            }
            return View(_aspMvcDbContext.Languages.ToList());
        }
    }
    
}