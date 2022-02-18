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
    public class CityController : Controller
    {
        private readonly AspMvcDbContext _aspMvcDbContext;

        public CityController(AspMvcDbContext aspMvcDbContext)
        {
            _aspMvcDbContext = aspMvcDbContext;
        }

        public IActionResult Index()
        {
            ViewData["CountryList"] = new SelectList(_aspMvcDbContext.Countries, "CountryId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CityViewModel cityViewModel)
        {
            ViewData["CountryList"] = new SelectList(_aspMvcDbContext.Countries, "CountryId", "Name");

            if (!cityViewModel.IsValidForm())
                return View(cityViewModel);

            if (ModelState.IsValid)
            {
                City city = new City();
                city.Name = cityViewModel.Name;
                city.CountryId = cityViewModel.CountryId;
                
                _aspMvcDbContext.Cities.Add(city);
                _aspMvcDbContext.SaveChanges();
                return RedirectToAction(nameof(Index), "Person");
            }
            return View(cityViewModel);
        }
    }
}