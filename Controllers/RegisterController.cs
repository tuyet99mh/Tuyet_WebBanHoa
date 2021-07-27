using Anemone.Models;
using Anemone.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Anemone.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.email == null || model.name == null || model.adress == null || model.phone == null ||
                    model.password == null)
                {
                    return View();
                }
                else
                {
                    var dao = new UserDAO();
                    int res = dao.Insert(model);
                    if (res == 1)
                    {
                        
                        ModelState.AddModelError("", "Tạo tài khoản thành công! đănh nhập để tiếp tục mua sắm!");
                    }
                    else if (res == -1)
                    {
                        ModelState.AddModelError("", "Tài khoản đã tồn tại!");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Lỗi hệ thống");
                    }
                }
            }
            return View();
        }
    }
}