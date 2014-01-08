using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using integrador_moodle.Models;

namespace integrador_moodle.Controllers
{
    public class InscricaoController : Controller
    {
        private ContextI _dbcontext;
        public InscricaoController(ContextI dbcontext)
        {
            _dbcontext = dbcontext;
        }

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
