using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;
using MES.Security;

public partial class List : AuthPageBase //System.Web.UI.Page 
{
    protected MetaTable table;

    protected void Page_Init(object sender, EventArgs e) {
        table = DynamicDataRouteHandler.GetRequestMetaTable(Context);
        GridView1.SetMetaTable(table, table.GetColumnValuesFromRoute(Context));
        GridDataSource.EntityTypeFilter = table.EntityType.Name;
        
    }

    protected void Page_Load(object sender, EventArgs e) {
        Title = table.DisplayName;
        GridDataSource.Include = table.ForeignKeyColumnsNames;

        this.GridView1.Columns[0].Visible = this.IsAuthorized();

        // Disable various options if the table is readonly
        if (table.IsReadOnly) {
            GridView1.Columns[0].Visible = false;
            InsertHyperLink.Visible = false;
            GridView1.EnablePersistedSelection = false;
        }

        string filterCacheName = String.Format("EntityWhereFilter_{0}", this.table.Name);

        if (this.Page.Cache[filterCacheName] != null)
        {
            this.GridDataSource.AutoGenerateWhereClause = false;
            this.GridDataSource.Where = this.Page.Cache[filterCacheName].ToString();
        }
        else
        {
            this.GridDataSource.AutoGenerateWhereClause = true;
        }
    }

    protected void Label_PreRender(object sender, EventArgs e) {
        Label label = (Label)sender;
        DynamicFilter dynamicFilter = (DynamicFilter)label.FindControl("DynamicFilter");
        QueryableFilterUserControl fuc = dynamicFilter.FilterTemplate as QueryableFilterUserControl;
        if (fuc != null && fuc.FilterControl != null) {
            label.AssociatedControlID = fuc.FilterControl.GetUniqueIDRelativeTo(label);
        }
    }

    protected override void OnPreRenderComplete(EventArgs e) {
        RouteValueDictionary routeValues = new RouteValueDictionary(GridView1.GetDefaultValues());
        InsertHyperLink.NavigateUrl = table.GetActionPath(PageAction.Insert, routeValues);
        base.OnPreRenderComplete(e);
    }

    protected void DynamicFilter_FilterChanged(object sender, EventArgs e) {
        GridView1.PageIndex = 0;
    }

    protected bool IsAuthorized()
    {
        return System.Web.Security.Roles.IsUserInRole(RoleManager.SystemRole_LineMaster);
    }
    protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if ((e.AffectedRows > 0) && (e.Exception == null))
        {
            string ID = e.Keys[0].ToString();
            string tableName = this.table.Name;
            string userName = System.Web.Security.Membership.GetUser(true).UserName;
            string roleName = System.Web.Security.Roles.GetRolesForUser()[0];

            MES.Utility.TracingUtility.Trace(new object[] { "Data Deleted!", String.Format("Table: {0}", tableName), String.Format("ID: {0}", ID), String.Format("Operator: {0}", userName), String.Format("Operator Role: {0}", roleName), String.Format("Recording Time: {0}", DateTime.Now) }, null);
        }
    }
}