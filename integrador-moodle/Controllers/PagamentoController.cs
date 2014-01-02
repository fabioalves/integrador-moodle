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
        //
        // GET: /Pagamento/

        public ActionResult Index()
        {
            return View(new Pagamento());
        }

    }
}
