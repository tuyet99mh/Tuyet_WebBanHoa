using Anemone.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Anemone.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: admin/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sess = Session[UserSession.LOGIN_SESSION];
            if (sess == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { action = "Index", controller = "Login", Area = "admin" }));

            }

        }
        /*protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }*/

    }
}