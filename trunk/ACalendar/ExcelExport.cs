using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using XL = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Web.Security;
using N2.Lms.Items;
using N2.Workflow;
using N2.Lms.Items.Lms.RequestStates;

namespace N2.ACalendar
{
	using N2;
	using Microsoft.Office.Interop.Excel;
	
	public static class ExcelExport
	{
		public static string ExportToFile(IEnumerable<ContentItem> acalendars)
		{
			Application aXL = startExcel();
			string _path = "C:\\work\\Lms\\LmsWeb\\Upload\\";
			string _fileName = "ac.xls";

			aXL.Workbooks.Open(
					_path + _fileName, // FileName
						false, // UpdateLinks
						false, //  ReadOnly
						Type.Missing, // Format
						Type.Missing, // Password
						Type.Missing, // WriteResPassword
						Type.Missing, // IgnoreReadOnlyRecommended
						Type.Missing, // Origin
						Type.Missing, // Delimiter
						true, // Editable
						Type.Missing, //  Notify
						Type.Missing, // Converter
						false, // AddToMru
						Type.Missing, // Local
						Type.Missing // CorruptLoad
					);



			XL.Workbook wb = aXL.ActiveWorkbook; // или создать новую рабочую книгу, а то может поверх написать -  oXL.Workbooks.Add(Type.Missing);
			XL.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets["График УП"]; //  (Microsoft.Office.Interop.Excel.Worksheet)oXL.ActiveSheet;
			string[,] data = new string[acalendars.Count(), 52];
			int _row = 21;
			foreach (var _a in acalendars) {

				foreach (var _e in ((ACalendar)_a).Children) {
					int first_week = week_for_date(((AEvent)_e).DateStart);
					int last_week = week_for_date(((AEvent)_e).DateEnd);

					for (int j = first_week; j < last_week; j++) {
						data[(_row - 21), j - 1] = ((AEvent)_e).Title.Remove(1);
					}
				}
				worksheet.get_Range("A" + _row.ToString(), "A" + _row.ToString()).Value2 = _a.Title;
				_row++;
			}
			worksheet.get_Range("C21", "BB" + (_row - 1).ToString()).Value2 = data;

			wb.Save();
			aXL.Quit();
			return _fileName;
		}

		public static string ExportToFileZV(string[] users)
		{
			

			string[,] data = new string[users.Length, 3];
			int _row = 20;
			foreach (string _u in users) {

				data[(_row - 20), 0] = (_row - 19).ToString();
				data[(_row - 20), 1] = _u;
				data[(_row - 20), 2] = "";
                string [] roles = Roles.GetRolesForUser(_u);
                if (roles.Any())
                {
                 foreach (string r in roles )
                    if (!r.ToLower().StartsWith("st")  )  //ищем название группы
                        data[(_row - 20), 2] = r;
                }
                    _row++;
			}

            Microsoft.Office.Interop.Excel.Application aXL = null;
            string _path = "C:\\work\\Lms\\LmsWeb\\Reporting\\ReportFiles\\";
            //_path  = Configuration.  
            string _fileName = "zv.xls";

            try
            {
                aXL = startExcel();
                aXL.Visible = false;
            }
            catch
            {
                _fileName=_fileName.Replace(".xls",".xml");
                ExportToXML(data, _path + _fileName);
                return _fileName;

            }
            aXL.Workbooks.Open(
                     _path + _fileName, // FileName
                        false, // UpdateLinks
                        false, //  ReadOnly
                        Type.Missing, // Format
                        Type.Missing, // Password
                        Type.Missing, // WriteResPassword
                        Type.Missing, // IgnoreReadOnlyRecommended
                        Type.Missing, // Origin
                        Type.Missing, // Delimiter
                        true, // Editable
                        Type.Missing, //  Notify
                        Type.Missing, // Converter
                        false, // AddToMru
                        Type.Missing, // Local
                        Type.Missing // CorruptLoad
                    );

            XL.Workbook wb = aXL.ActiveWorkbook; // или создать новую рабочую книгу, а то может поверх написать -  oXL.Workbooks.Add(Type.Missing);
            XL.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet; //  (Microsoft.Office.Interop.Excel.Worksheet)oXL.ActiveSheet;

            
            
            worksheet.get_Range("A20", "C" + (_row - 1).ToString()).Value2 = data;
			worksheet.get_Range("A" + (_row).ToString(), "C" + (_row + 20).ToString()).ClearContents();

			//Range("M31").Select
			//With Selection.Interior
			//    .ColorIndex = 0
			//    .Pattern = xlGray25
			//    .PatternColorIndex = xlAutomatic
			//End With

			//worksheet.get_Range("E21", "E22").Interior.Pattern = "xlGray25";
			wb.Save();
			//aXL.Save( wb);
			aXL.Quit();
			aXL = null;
			//FinishExcel(aXL);

			return _fileName;

		}


        private static void ExportToXML(string[,] data, string fileName)
        {
            string[][] ss = new string[data.GetUpperBound(0)+1][];
            for (int i=0 ; i<=data.GetUpperBound(0); i++) 
            {
                string[] s = new string[data.GetUpperBound(1)+1];               
                for (int j=0 ; j<=data.GetUpperBound(1); j++) 
                {
                    s[j] = data[i,j];
                }
                ss[i] = s;
            }

            var sr = new System.Xml.Serialization.XmlSerializer(ss.GetType()); 
                    using(FileStream fs = new FileStream(fileName, FileMode.Create))
                    {
                        sr.Serialize(fs,ss);
                    }
 
           
            //StringBuilder sb = new StringBuilder();
            
            //StringWriter w = new StringWriter(sb, System.Globalization.CultureInfo.InvariantCulture);
            
            //// сериализуем
            //sr.Serialize(w, data);// получаем строку Xml
            //string xml = sb.ToString();
            //Console.WriteLine(xml); 
            //Serialization.XmlReader.
             //DataSet ds = new DataSet ();

            // создаем reader
            //StringReader reader = new StringReader(xml);// создаем XmlSerializer
            //XmlSerializer dsr = new XmlSerializer(typeof(DataClass));
            //// десериализуем 
            //DataClass clone = (DataClass)dsr.Deserialize(reader);            
            
            //throw new NotImplementedException();
        }

		public static string ExportToFileOZ(Request[] reqs, string studName)
		{
            Microsoft.Office.Interop.Excel.Application aXL = null;
            string _path = "C:\\work\\Lms\\LmsWeb\\Reporting\\ReportFiles\\";
            string _fileName = "oz.xls";
            string[,] data = new string[reqs.Length, 5];
            int _row = 13;
            foreach (Request _r in reqs)
            {
                N2.Workflow.Items.ItemState _s = _r.GetCurrentState();
                data[(_row - 13), 0] = (_row - 12).ToString(); ;
                data[(_row - 13), 1] = (_r.Course != null ? _r.Course.Title : string.Empty);
                data[(_row - 13), 2] = _r.RequestDate.ToShortDateString();
                data[(_row - 13), 3] = _r.User.ToString() ;
                data[(_row - 13), 4] = "-";
                if ((_s is AcceptedState) && ((AcceptedState)_s).Grade != 1)
                    data[(_row - 13), 4] = ((AcceptedState)_s).Grade.ToString();

                //if (Roles.GetRolesForUser(_u).Any()) data[(_row - 20), 2] = (Roles.GetRolesForUser(_u))[0];
                _row++;
            }

            try
            {
                aXL = startExcel();
                aXL.Visible = false;
            }
            catch
            {
                _fileName = _fileName.Replace(".xls", ".xml");
                ExportToXML(data, _path + _fileName);
                return _fileName;

            }
            
            
			aXL.Workbooks.Open(
					 _path + _fileName, // FileName
						false, // UpdateLinks
						false, //  ReadOnly
						Type.Missing, // Format
						Type.Missing, // Password
						Type.Missing, // WriteResPassword
						Type.Missing, // IgnoreReadOnlyRecommended
						Type.Missing, // Origin
						Type.Missing, // Delimiter
						true, // Editable
						Type.Missing, //  Notify
						Type.Missing, // Converter
						false, // AddToMru
						Type.Missing, // Local
						Type.Missing // CorruptLoad
					);

			XL.Workbook wb = aXL.ActiveWorkbook; // или создать новую рабочую книгу, а то может поверх написать -  oXL.Workbooks.Add(Type.Missing);
			XL.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet; //  (Microsoft.Office.Interop.Excel.Worksheet)oXL.ActiveSheet;
			worksheet.get_Range("B6", "B6").Value2 = studName;
			string[] roles = Roles.GetRolesForUser(studName);
                if (roles.Any()) 
                    foreach (string r in roles )
                         if (!r.ToLower().StartsWith("st")  )  //ищем название группы
                             worksheet.get_Range("C7", "C7").Value2 = r;


			//            <a href='<%= _req.TemplateUrl %>'>
			//    <%= _req.Title%></a>
			//<%= _req.RequestDate%>
			//<%= _req.Comments%>




			worksheet.get_Range("A13", "E" + (_row - 1).ToString()).Value2 = data;
			worksheet.get_Range("A" + (_row).ToString(), "E" + (_row + 20).ToString()).ClearContents();

			//Range("M31").Select
			//With Selection.Interior
			//    .ColorIndex = 0
			//    .Pattern = xlGray25
			//    .PatternColorIndex = xlAutomatic
			//End With

			//worksheet.get_Range("E21", "E22").Interior.Pattern = "xlGray25";
			wb.Save();
			//aXL.Save( wb);
			aXL.Quit();
			aXL = null;
			//FinishExcel(aXL);

			return _fileName;

		}

		public static string ExportToFileIRP(Request[] reqs)
		{
			Microsoft.Office.Interop.Excel.Application aXL = null;
			string _path = "C:\\work\\Lms\\LmsWeb\\Reporting\\ReportFiles\\";
			string _fileName = "irp.xls";
 
            var corses_req = from req in reqs group req by req.Course.Title into corsesGroup select new { cour = corsesGroup.Key, requests = corsesGroup.Count() };

            string[,] data = new string[reqs.Length, 5];
            int _row = 10;
            foreach (var _cours in corses_req)
            {
                data[(_row - 10), 0] = (_row - 9).ToString();
                data[(_row - 10), 1] = _cours.cour;
                //int _reqsCount = 0;  foreach (var _reqs in _cours.requests) _reqsCount++;
                data[(_row - 10), 2] = _cours.requests.ToString();
                _row++;
            }

            try
            {
                aXL = startExcel();
                aXL.Visible = false;
            }
            catch
            {
                _fileName = _fileName.Replace(".xls", ".xml");
                ExportToXML(data, _path + _fileName);
                return _fileName;

            }
          
            
            
            
            aXL.Workbooks.Open(
					 _path + _fileName, // FileName
						false, // UpdateLinks
						false, //  ReadOnly
						Type.Missing, // Format
						Type.Missing, // Password
						Type.Missing, // WriteResPassword
						Type.Missing, // IgnoreReadOnlyRecommended
						Type.Missing, // Origin
						Type.Missing, // Delimiter
						true, // Editable
						Type.Missing, //  Notify
						Type.Missing, // Converter
						false, // AddToMru
						Type.Missing, // Local
						Type.Missing // CorruptLoad
					);

			XL.Workbook wb = aXL.ActiveWorkbook; // или создать новую рабочую книгу, а то может поверх написать -  oXL.Workbooks.Add(Type.Missing);
			XL.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet; //  (Microsoft.Office.Interop.Excel.Worksheet)oXL.ActiveSheet;

			//            string[] words = { "LINQ", "позволяет", "сделать", "многое", "гораздо", "проще" };
			//            var groups =from word in words group word by word.Length into lengthGroups orderby lengthGroups.Key descending select new { Length = lengthGroups.Key, Words = lengthGroups };
			//foreach (var group in groups){Console.WriteLine("Слова длиной {0} символов", group.Length);
			//    foreach (string word in group.Words)Console.WriteLine(" " + word);

			worksheet.get_Range("A10", "C" + (_row - 1).ToString()).Value2 = data;
			worksheet.get_Range("A" + (_row).ToString(), "C" + (_row + 20).ToString()).ClearContents();

			wb.Save();
			aXL.Quit();
			aXL = null;
			return _fileName;

		}




		private static Microsoft.Office.Interop.Excel.Application startExcel()
		{
			Microsoft.Office.Interop.Excel.Application aXL = null;
			try {
				aXL = System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application")
					as Microsoft.Office.Interop.Excel.Application;
			} catch {
				// aXL = null;
			}
			if (aXL == null) aXL = new Microsoft.Office.Interop.Excel.Application();
			aXL.DisplayAlerts = false;
			aXL.Visible = false;
			return aXL;
		}

		private static void FinishExcel(Microsoft.Office.Interop.Excel.Application XL)
		{
			if (XL != null) {
				XL.ScreenUpdating = true;
				if (!XL.Interactive) XL.Interactive = true;
				XL.UserControl = true;
				if (XL.Workbooks.Count == 0) {
					XL.Quit();
				} else {
					if (!XL.Visible) XL.Visible = true;
					XL.ActiveWorkbook.Saved = true;
				}
				// System.Runtime.InteropServices.Marshal.ReleaseComObject(XL);
				XL = null;
				GC.GetTotalMemory(true); // вызов сборщика мусора
				// Пока не закрыть приложение EXCEL.EXE будет висеть в процессах
			}
		}

		public static int week_for_date(string _date)
		{
			DateTime in_date = Convert.ToDateTime(_date);
			DateTime first_sept = new DateTime(DateTime.Now.Year, 9, 1);
			int _d = 8 - Convert.ToInt32(first_sept.DayOfWeek);
			DateTime first_dayofweek = new DateTime(DateTime.Now.Year, 9, 1 + _d);
			if (first_dayofweek > in_date) return 1;
			TimeSpan _weeks = in_date - first_dayofweek;
			double _days = _weeks.Days / 7;
			int num_week = Convert.ToInt32(Math.Ceiling(_days + 1));
			return num_week + 1;
		}

		public static string week_name(int n)
		{
			DateTime first_sept = new DateTime(DateTime.Now.Year, 9, 1);
			int _d = 7 - Convert.ToInt32(first_sept.DayOfWeek);
			DateTime first_dayofweek = new DateTime(DateTime.Now.Year, 9, 1 + _d);
			string _ret = n.ToString() + " (";
			if (n == 1)
				_ret += first_sept.ToShortDateString() + " - " + first_dayofweek.ToShortDateString();
			else {
				_ret += first_dayofweek.AddDays((7 * (n - 2)) + 1).ToShortDateString();
				_ret += " - " + first_dayofweek.AddDays(7 * (n - 1)).ToShortDateString();
			}
			return _ret + ")";

		}


	}
 
    //[ XmlRoot("Data")] // изменим имя корневого элемента 
//    public class DataClass
//    {
//        public DataClassZV() { }

//        [XmlAttribute]
//        public string N  = Guid.NewGuid().ToString();
//        [XmlAttribute]
//        public string Name = "Just Name";
//        [XmlAttribute]
//        public string Name = "Just Name";



////        [XmlElement("Reserved")] // изменим имя xml элемента 
//        [XmlAttribute]
//        public Decimal Count = 10;
//        [XmlIgnore]
//        public DateTime Date = DateTime.Now;
//    }


}
