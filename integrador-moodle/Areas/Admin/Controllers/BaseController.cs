using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using integrador_moodle.Areas.Admin.Utility;

namespace integrador_moodle.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!string.IsNullOrEmpty(SimpleSessionPersister.Username))
            {
                filterContext.HttpContext.User = new CustomPrincipal(
                    new CustomIdentity(
                        SimpleSessionPersister.Username,
                        SimpleSessionPersister.Id), "Administrador");
            }
            base.OnAuthorization(filterContext);
        }
    }
}
