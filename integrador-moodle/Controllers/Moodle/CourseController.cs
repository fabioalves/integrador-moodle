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

namespace integrador_moodle.Controllers.Moodle
{
    public class CourseController
    {
        public bool EnrollUserToCourseMoodle(Enrollment enrollment)
        {
            string enrollParams = "enrolments[0][roleid]=5" +
                                  "&enrolments[0][userid]=" + enrollment.userid +
                                  "&enrolments[0][courseid]=" + enrollment.courseid;
            
            string token = WebConfigurationManager.AppSettings["moodletoken"].ToString();
            string serviceurl = WebConfigurationManager.AppSettings["moodleserviceurl"].ToString();

            var client = new RestClient();
            string courseFunction = "enrol_manual_enrol_users";
            client.EndPoint = @"http://" + serviceurl + "?wstoken=" +
                                token + "&wsfunction=" +
                                courseFunction + "&moodlewsrestformat=json&" +
                                enrollParams;
            var json = client.MakeRequest();

            return true;

        }

    }
}
