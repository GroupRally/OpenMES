using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;
using System.Collections.Generic;
using System.Linq;
using MES.Security;
using MES.Persistency;
using MES.Utility;

public partial class List : AuthPageBase //System.Web.UI.Page 
{
    protected MetaTable table;

    protected void Page_Init(object sender, EventArgs e)
    {
        table = DynamicDataRouteHandler.GetRequestMetaTable(Context);
        GridView1.SetMetaTable(table, table.GetColumnValuesFromRoute(Context));
        GridDataSource.EntityTypeFilter = table.EntityType.Name;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Title = table.DisplayName;
        GridDataSource.Include = table.ForeignKeyColumnsNames;

        this.GridView1.Columns[(this.GridView1.Columns.Count - 1)].Visible = this.IsAuthorized();

        // Disable various options if the table is readonly
        if (table.IsReadOnly)
        {
            //GridView1.Columns[0].Visible = false;
            this.GridView1.Columns[(this.GridView1.Columns.Count - 1)].Visible = false;
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

    protected void Label_PreRender(object sender, EventArgs e)
    {
        Label label = (Label)sender;
        DynamicFilter dynamicFilter = (DynamicFilter)label.FindControl("DynamicFilter");
        QueryableFilterUserControl fuc = dynamicFilter.FilterTemplate as QueryableFilterUserControl;
        if (fuc != null && fuc.FilterControl != null)
        {
            label.AssociatedControlID = fuc.FilterControl.GetUniqueIDRelativeTo(label);
        }
    }

    protected override void OnPreRenderComplete(EventArgs e)
    {
        RouteValueDictionary routeValues = new RouteValueDictionary(GridView1.GetDefaultValues());
        InsertHyperLink.NavigateUrl = table.GetActionPath(PageAction.Insert, routeValues);
        base.OnPreRenderComplete(e);
    }

    protected void DynamicFilter_FilterChanged(object sender, EventArgs e)
    {
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
            string pairID = e.Keys["PairID"].ToString();
            string serialNumber = e.Values["SerialNumber"].ToString();
            string userName = System.Web.Security.Membership.GetUser(true).UserName;
            string roleName = System.Web.Security.Roles.GetRolesForUser()[0];

            MES.Utility.TracingUtility.Trace(new object[] { "Transaction Deleted!", String.Format("Pair ID: {0}", pairID), String.Format("Serial Number: {0}", serialNumber), String.Format("Operator: {0}", userName), String.Format("Operator Role: {0}", roleName), String.Format("Recording Time: {0}", DateTime.Now) }, null);
        }
    }

    protected void LinkButtonExportSelected_Click(object sender, EventArgs e)
    {
        CheckBox checkBoxSelect = null;

        List<string> selectedDataKeys = new List<string>();

        ProductKeyIDSerialNumberPairs[] pairs = null;

        string script = "window.alert('No available data to export!')";

        foreach (GridViewRow row in this.GridView1.Rows)
        {
            checkBoxSelect = row.Cells[0].FindControl("CheckBoxSelectItem") as CheckBox;

            if ((checkBoxSelect != null) && (checkBoxSelect.Checked))
            {
                //selectedDataKeys.Add(this.GridView1.DataKeys[row.DataItemIndex].Values[0].ToString());

                foreach (Control control in row.Cells[1].Controls)
                {
                    if (control is DynamicHyperLink)
                    {
                        selectedDataKeys.Add((control as DynamicHyperLink).Text.ToLower());

                        break;
                    }
                }

                //selectedDataKeys.Add((row.Cells[1].Controls[1] as DynamicHyperLink).Text);
            }
        }

        if ((selectedDataKeys != null) && (selectedDataKeys.Count > 0))
        {
            try
            {
                using (DBModelContainer context = new DBModelContainer())
                {
                    pairs = context.ProductKeyIDSerialNumberPairs.Where((o) => (selectedDataKeys.Contains(o.TransactionID.ToLower()))).ToArray();
                }

                XmlUtility xmlUtil = new XmlUtility();

                string pairXml = xmlUtil.XmlSerialize(pairs, null, "utf-8");

                if (!String.IsNullOrEmpty(pairXml))
                {
                    pairXml = XmlUtility.XmlTransform(pairXml, Server.MapPath("~/XSLT/Transform-ProductKeyIDSerialNumberPairs-Excel2003.xslt"), "utf-8");

                    byte[] fileBytes = System.Text.Encoding.UTF8.GetBytes(pairXml);

                    Context.Response.ContentType = "application/x-msdownload";

                    Context.Response.AppendHeader("Content-Disposition", String.Format("attachment;filename=DPK-SN-Bundle-Export-{0}.xml", DateTime.Now.ToString("yyyy-MM-ddThh-mm-ss-fff")));

                    Context.Response.OutputStream.Write(fileBytes, 0, fileBytes.Length);

                    Context.Response.Flush();

                    Context.Response.End();
                }
            }
            catch (Exception ex)
            {
                MES.Utility.TracingUtility.Trace(new object[] { "Export Failed!", ex, String.Format("Time: {0}", DateTime.Now) }, null);
                script = "window.alert('Export Failed!')";
                this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), Guid.NewGuid().ToString(), script, true);
                return;
            }
        }
        else
        {
            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), Guid.NewGuid().ToString(), script, true);
            return;
        }
    }
}
