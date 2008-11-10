<%@ Import Namespace="System.ComponentModel"%>
<%@ Control Language="C#" 
            AutoEventWireup="true" 
            Inherits="N2.Lms.Web.UI.MyAssignmentListControl" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="N2.Lms.Items" %>
<%@ Import Namespace="N2.Lms.Items.Lms.RequestStates" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="N2.Workflow" %>
<%@ Import Namespace="N2.Lms.Items.TrainingWorkflow" %>

<%@ Register Assembly="N2.Futures" Namespace="N2.Web.UI.WebControls" TagPrefix="n2" %>

<asp:ObjectDataSource 
	ID="dsMyGradedTrainings"
	runat="server"
	SelectMethod="FindMyGradedRequests"
	TypeName="N2.Lms.Items.MyAssignmentList"
	onobjectcreating="ds_ObjectCreating" EnableCaching="False" 
    OldValuesParameterFormatString="original_{0}" >
</asp:ObjectDataSource>

<n2:ChromeBox ID="ChromeBox1" runat="Server">
<asp:ListView
		ID="lv"
		runat="server"
		DataSourceID="dsMyGradedTrainings" >
	
	<LayoutTemplate>
		<table class="gridview" cellpadding="0" cellspacing="0">
			<tr class="header">
				<th>Тренинг</th>
				<th>Оценка</th>
			<tr id="itemPlaceholder" runat="server" />
		</table>
	</LayoutTemplate>
	
	<ItemTemplate>
		<tr class='<%# Container.DataItemIndex % 2 == 0 ? "row" : "altrow" %>'>
			<td><%# ((Request)Container.DataItem).Course.Title %> </td>
			<td><%# ((AcceptedState)((Request)Container.DataItem).GetCurrentState()).Grade %> </td>
		</tr>
	</ItemTemplate>
</asp:ListView>
</n2:ChromeBox>


