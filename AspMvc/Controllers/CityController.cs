using System;
using Microsoft.AspNetCore.Mvc;
using AspMvc.Models;
using AspMvc.Models.ViewModels;
using AspMvc.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace AspMvc.Controllers
{
    [Authorize(Roles = "Admin, Moderator, User")]
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
        [Authorize(Roles = "Admin, Moderator")]
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

        [HttpGet("{controller}/{action}/{cityId}")]
        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult Edit(int cityId)
        {
            ViewData["CountryList"] = new SelectList(_aspMvcDbContext.Countries, "CountryId", "Name");
            var editCity = _aspMvcDbContext.Cities.Find(cityId);

            if (editCity != null)
            {
                EditCityViewModel editCityViewModel = new EditCityViewModel();
                editCityViewModel.CityId = editCity.CityId;
                editCityViewModel.Name = editCity.Name;
                editCityViewModel.CountryId = editCity.CountryId;
                return View(editCityViewModel);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult Edit(EditCityViewModel editCityViewModel)
        {
            ViewData["CountryList"] = new SelectList(_aspMvcDbContext.Countries, "CountryId", "Name");
            if ((!editCityViewModel.IsValidForm()) || ((!ModelState.IsValid)))
                return View(editCityViewModel);

            int cityId = editCityViewModel.CityId;
            var editCity = _aspMvcDbContext.Cities.Find(cityId);
            if (editCity != null)
            {
                editCity.Name = editCityViewModel.Name;
                editCity.CountryId = editCityViewModel.CountryId;
                _aspMvcDbContext.Cities.Update(editCity);
                _aspMvcDbContext.SaveChanges();
                return RedirectToAction("Index", "Person");
            }
            
            ViewData["Message"] = "Failed to update city!";
            return View(editCityViewModel);
        }

        [HttpGet("{controller}/{action}/{cityId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Show(int cityId)
        {
            var deleteCity = _aspMvcDbContext.Cities.FirstOrDefault(ci => ci.CityId == cityId);
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