﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using integrador_moodle.Models;
using integrador_moodle.Controllers.Moodle;
using integrador_moodle.Models.Moodle;

namespace integrador_moodle.Controllers
{
    public class CursoController : Controller
    {
        private ContextI _dbcontext;
        public CursoController(ContextI dbcontext)
        {
            _dbcontext = dbcontext;
        }

        //
        // GET: /Curso/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Matricula(int id)
        {
            Aluno aluno = (Aluno)Session["aluno"];

            MoodleFacade moodle = new MoodleFacade();
            User user = moodle.GetUserFromMoodle(UserField.email, aluno.email);

            Curso curso = _dbcontext.Set<Curso>().Find(id);

            CourseController course = new CourseController();
            course.EnrollUserToCourseMoodle(
                new Enrollment()
                {
                    courseid = curso.idmoodle,
                    userid = user.id
                }
            );
            
            Matricula matricula = new Matricula()
            {
                alunoUID = aluno.alunoUID,
                cursoUID = id,
                status = 1
            };

            _dbcontext.Set<Matricula>().Add(matricula);
            _dbcontext.SaveChanges();

            return View(_dbcontext.Set<Curso>().Find(id));
        }

    }
}
