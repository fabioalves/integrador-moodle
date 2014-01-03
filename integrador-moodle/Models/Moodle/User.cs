using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace integrador_moodle.Models.Moodle
{
    public class User
    {
        public string username; //Username policy is defined in Moodle security config.
        public string password; //Plain text password consisting of any characters
        public string firstname; //The first name(s) of the user
        public string lastname; //The family name of the user
        public string email; //A valid and unique email address
        public string idnumber; //An arbitrary ID code number perhaps from the institution
    }
}