using Anemone.Models.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anemone.Areas.Admin.Controllers
{
    public class SlideController : BaseController
    {
        Database db = new Database();
        // GET: Admin/Slide

        public ActionResult ListSlide()
        {
            var modelSlide = db.slides.ToList().OrderBy(x => x.slideID);
            return View(modelSlide);

        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(slide sl, HttpPostedFileBase fileanh)
        {

            if (fileanh != null)
            {
                var filename = Path.GetFileName(fileanh.FileName);
                var path = Path.Combine(Server.MapPath("~/assets/images/slides/"), filename);
                fileanh.SaveAs(path);
                sl.image = fileanh.FileName;
            }
            try
            {
                if (ModelState.IsValid)
                {
                    db.slides.Add(sl);
                    db.SaveChanges();
                    return RedirectToAction("ListSlide", "Slide");
                }
                else
                {
                    return View(sl);
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int? id)
        {
            slide sl = db.slides.SingleOrDefault(x => x.slideID == id);
            if (sl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                db.slides.Remove(sl);
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}