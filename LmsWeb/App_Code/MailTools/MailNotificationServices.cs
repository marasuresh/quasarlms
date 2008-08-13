using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.IO;
using System.Threading;

public static class MailNotificationServices
{
    public static void SendSubscribeNotificationToSudent(
        Guid studentID,
        Guid trainingID)
    {
        TrainingDetails.dceaccess_GetUserByIDRow userRow =
            new TrainingDetailsTableAdapters.dceaccess_GetUserByIDTableAdapter()
                .GetData(studentID)[0];

        TrainingDetails.dcetools_Trainings_GetDetailsRow trainingRow =
            new TrainingDetailsTableAdapters.dcetools_Trainings_GetDetailsTableAdapter()
                .GetData(CurrentUser.Region.ID, trainingID)[0];

        if( userRow.IsEMailNull() )
            return;

        SendMessage(
            userRow.EMail,
            "Вас підписано на тренінг " + trainingRow.Name + " " + (trainingRow.IsRegionNameNull() ? "" : trainingRow.RegionName),
            @"Шановний користувачу!
Вас було підписано на тренінг дистанційного навчання " + (trainingRow.IsRegionNameNull() ? "" : trainingRow.RegionName) + @".
Для вивчення метеріалу тренінга та складання іспитів увійдіть до системи дистанційного навчання та натисніть кнопку <<Почати навчання>>.");
    }

    public static void SendMessage(
        string address,
        string subject,
        string body)
    {
        SmtpClient smtp = CreateSmtpClient();

        if( smtp == null )
            return;

        MailMessage message = new MailMessage(
            "postmaster@edu",
            address,
            subject,
            body);

        string sendErrorsFile = HttpContext.Current.Server.MapPath("~/App_Data/senderrors.log");

        try
        {
            smtp.Send(message);
        }
        catch( Exception error )
        {
            string dump = "\r\n\r\n" +
                smtp.Host + ":" + smtp.Port + " SMTP Send Failure to " + address + " " + subject + "\r\n" +
                error;

            if( File.Exists(sendErrorsFile) )
                File.AppendAllText(sendErrorsFile, dump);
            else
                File.WriteAllText(sendErrorsFile, dump);
        }
    }

    

    public static SmtpClient CreateSmtpClient()
    {
        string smtpSettings = ConfigurationManager.AppSettings["smtp"];
        if( string.IsNullOrEmpty(smtpSettings) )
            return null;

        int splitPos = smtpSettings.IndexOf(":");
        if( splitPos >= 0 )
        {
            string host = smtpSettings.Substring(0, splitPos);
            int port = int.Parse(smtpSettings.Substring(splitPos + 1), System.Globalization.CultureInfo.InvariantCulture);

            return new SmtpClient(host, port);
        }
        else
        {
            return new SmtpClient(smtpSettings);
        }
    }
}
