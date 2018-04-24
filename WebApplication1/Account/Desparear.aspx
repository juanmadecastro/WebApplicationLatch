<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Desparear.aspx.cs" Inherits="WebApplication1.Account.Desparear" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Desparear aplicacion:</h2>
<br/>
<asp:Button ID="bt_desparear" runat="server" Text="Desparear" onclick="bt_desparear_Click"/>
<br/>
<asp:Label ID="lb_error_account_id" runat="server" Text="Label"></asp:Label>
</asp:Content>

