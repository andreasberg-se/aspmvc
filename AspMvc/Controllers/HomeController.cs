using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspMvc.Models.Repositories;

namespace AspMvc.Controllers
{

    public class HomeController : Controller
    {
        private readonly IPersonRepository personRepository;

        public HomeController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View(this.personRepository.GetPersonById(1));
        }

        public IActionResult Contact()
        {
            return View(this.personRepository.GetPersonById(1));
        }

        public IActionResult Projects()
        {
            return View(this.personRepository.GetPersonById(1));
        }
    }

}
