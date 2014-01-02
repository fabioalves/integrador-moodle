using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace integrador_moodle.Areas.Admin.Utility
{
    public class CustomIdentity : System.Security.Principal.IIdentity
    {
        public CustomIdentity(string name, string id)
        {
            this.Name = name;
            this.Id = id;
        }

        #region IIdentity Members

        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public bool IsAuthenticated
        {
            get { return !string.IsNullOrEmpty(this.Name); }
        }

        public string Name { get; private set; }

        public string Id { get; private set; }


        #endregion
    }
}