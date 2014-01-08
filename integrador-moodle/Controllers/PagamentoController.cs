using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using integrador_moodle.Models;

namespace integrador_moodle.Controllers
{
    public class PagamentoController : Controller
    {
        private ContextI _dbcontext;
        public PagamentoController(ContextI dbcontext)
        {
            _dbcontext = dbcontext;
        }

        //
        // GET: /Pagamento/

        public ActionResult Index(int id)
        {
            ViewBag.curso = _dbcontext.Set<Curso>().Find(id);
            ViewBag.formasPagamento = _dbcontext.Set<FormaPagamento>().ToList();
            return View(new Pagamento());
        }

        [HttpPost]
        public ActionResult Pagar(Pagamento model, int id)
        {
            return RedirectToAction("Matricula", "Curso", new { id = id });
        }
    }
}
