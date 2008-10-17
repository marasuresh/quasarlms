<%@ Control Language="C#" 
            AutoEventWireup="true" 
            CodeBehind="MessageInput.ascx.cs" 
            Inherits="N2.Messaging.Messaging.UI.Parts.MessageInput" %>
<%@ Import Namespace="N2.Web"%>
<n2:Box ID="commentInput" runat="server" CssClass="box" meta:resourcekey="BoxResource1">
    <table style="table-layout: fixed" cellspacing="0" cellpadding="0" width="100%">
        <tbody>
            <tr>
                <td width="20%">
                    <asp:Label ID="lblTo" runat="server" AssociatedControlID="txtTo" 
                        CssClass="label" meta:resourcekey="lblToResource1" />    
                </td>
                <td>
                    <asp:TextBox ID="txtTo" runat="server" CssClass="tb"
                        meta:resourcekey="txtToResource1" Width="90%" />
                    <asp:RequiredFieldValidator ID="rfvTo" runat="server" 
                        ValidationGroup="CommentInput" ControlToValidate="txtTo" Text="*" 
                        Display="Dynamic" meta:resourcekey="rfvToResource1" />    
                </td>
                <td width="5%">
                    <asp:Button ID="btnUsers" runat="server" Text="..." onclick="btnUsers_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSubject" runat="server" AssociatedControlID="txtSubject" 
                        CssClass="label" meta:resourcekey="lblSubjectResource1" />
                </td>
                <td>
                    <asp:TextBox ID="txtSubject" runat="server" CssClass="tb" 
                        meta:resourcekey="txtSubjectResource1" Width="90%" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ValidationGroup="CommentInput" ControlToValidate="txtSubject" Text="*" 
                        Display="Dynamic" meta:resourcekey="rfvSubjectResource1" />
                </td>
                <td>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUpload" runat="server" AssociatedControlID="btnFileUpload" 
                        CssClass="label" meta:resourcekey="lblUploadResource1" />
                </td>
                <td>
                    <asp:FileUpload ID="btnFileUpload" runat="server" Width="90%" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <br />
                </td>  
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <N2:FreeTextArea ID="txtText" runat="server" TextMode="MultiLine" 
                        meta:resourcekey="txtTextResource1" CssClass="freeTextArea" 
                        EnableFreeTextArea="True" Width="100%" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" 
                        Text="Submit" meta:resourcekey="btnSubmitResource1" />
                </td>
                <td align="right" colspan="2">
                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" 
                        Text="Cancel" meta:resourcekey="btnCancelResource1" />
                    <asp:Button ID="btnToDr" runat="server" OnClick="btnToDr_Click" 
                        Text="ToDr" meta:resourcekey="btnToDrResource1" />
                </td>
            </tr>
        </tbody>
    </table>
</n2:Box>
