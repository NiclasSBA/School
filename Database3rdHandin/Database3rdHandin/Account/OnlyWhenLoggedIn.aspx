<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="OnlyWhenLoggedIn.aspx.cs" Inherits="Database3rdHandin.Account.OnlyWhenLoggedIn" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="OnlyWhenLogged.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentBG" runat="server">
    <section class="hero-img">
        <img src="/Images/DentistBG.jpg" alt="Top picture of Dr dentist website" />

         
    </section>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPageContent" runat="server">
    

    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:GridView ID="GridViewRes" runat="server" OnSelectedIndexChanged="GridViewRes_SelectedIndexChanged">
        <Columns>
            <asp:ButtonField CommandName="Select" Text="Select" />
        </Columns>
    </asp:GridView>
    <asp:GridView ID="GridViewPerson" runat="server" OnSelectedIndexChanged="GridViewPerson_SelectedIndexChanged">
        <Columns>
            <asp:ButtonField CommandName="Select" Text="Select" />
        </Columns>
    </asp:GridView>
    

    <asp:GridView ID="GridViewDays" runat="server" OnSelectedIndexChanged="GridViewDays_SelectedIndexChanged">
        <Columns>
            <asp:ButtonField CommandName="Select" Text="Select" />
        </Columns>
    </asp:GridView>
    <br />
    <div class="div-controls">
    <asp:Label ID="LabelMessage" runat="server" Text="No errors"></asp:Label>
    <br />
    <asp:Label ID="LabelDate" runat="server" Text="Date"></asp:Label>
    <asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
    <asp:Label ID="LabelPatientId" runat="server" Text="Patient"></asp:Label>
    <asp:TextBox ID="TextBoxP_id" runat="server"></asp:TextBox>
    <asp:Label ID="LabelDentist" runat="server" Text="Dentist"></asp:Label>
       <asp:TextBox ID="TextBoxD_id" runat="server"></asp:TextBox>
    <asp:Label ID="LabelTreatment" runat="server" Text="Treatment"></asp:Label>
       <asp:TextBox ID="TextBoxT_id" runat="server"></asp:TextBox>
    <asp:DropDownList ID="DropDownListPatients" runat="server" OnSelectedIndexChanged="DropDownListPatients_SelectedIndexChanged"  ></asp:DropDownList>
    <asp:DropDownList ID="DropDownListDays" runat="server" OnSelectedIndexChanged="DropDownListDays_SelectedIndexChanged"  ></asp:DropDownList>

<ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxDate"  DaysModeTitleFormat="dd/MM/yyyy" Format="dd/MM/yyyy"  FirstDayOfWeek="Monday" />
   
    <asp:Button ID="ButtonUpdate" runat="server" Text="Update" OnClick="ButtonUpdate_Click" />
    <asp:Button ID="ButtonDelete" runat="server" Text="Delete" OnClick="ButtonDelete_Click" />
        </div>
</asp:Content>
