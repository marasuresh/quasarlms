<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Curriculum.ascx.cs"
    Inherits="N2.Calendar.Curriculum.UI.Views.Curriculum" %>
<n2:ChromeBox ID="ChromeBox1" runat="server">
<%--    <% if (this.CourseInfos.Any() )
{%>
--%>  
 <table style="width: 100%;">
                  <tr>
                    <td>
                        <div>
                            <b> Курс </b>
                        </div>
                    </td>  
                    <td>
                         &nbsp;&nbsp;- &nbsp;&nbsp; Обяз &nbsp; Сел            
                    </td>
                </tr>
        <asp:Repeater runat="server" ID="rpt" OnItemCommand="rpt_ItemCommand">
            <ItemTemplate>
                <tr>
                    <td>
                        <div>
                            <asp:Label runat="server" ID="l1" Text='<%# Eval("CourseName") %>' />
                            <asp:HiddenField runat="server" ID="hf" Value='<%# Eval("CourseID") %>' />
                        </div>
                    </td>
                    <td>
                        <asp:RadioButtonList AutoPostBack="true" OnSelectedIndexChanged="rbl_SelectedIndexChanged"
                            ID="rbl" runat="server" RepeatDirection="Horizontal" SelectedValue='<%#((CourseInfo)Container.DataItem).CourseExclude %>'>
                            <asp:ListItem Value="0">&nbsp;.&nbsp;</asp:ListItem>
                            <asp:ListItem Value="1">&nbsp;.&nbsp;</asp:ListItem>
                            <asp:ListItem Value="2">&nbsp;.&nbsp;</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
<%--       <% }  %>
--%>
</n2:ChromeBox>
<%--'<%# ((CourseInfo)Container.DataItem).Title %>'--%>