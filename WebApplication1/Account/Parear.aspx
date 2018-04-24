<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Parear.aspx.cs" Inherits="WebApplication1.Account.Parear" %>
<%@ Register Assembly="LatchMembership" Namespace="LatchMembership.UI" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>Pairing token:</h2><cc1:PairingControl ID="PairingControl1" runat="server" 
            Visible="False" />
        <h2>Parear token:</h2><asp:TextBox ID="tb_token" runat="server" Text=""></asp:TextBox>
        <asp:Button ID="bt_parear" runat="server" Text="Parear" 
            onclick="bt_parear_Click" />
        <h3>Account ID: <asp:Label ID="lb_account_id" runat="server" Text=""></asp:Label> 
            <asp:Label ID="lb_error_account_id" runat="server" ForeColor="#FF3300"></asp:Label>                    
        </h3>        
    </div>
</asp:Content>
