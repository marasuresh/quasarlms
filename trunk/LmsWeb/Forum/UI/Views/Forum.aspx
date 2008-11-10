<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forum.aspx.cs" Inherits="N2.Templates.Forum.UI.Views.Forum" %>

<%@ Register TagPrefix="yaf" Namespace="yaf" Assembly="yaf" %>
<%@ Register TagPrefix="yc" Namespace="yaf.controls" Assembly="yaf" %>

<asp:Content ID="defaultContent" ContentPlaceHolderID="TextContent" runat="server">

    <%-- Include the forum --%>
    <div class="yaf">
	    <yaf:forum runat="server" id="forum" />
    </div>
</asp:Content>