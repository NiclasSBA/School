<%@ Page Title="" MaintainScrollPositionOnPostback="false" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Treatments.aspx.cs" Inherits="Database3rdHandin.Treatments" %>

<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="TreatmentStyles.min.css" rel="stylesheet" />
</asp:Content>


 
<asp:Content ID="Content2" ContentPlaceHolderID="MainPageContent" runat="server">
    <a href="Treatments.aspx" runat="server"></a>
    <asp:Label ID="ErrorLabel" runat="server" Text=""></asp:Label>
    <asp:Label ID="LabelUser" runat="server" Text="No user Logged in"></asp:Label>
    <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>
    <div><asp:Repeater ID="TreatmentRepeater" runat="server" >
           <HeaderTemplate>
                    <table class="MyTable">
                    <tr>
                        <td class="MyHeader">Id</td>
                        <td class="MyHeader">Name</td>
                        <td class="MyHeader">Price</td>
                        <td class="MyHeader">Description</td>
                        <td class="MyHeader">Image</td>
                        

                    </tr>
                    
                    
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="MyItem"><%# Eval("T_Id")%></td>
                        <td class="MyItem"><%# Eval("T_Name")%></td>
                        <td class="MyItem"><%# Eval("T_Price")%></td>
                        <td class="MyItem"><%# Eval("T_Desc")%></td>
                        <td class="MyItem"><img src="<%# Eval("T_Image")%>" /></td>
                        
                    </tr>
                </ItemTemplate>
                <FooterTemplate></table></FooterTemplate>
                </asp:Repeater>
            <div class="div-selection" id="div-selection">
                <a name="MOVEHERE"></a>
        <asp:DropDownList ID="DropDownListTreatments" runat="server" OnSelectedIndexChanged="DropDownListTreatments_SelectedIndexChanged"></asp:DropDownList>
        <asp:Label ID="LabelName" runat="server" Text="Name"></asp:Label>
        <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
        <asp:Label ID="LabelPrice" runat="server" Text="Price"></asp:Label>
    <asp:TextBox ID="TextBoxPrice" runat="server"></asp:TextBox>
        <asp:Label ID="LabelDesc" runat="server" Text="Description"></asp:Label>
    <asp:TextBox ID="TextBoxDesc" runat="server"></asp:TextBox>
        <asp:Label ID="LabelImage" runat="server" Text="Image Url"></asp:Label>
    <asp:TextBox ID="TextBoxImage" runat="server"></asp:TextBox>
    <asp:Button ID="ButtonDelete" runat="server" Text="Delete" OnClick="ButtonDelete_Click" />
    <asp:Button ID="ButtonUpdate" runat="server" Text="Update" OnClick="ButtonUpdate_Click"/>
        </div>
            </div>
</asp:Content>
