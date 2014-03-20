using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace integrador_moodle.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Login()
        {
            return View();
            //return RedirectToAction("Index", "Autenticacao", new { area = "Admin" });
        }

    }
}
