using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using integrador_moodle.Utils;

namespace integrador_moodle.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class IndexController : BaseController
    {
        //
        // GET: /Admin/Index/

        public ActionResult Index()
        {   
                
            return View();
        }

        public ActionResult IndexService()
        {
            var client = new RestClient();
            client.EndPoint = @"http://192.168.33.10/moodle/webservice/rest/server.php?wstoken=3d4a812478116dece0b85ee40ee47770&wsfunction=core_user_get_users_by_field&field=username&values[0]=admin&moodlewsrestformat=json";
            client.Method = HttpVerb.GET;
            var json = client.MakeRequest();            

            return View();
        }

        public ActionResult CreateCourse()
        {
            string urlParams = "courses[0][fullname]= string"+
                               "courses[0][shortname]= string"+
                               "courses[0][categoryid]= int";


            /*
             * CATEGORY
             * 
             * 
             * COURSE
             * fullname string   //full name
shortname string   //course short name
categoryid int   //category id
             * */

            var client = new RestClient();
            client.EndPoint = @"http://192.168.33.10/moodle/webservice/rest/server.php?wstoken=3d4a812478116dece0b85ee40ee47770&wsfunction=core_course_create_categories&"+urlParams;
            client.Method = HttpVerb.GET;
            var json = client.MakeRequest();



            return View("Index");
        }
    }
}
