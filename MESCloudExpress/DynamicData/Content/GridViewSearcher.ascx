<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GridViewSearcher.ascx.cs" Inherits="DynamicData_Content_GridViewSearcher" %>
<link rel="stylesheet" type="text/css" href="../Scripts/datetimepicker/jquery.datetimepicker.css"/>

<asp:DropDownList ID="dropDownListEntityMembers" runat="server" AutoPostBack="true" EnableViewState="true" OnSelectedIndexChanged="dropDownListEntityMembers_SelectedIndexChanged" />
<asp:DropDownList ID="dropDownListOperators" runat="server">
   <%-- <asp:ListItem Value="=">Equal To</asp:ListItem>
    <asp:ListItem Value="!=">NOT Equal To</asp:ListItem>
    <asp:ListItem Value=">">Greater Than</asp:ListItem>
    <asp:ListItem Value=">=">Greater Than or Equal To</asp:ListItem>
    <asp:ListItem Value="<">Less Than</asp:ListItem>
    <asp:ListItem Value=">=">Less Than or Equal To</asp:ListItem>--%>
</asp:DropDownList>

<asp:TextBox ID="textBoxEntityMemberValueString" runat="server" MaxLength="48" />
<asp:TextBox ID="textBoxEntityMemberValueNumber" runat="server" Visible="false" MaxLength="18" ValidationGroup="GridViewSearcher"/>
<asp:RegularExpressionValidator ID="revtextBoxEntityMemberValueNumber" runat="server" Visible="false" ValidationExpression="\d{1,}"  ControlToValidate="textBoxEntityMemberValueNumber" ErrorMessage="*" Display="Dynamic" Text="*" ForeColor="Red" ValidationGroup="GridViewSearcher"/>
<asp:TextBox ID="textBoxEntityMemberValueDate" class="j_datetime_picker" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
<asp:LinkButton ID="linkButtonSearch" runat="server" Text="Search" OnClick="linkButtonSearch_Click" ValidationGroup="GridViewSearcher"/>

<script src="../Scripts/datetimepicker/jquery.js"></script>
<script src="../Scripts/datetimepicker/jquery.datetimepicker.js"></script>
<script>
    $('.j_datetime_picker').datetimepicker();
</script>