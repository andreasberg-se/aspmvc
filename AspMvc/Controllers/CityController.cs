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

        [HttpGet]
        public IActionResult Show()
        {
            return View(_aspMvcDbContext.Cities.ToList());
        }

        [HttpGet("{controller}/{action}/{id}")]
        public IActionResult Show(int id)
        {
            var deleteCity = _aspMvcDbContext.Cities.FirstOrDefault(ci => ci.CityId == id);
            if (deleteCity == null)
            {
                ViewData["Message"] = "Failed to delete city (not found)!";
                return View(_aspMvcDbContext.Cities.ToList());
            }

            try
            {
                _aspMvcDbContext.Cities.Remove(deleteCity);
                _aspMvcDbContext.SaveChanges();
                return RedirectToAction(nameof(Index), "Person");
            }
            catch
            {
                ViewData["Message"] = $"Failed to delete {deleteCity.Name}!";
            }
            return View(_aspMvcDbContext.Cities.ToList());
        }
    }
}