using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace N2.Messaging
{
	public sealed class MailFactory
	{
		public ArrayList UploadFiles(List<HttpPostedFile> myFiles, string UploadPath, string UploadVirtualPath)
		{
			var urlToFileList = new ArrayList();

			foreach (HttpPostedFile myFile in myFiles) {
				//Short name of File.
				string strFileName = Path.GetFileName(myFile.FileName);
				string strUniqueName = Guid.NewGuid() + "$" + strFileName;

				string FilePath = UploadPath + strUniqueName;

				myFile.SaveAs(FilePath);

				urlToFileList.Add(UploadVirtualPath + strUniqueName);
			}

			return urlToFileList;
		}

		public static string[] GetRecipients(string userString)
		{
			//Example of userString = "student1; student2 student3,  student4: student5";
			var separators = new char[] { ',', ';', ':', ' ' };

			string[] users = userString.Split(separators, StringSplitOptions.RemoveEmptyEntries);

			return users;
		}
	}
}
