﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WordStockRoom.Models;
using WordStockRoom.Services;

namespace WordStockRoom.WebMVC.Controllers
{
    [Authorize]
    public class LanguageController : Controller
    {
        private LanguageService CreateLanguageService()
        {
            return new LanguageService(Guid.Parse(User.Identity.GetUserId()));
        }

        // GET: Language
        public ActionResult Index()
        {
            var service = CreateLanguageService();
            var model = service.GetLanguages();

            return View(model);
        }

        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LanguageCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateLanguageService();
            if (service.AddLanguage(model))
            {
                TempData["SaveResult"] = "Language added successfully.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Language was not added.");
            return View(model);
        }

        // GET: Edit
        public ActionResult Edit(int? id)
        {
            if (id is null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var service = CreateLanguageService();
            var language = service.GetLanguageById((int)id);
            if (language is null) return HttpNotFound();

            var model =
                new LanguageEdit
                {
                    LanguageId = (int)id,
                    Name = language.Name,
                    LanguageFamily = language.LanguageFamily
                };

            return View(model);
        }

        //POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LanguageEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.LanguageId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateLanguageService();
            if (service.UpdateLanguage(model))
            {
                TempData["SaveResult"] = "Language was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Language was not updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int? id)
        {
            if (id is null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var service = CreateLanguageService();
            var model = service.GetLanguageById((int)id);
            if (model is null) return HttpNotFound();

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteLanguage(int id)
        {
            var service = CreateLanguageService();
            if (service.DeleteLanguage(id))
            {
                TempData["SaveResult"] = "Language was deleted.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Language was not deleted.");
            return RedirectToAction("Index");
        }
    }
}