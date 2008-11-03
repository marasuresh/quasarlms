<%@ Page
		Language="C#"
		MasterPageFile="~/Messaging/Top+SubMenu.master"
		AutoEventWireup="true"
		CodeBehind="MailBox.aspx.cs"
		Inherits="Messaging_UI_MailBox" %>

<script runat="server">
	protected override void OnInit(EventArgs e)
	{
		Register.StyleSheet(this.Page, "~/Lms/UI/_assets/css/grid.css");
		Register.StyleSheet(this.Page, "~/Lms/UI/_assets/css/round.css");
		Register.StyleSheet(this.Page, "~/Lms/UI/Css/MyAssignmentList.css");
		base.OnInit(e);
	}
</script>

<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="N2.Web" %>
<%@ Import Namespace="N2.Messaging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TextContent" runat="Server">
    <asp:MultiView ID="mvMailBox" runat="server" ActiveViewIndex="0">
        <asp:View ID="Tab1" runat="server">
            <div style="width: 100%;">
                <br />

<%	var _url = Url.Parse(this.CurrentPage.Url);
	var _filterLinks = new[] {
		new { Filter = MailBox.C.Filter.Letters, Image = "email_open", Title = "Письма" },
		new { Filter = MailBox.C.Filter.Announcements, Image = "bell", Title = "Объявления" },
		new { Filter = MailBox.C.Filter.Tasks, Image = "wrench", Title = "Задания" },
	};
	foreach (var _link in _filterLinks) { %>
	<a href='<%= _url.AppendSegment(string.Concat(
			"folder/",
			this.CurrentItem.Folder,
			"/filter/",
			_link.Filter)) %>'><img
				src='<%= this.ResolveClientUrl("~/Messaging/UI/Images/" + _link.Image + ".png") %>' />
		<%= _link.Title %>
	</a>
	<%
	}
%>
<p>
<a href='<%= _url.AppendSegment("new") %>'>Создать новое сообщение&hellip;</a>
</p>
                <br />
            </div>
        </asp:View>
        <asp:View ID="Tab2" runat="server">
            <br />
            <br />
        </asp:View>
        <asp:View ID="Tab3" runat="server">
            <br />
            <div>
                <asp:Button ID="btnEmptyRecBin" runat="server" Text="Очистить корзину" 
                    onclick="btnEmptyRecBin_Click" />        
            </div>
            <br />
        </asp:View>
    </asp:MultiView>
	<n2:ChromeBox runat="server">
		<asp:ObjectDataSource runat="server" ID="ds" 
			OldValuesParameterFormatString="original_{0}" 
			SelectMethod="GetFilteredFolderMessages" TypeName="N2.Messaging.MailBox" 
			onobjectcreating="ds_ObjectCreating" />
		
		<asp:ListView
				ID="lv"
				DataKeyNames="ID"
				runat="server"
				DataSourceID="ds">
	
			<LayoutTemplate>
				<table class="gridview" cellpadding="0" cellspacing="0">
					<col align="center" />
					<col align="left" />
					<col align="left" />
					<col align="left" width="50%" />
					<col align="center" width="20%" />
					
					<tr class="header">
						<th>Тип</th>
						<th>Автор</th>
						<th>Получатель</th>
						<th>Тема</th>
						<th>Дата</th>
					</tr>
					<tr id="itemPlaceholder" runat="server" />
				</table>
			</LayoutTemplate>
		
			<EmptyDataTemplate>
				<tr><td colspan="5" align="center">У вас нет сообщений.</td></tr>
			</EmptyDataTemplate>
			
			<ItemTemplate>
				<tr class='<%# Container.DataItemIndex % 2 == 0 ? "row" : "altrow" %>'>
					<td><asp:LinkButton runat="server" CommandName="Edit">
							<asp:Image runat="server" ImageUrl='<%# Eval("IconUrl") %>' />
						</asp:LinkButton></td>
					<td><%# Eval("From") %></td>
					<td><%# Eval("To") %></td>
					<td><asp:LinkButton runat="server" CommandName="Edit" Text='<%# Eval("Subject")%>' /></td>
					<td><%# Eval("Created") %></td>
				</tr>
			</ItemTemplate>
	
			<EditItemTemplate>
				<tr class='edit-info <%# Container.DataItemIndex == 0 ? "first" : string.Empty %>'>
					<td><asp:LinkButton ID="LinkButton1" runat="server" CommandName="Cancel">
							<asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("IconUrl") %>' />
						</asp:LinkButton></td>
					<td><%# Eval("From") %></td>
					<td><%# Eval("To") %></td>
					<td><asp:LinkButton runat="server" CommandName="Cancel" Text='<%# Eval("Subject")%>' /></td>
					<td><%# Eval("Created") %></td>
				</tr>
				<tr><td class="edit" colspan="5">
				<div class="details">
					<div class="header">Edit details for '<%# Eval("Title")%>'</div>
					<table class="detailview" cellpadding="0" cellspacing="0">
						<tr><th>Текст</th>
							<td><asp:TextBox Width="100%" Height="100%" ID="tbComment" TextMode="MultiLine" ReadOnly="true" runat="server" Text='<%# Eval("Text") %>' /></td>
						</tr> 
					</table>
					<div class="footer command">
						<a href='<%# Url.Parse(this.CurrentPage.Url).AppendSegment("reply/" + Eval("ID").ToString()) %>'>Ответить</a>
						<a href='<%# Url.Parse(this.CurrentPage.Url).AppendSegment("forward/" + Eval("ID").ToString()) %>'>Переслать</a>
						<asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel" />
					</div>
				</div>
			</td>
				</tr>
			</EditItemTemplate>
		</asp:ListView>
	</n2:ChromeBox>
</asp:Content>
