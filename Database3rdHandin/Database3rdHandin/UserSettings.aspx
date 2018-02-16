<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="UserSettings.aspx.cs" Inherits="Database3rdHandin.UserSettings" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Signup.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPageContent" runat="server">

    <asp:Label ID="LabelMessage" runat="server" Text="No Errors"></asp:Label>
    <div id="CreatingFormPatient">
        <div></div>
        <div>
        <asp:Label ID="LabelFirstName" runat="server" Text="First Name"></asp:Label>
        <asp:TextBox ID="TextBoxFirstName" runat="server"></asp:TextBox>
        </div>
        <div>
        <asp:Label ID="LabelLastName" runat="server" Text="Last Name"></asp:Label>
        <asp:TextBox ID="TextBoxLastName" runat="server"></asp:TextBox>
        </div>
        <div>
        <asp:Label ID="LabelAge" runat="server" Text="Age"></asp:Label>
        <asp:TextBox ID="TextBoxAge" runat="server"></asp:TextBox>
        </div>
        <div>
        <asp:Label ID="LabelCPR" runat="server" Text="CPR Number"></asp:Label>
        <asp:TextBox ID="TextBoxCPR" runat="server"></asp:TextBox>
        </div>
        <div>
        <asp:Label ID="LabelEmail" runat="server" Text="Email"></asp:Label>
        <asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox>
        </div>
        <div>
        <asp:Label ID="LabelPassword" runat="server" Text="Password"></asp:Label>
        <asp:TextBox ID="TextBoxPassword" runat="server"></asp:TextBox>
        </div>
        <div>
        <asp:Label ID="LabelGender" runat="server" Text="Gender"></asp:Label>
        <asp:TextBox ID="TextBoxGender" runat="server"></asp:TextBox>
        </div>
        <asp:Button ID="ButtonCreate" runat="server" Text="Create" OnClick="ButtonCreate_Click" />
        </div>
</asp:Content>
