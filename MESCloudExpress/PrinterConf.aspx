<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="PrinterConf.aspx.cs" Inherits="PrinterConf" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   
    <h2 class="DDSubHeader">Barcode Printer Settings</h2>

    <br /><br />

   <div>
      <table>
        <tr>
            <td style="text-align:right">Printer Name:</td>
            <td style="text-align:left">
                <asp:DropDownList ID="DropDownListPrtiners" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td style="text-align:right">
                Barcode Type:
            </td>
            <td style="text-align:left">
                <asp:DropDownList ID="DropDownListBarcodeTypes" runat="server" Width="100%" />
            </td>
        </tr>
        <tr>
            <td style="text-align:right">
                Barcode X Position:
            </td>
            <td style="text-align:left">
                <asp:TextBox ID="TextBoxBarcodeXPosition" runat="server" MaxLength="4" Width="100%" />
                <asp:RequiredFieldValidator ID="RFVTextBoxBarcodeXPosition" runat="server" ControlToValidate="TextBoxBarcodeXPosition" Display="Dynamic" CssClass="failureNotification" ErrorMessage="Barcode X Position is required." ToolTip="Barcode X Position is required.">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="REVTextBoxBarcodeXPosition" runat="server" ControlToValidate="TextBoxBarcodeXPosition" Display="Dynamic" CssClass="failureNotification" ValidationExpression="[0-9]{1,4}" ErrorMessage="Value for Barcode X Position should be an integer!" Text="!" />
            </td>
        </tr>
        <tr>
            <td style="text-align:right">
                Barcode Y Position:
            </td>
            <td style="text-align:left">
                <asp:TextBox ID="TextBoxBarcodeYPosition" runat="server" MaxLength="4" Width="100%" />
                <asp:RequiredFieldValidator ID="RFVTextBoxBarcodeYPosition" runat="server" ControlToValidate="TextBoxBarcodeYPosition" Display="Dynamic" CssClass="failureNotification" ErrorMessage="Barcode X Position is required." ToolTip="Barcode Y Position is required.">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="REVTextBoxBarcodeYPosition" runat="server" ControlToValidate="TextBoxBarcodeYPosition" Display="Dynamic" CssClass="failureNotification" ValidationExpression="[0-9]{1,4}" ErrorMessage="Value for Barcode Y Position should be an integer!" Text="!" />
            </td>
        </tr>
         <tr>
            <td style="text-align:right">
                Barcode Image Width:
            </td>
            <td style="text-align:left">
                <asp:TextBox ID="TextBoxBarcodeImageWidth" runat="server" MaxLength="6" Width="100%" />
                <asp:RequiredFieldValidator ID="RFVTextBoxBarcodeImageWidth" runat="server" ControlToValidate="TextBoxBarcodeImageWidth" Display="Dynamic" CssClass="failureNotification" ErrorMessage="Barcode Image Width is required." ToolTip="Barcode Image Width is required.">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="REVTextBoxBarcodeImageWidth" runat="server" ControlToValidate="TextBoxBarcodeImageWidth" Display="Dynamic" CssClass="failureNotification" ValidationExpression="[0-9]{1,6}" ErrorMessage="Value for Barcode Image Width should be an integer!" Text="!" />
            </td>
        </tr>
        <tr>
            <td style="text-align:right">
                Barcode Image Height:
            </td>
            <td style="text-align:left">
                <asp:TextBox ID="TextBoxBarcodeImageHeight" runat="server" MaxLength="6" Width="100%" />
                <asp:RequiredFieldValidator ID="RFVTextBoxBarcodeImageHeight" runat="server" ControlToValidate="TextBoxBarcodeImageHeight" Display="Dynamic" CssClass="failureNotification" ErrorMessage="Barcode Image Height is required." ToolTip="Barcode Image Height is required.">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="REVTextBoxBarcodeImageHeight" runat="server" ControlToValidate="TextBoxBarcodeImageHeight" Display="Dynamic" CssClass="failureNotification" ValidationExpression="[0-9]{1,6}" ErrorMessage="Value for Barcode Image Height should be an integer!" Text="!" />
            </td>
        </tr>
          <tr>
            <td style="text-align:right">
                Paper Width (Inch / 100):
            </td>
            <td style="text-align:left">
                <asp:TextBox ID="TextBoxPaperWidth" runat="server" MaxLength="6" Width="100%" />
                <asp:RequiredFieldValidator ID="RFVTextBoxPaperWidth" runat="server" ControlToValidate="TextBoxPaperWidth" Display="Dynamic" CssClass="failureNotification" ErrorMessage="Paper Width is required." ToolTip="Paper Width is required.">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="REVTextBoxPaperWidth" runat="server" ControlToValidate="TextBoxPaperWidth" Display="Dynamic" CssClass="failureNotification" ValidationExpression="[0-9]{1,6}" ErrorMessage="Value for Paper Width should be an integer!" Text="!" />
            </td>
        </tr>
        <tr>
            <td style="text-align:right">
                Paper Height (Inch / 100):
            </td>
            <td style="text-align:left">
                <asp:TextBox ID="TextBoxPaperHeight" runat="server" MaxLength="6" Width="100%" />
                <asp:RequiredFieldValidator ID="RFVTextBoxPaperHeight" runat="server" ControlToValidate="TextBoxPaperHeight" Display="Dynamic" CssClass="failureNotification" ErrorMessage="Paper Width is required." ToolTip="Paper Height is required.">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="REVTextBoxPaperHeight" runat="server" ControlToValidate="TextBoxPaperHeight" Display="Dynamic" CssClass="failureNotification" ValidationExpression="[0-9]{1,6}" ErrorMessage="Value for Paper Height should be an integer!" Text="!" />
            </td>
        </tr>
        <tr>
            <td style="text-align:right">
                Sample:
            </td>
            <td style="text-align:left">
                <asp:TextBox ID="TextBoxSample" runat="server" MaxLength="18" Width="100%" />
            </td>
        </tr>
        <tr>
            <td style="text-align:left">
                
            </td>
            <td style="text-align:right">
                <asp:CheckBox ID="CheckBoxIsPrintingBarcodeCaption" runat="server" Text="Print Caption" Checked="true"/>
                &nbsp;&nbsp;
                <asp:Button ID="ButtonTest" runat="server" Text="Test" CausesValidation="true" OnClick="ButtonTest_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="ButtonSave" runat="server" Text="Save" CausesValidation="true" OnClick="ButtonSave_Click" />
            </td>
        </tr>
     </table>
   </div>
</asp:Content>

