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
            Stream requestStream = null;
            WebResponse response = null;
            StreamReader reader = null;

            string url = "https://qasecommerce.cielo.com.br/servicos/ecommwsec.do";
            WebRequest request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] byteBuffer = null;
            var stringXml = this.GetXmlTransacao(new Pagamento()
                {
                    formaPagamentoUID = 1,
                    matriculaUID = 3,
                    parcelas = 1,
                    valor = 100                    
                });

            var postData = String.Format("mensagem={0}", stringXml);
            byteBuffer = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteBuffer.Length;
            requestStream = request.GetRequestStream();
            requestStream.Write(byteBuffer, 0, byteBuffer.Length);
            requestStream.Close();


            response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            System.Text.Encoding encoding = System.Text.Encoding.Default;
            reader = new StreamReader(responseStream, encoding);
            Char[] charBuffer = new Char[256];
            int count = reader.Read(charBuffer, 0, charBuffer.Length);

            StringBuilder Dados = new StringBuilder();
            while (count > 0)
            {
                Dados.Append(new String(charBuffer, 0, count));
                count = reader.Read(charBuffer, 0, charBuffer.Length);
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(Dados.ToString());
            XmlNodeList listaElementos = xmlDoc.DocumentElement.GetElementsByTagName("url-autenticacao");
            XmlNodeList tid = xmlDoc.DocumentElement.GetElementsByTagName("tid");

            return Redirect(listaElementos.ToString());
        }

        private string GetXmlTransacao(Pagamento pagamento)
        {
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
                        new XElement("url-retorno", "http://localhost:64886"),
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
