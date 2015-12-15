using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.Security;

namespace MES.Security
{
    public class AuthPageBase : Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (!this.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }
        }
    }
}
