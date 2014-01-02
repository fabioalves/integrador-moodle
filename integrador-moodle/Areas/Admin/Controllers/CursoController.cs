using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using integrador_moodle.Models;

namespace integrador_moodle.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class CursoController : BaseController
    {
        integradorEntities db = new integradorEntities();
        //
        // GET: /Admin/Curso/

        public ActionResult Index()
        {
            return View(this.db.Curso.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new Curso());
        }

        [HttpPost]
        public ActionResult Create(Curso model)
        {
            if (ModelState.IsValid)
            {
                this.db.Curso.Add(model);
                this.db.SaveChanges();
            }
            return View(new Curso());
        }
        
    }
}
