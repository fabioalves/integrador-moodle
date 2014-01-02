using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace integrador_moodle.Controllers
{
    public class InscricaoController : Controller
    {
        //
        // GET: /Inscricao/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PossuiCadastro(int id)
        {
            ViewBag.id = id;
            return View();
        }

    }
}
