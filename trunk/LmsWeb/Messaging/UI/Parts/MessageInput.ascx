<%@ Control Language="C#" 
            AutoEventWireup="true" 
            CodeBehind="MessageInput.ascx.cs" 
            Inherits="N2.Messaging.Messaging.UI.Parts.MessageInput" %>
<%@ Import Namespace="N2.Web"%>
<n2:Box ID="commentInput" runat="server" CssClass="box" meta:resourcekey="BoxResource1">
    <div class="inputForm">
        <div class="row cf">
            <asp:Label ID="lblTo" runat="server" AssociatedControlID="txtTo" 
                CssClass="label" meta:resourcekey="lblToResource1" />
            <asp:TextBox ID="txtTo" runat="server" CssClass="tb"
                meta:resourcekey="txtToResource1" />
            <asp:RequiredFieldValidator ID="rfvTo" runat="server" 
                ValidationGroup="CommentInput" ControlToValidate="txtTo" Text="*" 
                Display="Dynamic" meta:resourcekey="rfvToResource1" />
        </div>
        <div class="row cf">
            <asp:Label ID="lblSubject" runat="server" AssociatedControlID="txtSubject" 
                CssClass="label" meta:resourcekey="lblSubjectResource1" />
            <asp:TextBox ID="txtSubject" runat="server" CssClass="tb" 
                meta:resourcekey="txtSubjectResource1" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ValidationGroup="CommentInput" ControlToValidate="txtSubject" Text="*" 
                Display="Dynamic" meta:resourcekey="rfvSubjectResource1" />
        </div>
        <br />
        <div class="row cf">
            <N2:FreeTextArea ID="txtText" runat="server" TextMode="MultiLine" 
                meta:resourcekey="txtTextResource1" CssClass="freeTextArea" 
                EnableFreeTextArea="True" />
        </div>
        <br />
        <div class="row cf" style="text-align:center">
            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" 
                Text="Submit" meta:resourcekey="btnSubmitResource1" />
            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" 
                Text="Cancel" meta:resourcekey="btnCancelResource1" />
        </div>
    </div>
</n2:Box>
