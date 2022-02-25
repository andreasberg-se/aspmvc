using Microsoft.AspNetCore.Mvc;
using AspMvc.Models;
using AspMvc.Models.ViewModels;
using AspMvc.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace AspMvc.Controllers
{
    [Authorize(Roles = "Admin, Moderator, User")]
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
        [Authorize(Roles = "Admin, Moderator")]
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

        [HttpGet("{controller}/{action}/{languageId}")]
        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult Edit(int languageId)
        {
            var editLanguage = _aspMvcDbContext.Languages.Find(languageId);

            if (editLanguage != null)
            {
                EditLanguageViewModel editLanguageViewModel = new EditLanguageViewModel();
                editLanguageViewModel.LanguageId = editLanguage.LanguageId;
                editLanguageViewModel.LanguageName = editLanguage.LanguageName;
                return View(editLanguageViewModel);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult Edit(EditLanguageViewModel editLanguageViewModel)
        {
            if ((!editLanguageViewModel.IsValidForm()) || ((!ModelState.IsValid)))
                return View(editLanguageViewModel);

            int languageId = editLanguageViewModel.LanguageId;
            var editLanguage = _aspMvcDbContext.Languages.Find(languageId);
            if (editLanguage != null)
            {
                editLanguage.LanguageName = editLanguageViewModel.LanguageName;
                _aspMvcDbContext.Languages.Update(editLanguage);
                _aspMvcDbContext.SaveChanges();
                return RedirectToAction("Index", "Person");
            }
            
            ViewData["Message"] = "Failed to update language!";
            return View(editLanguageViewModel);
        }

        [HttpGet("{controller}/{action}/{languageId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Show(int languageId)
        {
            var deleteLanguage = _aspMvcDbContext.Languages.FirstOrDefault(l => l.LanguageId == languageId);
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