using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AspMvc.Models;

namespace AspMvc.Controllers
{

    public class GuessingGameController : Controller
    {
        private const string SessionKeyNumber = "Number";
        private const string SessionKeyGuesses = "Guesses";
        private const string SessionKeyGuessedNumber = "GuessedNumber";

        private void NewGame()
        {
            Random random = new Random();
            HttpContext.Session.SetInt32(SessionKeyNumber, random.Next(100) + 1);
            HttpContext.Session.SetInt32(SessionKeyGuesses, 0);
            HttpContext.Session.SetInt32(SessionKeyGuessedNumber, 1);
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32(SessionKeyNumber) == null)
            {
                NewGame();
                ViewBag.Message = "--- New game ---";
            }
            else
                ViewBag.Message = "--- Resuming game ---";

            ViewBag.GuessedNumber = (int) HttpContext.Session.GetInt32(SessionKeyGuessedNumber);
            ViewBag.Guesses = (int) HttpContext.Session.GetInt32(SessionKeyGuesses);
            return View();
        }

        [HttpPost]
        public IActionResult Index(int guessedNumber)
        {
            if (HttpContext.Session.GetInt32(SessionKeyNumber) == null)
                NewGame();

            int correctNumber = (int) HttpContext.Session.GetInt32(SessionKeyNumber);
            int guesses = (int) HttpContext.Session.GetInt32(SessionKeyGuesses);
            string message;
            if (GuessingGame.GuessNumber(guessedNumber, correctNumber, ref guesses, out message))
            {
                message += " - A new game has started.";
                NewGame();
            }
            else
            {
                HttpContext.Session.SetInt32(SessionKeyGuesses, guesses);
                HttpContext.Session.SetInt32(SessionKeyGuessedNumber, guessedNumber);
            }

            ViewBag.Message = message;
            ViewBag.GuessedNumber = (int) HttpContext.Session.GetInt32(SessionKeyGuessedNumber);
            ViewBag.Guesses = (int) HttpContext.Session.GetInt32(SessionKeyGuesses);
            return View();
        }
    }

}
