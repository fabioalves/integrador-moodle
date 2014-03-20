using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using integrador_moodle.Models;

namespace integrador_moodle.Areas.Discente.Controllers
{
    public class AutenticacaoController : Controller
    {
        private ContextI _dbcontext;
        public AutenticacaoController(ContextI dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new integrador_moodle.Areas.Admin.Models.LoginModel());
        }

        [HttpPost]
        public ActionResult Login(integrador_moodle.Areas.Admin.Models.LoginModel model)
        {
            string senha = integrador_moodle.Utils.SecurityUtil.CalculateMd5Hash(model.Password);

            var aluno = _dbcontext.Set<Aluno>().Where(a =>
                            a.email.Equals(model.Username) &&
                            a.senha.Equals(
                                senha
                                ))
                        .SingleOrDefault();

            if (aluno != null)
            {
                Session["aluno"] = aluno;
                integrador_moodle.Areas.Admin.Utility.SimpleSessionPersister.Username = aluno.login;
                integrador_moodle.Areas.Admin.Utility.SimpleSessionPersister.Id = aluno.alunoUID.ToString();

                return RedirectToAction("Index", "Index", new { area = "Discente" });
            }
            else
            {
                integrador_moodle.Areas.Admin.Utility.SimpleSessionPersister.Username = null;
                integrador_moodle.Areas.Admin.Utility.SimpleSessionPersister.Id = null;

                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            
            integrador_moodle.Areas.Admin.Utility.SimpleSessionPersister.Username = null;
            integrador_moodle.Areas.Admin.Utility.SimpleSessionPersister.Id = null;
            Session.Abandon();

            return RedirectToAction("Index", "Home", new { area = "Default" });
        }
    }
}
