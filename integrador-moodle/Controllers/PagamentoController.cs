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
        integradorEntities db = new integradorEntities();
        //
        // GET: /Pagamento/

        public ActionResult Index(int id)
        {
            ViewBag.curso = db.Curso.Find(id);
            ViewBag.formasPagamento = db.FormaPagamento.ToList();
            return View(new Pagamento());
        }

        [HttpPost]
        public ActionResult Pagar(Pagamento model, int id)
        {
            return RedirectToAction("Matricula", "Curso", new { id = id });
        }
    }
}
