using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using integrador_moodle.Models;

namespace integrador_moodle.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new integradorEntities();

            return View(db.Curso.ToList());
        }
    }
}
