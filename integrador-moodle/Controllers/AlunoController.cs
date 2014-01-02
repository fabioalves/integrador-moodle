using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using integrador_moodle.Models;

namespace integrador_moodle.Controllers
{
    public class AlunoController : Controller
    {
        //
        // GET: /Aluno/

        public ActionResult Login(int id)
        {
            ViewBag.id = id;
            return View(new integrador_moodle.Areas.Admin.Models.LoginModel());
        }

        public ActionResult Cadastrar(int id)
        {
            ViewBag.id = id;
            integradorEntities db = new integradorEntities();
            
            var estados = new List<SelectListItem>();

            foreach (var uf in db.UF.ToList())
            {
                estados.Add(new SelectListItem()
                {
                    Text = uf.estado,
                    Value = uf.estado
                });
            }
            ViewBag.estados = estados;
            return View(new Aluno());
        }
    }
}
