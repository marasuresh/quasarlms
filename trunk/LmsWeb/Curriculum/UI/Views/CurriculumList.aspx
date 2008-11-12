<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Reporting/UI/Layouts/Top+SubMenu.Master"
    CodeBehind="CurriculumList.aspx.cs" Inherits="N2.Calendar.Curriculum.UI.Views.CurriculumList" %>

<%@ Register Src="Curriculum.ascx" TagName="Curriculum" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TextContent" runat="Server">

    <script type="text/javascript">

        $(function() {
           if(<%= (!IsPostBack).ToString().ToLower() %>) 
            {
            }
            
             $('#add').hide();

             $('#shAdd').click(
            function() {
               $('#add').show('slow');
            });           

            $('#<%= this.btnAddTUP.ClientID %>').click(
            function() {
               $('#add').hide();
            });


        });
     
    </script>

    <% if (this.CourseInfos.Any())
       {%>
    <table>
        <tr>
            <td>
                <asp:DropDownList ID="ddltup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddltup_SelectedIndexChanged">
                </asp:DropDownList>
                <% if (this.IsEditable)
                   { %>
                <asp:ImageButton ID="btSave" runat="server" OnClick="btnSave_Click" ImageUrl="../../../Edit/img/ico/png/accept.png" alt = "Сохранить"
                    Visible="False" />
                <asp:ImageButton ID="btnDelTUP" runat="server" OnClick="btnDelTUP_Click" ImageUrl="../../../Edit/img/ico/png/delete.png" alt = "Удалить" />
                <img id="shAdd" src="../../../Edit/img/ico/png/add.png" alt = "Новый" />
               
            </td>
            
            <td id="add">
                <asp:TextBox ID="txtAddTUP" runat="server"></asp:TextBox>
                <asp:Button ID="btnAddTUP" runat="server" Text="OK" OnClick="btnAddTUP_Click" />
                <% } %>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <uc1:Curriculum ID="CurrentCurriculum" runat="server" OnChanged="cc_Changed" />
            </td>
        </tr>

                  <% if (this.IsEditable)
                   { %>
        <tr>
            <td >
                <n2:SelectUser ID="SelectUser" runat=server ></n2:SelectUser>
              </td>
            
            <td >   
                <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                    Text="Присвоить пользователям" />
            </td>
        </tr>
                   <% } %>
     
        
    </table>
    
    <% }
       else
       {
    %>
    <div>
        не найдены курсы
    </div>
    <%  }    %>
</asp:Content>
