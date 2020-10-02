using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WordStockRoom.Models;
using WordStockRoom.Services;

namespace WordStockRoom.WebMVC.Controllers
{
    public class WordController : Controller
    {
        private WordService CreateWordService()
        {
            return new WordService(Guid.Parse(User.Identity.GetUserId()));
        }

        // GET: Word
        public ActionResult Index()
        {
            var service = CreateWordService();
            var model = service.GetWords();
            return View(model);
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        public ActionResult Create(WordCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateWordService();

            if (service.AddWord(model))
            {
                TempData["SaveResult"] = "Word added successfully.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Word was not added.");
            return View(model);
        }
    }
}