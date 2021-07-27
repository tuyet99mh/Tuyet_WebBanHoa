using Anemone.Models.DAO;
using Anemone.Models.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anemone.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        Database db = new Database();
        // GET: Admin/Contact
        public ActionResult Index()
        {
            /*var dao = new ContactDAO();           
            ViewBag.count = dao.ListAll().Count();*/
            var dao = db.contacts.ToList();
            ViewBag.count = dao.Count();

            return View(dao);
        }
       

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(contact sl, HttpPostedFileBase fileanh)
        {                  

                      

            if (ModelState.IsValid)
            {
                var dao = new ContactDAO();
                sl.logo = fileanh.FileName;
                int res = dao.Create(sl);

                if (fileanh != null)
                {
                    var filename = Path.GetFileName(fileanh.FileName);
                    var path = Path.Combine(Server.MapPath("~/assets/images/"), filename);
                    fileanh.SaveAs(path);
                    db.SaveChanges();

                }

            }

            return RedirectToAction("Index", "Contact");


        }

        public ActionResult Delete(int? id)
        {
            contact cnt = db.contacts.SingleOrDefault(x => x.contactID == id);
            if (cnt == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                db.contacts.Remove(cnt);
                db.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

    }

}