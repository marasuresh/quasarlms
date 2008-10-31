<%@ Page Language="C#" MasterPageFile="~/Reporting/UI/Layouts/Top+SubMenu.Master"
    AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="Reporting_UI_Report" %>

<%@ Import Namespace="System.Linq" %>
<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register Assembly="N2.Futures" Namespace="N2.Web.UI.WebControls" TagPrefix="cc1" %>
<%@ Register Assembly="N2.Futures" Namespace="N2.Web.UI.WebControls.Test" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TextContent" runat="Server">
    <asp:PlaceHolder ID="PlaceHolder2" runat="server" Visible="false">

       <script type="text/javascript" src="~/Lms/UI/Js/jQuery.intellisense.js"></script>

    </asp:PlaceHolder>

    <script type="text/javascript">

        $(function() {
            if(<%= (!IsPostBack).ToString().ToLower() %>) 
            {
            $('#su').hide();
            $('#<%= this.btnGet.ClientID %>').hide();
            }

            $('#<%= this.ddlReportType.ClientID %>').click(
            function() {
                $('#<%= this.btnGet.ClientID %>').hide();
                $('#<%= this.hlnkReport.ClientID %>').hide();  
                $('#su').show('slow');

            });
            
             $('#<%= this.SelectUser.ClientID %>').click(
            function() {
                $('#<%= this.btnGet.ClientID %>').show('slow');
            });
           
//              $('#<%= this.btnGet.ClientID %>').click(
//            function() {
//                $('#<%= this.btnGet.ClientID %>').hide('slow');
//                $('#su').hide('slow');
//            });
//           
               $('#<%= this.hlnkReport.ClientID %>').change(
            function() {
                $('#<%= this.btnGet.ClientID %>').hide('slow');
                $('#su').hide('slow');
            });

            //$('#ddlRT').change(function() {
            //onchange = "$('#<%= this.btnGet.ClientID %>').show();"


        });


//        function reptype() {
//            $('#hidableTr').show();
//        };

//        function arg() {
//            var dd = document.getElementById("ddlRT");
//            return dd.value;
//        };

        //
    </script>

    <div style="width: 100%">
        <table>
            <tr>
                <td align="left">
                    <b><span style="font-size: medium">Вид отчета: </span></b>&nbsp;&nbsp;
<%--                    <select id="ddlRT" onchange="reptype();" >
                        <option value="Экзаменационно рейтинговые ведомости">Экзаменационно рейтинговые ведомости
                        </option>
                        <option value="экзаменационная сессия">экзаменационная сессия</option>
                        <option value="каникулы, отпуск">каникулы, отпуск</option>
                        <option value="войсковая стажировка">войсковая стажировка</option>
                        <option value="аттестационная комиссия">аттестационная комиссия</option>
                        <option value="учеба">учеба</option>
                    </select>
--%>                    
                    <asp:DropDownList ID="ddlReportType" runat="server"  OnSelectedIndexChanged=" ddlReportType_SelectedIndexChanged">
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
            <tr id="su">
                <td align="left">
                    <b><span style="font-size: medium">Студент/группа: </span></b>
                    <cc1:SelectUser ID="SelectUser" runat="server" AllowMultipleSelection="false" DisplayMode='Roles'
                        SelectionMode='Roles' />
                    &nbsp;
                    <%--                        ;   --%>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Button ID="btnGet" runat="server" Text="Сформировать" OnClick="btnGet_Click"
                         Width="95px" />
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:HyperLink ID="hlnkReport" runat="server"></asp:HyperLink>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
