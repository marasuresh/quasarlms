<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Reporting/UI/Layouts/Top+SubMenu.Master"
    CodeBehind="CurriculumList.aspx.cs" Inherits="N2.Calendar.Curriculum.UI.Views.CurriculumList" %>
<%@ Register src="Curriculum.ascx" tagname="Curriculum" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TextContent" runat="Server">
    <asp:DropDownList ID="ddltup" runat="server" AutoPostBack=true
       onselectedindexchanged="ddltup_SelectedIndexChanged">
    </asp:DropDownList>   
    <% if (this.CourseInfos.Any() )
       {%><asp:TextBox ID="txtAddTUP" runat="server"></asp:TextBox>
    <asp:Button ID="btnAddTUP" runat="server"  Text="Add" 
        onclick="btnAddTUP_Click" />
    <asp:Button ID="btnDelTUP" runat="server"  Text="Del" onclick="btnDelTUP_Click" 
        />
        
&nbsp;<table>
<%--                      <tr><td>
                        <b>Курс</b>
                        </td>
                    <td> 
                        <b>&nbsp&nbsp&nbsp;&nbsp;-&nbsp;&nbsp&nbsp;&nbsp; о&nbsp;&nbsp&nbsp; э </b></td></tr>
--%>                    <tr><td>
        <asp:Repeater
            runat="server"
            ID="rpt">
            <ItemTemplate>
                <div>
                    <asp:Label runat="server" ID="l" Text='<%# ((CourseInfo)Container.DataItem).Title %>' />
                    <asp:HiddenField runat="server" ID="hf" Value='<%# Eval("ID") %>' />
                </div>
                <div>
                    <asp:RadioButtonList 
                        ID="rbl"  runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem>-</asp:ListItem>
                            <asp:ListItem>o</asp:ListItem>
                            <asp:ListItem>э</asp:ListItem>
                      </asp:RadioButtonList>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        </td></tr>
    </table>
    <% }
       else
       {
    %>
    <div>
        не найдены курсы
        <br />
        <br />
        <br />
    </div>
    <%  }    %> 
 
                    <asp:Label runat="server" ID="debugl" Text='-------' />
  Control:
        <uc1:Curriculum ID="CurrentCurriculum" runat="server" />

    <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" 
        Text="Сохранить" />

</asp:Content>
