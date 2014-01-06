using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using integrador_moodle.Models;

namespace integrador_moodle.Controllers
{
    public class CursoController : Controller
    {
        integradorEntities db = new integradorEntities();
        //
        // GET: /Curso/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Matricula(int id)
        {


            return View(db.Curso.Find(id));
        }

    }
}
