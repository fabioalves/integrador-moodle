using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using integrador_moodle.Models;
using System.Xml.Linq;
using System.Net;
using System.Text;
using System.IO;
using System.Xml;
using System.Web.Configuration;

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
            ViewBag.bandeiras = _dbcontext.Set<BandeiraCartao>().ToList();

            return View(new Pagamento());
        }

        [HttpPost]
        public ActionResult Pagar(Pagamento model, int id, int bandeira, int parcela)
        {            
            CursoController cursocontroller = new CursoController(this._dbcontext);
            Matricula matricula = cursocontroller.RealizarMatricula(id);

            string url = "https://qasecommerce.cielo.com.br/servicos/ecommwsec.do";

            ServiceController service = new ServiceController();
            Curso curso = _dbcontext.Set<Curso>().Find(id);

            model.formaPagamentoUID = 1;
            model.parcelas = parcela;
            model.matriculaUID = matricula.matriculaUID;
            model.valor = curso.valor;            
            model.bandeiraUID = bandeira;

            var stringXml = this.GetXmlTransacao(model);

            Stream responseStream = service.Post(url, stringXml);

            XmlDocument xmlDoc = service.LoadToXml(responseStream, Encoding.Default);
            XmlNodeList urlAutenticacao = xmlDoc.DocumentElement.GetElementsByTagName("url-autenticacao");
            XmlNodeList tid = xmlDoc.DocumentElement.GetElementsByTagName("tid");

            model.transacaoUID = tid[0].InnerText;
            this.Create(model);

            return Redirect(urlAutenticacao[0].InnerText);
        }

        private void Create(Pagamento model)
        {
            this._dbcontext.Set<Pagamento>().Add(model);
            this._dbcontext.SaveChanges();
        }

        private string GetXmlTransacao(Pagamento pagamento)
        {
            string produto = "1";
            if (pagamento.parcelas > 1)
                produto = "2";

            string bandeira = "visa";
            if(pagamento.bandeiraUID.Value == 2)
                bandeira = "mastercard";

            string absoluteUrl = WebConfigurationManager.AppSettings["absoluteurl"].ToString();

            XDocument doc = new XDocument(
                    new XDeclaration("1.0", "ISO-8859-1", string.Empty),
                    new XElement("requisicao-transacao",
                        new XAttribute("id", pagamento.matriculaUID),
                        new XAttribute("versao", "1.1.0"),
                        new XElement("dados-ec",
                                new XElement("numero", "1001734898"),
                                new XElement("chave", "e84827130b9837473681c2787007da5914d6359947015a5cdb2b8843db0fa832")
                            ),
                        new XElement("dados-pedido",
                                new XElement("numero", pagamento.matriculaUID.ToString()),
                                new XElement("valor", ((int)pagamento.valor * 100).ToString()),
                                new XElement("moeda", "986"),
                                new XElement("data-hora", DateTime.Now),
                                new XElement("idioma", "PT")
                            ),
                        new XElement("forma-pagamento",
                                new XElement("bandeira", bandeira),
                                new XElement("produto", produto),
                                new XElement("parcelas", pagamento.parcelas.ToString())
                            ),
                        new XElement("url-retorno", absoluteUrl + Url.Action("InscricaoRealizada", "Index", new { area = "Discente" })),
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
