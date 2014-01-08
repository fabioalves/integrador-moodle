using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using integrador_moodle.Controllers.Moodle;
using integrador_moodle.Models.Moodle;

namespace integrador_moodle.Controllers
{
    public class MoodleFacade
    {
        private CourseController _courseController;
        private UserController _userController;

        public MoodleFacade()
        {
            this._courseController = new CourseController();
            this._userController = new UserController();
        }

        public bool EnrollUserToCourseMoodle(Enrollment enrollment)
        {
            return _courseController.EnrollUserToCourseMoodle(enrollment);
        }

        public User AddUserToMoodle(User user, System.Web.Mvc.UrlHelper helper)
        {
            return _userController.AddUserToMoodle(user, helper);
        }

        public User GetUserFromMoodle(string field, string value)
        {
            return _userController.GetUserFromMoodle(field, value);
        }
    }
}