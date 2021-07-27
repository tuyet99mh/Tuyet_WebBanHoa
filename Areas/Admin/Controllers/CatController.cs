using Anemone.Models.DAO;
using Anemone.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anemone.Areas.Admin.Controllers
{
    public class CatController : BaseController
    {
        Database dbCate = new Database();
        // GET: Admin/Cat
        public ActionResult Index()
        {
            
            var modelCate = dbCate.categories.ToList();
            return View(modelCate);
        }

        [HandleError]
        public ActionResult Delete(int id)
        {
           /* if (Session["accname"] == null)
            {
                Session["accname"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {*/
                var model = dbCate.categories.SingleOrDefault(h => h.catID.Equals(id));
                try
                {
                    if (model != null)
                    {
                        dbCate.categories.Remove(model);
                        dbCate.SaveChanges();
                        return RedirectToAction("Index", "Cat", new { error = "Xoá danh mục thành công." });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Cat", new { error = "Danh mục không tồn tại." });
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Cat", new { error = "Không thể xoá danh mục." });
                }
           /* }*/
        }


        public ActionResult Deletee(int id)
        {
            category st = dbCate.categories.Find(id);
            dbCate.categories.Remove(st);
            dbCate.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(category ct)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDAO();
                int res = dao.Insert(ct);
                if (res == 1)
                {

                    return RedirectToAction("Index", "Cat");
                }
                else
                {
                    ModelState.AddModelError("", "Danh mục sản phẩm đã tồn tại");
                }
            }
            return View();

        }
    }
}