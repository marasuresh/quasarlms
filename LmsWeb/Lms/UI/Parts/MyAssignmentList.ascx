<%@ Control Language="C#" AutoEventWireup="true" Inherits="N2.Templates.Web.UI.TemplateUserControl`2[[N2.Templates.Items.AbstractContentPage, N2.Templates], [N2.Lms.Items.MyAssignmentList, N2.Lms]], N2.Templates" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="N2.Lms.Items" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="N2.Workflow" %>
<%@ Import Namespace="N2.Lms.Items.TrainingWorkflow" %>
<%@ Register TagPrefix="lms" TagName="MyCourses" Src="~/Lms/UI/Parts/MyAssignmentList/MyCourses.ascx" %>
<%@ Register TagPrefix="lms" TagName="MyRequests" Src="~/Lms/UI/Parts/MyAssignmentList/MyRequests.ascx" %>
<%@ Register TagPrefix="lms" TagName="MyTrainings" Src="~/Lms/UI/Parts/MyAssignmentList/MyTrainings.ascx" %>
<%@ Register TagPrefix="lms" TagName="TrainingsToGrade" Src="~/Lms/UI/Parts/MyAssignmentList/TrainingsToGrade.ascx" %>
<%@ Register TagPrefix="lms" TagName="MyGradedTrainings" Src="~/Lms/UI/Parts/MyAssignmentList/MyGradedTrainings.ascx" %>

<script runat="server">
    protected override void OnInit(EventArgs e)
    {
		Register.StyleSheet(this.Page, "~/Lms/UI/_assets/css/grid.css");
		Register.StyleSheet(this.Page, "~/Lms/UI/_assets/css/round.css");
		Register.StyleSheet(this.Page, "~/Lms/UI/Css/MyAssignmentList.css");
        base.OnInit(e);
    }
</script>

<n2:H4 runat="server" Text='<%$ CurrentItem: Title %>' />
<n2:Box runat="server">
    <style>
        .hidden
        {
            display: none;
        }
    </style>
    <% if (!this.Context.User.IsInRole("Administrators"))
       {%>
    <asp:Wizard runat="server" ID="Wizard1" Font-Names="Verdana" Font-Size="0.8em" CssClass="wza"
        BackColor="#EFF3FB" Height="300" Width="100%">
        <NavigationStyle Height="0" CssClass="hidden" />
        <StepStyle Font-Size="0.8em" ForeColor="#333333" VerticalAlign="Top" />
        <WizardSteps>
            <asp:WizardStep ID="WizardStep1" runat="server" Title="Предметы">
                <lms:MyCourses ID="MyCourses1" runat="server" />
            </asp:WizardStep>
            <asp:WizardStep ID="WizardStep2" runat="server" Title="Заявки">
                <lms:MyRequests ID="MyRequests1" runat="server" />
            </asp:WizardStep>
            <asp:WizardStep ID="WizardStep3" runat="server" Title="Тренинги">
                <lms:MyTrainings ID="MyTrainings2" runat="server" />
            </asp:WizardStep>
            <asp:WizardStep ID="WizardStep4" runat="server" Title="Оценки">
                <lms:MyGradedTrainings ID="MyGradedTrainings2" runat="server" />
            </asp:WizardStep>
        </WizardSteps>
        <StepStyle BackColor="White" CssClass="wzs" />
        <SideBarStyle VerticalAlign="Top" Width="150" />
        <SideBarTemplate>
            <asp:DataList runat="server" ID="SideBarList" Width="100%" SelectedItemStyle-CssClass="selected">
                <ItemTemplate>
                    <asp:LinkButton runat="server" ID="SideBarButton" CssClass="sba" />
                </ItemTemplate>
            </asp:DataList>
        </SideBarTemplate>
    </asp:Wizard>   
    <%
        }
       else
       {%>
    <asp:Wizard runat="server" ID="wza" Font-Names="Verdana" Font-Size="0.8em" CssClass="wza"
        BackColor="#EFF3FB" Height="300" Width="100%">
        <NavigationStyle Height="0" CssClass="hidden" />
        <StepStyle Font-Size="0.8em" ForeColor="#333333" VerticalAlign="Top" />
        <WizardSteps>
            <asp:WizardStep runat="server" Title="Заявки">
                <lms:MyRequests runat="server" />
            </asp:WizardStep>
            <asp:WizardStep runat="server" Title="Оценки">
                <lms:TrainingsToGrade ID="TrainingsToGrade1" runat="server" />
            </asp:WizardStep>
        </WizardSteps>
        <StepStyle BackColor="White" CssClass="wzs" />
        <SideBarStyle VerticalAlign="Top" Width="150" />
        <SideBarTemplate>
            <asp:DataList runat="server" ID="SideBarList" Width="100%" SelectedItemStyle-CssClass="selected">
                <ItemTemplate>
                    <asp:LinkButton runat="server" ID="SideBarButton" CssClass="sba" />
                </ItemTemplate>
            </asp:DataList>
        </SideBarTemplate>
    </asp:Wizard>
    <% } %>
</n2:Box>
