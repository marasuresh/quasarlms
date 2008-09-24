<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/UI/Layouts/Top+SubMenu.Master" AutoEventWireup="true" CodeFile="ACalendar.aspx.cs" Inherits="ACalendar_UI_ACalendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TextContent" Runat="Server">


	<% if (this.AEvents.Length > 0)
    {
        int i = 1;
        %>
    <table>
        <tr><th>Начало</th><th>Конец</th><th>Активность</th><th>Описание</th></tr>
	<% foreach (var _ev in this.AEvents)
    { %>
        <tr>
             <td><%= _ev.DateStart%></td>
             <td><%= _ev.DateEnd%></td>
             <td><%= _ev.Title%></td>
             <td><%= _ev.Description%></td>
             </tr>
           
	<% 
        i++;
    } %>
	</table>
	<% }
    else
    { %>
    no weeks
	<% } %>


</asp:Content>

