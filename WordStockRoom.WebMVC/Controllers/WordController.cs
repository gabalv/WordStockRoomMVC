using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        //[Route("Word/{id}")]
        public ActionResult Index(int? languageId)
        {
            if (languageId is null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var service = CreateWordService();
            var model = service.GetWordsByLanguage((int)languageId);
            if (model is null) return HttpNotFound();

            return View(model);
        }

        // GET: Create
        public ActionResult Create(int languageId)
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
            ViewData["LanguageName"] = langService.GetLanguageById(languageId).Name;

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

        // GET: Details
        public ActionResult Details(int id)
        {
            var service = CreateWordService();
            var model = service.GetWordById(id);

            return View(model);
        }

        // GET: Edit
        public ActionResult Edit(int? id)
        {
            if (id is null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var service = CreateWordService();
            var word = service.GetWordById((int)id);
            if (word is null) return HttpNotFound();

            var model =
                new WordEdit
                {
                    WordId = (int)id,
                    WordName = word.WordName,
                    Translation = word.Translation,
                    PartOfSpeech = word.PartOfSpeech,
                    Language = word.Language
                };

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

            return View(model);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, WordEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.WordId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateWordService();
            if (service.UpdateWord(model))
            {
                TempData["SaveResult"] = "Word was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Word was not updated.");
            return View(model);
        }

        // GET: Delete
        [ActionName("Delete")]
        public ActionResult Delete(int? id)
        {
            if (id is null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var service = CreateWordService();
            var model = service.GetWordById((int)id);
            if (model is null) return HttpNotFound();

            return View(model);
        }

        // POST: Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var service = CreateWordService();
            if (service.DeleteWord(id))
            {
                TempData["SaveResult"] = "Word was deleted.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Word was not deleted.");
            return RedirectToAction("Index");
        }
    }
}