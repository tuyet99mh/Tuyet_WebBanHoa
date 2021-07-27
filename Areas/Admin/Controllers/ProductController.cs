using Anemone.Common;
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
    public class ProductController : Controller
    {
        Database db = new Database();
        // GET: Admin/Product
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            var dao = new ProductDAO();

            var model = dao.ListAllPaging(page, pageSize);

            return View(model);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(product pr, HttpPostedFileBase uploadhinh)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDAO();
                int res = dao.Create(pr);

                if (uploadhinh != null && uploadhinh.ContentLength > 0)
                {
                    int id = int.Parse(db.products.ToList().Last().proID.ToString());

                    string _FileName = "";
                    int index = uploadhinh.FileName.IndexOf('.');
                    _FileName = "SP" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/assets/images/product/"), _FileName);
                    uploadhinh.SaveAs(_path);

                    product unv = db.products.FirstOrDefault(x => x.proID == id);
                    unv.image = _FileName;


                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        /* [HttpDelete]
         public ActionResult Delete(int id)
         {
             try
             {
                 new ProductDAO().Delete(id);

                 //return RedirectToAction("Index");
                 return View("Index");
             }
             catch (Exception)
             {

                 throw;
             }

         }*/

      /*  [HttpDelete]*/
        public ActionResult XoaSanPham(int SanPhamID)
        {

            var dao = new ProductDAO();
            int res = dao.getPrcatbyPr(SanPhamID);
            if (res == 1)
            {
                new ProductDAO().Delete(SanPhamID);
                return RedirectToAction("Index", "Product");
            }
            else
            {
                /*ModelState.AddModelError("", "không thể xóa sản phẩm hãy chọn cách Hide sản phẩm thay vì xóa");*/
                TempData["AlertBox"] = "Sản phẩm không thể xóa. Bạn có thể ấn sản phẩm";
                return RedirectToAction("Index", "Product");
            }
            /* new ProductDAO().Delete(SanPhamID);*/

            //return RedirectToAction("Index");
                

        }

        [HttpPost]
        public JsonResult delete(int idp)
        {
            var dao = new ProductDAO();
            var res = dao.DeletePr(idp);
            return Json(new { res = res }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Details(int? id)
        {
            var dao = new ProductDAO();
            var res = dao.findByID(id);

            if (res == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
                return View(res);
        }

        /*       [HttpGet]
               public ActionResult Edit(int id)
               {
                   var pr = new ProductDAO().findByID(id);
                   return View(pr);

               }
               [HttpPost]
               [ValidateInput(false)]
               public ActionResult Edit(product pr, HttpPostedFileBase fileanh)
               {
                   if (ModelState.IsValid)
                   {
                       var dao = new ProductDAO();

                       pr.image = fileanh.FileName;
                       var res = dao.Edit(pr);
                       if (fileanh != null && fileanh.ContentLength > 0)
                       {

                           string _path = Path.Combine(Server.MapPath("~/assets/images/product/"), fileanh.FileName);
                           fileanh.SaveAs(_path);

                           db.SaveChanges();

                       }

                       return RedirectToAction("Index", "Product");
                   }
                   else
                   {
                       ModelState.AddModelError("", "update không thành công");
                   }

                   return View();
               }*/

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var dao = new ProductDAO();
            var sp = dao.findByID(id);

            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {

                return View(sp);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(product sp, HttpPostedFileBase fileanh)
        {

            if (ModelState.IsValid)
            {
                var dao = new ProductDAO();
                sp.image = fileanh.FileName;
                var res = dao.Edit(sp);

                if (fileanh != null)
                {
                    var filename = Path.GetFileName(fileanh.FileName);
                    var path = Path.Combine(Server.MapPath("~/assets/images/product/"), filename);
                    fileanh.SaveAs(path);
                    db.SaveChanges();

                }

            }

            return RedirectToAction("Index", "Product");


        }

    }
}