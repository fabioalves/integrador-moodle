using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using integrador_moodle.Models;
using integrador_moodle.Models.ViewModels;
using integrador_moodle.Models.Moodle;
using integrador_moodle.Controllers.Moodle;

namespace integrador_moodle.Controllers
{
    public class AlunoController : Controller
    {
        private ContextI _dbcontext;
        public AlunoController(ContextI dbcontext)
        {
            _dbcontext = dbcontext;
        }


        //
        // GET: /Aluno/
        [HttpGet]
        public ActionResult Login(int id)
        {
            ViewBag.id = id;
            return View(new integrador_moodle.Areas.Admin.Models.LoginModel());
        }

        [HttpPost]
        public ActionResult Login(integrador_moodle.Areas.Admin.Models.LoginModel model, int id)
        {
            string senha = integrador_moodle.Utils.SecurityUtil.CalculateMd5Hash(model.Password);
            
            var aluno = _dbcontext.Set<Aluno>().Where(a => 
                            a.email.Equals(model.Username) && 
                            a.senha.Equals(
                                senha
                                ))
                        .SingleOrDefault();

            if (aluno != null)
            {
                Session["aluno"] = aluno;

                return RedirectToAction("Index", "Pagamento", new { id = id });
            }
            else
            {
                return RedirectToAction("Login", new { id = id });
            }            
        }

        public ActionResult Cadastrar(int id)
        {
            ViewBag.id = id;
            
            var estados = new List<SelectListItem>();

            foreach (var uf in _dbcontext.Set<UF>().ToList())
            {
                estados.Add(new SelectListItem()
                {
                    Text = uf.estado,
                    Value = uf.abr
                });
            }
            ViewBag.estados = estados;

            if (TempData["message"] != null)
            {
                ViewBag.errormessage = TempData["message"];
            }

            return View(new CadastroAlunoViewModel());
        }

        public ActionResult Create(CadastroAlunoViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                Aluno aluno = new Aluno()
                {
                    bairro = model.bairro,
                    celular = model.celular,
                    cep = model.cep,
                    cidade = model.cidade,
                    cpf = model.cpf,
                    dataNascimento = model.dataNascimento,
                    email = model.email,
                    endereco = model.endereco,
                    estado = model.estado,
                    login = model.email,
                    nome = model.nome + " " + model.sobrenome,
                    senha = Utils.SecurityUtil.CalculateMd5Hash(model.senha),
                    sexo = model.sexo,
                    telefoneFixo = model.telefoneFixo
                };

                try
                {
                    _dbcontext.Set<Aluno>().Add(aluno);
                    _dbcontext.SaveChanges();

                }
                catch (Exception ex)
                {
                    TempData["message"] = ex.Message;
                    return RedirectToAction("Cadastrar", new { id = id });
                }

                try
                {
                    User user = new User()
                    {
                        firstname = model.nome,
                        lastname = model.sobrenome,
                        email = model.email,
                        password = model.senha,
                        username = model.email,
                        idnumber = aluno.alunoUID.ToString()
                    };

                    UserController userController = new UserController();
                    userController.AddUserToMoodle(user, Url);
                }
                catch (Exception ex)
                {
                    _dbcontext.Set<Aluno>().Remove(aluno);
                    _dbcontext.SaveChanges();

                    TempData["message"] = ex.Message;
                    return RedirectToAction("Cadastrar", new { id = id });
                }

                Session["aluno"] = aluno;
                return RedirectToAction("Index", "Pagamento", new { id = id });
            }
            else
            {
                TempData["message"] = "Os dados preenchidos estão incorretos";
                return RedirectToAction("Cadastrar", new { id = id });
            }
            
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

    }
}
