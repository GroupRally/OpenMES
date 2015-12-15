using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using MES.Security;

public partial class Account_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LoginUser_Authenticate(object sender, AuthenticateEventArgs e)
    {
        string userName = this.LoginUser.UserName;
        string password = this.LoginUser.Password;

        if (Roles.IsUserInRole(userName, RoleManager.SystemRole_ForbiddenUser))
        {
            e.Authenticated = false;
        }
        else if (Membership.ValidateUser(userName, password))
        {
            e.Authenticated = true;
        }

        //if (Membership.ValidateUser(userName, password))
        //{
        //    e.Authenticated = true;
        //}
    }
}