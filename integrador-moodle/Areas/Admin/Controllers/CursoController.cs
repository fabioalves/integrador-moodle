using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using integrador_moodle.Models;
using integrador_moodle.Controllers.Areas.Admin.Moodle;
using integrador_moodle.Models.Moodle;

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
                try
                {
                    Course course = this.CreateCourseMoodle(model.fullname, model.nomeBreve);
                    model.idmoodle = course.id;

                    this.db.Curso.Add(model);
                    this.db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Create");
                }
                
            }
            return View(new Curso());
        }

        public Course CreateCourseMoodle(string fullname, string shortname)
        {
            try
            {
                Course course = new Course()
                {
                    fullname = fullname,
                    shortname = shortname
                };

                CourseController courseController = new CourseController();
                return courseController.AddCourseToMoodle(course, Url);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
