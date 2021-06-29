<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Report.ascx.cs" Inherits="Pwa.Web.Views.Report_GUI_002.Report" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<script runat="server">

</script>
<form id="Form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <rsweb:ReportViewer ID="rptViewer" runat="server" Width="100%">
    </rsweb:ReportViewer>
</form>
