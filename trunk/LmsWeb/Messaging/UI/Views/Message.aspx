<%@ Page Title="" Language="C#" MasterPageFile="~/Messaging/Top+SubMenu.master" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="Messaging_UI_Message" %>

<asp:Content ID="Content3" ContentPlaceHolderID="TextContent" Runat="Server">
    <div style="width:100%">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Label ID="lbFrom" runat="server" Text="lbFrom" meta:resourcekey="lblFromResource1" ></asp:Label>    
                        </td>
                        <td>
                            <asp:TextBox ID="tbFrom" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>            
                        </td>  
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbSubject" runat="server" Text="lbSubject" meta:resourcekey="lbSubjectResource1" ></asp:Label>    
                        </td>
                        <td>
                            <asp:TextBox ID="tbSubject" runat="server" ReadOnly="true" Width="100%"></asp:TextBox>            
                        </td>  
                    </tr>
                </table>
                <br />
                <N2:FreeTextArea ID="txtText" runat="server" TextMode="MultiLine" 
                                meta:resourcekey="txtTextResource1" CssClass="freeTextArea" 
                                EnableFreeTextArea="True" ReadOnly="true" Width="100%"/>
    </div>
</asp:Content>