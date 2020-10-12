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
        private WordService CreateWordService(int languageId)
        {
            return new WordService(Guid.Parse(User.Identity.GetUserId()), languageId);
        }

        // GET: Word
        //[Route("Word/{id}")]
        public ActionResult Index(int? languageId)
        {
            if (languageId is null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var service = CreateWordService((int)languageId);
            var model = service.GetWordsByLanguage((int)languageId);
            if (model is null) return HttpNotFound();

            var languageService = new LanguageService(Guid.Parse(User.Identity.GetUserId()));
            ViewData["Language"] = languageService.GetLanguageById((int)languageId).Name;

            return View(model);
        }

        // GET: Create
        public ActionResult Create(int? languageId)
        {
            if (languageId is null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var languageService = new LanguageService(Guid.Parse(User.Identity.GetUserId()));
            ViewData["Language"] = languageService.GetLanguageById((int)languageId).Name;

            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int languageId, WordCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateWordService(languageId);
            if (service.AddWord(model))
            {
                TempData["SaveResult"] = "Word added successfully.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Word was not added.");
            return View(model);
        }

        // GET: Details
        public ActionResult Details(int? languageId, int? id)
        {
            if (languageId is null || id is null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var service = CreateWordService((int)languageId);
            var model = service.GetWordById((int)id);

            var sentenceService = new SentenceService(Guid.Parse(User.Identity.GetUserId()), (int)id);
            List<SentenceListItem> sentences = new List<SentenceListItem>();
            foreach (var item in model.Sentences)
            {
                sentences.Add(sentenceService.GetSentenceById(item.Key));
            }
            ViewBag.Sentences = sentences;

            var videoService = new VideoService(Guid.Parse(User.Identity.GetUserId()), (int)id);
            List<VideoDetail> videos = new List<VideoDetail>();
            foreach (var item in model.Videos)
            {
                videos.Add(videoService.GetVideoById(item.Key));
            }
            ViewBag.Videos = videos;

            return View(model);
        }

        // GET: Edit
        public ActionResult Edit(int? languageId, int? id)
        {
            if (languageId is null || id is null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var service = CreateWordService((int)languageId);
            var word = service.GetWordById((int)id);
            if (word is null) return HttpNotFound();

            var model =
                new WordEdit
                {
                    WordId = (int)id,
                    WordName = word.WordName,
                    Translation = word.Translation,
                    PartOfSpeech = word.PartOfSpeech
                };

            return View(model);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int languageId, int id, WordEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.WordId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateWordService(languageId);
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
        public ActionResult Delete(int? languageId, int? id)
        {
            if (languageId is null || id is null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var service = CreateWordService((int)languageId);
            var model = service.GetWordById((int)id);
            if (model is null) return HttpNotFound();

            return View(model);
        }

        // POST: Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int languageId, int id)
        {
            var service = CreateWordService(languageId);
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