<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Message.ascx.cs" Inherits="N2.Messaging.Messaging.UI.Parts.Message" %>
<%@ Import Namespace="N2.Web" %>
<n2:Box ID="messageBox" runat="server" CssClass="box" meta:resourcekey="BoxResource1">
    <div style="width: 100%">
        <asp:MultiView ID="mvMsgContent" runat="server">
            <asp:View ID="vShowMsg" runat="server">
                <table width="100%">
                    <tr>
                        <td width="15%">
                            <asp:Label ID="lbFrom" runat="server" Text="lbFrom" meta:resourcekey="lblFromResource1"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbFrom" runat="server" ReadOnly="True" Width="100%" meta:resourcekey="tbFromResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbSubject" runat="server" Text="lbSubject" meta:resourcekey="lbSubjectResource1"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbSubject" runat="server" ReadOnly="True" Width="100%" meta:resourcekey="tbSubjectResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbDate" runat="server" Text="lbDate" meta:resourcekey="lblDateResource1"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbDate" runat="server" ReadOnly="True" Width="100%" meta:resourcekey="tbDateResource1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div>
                                <asp:Image ID="imgAttach" runat="server" ImageUrl="~/Messaging/UI/Images/attach.png"
                                    meta:resourcekey="imgAttachResource1" />
                                <asp:HyperLink ID="hlAttach" runat="server" meta:resourcekey="hlAttachResource1">HyperLink</asp:HyperLink>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <n2:FreeTextArea ID="txtText" runat="server" TextMode="MultiLine" meta:resourcekey="txtTextResource1"
                                CssClass="freeTextArea" ReadOnly="True" Width="100%" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="vEditMsg" runat="server">
                <table style="table-layout: fixed" cellspacing="0" cellpadding="0" width="100%">
                    <tbody>
                        <tr>
                            <td width="20%">
                                <asp:Label ID="lblTo" runat="server" Text="lblTo" AssociatedControlID="txtTo" CssClass="label"
                                    meta:resourcekey="lblToResource1" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtTo" runat="server" CssClass="tb" meta:resourcekey="txtToResource1"
                                    Width="90%" />
                                <asp:RequiredFieldValidator ID="rfvTo" runat="server" ValidationGroup="CommentInput"
                                    ControlToValidate="txtTo" Text="*" Display="Dynamic" meta:resourcekey="rfvToResource1" />
                            </td>
                            <td width="5%">
                                <asp:Button ID="btnUsers" runat="server" Text="..." />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSubject" runat="server" Text="lblSubject" AssociatedControlID="txtSubject" CssClass="label"
                                    meta:resourcekey="lblSubjectResource1" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtSubject" runat="server" CssClass="tb" meta:resourcekey="txtSubjectResource1"
                                    Width="90%" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="CommentInput"
                                    ControlToValidate="txtSubject" Text="*" Display="Dynamic" meta:resourcekey="rfvSubjectResource1" />
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblUpload" runat="server" Text="lblUpload" AssociatedControlID="btnFileUpload" CssClass="label"
                                    meta:resourcekey="lblUploadResource1" />
                            </td>
                            <td>
                                <div>
                                    <asp:FileUpload ID="btnFileUpload" runat="server" />
                                    <asp:Image ID="imgAttachEdit" runat="server" ImageUrl="~/Messaging/UI/Images/attach.png"
                                        meta:resourcekey="imgAttachResource1" />
                                    <asp:HyperLink ID="hlAttachEdit" runat="server" meta:resourcekey="hlAttachResource1">HyperLink</asp:HyperLink>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3">
                                <n2:FreeTextArea ID="ftaEdit" runat="server" TextMode="MultiLine"
                                    CssClass="freeTextArea" EnableFreeTextArea="True" Width="100%" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </asp:View>
        </asp:MultiView>
        <asp:MultiView ID="mvActionPanel" runat="server" ActiveViewIndex="0">
            <asp:View ID="vMsg" runat="server">
                <div>
                    <asp:Button ID="btnToRecBin" runat="server" Text="ToRecBin" meta:resourcekey="btnRecBinResource1" />
                    <asp:Button ID="btnExit" runat="server" Text="Exit" meta:resourcekey="btnExitResource1" />
                </div>
            </asp:View>
            <asp:View ID="vDrMsg" runat="server">
                <div>
                    <asp:Button ID="btnSend" runat="server" Text="Send" 
                        meta:resourcekey="btnSendResource1" onclick="btnSend_Click" />
                    <asp:Button ID="btnExit2" runat="server" Text="Exit" meta:resourcekey="btnExitResource1" />
                </div>
            </asp:View>
            <asp:View ID="vRecMsg" runat="server">
                <div>
                    <asp:Button ID="btnRestore" runat="server" Text="Restore" meta:resourcekey="btnRestoreResource1" />
                    <asp:Button ID="btnExit3" runat="server" Text="Exit" meta:resourcekey="btnExitResource1" />
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</n2:Box>
