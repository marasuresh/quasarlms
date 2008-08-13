using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Reflection;

public static class MailErrorLog
{
    public static void SendMessage(string title, string message)
    {
        string errorLogAddress = ConfigurationManager.AppSettings["ErrorLogAddress"];
        if( string.IsNullOrEmpty(errorLogAddress) )
            return;

        MailNotificationServices.SendMessage(
            errorLogAddress,
            title,
            message);
    }

    static readonly bool IsDebugWebServer = DetectDebugWebServer();

    static bool DetectDebugWebServer()
    {
        try
        {
            System.Diagnostics.Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            if( Assembly.ReflectionOnlyLoadFrom(currentProcess.MainModule.FileName) != null )
                return true;
        }
        catch { }

        return false;
    }

    public static void SendErrorReport(Exception error)
    {
        if( IsDebugWebServer )
            return;

        StringWriter errorDump = new StringWriter();
        errorDump.Write(error.GetType().Name + " at " + DateTime.Now + " " + Environment.MachineName);
        int subCount = errorDump.ToString().Length;
        errorDump.WriteLine();
        errorDump.WriteLine(new string('=',subCount));
        errorDump.WriteLine();
        errorDump.WriteLine(error);
        errorDump.WriteLine();
        errorDump.WriteLine("Exception Details:");
        foreach( PropertyInfo prop in error.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance) )
        {
            if( prop.GetGetMethod()==null )
                continue;

            if( prop.GetGetMethod().GetParameters().Length>0 )
            {
                errorDump.WriteLine(" "+prop.Name+"["+prop.GetGetMethod().GetParameters().Length+" args]");
                continue;
            }

            errorDump.Write(" " + prop.Name + " = ");
            try
            {
                object value = prop.GetValue(error, new object[] { });
                if( value == null )
                    errorDump.WriteLine("(null)");
                else if( value.ToString() == value.GetType().FullName )
                    errorDump.WriteLine(value);
                else
                    errorDump.WriteLine(value+" ("+value.GetType().Name+")");
            }
            catch( Exception errorGetProperty )
            {
                errorDump.WriteLine("["+errorGetProperty.GetType().Name+"] "+error.Message);
            }
        }

        errorDump.WriteLine();
        errorDump.WriteLine("Session:");
        foreach( string sessionKey in HttpContext.Current.Session )
        {
            errorDump.WriteLine(" " + sessionKey + " = " + HttpContext.Current.Session[sessionKey]);
        }
        errorDump.WriteLine();
        errorDump.WriteLine();
        errorDump.WriteLine();

        SendMessage(
            error.GetType().Name + " at " + DateTime.Now + " " + Environment.MachineName,
            errorDump.ToString());

        string lastErrorFile = HttpContext.Current.Server.MapPath("~/App_Data/lasterror.txt");
        if( File.Exists(lastErrorFile) )
            File.AppendAllText(lastErrorFile, errorDump.ToString());
        else
            File.WriteAllText(lastErrorFile, errorDump.ToString());
    }
}
