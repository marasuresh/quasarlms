<%@ Import Namespace="System.ComponentModel" %>
<%@ Control
		Language="C#"
		CodeBehind="MultiUpload.ascx.cs"
		Inherits="N2.Messaging.Messaging.UI.Parts.MultiUpload" %>
<style media="all" type="text/css">
	div.MultiUpload {
		border: 1px solid #CCCCCC;
		width: 100%;
	}
	div.MultiUpload .FileIndex, div.MultiUpload .FileName {
		color: #7E828C;
	}
	table.UploadedFiles {
		width: 100%;
		margin-bottom: 4px;
		margin-top: 4px;
	}
</style>
<div class="MultiUpload">
	<asp:Repeater
			ID="rptFiles"
			runat="server"
			OnItemCommand="rptFiles_ItemCommand">
		<HeaderTemplate><ol class="UploadedFiles"></HeaderTemplate>
		
		<FooterTemplate></ol></FooterTemplate>
		
		<ItemTemplate>
			<li><n2:FileSelector
						runat="server"
						ID="fs"
						Url='<%# Container.DataItem %>' />
				<asp:ImageButton
						runat="server"
						CommandName="Delete"
						ImageUrl="~/Edit/Img/ico/cross.gif"
						AlternateText="[Удалить]" />
			</li>



		</ItemTemplate>
	</asp:Repeater>
	
	<asp:ImageButton
			ImageUrl="~/Edit/Img/ico/add.gif"
			runat="server"
			AlternateText="Прикрепить"
			OnClick="btnAddFile_Click" />





</div>
