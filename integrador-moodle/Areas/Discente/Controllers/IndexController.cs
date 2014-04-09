using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace integrador_moodle.Areas.Discente.Controllers
{
    [Authorize(Roles = "Discente")]
    public class IndexController : BaseController
    {
        //
        // GET: /Aluno/Index/
        public ActionResult Index()
        {
            if (TempData["mensagem"] != null)
                ViewBag.mensagem = TempData["mensagem"];

            return View();
        }

        public ActionResult InscricaoRealizada()
        {
            TempData["mensagem"] = "Inscrição realizada com sucesso!";
            return RedirectToAction("Index");
        }

    }
}
