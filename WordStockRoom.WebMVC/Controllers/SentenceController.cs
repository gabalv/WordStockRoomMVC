using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WordStockRoom.Models;
using WordStockRoom.Services;

namespace WordStockRoom.WebMVC.Controllers
{
    public class SentenceController : Controller
    {

        private SentenceService CreateSentenceService(int wordId)
        {
            return new SentenceService(Guid.Parse(User.Identity.GetUserId()), wordId);
        }

        // GET: Sentence
        public ActionResult Index(int? wordId)
        {
            if (wordId is null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var service = CreateSentenceService((int)wordId);
            var model = service.GetSentencesByWord();
            if (model is null) return HttpNotFound();

            return View(model);
        }

        // GET: Create
        public ActionResult Create(int? wordId)
        {
            if (wordId is null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewData["WordId"] = (int)wordId;
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int languageId, int wordId, SentenceCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateSentenceService(wordId);
            if (service.AddSentence(model))
            {
                TempData["SaveResult"] = "Sentence added successfully.";
                return RedirectToAction("Details", "Word", new { languageId, id=wordId });
            }

            ModelState.AddModelError("", "Sentence was not added.");
            return View(model);
        }

        // GET: Edit
        public ActionResult Edit(int? wordId, int? id)
        {
            if (wordId is null || id is null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var service = CreateSentenceService((int)wordId);
            var sentence = service.GetSentenceById((int)id);
            if (sentence is null) return HttpNotFound();

            var model =
                new SentenceEdit
                {
                    SentenceId = (int)id,
                    SentenceContent = sentence.SentenceContent,
                    SentenceTranslation = sentence.SentenceTranslation
                };

            ViewData["WordId"] = (int)wordId;
            return View(model);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int languageId, int wordId, int id, SentenceEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.SentenceId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateSentenceService(wordId);
            if (service.UpdateSentence(model))
            {
                TempData["SaveResult"] = "Sentence was successfully updated.";
                return RedirectToAction("Details", "Word", new { languageId, id = wordId });
            }

            ModelState.AddModelError("", "Sentence was not updated.");
            return View(model);
        }

        // GET: Delete
        [ActionName("Delete")]
        public ActionResult Delete(int? wordId, int? id)
        {
            if (wordId is null || id is null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var service = CreateSentenceService((int)wordId);
            var model = service.GetSentenceById((int)id);
            if (model is null) return HttpNotFound();

            ViewData["WordId"] = (int)wordId;
            return View(model);
        }

        // POST: Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int languageId, int wordId, int id)
        {
            var service = CreateSentenceService(wordId);
            if (service.DeleteSentence(id))
            {
                TempData["SaveResult"] = "Sentence was successfully deleted.";
                return RedirectToAction("Details", "Word", new { languageId, id = wordId });
            }

            ModelState.AddModelError("", "Sentence was not deleted.");
            return RedirectToAction("Details", "Word", new { languageId, id = wordId });
        }
    }
}