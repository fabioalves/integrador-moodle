using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;

namespace integrador_moodle.Areas.Admin.Utility
{
    public class CustomPrincipal : System.Security.Principal.IPrincipal
    {
        public CustomPrincipal(CustomIdentity identity, string role)
        {
            this.Identity = identity;
            this.Role = role;
        }

        #region IPrincipal Members

        public IIdentity Identity { get; private set; }
        public string Role { get; private set; }

        public bool IsInRole(string role)
        {
            if (this.Role == role)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
