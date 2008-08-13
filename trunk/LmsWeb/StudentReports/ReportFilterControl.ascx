<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportFilterControl.ascx.cs" Inherits="StudentReports_ReportFilterControl" %>
<table	width="100%"
		border="2"
		class="printer-invisible"><tr><td colspan=2 align=center>
    <h3>Фiльтри звiту</h3>
</td></tr>
<tr><td>

<table><tr><td>
Дата: вiд
</td><td>
    <asp:CheckBox ID="useStartDateCheckBox" runat="server" AutoPostBack=true />
    <asp:Calendar ID="startDateCalendar" runat="server"></asp:Calendar> 
</td><td>
до
</td><td>
    <asp:CheckBox ID="useEndDateCheckBox" runat="server" AutoPostBack=true />
    <asp:Calendar ID="endDateCalendar" runat="server"></asp:Calendar> 
</td></tr></table>
<hr width=50% />

<nobr>
Слухач:
<asp:TextBox ID="studentTextBox" runat="server"></asp:TextBox>
</nobr>
<nobr>
Група:
    <asp:DropDownList ID="groupDropDownList" runat="server">
    </asp:DropDownList>
</nobr>
<nobr>
Підрозділ банку:
<asp:TextBox ID="commentTextBox" runat="server"></asp:TextBox>
</nobr>
<nobr>
Регiон:
    <asp:DropDownList ID="regionDropDownList" runat="server">
    </asp:DropDownList>
</nobr>

<hr width=50% />

<nobr>
Область курсiв:
<asp:TextBox ID="courseDomainTextBox" runat="server"></asp:TextBox>
</nobr>
<nobr>
Курс:
<asp:TextBox ID="courseTextBox" runat="server"></asp:TextBox>
</nobr>
<nobr>
Тема:
<asp:TextBox ID="themeTextBox" runat="server"></asp:TextBox><br />
</nobr>
<nobr>
Тест:
<asp:TextBox ID="testTextBox" runat="server"></asp:TextBox>
</nobr>
</td><td>

    <asp:Button ID="applyButton" runat="server" Text="Застосувати" />

</td></tr></table>