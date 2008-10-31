<%@ Page Title="" Language="C#" MasterPageFile="~/Reporting/UI/Layouts/Top+SubMenu.Master"
    AutoEventWireup="true" CodeFile="ACalendarList.aspx.cs" Inherits="ACalendar_UI_ACalendarList" %>

<%@ Import Namespace="System.Linq" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TextContent" runat="Server">
    <table>
        <tr>
            <th>
                <% if (this.IsEditable)
                   { %>
                <asp:ImageButton ID="btnXL" runat="server" ImageUrl="~/ACalendar/UI/Img/Excel_ico.gif"
                      onclick="btnXL_Click" />
                      
                <asp:HyperLink ID="lnkExcel" runat="server"></asp:HyperLink>
                <% } %>
            </th>
        </tr>
        <tr>
            <th>
                Название
            </th>
        </tr>
        <% foreach (var _acal in this.ACalendaries)
           { %>
        <tr>
            <td>
                <a href="<%= _acal.Url%>">
                    <%= _acal.Title%></a>
            </td>
        </tr>
        <% } %>
    </table>
</asp:Content>
<%--
	<% if (this.CurrentItem.ACalendarContainer.GetChildren().Any())
    { %>
    <table><tr><th>Название</th></tr>
	<% foreach (var _acal in this.CurrentItem.ACalendarContainer.MyCalendars %>
		<tr><td><a href='<%= _acal.TemplateUrl %>'><%= _acal.Title%></a></td></tr>    
	<% } %>
	</table>
	<% } else { %>
	no calendars
	<% } %>--%>