<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LoginPatient.aspx.cs" Inherits="Database3rdHandin.LoginPatient" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="PatientLogin.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPageContent" runat="server">
    <asp:Label ID="ErrorLabel" runat="server" Text=""></asp:Label>
    <asp:Label ID="MessageLabel" runat="server" Text="Login in as a Patient"></asp:Label>
    <br />
    <br />
    
    <asp:Label ID="LabelName" runat="server" Text="Name"></asp:Label>
    <asp:TextBox ID="TextEmail" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LabelPass" runat="server" Text="Password"></asp:Label>
    <asp:TextBox ID="TextPass" runat="server" TextMode="Password"></asp:TextBox>
    <br />
    <asp:Button ID="ButtonLogin" runat="server" Text="Login" OnClick="ButtonLogin_Click" />
    
</asp:Content>
