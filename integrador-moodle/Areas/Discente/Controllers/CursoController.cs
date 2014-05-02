using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using integrador_moodle.Models;
using integrador_moodle.Areas.Discente.ViewModels;
using System.Xml.Linq;
using integrador_moodle.Controllers;
using System.IO;
using System.Xml;
using System.Text;

namespace integrador_moodle.Areas.Discente.Controllers
{
    [Authorize(Roles = "Discente")]
    public class CursoController : BaseController
    {
        private ContextI _dbcontext;
        public CursoController(ContextI dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public ActionResult Matriculas()
        {
            Aluno aluno = (Aluno)Session["aluno"];
            
            List<VmMatriculaPagamento> matriculasPagamento = 
                (from m in _dbcontext.Set<Matricula>()
                 join p in _dbcontext.Set<Pagamento>()
                    on m.matriculaUID equals p.matriculaUID
                where m.alunoUID == aluno.alunoUID
                 select new VmMatriculaPagamento()
                 {
                     Matricula = m,
                     Pagamento = p
                 }).ToList();

            List<VmMatriculaPagamento> mps = new List<VmMatriculaPagamento>();

            foreach (var matricula in matriculasPagamento)
            {
                string url = "https://qasecommerce.cielo.com.br/servicos/ecommwsec.do";
                ServiceController service = new ServiceController();
                var stringXml = this.GetXmlConsulta(matricula.Matricula.matriculaUID, matricula.Pagamento.transacaoUID);
                Stream responseStream = service.Post(url, stringXml);
                XmlDocument xmlDoc = service.LoadToXml(responseStream, Encoding.Default);
                               
                XmlNodeList mensagem = xmlDoc.DocumentElement.GetElementsByTagName("mensagem");
                if (mensagem.Count > 1)
                {
                    matricula.situacao = mensagem[1].InnerText;
                }
                else
                {
                    matricula.situacao = "Em processamento";
                }
                mps.Add(matricula);
            }

            //var matriculas = _dbcontext.Set<Matricula>().Where(m => m.alunoUID == aluno.alunoUID).ToList();
            return View(mps);
        }

     

        public string GetXmlConsulta(int inscricaoID, string tid)
        {
            XDocument doc = new XDocument(
                    new XDeclaration("1.0", "ISO-8859-1", string.Empty),
                    new XElement("requisicao-consulta",
                        new XAttribute("id", inscricaoID),
                        new XAttribute("versao", "1.2.1"),
                        new XElement("tid", tid),
                        new XElement("dados-ec",
                                new XElement("numero", "1001734898"),
                                new XElement("chave", "e84827130b9837473681c2787007da5914d6359947015a5cdb2b8843db0fa832")
                            )
                        )
                );
            return doc.ToString();


         //   Inscricoes.Data.Model.Inscricoes dadosInscricao = InscricoesService.GetById(inscricaoID);

            //StringBuilder strXML = new StringBuilder();
            //strXML.AppendLine("<?xml version=\"1.0\" encoding=\"ISO-8859-1\" ?>");
            //strXML.AppendLine(string.Format("<requisicao-consulta id=\"{0}\" versao=\"1.2.1\">", dadosInscricao.InscricaoID));
            //strXML.AppendLine(string.Format("<tid>{0}</tid>", tid));
            //strXML.AppendLine("<dados-ec>");
            //strXML.AppendLine(string.Format("      <numero>{0}</numero>", ParametrosService.GetValorParametro("CIELO_ESTABELECIMENTO")));
            //strXML.AppendLine(string.Format("      <chave>{0}</chave>", ParametrosService.GetValorParametro("CIELO_CHAVE")));
            //strXML.AppendLine("</dados-ec>");
            //strXML.AppendLine("</requisicao-consulta>");

            //return strXML.ToString();

            //EXEMPLO RETORNO
            /*
             * <?xml version="1.0" encoding="ISO-8859-1"?>
	<transacao versao="1.2.1" id="16" xmlns="http://ecommerce.cbmp.com.br">
	<tid>10017348980129072002</tid>
	<pan>5UeSVNFr1L76SHYZclDdTlr3Ax2Z84mstUQoXbV9AE0=</pan>
	<dados-pedido>
		<numero>16</numero>
		<valor>35000</valor>
		<moeda>986</moeda>
		<data-hora>2014-04-23T19:16:09.648-03:00</data-hora>
		<idioma>PT</idioma>
		<taxa-embarque>0</taxa-embarque>
	</dados-pedido>
	<forma-pagamento>
		<bandeira>visa</bandeira>
		<produto>2</produto>
		<parcelas>2</parcelas>
	</forma-pagamento>
	<status>6</status>
	<autenticacao>
		<codigo>6</codigo>
		<mensagem>Transacao sem autenticacao</mensagem>
		<data-hora>2014-04-23T19:19:20.776-03:00</data-hora>
		<valor>35000</valor>
		<eci>6</eci>
	</autenticacao>
	<autorizacao>
		<codigo>6</codigo>
		<mensagem>Transação autorizada</mensagem>
		<data-hora>2014-04-23T19:19:20.991-03:00</data-hora>
		<valor>35000</valor>
		<lr>00</lr>
		<arp>123456</arp>
		<nsu>076039</nsu>
	</autorizacao>
	<captura>
		<codigo>6</codigo>
		<mensagem>Transacao capturada com sucesso</mensagem>
		<data-hora>2014-04-23T19:19:21.036-03:00</data-hora>
		<valor>35000</valor>
	</captura>
</transacao>
             * */
        }

    }
}
