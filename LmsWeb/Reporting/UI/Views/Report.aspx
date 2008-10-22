<%@ Page Language="C#" MasterPageFile="~/Reporting/UI/Layouts/Top+SubMenu.Master"
    AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="Reporting_UI_Report" %>

<%@ Import Namespace="System.Linq" %>
<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>
<%@ Register assembly="N2.Futures" namespace="N2.Web.UI.WebControls" tagprefix="cc1" %>
<%@ Register assembly="N2.Futures" namespace="N2.Web.UI.WebControls.Test" tagprefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TextContent" runat="Server">
    <div style="width: 100%">
        <table>
            <tr>
                <td align="left" colspan="2">
                    <b><span style="font-size: medium">Вид отчета: </span></b>&nbsp;
                    <asp:DropDownList ID="ddlReportType" runat="server" 
                        onselectedindexchanged="ddlReportType_SelectedIndexChanged">
                        <asp:ListItem Value="erv">
                            Экзаменационно рейтинговые ведомости
                        </asp:ListItem>
                        <asp:ListItem Value="svrs">
                            Сводные ведомости по результатам сессии
                        </asp:ListItem>
                        <asp:ListItem Value="oaz">
                            Отчеты по академическим задолженностиям
                        </asp:ListItem>
                        <asp:ListItem Value="pv">
                            Предварительные ведомости
                        </asp:ListItem>
                        <asp:ListItem Value="oes">
                            Отчеты по экзаменационным сессиям
                        </asp:ListItem>
                        <asp:ListItem Value="oz">
                            Отчет по заявкам
                        </asp:ListItem>
                        <asp:ListItem Value="irp">
                            Информация о ренабельности потоков
                        </asp:ListItem>
                    </asp:DropDownList>
        
<%--<cc2:UserTreeTestBed ID="SelectUsertest" runat="server" />
                 
                    <asp:Button ID="btnGet" runat="server" Text="Сформировать" OnClick="btnGet_Click" />
 --%>               
                      
                   
                    </td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <b><span style="font-size: medium">Студент/группа: </span></b>
                    
                   
                                    <cc1:SelectUser ID="SelectUser" runat="server"  AllowMultipleSelection ="false" DisplayMode=Roles SelectionMode = Roles    />
        
                   
                    <asp:Button ID="btnGet" runat="server" Text="Сформировать" 
                        onclick="btnGet_Click" Width="95px" />
                      
                   
  </td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                      
                   
        <asp:HyperLink ID="hlnkReport" runat="server"></asp:HyperLink>
        
  </td>
            </tr>
  <%-- 
            <tr>
                <td>
                    
                   
                                    &nbsp;</td>
                <td>
                    <% if (this.Requests.Any())
                       { %>
                    <table>
                        <tr>
                            <th>
                                Заявки
                            </th>
                        </tr>
                        <% foreach (var _req in this.Requests)
                           { %>
                        <tr>
                            <td>
                                <a href='<%= _req.TemplateUrl %>'>
                                    <%= _req.Title%></a>
                                <%= _req.RequestDate%>
                                <%= _req.Comments%>
                                
                            </td>
                        </tr>
                        <% } %>
                    </table>
                    <% }
                       else
                       { %>
                    no Requests
                                            <% } %>

                </td>
               <td>
                    <% if (this.Trainings.Any())
                       { %>
                    <table>
                        <tr>
                            <th>
                                Тренинги
                            </th>
                        </tr>
                        <% foreach (Training _t in this.Trainings)
                           { %>
                        <tr>
                            <td>
                                <%= _t.Title %>
                            </td>
                        </tr>
                        <% } %>
                    </table>
                    <% }
                       else
                       { %>
                    no Trainings
                                            <% } %>

                </td>
--%>            
           </tr>
        </table>
    </div>
</asp:Content>
