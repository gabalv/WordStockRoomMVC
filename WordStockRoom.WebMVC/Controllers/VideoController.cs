using Microsoft.AspNet.Identity;
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
    public class VideoController : Controller
    {
        private VideoService CreateVideoService(int wordId)
        {
            return new VideoService(Guid.Parse(User.Identity.GetUserId()), wordId);
        }

        // GET: Video
        public ActionResult Index(int? wordId)
        {
            if (wordId is null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var service = CreateVideoService((int)wordId);
            var model = service.GetVideosByWord();
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
        public ActionResult Create(int languageId, int wordId, VideoCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateVideoService(wordId);
            if (service.AddVideo(model))
            {
                TempData["SaveResult"] = "Video was added successfully.";
                return RedirectToAction("Details", "Word", new { languageId, id = wordId });
            }

            ModelState.AddModelError("", "Video was not added.");
            return View(model);
        }

        // GET: Details
        public ActionResult Details(int? wordId, int? id)
        {
            if (wordId is null || id is null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var service = CreateVideoService((int)wordId);
            var model = service.GetVideoById((int)id);

            ViewData["WordId"] = (int)wordId;
            return View(model);
        }

        // GET: Edit
        public ActionResult Edit(int? wordId, int? id)
        {
            if (wordId is null || id is null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var service = CreateVideoService((int)wordId);
            var video = service.GetVideoById((int)id);
            if (video is null) return HttpNotFound();

            var model =
                new VideoEdit
                {
                    VideoId = (int)id,
                    Description = video.Description
                };

            ViewData["WordId"] = (int)wordId;
            return View(model);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int languageId, int wordId, int id, VideoEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.VideoId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateVideoService(wordId);
            if (service.UpdateVideo(model))
            {
                TempData["SaveResult"] = "Video was successfully updated.";
                return RedirectToAction("Details", "Word", new { languageId, id = wordId });
            }

            ModelState.AddModelError("", "Video was not updated.");
            return View(model);
        }

        // GET: Delete
        [ActionName("Delete")]
        public ActionResult Delete(int? wordId, int? id)
        {
            if (wordId is null || id is null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var service = CreateVideoService((int)wordId);
            var model = service.GetVideoById((int)id);
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
            var service = CreateVideoService(wordId);
            if (service.DeleteVideo(id))
            {
                TempData["SaveResult"] = "Video was successfully deleted.";
                return RedirectToAction("Details", "Word", new { languageId, id = wordId });
            }

            ModelState.AddModelError("", "Video was not deleted.");
            return RedirectToAction("Details", "Word", new { languageId, id = wordId });
        }
    }
}