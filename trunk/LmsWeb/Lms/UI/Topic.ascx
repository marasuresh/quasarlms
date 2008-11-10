<%@ Control
		Language="c#"
		Inherits="N2.Templates.Web.UI.TemplateUserControl`2[[N2.Templates.Items.AbstractContentPage, N2.Templates], [N2.Lms.Items.Topic, N2.Lms]], N2.Templates" %>
<h3 class="cap3"><%= this.CurrentItem.Title %></h3>
<div><%= this.CurrentItem.Text %></div>