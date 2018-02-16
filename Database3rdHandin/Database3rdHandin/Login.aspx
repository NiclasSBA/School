<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Database3rdHandin.Login" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="LoginStyle.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPageContent" runat="server">
    <section class="hero-img">
        
        <div class="login">
        <asp:Label ID="ErrorLabel" runat="server" Text=""></asp:Label>
    <asp:Label ID="MessageLabel" runat="server" Text=""></asp:Label>
   
    <br />
    <br />
    <div>
    <asp:Label ID="LabelName" runat="server" Text="Name"></asp:Label>
    <asp:TextBox ID="TextEmail" runat="server"></asp:TextBox>
        </div>
            <div>
    <asp:Label ID="LabelPass" runat="server" Text="Password"></asp:Label>
    <asp:TextBox ID="TextPass" runat="server" TextMode="Password"></asp:TextBox>
    </div>
                <br />
            <div>
    <asp:Button ID="ButtonLogin" runat="server" Text="Login" OnClick="ButtonLogin_Click" />
                </div>
    <br />
         </div>
    </section>
    
     <asp:Repeater ID="RepeaterSponsorsFooter" runat="server">
            <HeaderTemplate>
                <table id="TableSponsors">
                 
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><img src='Images/<%# Eval("Picture") %>' /></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
</asp:Content>
