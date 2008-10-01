<%@ Page Title="" Language="C#" MasterPageFile="~/Templates/UI/Layouts/Top+SubMenu.Master" AutoEventWireup="true" CodeFile="Message.aspx.cs" Inherits="Messaging_UI_Message" %>

<asp:Content ID="Content3" ContentPlaceHolderID="TextContent" Runat="Server">
    <n2:ItemDataSource runat="server" ID="dsMessage" />
    <asp:FormView runat="server" ID="fvMessage" DefaultMode="ReadOnly">
        <EmptyDataTemplate>
            Источник данных пуст.
        </EmptyDataTemplate>
        <ItemTemplate>
            <%# Eval("Subject") %><br />
            <%# Eval("Text") %>
        </ItemTemplate>
    </asp:FormView>
    <%--<asp:Repeater ID="Repeater1" runat="server" DataSourceID="dsMessage">
        <ItemTemplate>
            <%# Eval("Subject") %><br />
            <%# Eval("Text") %>       
        </ItemTemplate>
    </asp:Repeater>--%>
    <%--<n2:ItemEditor runat="server" ID="ie" />--%>
</asp:Content>