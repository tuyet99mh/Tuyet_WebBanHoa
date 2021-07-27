using Anemone.Common;
using Anemone.Models;
using Anemone.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anemone.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index(LoginModel model)
        {
            if (Session[UserSession.LOGIN_SESSION] != null)
            {
                return RedirectToAction("Index", "AccAdmin", new { user = Session[UserSession.LOGIN_SESSION].ToString() });
            }
            if (ModelState.IsValid)
            {
                if (model.email == null && model.password == null)
                {
                    return View();
                }
                else
                {
                    var dao = new UserDAO();
                    int res = dao.Login(model);
                    if (res == 1)
                    {
                       /* Session[UserSession.LOGIN_SESSION] = model.email;*/
                        Session.Add(UserSession.LOGIN_SESSION, model.email);
                        /*Session[UserSession.UserName] = dao.getNameByEmail(model.email);*/
                        return RedirectToAction("Index", "AccAdmin");
                    }
                    else if (res == 0)
                    {
                        ModelState.AddModelError("", "Tài khoản chưa được kích hoạt");
                    }
                    else if (res == -2)
                    {
                        ModelState.AddModelError("", "Tài khoản không được cấp quyền truy cập");
                    }
                    else if (res == -1)
                    {
                        ModelState.AddModelError("", "Tài khoản không tồn tại");

                    }
                    else
                    {
                        ModelState.AddModelError("", "Sai mật khẩu");

                    }

                }
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove(UserSession.LOGIN_SESSION);

            return RedirectToAction("Index", "AccAdmin");
        }
    }
}