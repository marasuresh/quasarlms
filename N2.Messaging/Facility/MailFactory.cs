using System;
using System.IO;
using System.Web;

namespace N2.Messaging
{
    public sealed class MailFactory
    {
        public string[] UploadFile(HttpPostedFile myFile, string UploadPath, string UploadVirtualPath)
        {
            string[] urlToFile = new string[1];

            //Short name of File.
            string strFileName = Path.GetFileName(myFile.FileName);
            string strUniqueName = Guid.NewGuid() + "$" + strFileName;

            string FilePath = UploadPath + strUniqueName;

            myFile.SaveAs(FilePath);

            urlToFile[0] = UploadVirtualPath + strUniqueName;

            return urlToFile;
        }

        public string[] GetRecipients(string userString)
        {
            //Example of userString = "student1; student2 student3,  student4: student5";
            char[] separators = new char[] { ',', ';', ':', ' ' };

            string[] users = userString.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            return users;
        }
    }
}
