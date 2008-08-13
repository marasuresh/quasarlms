<%@ Page Language="C#" MasterPageFile="~/Tools/Template.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Untitled Page" Culture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Src="RegionEditControl.ascx" TagName="RegionEditControl" TagPrefix="uc2" %>

<%@ Register Src="Orders/OrderList.ascx" TagName="OrderList" TagPrefix="uc1" %>
<%@ Register Src="Trainings/TrainingList.ascx" TagName="TrainingList" TagPrefix="mi" %>
<%@ Register Src="News/NewsList.ascx" TagName="NewsList" TagPrefix="mi" %>
<asp:Content ID="mainContent" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <p style="margin-bottom:4px;">
    <asp:HyperLink ID="newsIconHyperLink" runat="server" ImageUrl="~/Images/news-item-yellow.gif"
        NavigateUrl="~/Tools/News" meta:resourcekey="newsIconHyperLinkResource1"></asp:HyperLink><asp:HyperLink ID="newsHyperLink" runat="server"
            NavigateUrl="~/Tools/News" Font-Bold="True" ForeColor="Blue" meta:resourcekey="newsHyperLinkResource1" Text="Latest News"></asp:HyperLink></p>
    <mi:NewsList id="smallNewsList" runat="server" PageSize=4/>
    <br />
    <p style="margin-bottom:4px;">
    <asp:HyperLink
			ID="trainingsIconHyperLink"
			runat="server"
			ImageUrl="~/Images/calendar.gif"
			NavigateUrl="~/Tools/Trainings"
			meta:resourcekey="trainingsIconHyperLinkResource1" />
    <asp:HyperLink
			ID="trainingsHyperLink"
			runat="server"
			NavigateUrl="~/Tools/Trainings"
			Font-Bold="True"
			ForeColor="Blue"
			meta:resourcekey="trainingsHyperLinkResource1"
			Text="Recent Trainings" /></p>
    <mi:TrainingList ID="smallTrainingList" runat="server" PageSize="5" />
    <br />
    <p style="margin-bottom:4px;">
    <asp:HyperLink
			ID="ordersImageHyperLink"
			runat="server"
			ImageUrl="~/Images/order-greeb.gif"
			NavigateUrl="~/Tools/Orders" />
    <asp:HyperLink
			ID="ordersHyperLink"
			runat="server"
			NavigateUrl="~/Tools/Orders"
			Font-Bold="True"
			ForeColor="Blue"
			meta:resourcekey="ordersHyperLinkResource1" /></p>
    <uc1:OrderList ID="OrderList1" runat="server" PageSize="7" />
    
    
</asp:Content>

