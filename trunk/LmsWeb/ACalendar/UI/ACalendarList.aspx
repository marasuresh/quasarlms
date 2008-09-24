<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/UI/Layouts/Top+SubMenu.Master" AutoEventWireup="true" CodeFile="ACalendarList.aspx.cs" Inherits="ACalendar_UI_ACalendarList" %>
<%@ Import Namespace="System.Linq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TextContent" Runat="Server">
	<% if (this.ACalendars.Any())
    { %>
    <table>
        <tr><th>Название</th></tr>
	<% foreach (var _acal in this.ACalendars)
    { %>
        <tr>
            <td><a href='<%= _acal.Url %>'><%= _acal.Title%></a></td>
        </tr>    
             
<%--             <td><%= _acal.Title%></td></tr>
           
            <td><%= _acal.Title%></td></tr>
--%>	<% } %>
	</table>
	<% }
    else
    { %>
    no calendars
	<% } %>
</asp:Content>

