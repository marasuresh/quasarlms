<%@ Control Language="C#" AutoEventWireup="true" Inherits="N2.Templates.Web.UI.TemplateUserControl`2[[N2.Templates.Items.AbstractContentPage, N2.Templates], [N2.Lms.Items.MyAssignmentList, N2.Lms]], N2.Templates" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="N2.Lms.Items" %>
<%@ Import Namespace="N2.Lms.Items.Lms.RequestStates" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="N2.Workflow" %>
<%@ Import Namespace="N2.Lms.Items.TrainingWorkflow" %>

<script runat="server">
    
    protected override void OnInit(EventArgs e)
    {
        MyAssignmentList myAss = this.CurrentItem;
    }
</script>

<table>
    <tr>
        <th>
            Тренинг
        </th>
        <th>
            Оценка
        </th>
    </tr>
    <% foreach (Request _req in this.CurrentItem.RequestContainer.MyGradedAssignments)
       { %>
    <tr>
        <td>
            <%= _req.Course.Title %>
        </td>
        <td>
            <%= ((AcceptedState)_req.GetCurrentState()).Grade %>
        </td>
    </tr>
    <% } %>
</table>
