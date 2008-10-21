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
                <td align="left" colspan="3">
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
                    <asp:CheckBoxList ID="chblRoles" runat="server">
                    </asp:CheckBoxList>
                    <cc1:SelectUser ID="SelectUser" runat="server" />
        <asp:HyperLink ID="hlnkReport" runat="server"></asp:HyperLink>
                    <cc2:UserTreeTestBed ID="SelectUsertest" runat="server" />
                    <asp:Button ID="btnGet" runat="server" Text="Сформировать" OnClick="btnGet_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <% if (this.Courses.Any())
                       { %>
                    <table>
                        <tr>
                            <th>
                                Курсы
                            </th>
                        </tr>
                        <% foreach (var _cou in this.Courses)
                           { %>
                        <tr>
                            <td>
                                <a href='<%= _cou.TemplateUrl %>'>
                                    <%= _cou.Title%></a>
                            </td>
                        </tr>
                        <% } %>
                    </table>
                    <% }
                       else
                       { %>
                    no Сourses
                        <% } %>
                    
                </td>
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
                                <%= _req.SavedBy%>
                                
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
                    <% if (this.users.Count > 0)
                       { %>
                    <table>
                        <tr>
                            <th>
                                Пользователи
                            </th>
                        </tr>
                        <% foreach (MembershipUser _u in this.users)
                           { %>
                        <tr>
                            <td>
                                <%= _u.UserName %>
                            </td>
                        </tr>
                        <% } %>
                    </table>
                    <% }
                       else
                       { %>
                    no User
                                            <% } %>

                </td>
            </tr>
        </table>
    </div>
</asp:Content>
