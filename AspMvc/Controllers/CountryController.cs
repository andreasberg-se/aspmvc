using System;
using Microsoft.AspNetCore.Mvc;
using AspMvc.Models;
using AspMvc.Models.ViewModels;
using AspMvc.Data;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace AspMvc.Controllers
{
    [Authorize(Roles = "Admin, Moderator, User")]
    public class CountryController : Controller
    {
        private readonly AspMvcDbContext _aspMvcDbContext;

        public CountryController(AspMvcDbContext aspMvcDbContext)
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
        public IActionResult Index(CountryViewModel countryViewModel)
        {
            if (!countryViewModel.IsValidForm())
                return View(countryViewModel);

            if (ModelState.IsValid)
            {
                Country country = new Country();
                country.Name = countryViewModel.Name;
                
                _aspMvcDbContext.Countries.Add(country);
                _aspMvcDbContext.SaveChanges();
                return RedirectToAction(nameof(Index), "Person");
            }
            return View(countryViewModel);
        }

        [HttpGet]
        public IActionResult Show()
        {
            return View(_aspMvcDbContext.Countries.ToList());
        }

        [HttpGet("{controller}/{action}/{countryId}")]
        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult Edit(int countryId)
        {
            var editCountry = _aspMvcDbContext.Countries.Find(countryId);

            if (editCountry != null)
            {
                EditCountryViewModel editCountryViewModel = new EditCountryViewModel();
                editCountryViewModel.CountryId = editCountry.CountryId;
                editCountryViewModel.Name = editCountry.Name;
                return View(editCountryViewModel);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult Edit(EditCountryViewModel editCountryViewModel)
        {
            if ((!editCountryViewModel.IsValidForm()) || ((!ModelState.IsValid)))
                return View(editCountryViewModel);

            int countryId = editCountryViewModel.CountryId;
            var editCountry = _aspMvcDbContext.Countries.Find(countryId);
            if (editCountry != null)
            {
                editCountry.Name = editCountryViewModel.Name;
                _aspMvcDbContext.Countries.Update(editCountry);
                _aspMvcDbContext.SaveChanges();
                return RedirectToAction("Index", "Person");
            }
            
            ViewData["Message"] = "Failed to update country!";
            return View(editCountryViewModel);
        }

        [HttpGet("{controller}/{action}/{countryId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Show(int countryId)
        {
            var deleteCountry = _aspMvcDbContext.Countries.FirstOrDefault(co => co.CountryId == countryId);
            if (deleteCountry == null)
            {
                ViewData["Message"] = "Failed to delete country (not found)!";
                return View(_aspMvcDbContext.Countries.ToList());
            }

            try
            {
                _aspMvcDbContext.Countries.Remove(deleteCountry);
                _aspMvcDbContext.SaveChanges();
                return RedirectToAction(nameof(Index), "Person");
            }
            catch
            {
                ViewData["Message"] = $"Failed to delete {deleteCountry.Name}!";
            }
            return View(_aspMvcDbContext.Countries.ToList());
        }
    }
}