using Anemone.Models.DAO;
using Anemone.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Anemone.Areas.Admin.Controllers
{
    public class AccAdminController : Controller
    {
        // GET: Admin/AccAdmin
        public ActionResult Index(int? page, int pageSize = 6)
        {
            var dao = new UserDAO();
            int pageNumber = (page ?? 1);
            var model = dao.ListAllPaging(pageNumber, pageSize);
            return View(model);
            
        }
        /*[HttpPost]*/
        /*  public ActionResult Index(int id_urtype, int? page , int pageSize = 6)
          {
              if(id_urtype == null)
              {
                  id_urtype = 1;
              }
              var dao = new UserDAO();
              int pageNumber = (page ?? 1);
              var model = dao.ListAllPagingtype( id_urtype, pageNumber, pageSize);
              return View(model);
          }*/

        public ActionResult ListUrType(int id_urtype, int? page, int pageSize = 6)
        {
            if (id_urtype == null)
            {
                id_urtype = 1;
            }
            var dao = new UserDAO();
            int pageNumber = (page ?? 1);
            var model = dao.ListAllPagingtype(id_urtype, pageNumber, pageSize);
            return View(model);
        }


        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(user ur)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                int res = dao.Insert(ur);
                if (res == 1)
                {

                    return RedirectToAction("Index", "AccAdmin");
                }
                else
                {
                    ModelState.AddModelError("", "email đã đăng ký tài khoản trước đó");
                }
            }
            return View();

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var ur = new UserDAO().findByID(id);
            return View(ur);

        }
        [HttpPost]
        public ActionResult Edit(user ur)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var res = dao.Update(ur);
                if (res)
                {

                    return RedirectToAction("Index", "AccAdmin");
                }
                else
                {
                    ModelState.AddModelError("", "update không thành công");
                }
            }
            return View();
        }

        public ActionResult Details(int id_ur)
        {
            if (id_ur == null)
            {
                return View("Error");
            }
            var dao = new UserDAO();
            var ur = dao.findByID(id_ur);
            return View(ur);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new UserDAO().Delete(id);

            //return RedirectToAction("Index");
            return Redirect("Index");
        }


    }
}
