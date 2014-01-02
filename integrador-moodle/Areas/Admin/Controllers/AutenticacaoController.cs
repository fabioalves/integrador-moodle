using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using integrador_moodle.Areas.Admin.Models;
using integrador_moodle.Models;

namespace integrador_moodle.Areas.Admin.Controllers
{
    public class AutenticacaoController : Controller
    {
        //
        // GET: /Admin/Autenticacao/

        public ActionResult Index()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {                
                UsuarioAdmin usuario = this.GetUsuario(model.Username, model.Password);

                if (usuario != null)
                {
                    Utility.SimpleSessionPersister.Username = usuario.login;
                    Utility.SimpleSessionPersister.Id = usuario.usuarioUID.ToString();   
                 
                    return RedirectToAction("Index", "Index");
                }
                else
                {
                    Utility.SimpleSessionPersister.Username = null;
                    Utility.SimpleSessionPersister.Id = null;
                 
                    return RedirectToAction("Index");
                }
                
            }
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            Utility.SimpleSessionPersister.Username = null;
            Utility.SimpleSessionPersister.Id = null;

            return RedirectToAction("Index", "Admin");
        }

        private UsuarioAdmin GetUsuario(string username, string password)
        {
            integradorEntities db = new integradorEntities();

            string md5pass = integrador_moodle.Areas.Admin.Utility.SecurityUtil.CalculateMd5Hash(password);

            return db.UsuarioAdmin
                        .Where(u => u.login.Equals(username) && u.senha.Equals(md5pass))
                        .SingleOrDefault();
        }

    }
}
