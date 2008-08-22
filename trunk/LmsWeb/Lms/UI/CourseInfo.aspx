<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" %>

<%@ Register src="CourseIntro.ascx" tagname="CourseIntro" tagprefix="uc1" %>

<script runat="server">

</script>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCenterColumn" Runat="Server">
	<uc1:CourseIntro ID="CourseIntro1" runat="server" />
</asp:Content>

