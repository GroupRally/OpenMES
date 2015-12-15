using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Main : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.User.Identity.IsAuthenticated)
        {
            this.NavigationMenu.Visible = false;
        }
        else
        {
            bool isAuthorized = this.IsAuthorized();

            this.NavigationMenu.Items[2].ChildItems[1].Enabled = isAuthorized;
            this.NavigationMenu.Items[2].ChildItems[2].Enabled = isAuthorized;
            this.NavigationMenu.Items[3].Enabled = isAuthorized;
        }
    }

    protected bool IsAuthorized()
    {
        return System.Web.Security.Roles.IsUserInRole(MES.Security.RoleManager.SystemRole_SupperUser);
    }
}
