<%@ Control Language="c#" Inherits="DCE.Common.About" CodeFile="About.ascx.cs" %>
<script language="javascript">
function resizeFrame()
{
   try
   {
      theHeight = document.getElementById("contFrameId").contentWindow.document.body.scrollHeight;
      
      if (theHeight != 0)
      {
         document.getElementById("contFrameId").style.height = theHeight+50;
      }
      else
      {
         document.getElementById("contFrameId").scrolling="auto";
         document.getElementById("contFrameId").style.height = 1000;
      }
      
   }
   catch(e)
   {
      document.getElementById("contFrameId").scrolling="auto";
      document.getElementById("contFrameId").style.height = 1000;
   }
}
</script>
<table height="100%" cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
   <tr>
      <td height="100%" width="100%" valign="top">
         <iframe name="contFrame" width="100%" id="contFrameId" onresize="javascript:resizeFrame();" onLoad="javascript:resizeFrame();" frameborder="no" height="100%" align="top" scrolling="no" src="<%
   string defLang = LocalisationService.DefaultLanguage;
   this.Response.Write((DCE.Service.DefaultLangPath.Replace("\\", "/") + "About.htm"));
%>">
         </iframe>
      </td>
   </tr>
</table>
