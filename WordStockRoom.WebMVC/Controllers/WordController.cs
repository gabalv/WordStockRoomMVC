using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WordStockRoom.Data;
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
            var partsOfSpeech = new List<ConvertEnum>();
            foreach (PartOfSpeech part in Enum.GetValues(typeof(PartOfSpeech)))
            {
                partsOfSpeech.Add(new ConvertEnum
                {
                    Value = (int)part,
                    Text = part.ToString()
                });
            }
            ViewBag.PartOfSpeechEnum = partsOfSpeech;

            LanguageService langService = new LanguageService(Guid.Parse(User.Identity.GetUserId()));
            var fromDatabaseEF = new SelectList(langService.GetLanguages().ToList(), "LanguageId", "Name");
            ViewData["Languages"] = fromDatabaseEF;

            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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