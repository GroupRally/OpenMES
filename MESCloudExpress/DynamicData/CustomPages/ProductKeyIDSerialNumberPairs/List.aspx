<%@ Page Language="C#" MasterPageFile="~/Site.master" CodeFile="List.aspx.cs" Inherits="List" %>

<%@ Register src="~/DynamicData/Content/GridViewPager.ascx" tagname="GridViewPager" tagprefix="asp" %>
<%@ Register Src="~/DynamicData/Content/GridViewSearcher.ascx" TagPrefix="asp" TagName="GridViewSearcher" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:DynamicDataManager ID="DynamicDataManager1" runat="server" AutoLoadForeignKeys="true">
        <DataControls>
            <asp:DataControlReference ControlID="GridView1" />
        </DataControls>
    </asp:DynamicDataManager>

    <h2 class="DDSubHeader"><%= table.DisplayName%></h2>

    <script type="text/javascript">
        function SetSelection(sender)
        {
            var container = document.getElementById("MES.DataGridList");

            if (container != null)
            {
                var inputs = container.getElementsByTagName("input");

                if ((inputs != null) && (inputs.length > 0))
                {
                    for (var i = 0; i < inputs.length; i++)
                    {
                        if ((inputs[i].type == "checkbox") && (inputs[i].id != sender.id))
                        {
                            inputs[i].checked = sender.checked;
                        }
                    }
                }
            }
        }

        function CheckSelection()
        {
            var container = document.getElementById("MES.DataGridList");

            var selectionCount = 0;

            if (container != null)
            {
                var inputs = container.getElementsByTagName("input");

                if ((inputs != null) && (inputs.length > 0))
                {
                    for (var i = 0; i < inputs.length; i++)
                    {
                        if ((inputs[i].type == "checkbox") && (inputs[i].checked == true))
                        {
                            selectionCount++;
                        }
                    }
                }
            }

            if (selectionCount <= 0)
            {
                window.alert("Please select at least 1 item from the list to export!");

                return false;
            }

            return true;
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="DD">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" EnableClientScript="true"
                    HeaderText="List of validation errors" CssClass="DDValidator" />
                <asp:DynamicValidator runat="server" ID="GridViewValidator" ControlToValidate="GridView1" Display="None" CssClass="DDValidator" />

                <asp:QueryableFilterRepeater runat="server" ID="FilterRepeater">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("DisplayName") %>' OnPreRender="Label_PreRender" />
                        <asp:DynamicFilter runat="server" ID="DynamicFilter" OnFilterChanged="DynamicFilter_FilterChanged" /><br />
                    </ItemTemplate>
                </asp:QueryableFilterRepeater>
                <br />
            </div>

            <div class="DDBottomHyperLink" style="text-align:right">
                <asp:GridViewSearcher runat="server" ID="GridViewSearcher" GridViewID="GridView1" GridViewDataSourceID="GridDataSource" />
                &nbsp;
                <asp:LinkButton ID="LinkButtonExportSelected" Text="Export selected" runat="server" OnClientClick="return CheckSelection();" OnClick="LinkButtonExportSelected_Click"/> 
                &nbsp;&nbsp;
                <asp:DynamicHyperLink ID="InsertHyperLink" runat="server" Action="Insert" Visible="false"><img runat="server" src="~/DynamicData/Content/Images/plus.gif" alt="Insert new item" />Insert new item</asp:DynamicHyperLink>
                &nbsp;
            </div>

            <br />

            <div id="MES.DataGridList">
                 <asp:GridView ID="GridView1" runat="server" DataSourceID="GridDataSource" EnablePersistedSelection="true"
                AllowPaging="True" AllowSorting="True" CssClass="DDGridView"
                RowStyle-CssClass="td" HeaderStyle-CssClass="th" CellPadding="6" AutoGenerateColumns="false" OnRowDeleted="GridView1_RowDeleted">
                <Columns>
                     <asp:TemplateField>
                         <HeaderTemplate>
                             <asp:CheckBox ID="CheckBoxSelectAll" runat="server" onclick="SetSelection(this);" />
                         </HeaderTemplate>
                        <ItemTemplate>
                           <asp:CheckBox ID="CheckBoxSelectItem" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Transaction">
                        <ItemTemplate>
                            <%--
                                <asp:DynamicHyperLink runat="server" Action="Edit" Text="Edit" Visible="false"/>
                                &nbsp;&nbsp;
                                <asp:DynamicHyperLink runat="server" Text="Details" />
                                --%>
                            <asp:DynamicHyperLink DataField="TransactionID" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                  <%-- <asp:DynamicField DataField="TransactionID"  HeaderText="Transaction ID" />--%>
                    <asp:BoundField DataField="SerialNumber" HeaderText="Serial Number" />
                   <%--<asp:DynamicField DataField="SerialNumber" HeaderText = "Serial Number"  />--%>
                   <asp:DynamicField DataField="ProductKeyID" HeaderText="DPK ID"/>
                   <asp:DynamicField DataField="SeriesName" HeaderText="Series" />
                   <asp:DynamicField DataField="ModelName" HeaderText="Model" />
                   <asp:DynamicField DataField="DeviceName" HeaderText="Device" />
                   <asp:DynamicField DataField="OperatorName" HeaderText="Operator" />
                   <asp:DynamicField DataField="StationName" HeaderText="Station" />
                   <asp:DynamicField DataField="LineName" HeaderText="Line" />
                   <asp:DynamicField DataField="BusinessName" HeaderText="Business" />
                   <asp:DynamicField DataField="CreationTime" HeaderText="Creation Time" />

                   <asp:TemplateField HeaderText="Delete..." >
                       <ItemTemplate>
                          <asp:LinkButton runat="server" CommandName="Delete" Text="Delete" 
                              OnClientClick='return confirm("Are you sure you want to delete this item?");'/>
                       </ItemTemplate>
                   </asp:TemplateField>

                </Columns>
                <PagerStyle CssClass="DDFooter"/>        
                <PagerTemplate>
                    <asp:GridViewPager runat="server" />
                </PagerTemplate>
                <EmptyDataTemplate>
                    There are currently no items.
                </EmptyDataTemplate>
            </asp:GridView>

            <asp:EntityDataSource ID="GridDataSource" runat="server" EnableDelete="true" />
            
            <asp:QueryExtender TargetControlID="GridDataSource" ID="GridQueryExtender" runat="server">
                <asp:DynamicFilterExpression ControlID="FilterRepeater" />
            </asp:QueryExtender>

               <br />

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

