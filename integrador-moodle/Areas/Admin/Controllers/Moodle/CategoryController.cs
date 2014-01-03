using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using integrador_moodle.Models.Moodle;
using integrador_moodle.Utils;
using System.Web.Script.Serialization;
using System.Web.Configuration;

namespace integrador_moodle.Areas.Admin.Controllers.Moodle
{
    public class CategoryController : Controller
    {
        public Category GetCategoryFromMoodle(int id)
        {
            string token = WebConfigurationManager.AppSettings["moodletoken"].ToString();
            string serviceurl = WebConfigurationManager.AppSettings["moodleserviceurl"].ToString();
            
            string urlParams = "criteria[0][key]=id&criteria[0][value]=1";
            
            string function = "core_course_get_categories";

            var client = new RestClient();
            client.EndPoint = @"http://" + serviceurl + "?wstoken="
                              + token + "&wsfunction="
                              + function + "&moodlewsrestformat=json&"
                              + urlParams;

            var json = client.MakeRequest();

            var serializer = new JavaScriptSerializer();
            Category categoria = serializer.Deserialize<List<Category>>(json).FirstOrDefault();
            return categoria;
        }
    }
}
