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
    }
}