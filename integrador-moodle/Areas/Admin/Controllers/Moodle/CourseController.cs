using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using integrador_moodle.Models.Moodle;
using integrador_moodle.Areas.Admin.Controllers.Moodle;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using integrador_moodle.Utils;

namespace integrador_moodle.Controllers.Areas.Admin.Moodle
{
    public class CourseController : Controller
    {
        public Course AddCourseToMoodle(Course course, UrlHelper helper)
        {
            Category categoria = new CategoryController().GetCategoryFromMoodle(1);
                        
            string courseParams1 = "courses[0][fullname]=" + helper.Encode(course.fullname);
            string courseParams2 = "&courses[0][shortname]=" + course.shortname;
            string courseParams3 = "&courses[0][categoryid]=" + categoria.id;
        
            string courseParams = courseParams1 + courseParams2 + courseParams3;

            string token = WebConfigurationManager.AppSettings["moodletoken"].ToString();
            string serviceurl = WebConfigurationManager.AppSettings["moodleserviceurl"].ToString();

            var client = new RestClient();
            string courseFunction = "core_course_create_courses";
            client.EndPoint = @"http://" + serviceurl + "?wstoken=" +
                                token + "&wsfunction=" +
                                courseFunction + "&moodlewsrestformat=json&" +
                                courseParams;
            var json = client.MakeRequest();

            var serializer = new JavaScriptSerializer();
            Course curso = serializer.Deserialize<List<Course>>(json).FirstOrDefault();

            return curso;
        }


    }
}
