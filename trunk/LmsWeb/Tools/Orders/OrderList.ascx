<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderList.ascx.cs" Inherits="Orders_OrderList" %>
<asp:GridView
		ID="ordersGridView"
		runat="server"
		AllowPaging="True"
		AllowSorting="True"
		AutoGenerateColumns="False"
		CssClass="dataList"
		DataKeyNames="ID"
		DataSourceID="OrdersDataSource"
		Width="100%"
		OnRowCommand="ordersGridView_RowCommand"
		GridLines="None"
		meta:resourcekey="ordersGridViewResource1">
    <Columns>
        <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
            <ItemTemplate>
                <asp:Image
						ID="orderIconImage"
						runat="server"
						ImageUrl="~/Images/order-greeb.gif"
						meta:resourcekey="orderIconImageResource1" />
            </ItemTemplate>
            <ItemStyle Width="1px" />
        </asp:TemplateField>
        
        <asp:BoundField DataField="StudentName" HeaderText="Student" ReadOnly="True" SortExpression="StudentName" meta:resourcekey="BoundFieldResource1" />
        <asp:BoundField DataField="CourseName" HeaderText="Course" ReadOnly="True" SortExpression="CourseName" meta:resourcekey="BoundFieldResource2" />
        <asp:BoundField DataField="Comments" HeaderText="Comments" SortExpression="Comments" meta:resourcekey="BoundFieldResource3" />
        <asp:BoundField DataField="RequestDate" HeaderText="Request Date" SortExpression="RequestDate" DataFormatString="{0:d}" HtmlEncode="False" meta:resourcekey="BoundFieldResource4" />
        <asp:BoundField DataField="BestAvailableTrainingDate" HeaderText="AvailableDate"
            ReadOnly="True" SortExpression="BestAvailableTrainingDate" DataFormatString="{0:d}" HtmlEncode="False" meta:resourcekey="BoundFieldResource5" />
        <asp:ButtonField CommandName="Subscribe" Text="Subscribe" meta:resourcekey="ButtonFieldResource1" />
        <asp:ButtonField CommandName="Reject" Text="Reject" meta:resourcekey="ButtonFieldResource2" />
    </Columns>
    <RowStyle BackColor="White" />
    <HeaderStyle BackColor="SteelBlue" ForeColor="White" />
    <AlternatingRowStyle BackColor="Lavender" />
</asp:GridView>
<asp:SqlDataSource ID="OrdersDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>"
    SelectCommand="dcetools_Orders_GetList" SelectCommandType="StoredProcedure" CancelSelectOnNullParameter="False">
    <SelectParameters>
        <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
    </SelectParameters>
</asp:SqlDataSource>
