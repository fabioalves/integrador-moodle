using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using integrador_moodle.Models;

namespace integrador_moodle.Areas.Discente.Controllers
{
    [Authorize(Roles = "Discente")]
    public class CursoController : BaseController
    {
        private ContextI _dbcontext;
        public CursoController(ContextI dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public ActionResult Matriculas()
        {
            Aluno aluno = (Aluno)Session["aluno"];
            var matriculas = _dbcontext.Set<Matricula>().Where(m => m.alunoUID == aluno.alunoUID).ToList();
            return View(matriculas);
        }

    }
}
