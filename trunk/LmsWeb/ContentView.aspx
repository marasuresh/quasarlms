<%@ Page language="c#" Inherits="DCE.Migrated_ContentView" codePage="1251" CodeFile="ContentView.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="ContentView" Src="Common/ContentView.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
   <HEAD runat="server">
      <meta http-equiv="Content-Type" content="text/html; charset=windows-1251" />
<title>ContentView</title>
      <meta name="GENERATOR" Content="Microsoft Visual Studio 7.0" />
      <meta name="CODE_LANGUAGE" Content="C#" />
      <meta name="vs_defaultClientScript" content="JavaScript" />
      <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
   </HEAD>
   <body>
      <form method="post" runat="server" action="">
		  <asp:ScriptManager ID="ScriptManager1" runat="server">
		  </asp:ScriptManager>
         <uc1:ContentView id="ContentView1" runat="server" />
      </form>
   </body>
</HTML>
