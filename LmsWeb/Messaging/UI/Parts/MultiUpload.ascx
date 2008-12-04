<%@ Import Namespace="System.ComponentModel" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiUpload.ascx.cs"
    Inherits="N2.Messaging.Messaging.UI.Parts.MultiUpload" %>
<style media="all" type="text/css">
    div.MultiUpload
    {
        border: 1px solid #CCCCCC;
        width: 100%;
    }
    div.MultiUpload .FileIndex, div.MultiUpload .FileName
    {
    	color: #7E828C;
    }
    table.UploadedFiles
    {
    	width: 100%;
    	margin-bottom: 4px;
    	margin-top: 4px;
    }
</style>
<div class="MultiUpload">
    <asp:Repeater ID="UploadedFiles" runat="server">
        <HeaderTemplate>
            <table class="UploadedFiles" cellpadding="1" cellspacing="0">
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td style="width: 5%; text-align:right">
                    <div class="FileIndex">
                        <%# (Container.ItemIndex + 1).ToString() + "." %></div>
                </td>
                <td style="width: 80%">
                    <div class="FileName">
                        <%# ((linkToFile)Container.DataItem).fileName %></div>
                </td>
                <td style="width: 15%">
                    <asp:LinkButton ID="btnDeleteFile" runat="server" OnClick="btnDeleteFile_Click">[Удалить]</asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <table width="100%">
        <tr>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
            <td style="text-align: right">
                <asp:Button ID="btnAddFile" runat="server" Text="Прикрепить" 
                    onclick="btnAddFile_Click" />
            </td>
        </tr>
    </table>
</div>
