using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using integrador_moodle.Models;
using integrador_moodle.Models.Moodle;
using integrador_moodle.Controllers.Moodle;

namespace integrador_moodle.Controllers
{
    public class HomeController : Controller
    {
        private ContextI _dbcontext;
        public HomeController(ContextI dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public ActionResult Index()
        {
            return View(_dbcontext.Set<Curso>().ToList());
        }

        public ActionResult Test()
        {
            MoodleFacade moodle = new MoodleFacade();
            moodle.GetUserFromMoodle("email", "fabioalves@ufmt.br");


            return RedirectToAction("Index");
        }
        
    }
}
