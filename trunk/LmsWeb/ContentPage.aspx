
<%@ Page language="c#" Inherits="DCE.ContentPage" CodeFile="ContentPage.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 
<html>
  <head>
    <title>ContentPage</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <base href="<%
      string path = this.Request["path"];
      string basePath = CoursesRootUrl;//+CourseRoot;
      int start = path.LastIndexOf(CourseRoot);
      int end = path.LastIndexOf('/');
      if (start > -1 && end > -1 && end > start)
         basePath += path.Substring(start, end-start+1);
      else
         basePath += CourseRoot;
      if (CoursesRootUrl.IndexOf("file:///") == -1)
      {
         int cpIndex = Request.Url.AbsoluteUri.IndexOf("contentpage.aspx");
         if (cpIndex != -1)
         {
            string origBase = Request.Url.AbsoluteUri.Substring(0, cpIndex);
            basePath = origBase+basePath;
         }
      }
      
      Response.Write(basePath);%>">
  </head>
  <body>
<%
try
{
   this.Server.Execute(path);
}
catch (System.Exception e)
{
}

%>
  </body>
</html>
