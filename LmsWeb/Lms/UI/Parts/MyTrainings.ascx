<%@ Control
		Language="C#"
		AutoEventWireup="true"
		Inherits="N2.Templates.Web.UI.TemplateUserControl`2[[N2.Templates.Items.AbstractContentPage, N2.Templates], [N2.Lms.Items.MyTrainings, N2.Lms]], N2.Templates" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="N2.Lms.Items" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="N2.Workflow" %>
<%@ Import Namespace="N2.Lms.Items.TrainingWorkflow" %>

<n2:H4 runat="server" Text='<%$ CurrentItem: Title %>' />

<n2:Box runat="server">
<% var _tickets = this.CurrentItem.RequestContainer.MyActiveTrainingTickets; %>
<table>
<% foreach (TrainingTicket _ticket in _tickets) { %>
	<tr><td><a href='<%= this.ResolveClientUrl(_ticket.Url) %>'><%= _ticket.Training != null ? _ticket.Training.Title : "Error: Training is NULL" %></a></td></tr>
<% } %>
</table>
</n2:Box>