<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BookReservation.aspx.cs" Inherits="Database3rdHandin.BookReservation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/MasterPage.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="JavaScript.js"></script>
    <link href="BookReservation.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPageContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   
    <br />
    <br />
    <asp:Label ID="LabelDay" runat="server" Text="Select a day"></asp:Label>
    <asp:TextBox ID="TextBoxCalendar" runat="server" ></asp:TextBox>
    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxCalendar"  DaysModeTitleFormat="dd/MM/yyyy" FirstDayOfWeek="Monday" />
    
 
    
   
    <br />
    <br />
    
    <asp:Label ID="LabelDentist" runat="server" Text="Dentist"></asp:Label>
     <asp:DropDownList ID="DropDownListDentist" runat="server" OnSelectedIndexChanged="DropDownListDentist_SelectedIndexChanged"></asp:DropDownList>
       
    <br />
    <br />
    <asp:Label ID="LabelTreatment" runat="server" Text="Treament"></asp:Label>
    <asp:DropDownList ID="DropDownListTreatments" runat="server" ></asp:DropDownList>
  
    <br />
    
    <asp:Label ID="LabelImage" runat="server" Text=""></asp:Label>
    <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>
    <asp:Button ID="Button1" runat="server" Text="Book" OnClick="Button1_Click" />

    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>

</asp:Content>
