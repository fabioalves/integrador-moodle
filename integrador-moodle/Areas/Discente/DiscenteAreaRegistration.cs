using System.Web.Mvc;

namespace integrador_moodle.Areas.Discente
{
    public class DiscenteAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Discente";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Discente_default",
                "Discente/{controller}/{action}/{id}",
                new { controller = "Index", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
