<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderDetailsHead.ascx.cs" Inherits="Orders_OrderDetailsHead" %>
<asp:DetailsView
		ID="DetailsView1"
		runat="server"
		AutoGenerateRows="False"
		DataKeyNames="ID"
		DataSourceID="OrderDetailsDataSource"
		Width="100%"
		meta:resourcekey="DetailsView1Resource1">
    <Fields>
        <asp:BoundField DataField="StudentName" HeaderText="Student" ReadOnly="True" SortExpression="StudentName" meta:resourcekey="BoundFieldResource1" />
        <asp:BoundField DataField="CourseName" HeaderText="Course" ReadOnly="True" SortExpression="CourseName" meta:resourcekey="BoundFieldResource2" />
        <asp:BoundField DataField="RequestDate" HeaderText="Request Date" SortExpression="RequestDate" meta:resourcekey="BoundFieldResource3" />
        <asp:BoundField DataField="Comments" HeaderText="Comments" SortExpression="Comments" meta:resourcekey="BoundFieldResource4" />
        <asp:BoundField DataField="BestAvailableTrainingDate" HeaderText="Available Training Date"
            ReadOnly="True" SortExpression="BestAvailableTrainingDate" meta:resourcekey="BoundFieldResource5" />
    </Fields>
</asp:DetailsView>
<asp:SqlDataSource ID="OrderDetailsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Dce2005ConnectionString %>"
    SelectCommand="dcetools_Orders_GetOrderDetails" SelectCommandType="StoredProcedure"
    CancelSelectOnNullParameter="False">
    <SelectParameters>
        <asp:SessionParameter Name="homeRegion" SessionField="homeRegion" />
        <asp:QueryStringParameter DefaultValue="" Name="orderID" QueryStringField="id" />
    </SelectParameters>
</asp:SqlDataSource>
