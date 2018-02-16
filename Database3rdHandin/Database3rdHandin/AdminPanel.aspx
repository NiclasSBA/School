<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="Database3rdHandin.AdminPanel" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="AdminPanel.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPageContent" runat="server">
    <asp:Label ID="LabelMessage" runat="server" Text="Create Treatment"></asp:Label>
    <br />
    <asp:Label ID="LabelName" runat="server" Text="Name"></asp:Label>
    <asp:TextBox ID="TextName" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LabelPrice" runat="server" Text="Price"></asp:Label>
    <asp:TextBox ID="TextPrice" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LabelDesc" runat="server" Text="Description"></asp:Label>
    <asp:TextBox ID="TextDesc" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="LabelImage" runat="server" Text="Image"></asp:Label>
    <asp:TextBox ID="TextImage" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="ButtonCreate" runat="server" Text="Create" OnClick="ButtonCreate_Click" />

    <br />
    <br />

    <asp:Repeater ID="RepeaterSponsor" runat="server">
         <HeaderTemplate>
                <table border="double">
                    <tr>
                        <td>ID</td>
                        <td>Name</td>
                        <td>Website</td>
                        <td>Link</td>
                        <td>Picture</td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("SponsorID") %></td>
                    <td><%# Eval("Name") %></td>
                    <td><%# Eval("Website") %></td>
                    <td><%# Eval("Picture") %></td>
                    <td><img src="Pictures/<%# Eval("Picture") %>" alt="SponsorLogo" /></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        

    </asp:Repeater>
    <br />
    <br />

    <asp:DropDownList ID="DropDownListSponsor" runat="server"></asp:DropDownList>
    <asp:TextBox ID="TextBoxID" runat="server"></asp:TextBox>
    <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
    <asp:TextBox ID="TextBoxWebsite" runat="server"></asp:TextBox>
    <asp:TextBox ID="TextBoxPicture" runat="server"></asp:TextBox>
    <div class="buttons">
    <asp:Button ID="ButtonCreateSponsor" runat="server" Text="Create" OnClick="ButtonCreateSponsor_Click" />

        <asp:Button ID="ButtonUpdateSponsor" runat="server" Text="Update" OnClick="ButtonUpdateSponsor_Click" />

        <asp:Button ID="ButtonDeleteSponsor" runat="server" Text="Delete" OnClick="ButtonDeleteSponsor_Click" />

        <asp:Button ID="ButtonCancelSponsor" runat="server" Text="Cancel" OnClick="ButtonCancelSponsor_Click" />
        </div>
        </asp:Content>
