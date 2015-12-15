using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Web.DynamicData;

public partial class DynamicData_Content_GridViewSearcher : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.gridView = this.getGridView();
        this.dataSource = this.getGridViewDataSource();

        if (!this.Page.IsPostBack)
        {
            this.populateEntityMemberNameList();
            this.populateOperatorList();

            this.dropDownListEntityMembers.SelectedIndex = 0;
            this.dropDownListEntityMembers_SelectedIndexChanged(sender, e);
        }
    }

    private GridView gridView;
    private EntityDataSource dataSource;

    private Dictionary<string, string> operatorsDict = new Dictionary<string, string>()
    {
        { "Equal To (=)", "=" },
        { "NOT Equal To (<>)", "<>" },
        { "Greater Than (>)", ">" },
        { "Greater Than or Equal To (>=)", ">=" },
        { "Less Than (<)", "<" },
        { "Less Than or Equal To (<=)", "<=" }
    };

    [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
    public string GridViewID { get; set; }

    [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
    public string GridViewDataSourceID { get; set; }

    private GridView getGridView()
    {
        GridView gridView = null;

        Control parent = this.Parent;

        gridView = parent.FindControl(this.GridViewID) as GridView;

        while (gridView == null)
        {
            parent = parent.Parent;

            if (parent == null)
            {
                break;
            }

            gridView = parent.FindControl(this.GridViewID) as GridView;
        }

        return gridView;
    }

    private EntityDataSource getGridViewDataSource()
    {
        EntityDataSource dataSource = null;

        Control parent = this.Parent;

        dataSource = parent.FindControl(this.GridViewDataSourceID) as EntityDataSource;

        while (dataSource == null)
        {
            parent = parent.Parent;

            if (parent == null)
            {
                break;
            }

            dataSource = parent.FindControl(this.GridViewDataSourceID) as EntityDataSource;
        }

        return dataSource;
    }

    private void populateEntityMemberNameList()
    {
        if (this.gridView != null)
        {
           MetaTable table = this.gridView.GetMetaTable();

            if (table != null)
            {
                List<MetaColumn> columns = new List<MetaColumn>();

                foreach (MetaColumn column in table.Columns)
                {
                    if ((column.ColumnType == typeof(string)) || (column.ColumnType == typeof(int)) || (column.ColumnType == typeof(long)) || (column.ColumnType == typeof(DateTime)))
                    {
                        columns.Add(column);
                    }
                }

                this.dropDownListEntityMembers.DataSource = columns;
                this.dropDownListEntityMembers.DataTextField = "DisplayName";
                this.dropDownListEntityMembers.DataValueField = "Name";

                this.dropDownListEntityMembers.DataBind();
            }
        }
    }

    private void populateOperatorList()
    {
        this.dropDownListOperators.DataSource = this.operatorsDict;
        this.dropDownListOperators.DataTextField = "Key";
        this.dropDownListOperators.DataValueField = "Value";
        this.dropDownListOperators.DataBind();
    }

    private Type getSelectedEntityMemberType(string memberName)
    {
        return this.gridView.GetMetaTable().Columns.FirstOrDefault((c) => (c.Name.ToLower() == memberName.ToLower())).ColumnType;
    }

    private string getEntityDataSourceWhereFilter()
    {
        string filter = null;

        Type memberType = this.getSelectedEntityMemberType(this.dropDownListEntityMembers.SelectedValue);

        if (memberType == typeof(string))
        {
            if (String.IsNullOrEmpty(this.textBoxEntityMemberValueString.Text))
            {
                return null;
            }

            if (this.dropDownListOperators.SelectedItem.Value != "LIKE '%{0}%'")
            {
                filter = String.Format("it.{0} {1} '{2}'", this.dropDownListEntityMembers.SelectedValue, this.dropDownListOperators.SelectedValue, this.textBoxEntityMemberValueString.Text);
            }
            else
            {
                filter = String.Format("it.{0} LIKE '%{2}%'", this.dropDownListEntityMembers.SelectedValue, this.dropDownListOperators.SelectedValue, this.textBoxEntityMemberValueString.Text);
            }
        }
        else if ((memberType == typeof(int)) || (memberType == typeof(long)))
        {
            if (String.IsNullOrEmpty(this.textBoxEntityMemberValueNumber.Text))
            {
                return null;
            }

            filter = String.Format("it.{0} {1} {2}", this.dropDownListEntityMembers.SelectedValue, this.dropDownListOperators.SelectedValue, this.textBoxEntityMemberValueNumber.Text);
        }
        else if (memberType == typeof(DateTime))
        {
            if (String.IsNullOrEmpty(this.textBoxEntityMemberValueDate.Text))
            {
                return null;
            }

            filter = String.Format("it.{0} {1} DATETIME'{2}'", this.dropDownListEntityMembers.SelectedValue, this.dropDownListOperators.SelectedValue, DateTime.Parse(this.textBoxEntityMemberValueDate.Text).ToString("yyyy-MM-dd HH:mm"));
        }

        return filter;
    }

    protected void dropDownListEntityMembers_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.getSelectedEntityMemberType(this.dropDownListEntityMembers.SelectedValue) == typeof(string))
        {
            this.textBoxEntityMemberValueString.Visible = true;
            this.textBoxEntityMemberValueNumber.Visible = false;
            this.revtextBoxEntityMemberValueNumber.Visible = false;
            this.textBoxEntityMemberValueDate.Visible = false;

            this.operatorsDict.Add("Contains", "LIKE '%{0}%'");
        }
        else if ((this.getSelectedEntityMemberType(this.dropDownListEntityMembers.SelectedValue) == typeof(int)) || (this.getSelectedEntityMemberType(this.dropDownListEntityMembers.SelectedValue) == typeof(long)))
        {
            this.textBoxEntityMemberValueString.Visible = false;
            this.textBoxEntityMemberValueNumber.Visible = true;
            this.revtextBoxEntityMemberValueNumber.Visible = true;
            this.textBoxEntityMemberValueDate.Visible = false;
        }
        else if (this.getSelectedEntityMemberType(this.dropDownListEntityMembers.SelectedValue) == typeof(DateTime))
        {
            this.textBoxEntityMemberValueString.Visible = false;
            this.textBoxEntityMemberValueNumber.Visible = false;
            this.revtextBoxEntityMemberValueNumber.Visible = false;
            this.textBoxEntityMemberValueDate.Visible = true;
        }

        this.populateOperatorList();
    }

    protected void linkButtonSearch_Click(object sender, EventArgs e)
    {
        string whereFilter = this.getEntityDataSourceWhereFilter();

        string filterCacheName = String.Format("EntityWhereFilter_{0}", this.getGridView().GetMetaTable().Name);

        if (whereFilter != null)
        {
            this.dataSource.AutoGenerateWhereClause = false;
            this.dataSource.Where = whereFilter;
            this.Page.Cache[filterCacheName] = whereFilter;
        }
        else
        {
            this.dataSource.AutoGenerateWhereClause = false;
            this.dataSource.Where = null;
            this.dataSource.AutoGenerateWhereClause = true;

            this.Page.Cache.Remove(filterCacheName);
        }
    }
}