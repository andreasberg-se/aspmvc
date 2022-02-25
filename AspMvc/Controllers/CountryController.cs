using System;
using Microsoft.AspNetCore.Mvc;
using AspMvc.Models;
using AspMvc.Models.ViewModels;
using AspMvc.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AspMvc.Controllers
{
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

        [HttpGet("{controller}/{action}/{id}")]
        public IActionResult Show(int id)
        {
            var deleteCountry = _aspMvcDbContext.Countries.FirstOrDefault(co => co.CountryId == id);
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