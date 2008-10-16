<%@ Page Language="C#" MasterPageFile="~/Reporting/UI/Layouts/Top+SubMenu.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="Reporting_UI_Report" %>
<%@ Import Namespace="System.Linq" %>

<asp:Content  ID="Content1" ContentPlaceHolderID="TextContent" Runat="Server">

    <div style="width:100%">
        <table>
            <tr>
                <td align="left">
                    <asp:DropDownList ID="ddlReportType" runat="server">
                        <asp:ListItem value="erv" selected="True">
                            Экзаменационно рейтинговые ведомости
                        </asp:ListItem>
                        <asp:ListItem value="svrs">
                            Сводные ведомости по результатам сессии
                        </asp:ListItem>
                        <asp:ListItem value="oaz">
                            Отчеты по академическим задолженностиям
                        </asp:ListItem>
                        <asp:ListItem value="pv">
                            Предварительные ведомости
                        </asp:ListItem>
                        <asp:ListItem value="oes">
                            Отчеты по экзаменационным сессиям
                        </asp:ListItem>
                        <asp:ListItem value="oz">
                            Отчет по заявкам
                        </asp:ListItem>
                        <asp:ListItem value="irp">
                            Информация о ренабельности потоков
                        </asp:ListItem>
                     </asp:DropDownList>
                     
                     <asp:Button ID="btnGet" runat="server" Text="Сформировать" 
                        onclick="btnGet_Click" />

                </td>
            </tr>
            
        </table>
        <br />
        <asp:Label ID="lbl" runat="server" Text="-----------"></asp:Label>
        
        
 	<% if (this.Requests.Any())
    { %>
    <table>
        <tr><th>Название</th></tr>
	<% foreach (var _req in this.Requests)
    { %>
        <tr>
            <td>
            
           <a href='<%= _req.TemplateUrl %>'><%= _req.Title%></a>
       
            </td>
        </tr>    
             
	<% } %>
	</table>
	<% }
    else
    { %>
    no Requests
	<% } %>       
        
        
        
        
        </div>

</asp:Content>