using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Text;
using System.IO;
using System.Xml;

namespace integrador_moodle.Controllers
{
    public class ServiceController : Controller
    {
        //
        // GET: /Service/


        internal Stream Post(string url, string stringXml)
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "application/x-www-form-urlencoded";

            byte[] byteBuffer = null;
            var postData = String.Format("mensagem={0}", stringXml);

            byteBuffer = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteBuffer.Length;
            
            Stream requestStream = null;
            requestStream = request.GetRequestStream();
            requestStream.Write(byteBuffer, 0, byteBuffer.Length);
            requestStream.Close();

            return request.GetResponse().GetResponseStream();
        }

        internal System.Xml.XmlDocument LoadToXml(Stream responseStream, Encoding encoding)
        {
            StreamReader reader = new StreamReader(responseStream, encoding);
            Char[] charBuffer = new Char[256];

            int count = reader.Read(charBuffer, 0, charBuffer.Length);

            StringBuilder Dados = new StringBuilder();
            while (count > 0)
            {
                Dados.Append(new String(charBuffer, 0, count));
                count = reader.Read(charBuffer, 0, charBuffer.Length);
            }
            XmlDocument xmlDoc =  new XmlDocument();
            xmlDoc.LoadXml(Dados.ToString());
            return xmlDoc;
        }
    }
}
