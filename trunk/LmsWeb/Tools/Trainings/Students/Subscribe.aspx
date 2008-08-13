<%@ Page Language="C#" MasterPageFile="~/Tools/Template.master" AutoEventWireup="true" CodeFile="Subscribe.aspx.cs" Inherits="Trainings_Students_Subscribe" Title="Untitled Page" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="SubscribeGroupList.ascx" TagName="SubscribeGroupList" TagPrefix="uc1" %>

<%@ Register Src="../TrainingLink.ascx" TagName="TrainingLink" TagPrefix="mi" %>

<%@ Register Src="SubscribeStudentList.ascx" TagName="SubscribeStudentList" TagPrefix="mi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="mainContentPlaceHolder" Runat="Server">
    <mi:TrainingLink ID="TrainingLink1" runat="server" />
    <br />
    <br />
    <table width=100%><tr><td>
    <span runat=server id=subscribeCaption> Підписати студента:</span>
    </td><td align=right>
        <asp:CheckBox ID="multipleCheckBox" runat="server" Font-Size="80%" Text="Subscribe multiple students" meta:resourcekey="multipleCheckBoxResource1" /></td></tr> 
    </table>  
    <br />
    <mi:SubscribeStudentList ID="SubscribeStudentList" runat="server" PageSize="30" OnStudentSubscribed="SubscribeStudentList_StudentSubscribed" />
    <br />
    Підписати групу:<br />
    <uc1:SubscribeGroupList ID="SubscribeGroupList1" runat="server" />
</asp:Content>

