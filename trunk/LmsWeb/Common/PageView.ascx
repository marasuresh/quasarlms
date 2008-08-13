<%@ Control Language="c#" Inherits="DCE.Common.PageView" CodeFile="PageView.ascx.cs" %>
<script language="javascript">
function resizeFrame()
{
   theHeight = document.getElementById("contFrameId").contentWindow.document.body.scrollHeight;
   document.getElementById("contFrameId").style.height = theHeight;
}
</script>
<table height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
   <tr>
      <td height="100%" width="100%" valign="top">
         <iframe name="contFrame" width="100%" id="contFrameId" onresize="resizeFrame()" onLoad="resizeFrame()" frameborder="no" height="100%" align="top" scrolling="no" src="
<%
   this.Response.Write((this.url));
%>
 ">
         </iframe>
      </td>
   </tr>
</table>
