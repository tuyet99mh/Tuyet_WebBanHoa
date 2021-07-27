using Anemone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Anemone.Common;
using Anemone.Models.DAO;

namespace Anemone.Controllers
{
    public class UserController : Controller
    {
        // GET: Login
        public ActionResult Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                if (model.email == null && model.password == null)
                {
                    return View();
                }
                else
                {
                    var dao = new UserDAO();
                    int res = dao.Login(model);
                    if (res == 1 || res ==-2)
                    {
                        Session.Add(UserSession.LOGIN_SESSION, model.email);
                     
                        return RedirectToAction("Home", "Product");
                    }
                    else if (res == 0)
                    {
                        ModelState.AddModelError("", "Tài khoản đang bị khóa");
                    }
                    else if (res == -1)
                    {
                        ModelState.AddModelError("", "Tài khoản không tồn tại");
                    }
                    else if (res == -3)
                    {
                        ModelState.AddModelError("", "Sai mật khẩu");

                    }
                    else
                    {
                        ModelState.AddModelError("", "Lỗi hệ thống");

                    }

                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove(UserSession.LOGIN_SESSION);

            return RedirectToAction("Home", "Product");
        }
        
    }
}