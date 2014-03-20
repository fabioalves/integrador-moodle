using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using integrador_moodle.Models;
using System.Xml.Linq;

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

        private string GetXmlTransacao(Pagamento pagamento)
        {
            XDocument doc = new XDocument(
                    new XDeclaration("1.0", "ISO-8859-1", string.Empty),
                    new XElement("requisicao-transacao", 
                        new XAttribute("id", pagamento.matriculaUID),
                        new XElement("dados-ec", 
                                new XElement("numero", "1001734898"),
                                new XElement("chave", "e84827130b9837473681c2787007da5914d6359947015a5cdb2b8843db0fa832")                                
                            ),
                        new XElement("dados-pedido",
                                new XElement("numero", pagamento.matriculaUID.ToString()),
                                new XElement("valor", pagamento.valor.ToString()),
                                new XElement("moeda", "986"),
                                new XElement("data-hora", DateTime.Now),
                                new XElement("idioma", "PT")
                            ),
                        new XElement("forma-pagamento",
                                new XElement("bandeira", "visa"),
                                new XElement("produto", "1"),
                                new XElement("parcelas", pagamento.parcelas.ToString())
                            ),
                        new XElement("url-retorno", "MODIFICAR"),
                        new XElement("autorizar", "2"),
                        new XElement("capturar", "true") 
                        )
                );

            return doc.ToString();
            /*
            return "<?xml version=\"1.0\" encoding=\"ISO-8859-1\" ?>" +
                    "<requisicao-transacao id=\"9fffc33eb2919fbd2c88ddd868bf627a\" versao=\"1.1.0\">" +
                    "   <dados-ec>" +
                    "      <numero>1001734898</numero>" +
                    "      <chave>e84827130b9837473681c2787007da5914d6359947015a5cdb2b8843db0fa832</chave>" +
                    "   </dados-ec>" +
                    "   <dados-pedido>" +
                    "      <numero>9031280</numero>" +
                    "      <valor>10000</valor>" +
                    "      <moeda>986</moeda>" +
                    "      <data-hora>2013-09-07T18:26:14</data-hora>" +
                    "      <idioma>PT</idioma>" +
                    "   </dados-pedido>" +
                    "   <forma-pagamento>" +
                    "      <bandeira>visa</bandeira>" +
                    "      <produto>3</produto>" +
                    "      <parcelas>3</parcelas>" +
                    "   </forma-pagamento>" +
                    "   <url-retorno>http://localhost/InscricoesCurso/Cursos/DadosInscricao/" + inscricaoID.ToString() + "</url-retorno>" +
                    "   <autorizar>1</autorizar>" +
                    "   <capturar>true</capturar>" +
                    "</requisicao-transacao>";
             */
        }
    }
}
