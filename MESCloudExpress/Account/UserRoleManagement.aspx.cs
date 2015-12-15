using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using MES.Security;

public partial class Account_UserRoleManagement : System.Web.UI.Page
{

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        if (!this.User.Identity.IsAuthenticated)
        {
            FormsAuthentication.RedirectToLoginPage();
        }

        if (!Roles.IsUserInRole(RoleManager.SystemRole_SupperUser))
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            this.DropDownListRoles.SelectedValue = MES.Security.RoleManager.SystemRole_LineOperator;
            this.BindData(MES.Security.RoleManager.SystemRole_LineOperator);
        }
    }

    private List<MembershipUser> users = null;

    private void BindData(string roleName) 
    {
        this.users = new List<MembershipUser>();

        string[] userNames = Roles.GetUsersInRole(roleName);

        foreach (var userName in userNames)
        {
            if (userName.ToUpper() != RoleManager.SystemUser_BuiltIn)
            {
                users.Add(Membership.GetUser(userName, false));
            }
        }

        this.GridViewUsersInRole.DataSource = this.users;
        this.GridViewUsersInRole.DataBind();
    }
    protected void DropDownListRoles_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.BindData(this.DropDownListRoles.SelectedValue);
    }
    protected void GridViewUsersInRole_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string selectedRoleName = (this.GridViewUsersInRole.Rows[e.RowIndex].FindControl("DropDownListRoleSelection") as DropDownList).SelectedValue;
        //string userName = this.GridViewUsersInRole.Rows[e.RowIndex].Cells[0].Text;

        string userName = this.GridViewUsersInRole.DataKeys[e.RowIndex].Value.ToString();

        (new MES.Security.RoleManager()).SetUserRole(userName, selectedRoleName);

        this.GridViewUsersInRole.EditIndex = -1;

        this.DropDownListRoles.SelectedValue = selectedRoleName;
        this.BindData(selectedRoleName);
    }
    protected void GridViewUsersInRole_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.GridViewUsersInRole.EditIndex = e.NewEditIndex;

        this.BindData(this.DropDownListRoles.SelectedValue);
    }
    protected void GridViewUsersInRole_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        this.GridViewUsersInRole.EditIndex = -1;

        this.BindData(this.DropDownListRoles.SelectedValue);
    }
}