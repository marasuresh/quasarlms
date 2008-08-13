<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsList.ascx.cs" Inherits="News_NewsList" %>
<asp:GridView ID="NewsGridView" runat="server" AllowPaging="True" AllowSorting="True"
    AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="NewsTitleListDataSource" Width="100%" EmptyDataText="No news at this time." CssClass="dataList" GridLines="None" meta:resourcekey="NewsGridViewResource1">
    <Columns>
        <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
            <ItemTemplate>
                <asp:Image
						ID="newsIconImage"
						runat="server"
						ImageUrl="~/Images/news-item-yellow.gif"
						meta:resourcekey="newsIconImage" />
            </ItemTemplate>
            <ItemStyle Width="1px" />
        </asp:TemplateField>
        <asp:BoundField
            DataField="Date"
            HeaderText="Date"
            SortExpression="Date"
            DataFormatString="{0:d}" HtmlEncode="False" meta:resourcekey="BoundFieldResource1" >
            <ItemStyle VerticalAlign="Top" Width="1em" Wrap="False" />
        </asp:BoundField>
        <asp:TemplateField
				HeaderText="Title"
				SortExpression="Title"
				meta:resourcekey="TemplateFieldResource2">
            <ItemStyle VerticalAlign="Top" />
            <ItemTemplate>
                <asp:HyperLink ID="titleHyperLink" runat="server" NavigateUrl='<%# Eval("ID", "~/Tools/News/Details.aspx?id={0}") %>'
                    Text='<%# Eval("Title") %>' ToolTip='<%# Eval("ContentText") %>'></asp:HyperLink>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="RegionName" HeaderText="Region" ReadOnly="True" SortExpression="RegionName" meta:resourcekey="BoundFieldResource2" >
            <ItemStyle VerticalAlign="Top" />
        </asp:BoundField>
    </Columns>
    <RowStyle BackColor="White" />
    <HeaderStyle BackColor="SteelBlue" ForeColor="White" />
    <AlternatingRowStyle BackColor="Lavender" />
</asp:GridView>
<asp:SqlDataSource ID="NewsTitleListDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>"
    SelectCommand="dcetools_News_GetTitles" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
    <SelectParameters>
        <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" Type="Empty" />
    </SelectParameters>
</asp:SqlDataSource>
