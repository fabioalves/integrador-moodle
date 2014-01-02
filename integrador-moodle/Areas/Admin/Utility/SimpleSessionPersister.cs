using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace integrador_moodle.Areas.Admin.Utility
{
    public class SimpleSessionPersister
    {
        static string usernameSessionVar = "username";
        static string idSessionVar = "id";

        public static string Id
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }

                var sessionVar = HttpContext.Current.Session[idSessionVar];

                if (sessionVar != null)
                {
                    return sessionVar as string;
                }
                return null;
            }

            set { HttpContext.Current.Session[idSessionVar] = value; }
        }

        public static string Username
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }

                var sessionVar = HttpContext.Current.Session[usernameSessionVar];
                if (sessionVar != null)
                {
                    return sessionVar as string;
                }
                return null;
            }

            set
            {
                HttpContext.Current.Session[usernameSessionVar] = value;
            }
        }
    }
}
