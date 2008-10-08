using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XL = Microsoft.Office.Interop.Excel;
using System.IO;

namespace N2.ACalendar
{
    public static class ExcelExport
    {
        public static string ExportToFile(IList<N2.ContentItem> acalendars)
        {
            Microsoft.Office.Interop.Excel.Application aXL = null;
                try
                {
                    aXL = System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application")
                        as Microsoft.Office.Interop.Excel.Application;
                }
                catch
                {
                    // aXL = null;
                }
            if (aXL == null) aXL = new Microsoft.Office.Interop.Excel.Application();

            string _path =  "C:\\work\\Lms\\LmsWeb\\Upload\\"; 
            string _fileName = "ac.xls";

            aXL.Workbooks.Open(
                    _path+_fileName, // FileName
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
              string[,] data = new string[ acalendars.Count , 52];
             int _row = 21;
             foreach (var _a in acalendars)
             {

                 foreach (var _e in ((ACalendar)_a).Children)
                 {
                     int first_week = week_for_date(((AEvent)_e).DateStart);
                     int last_week = week_for_date(((AEvent)_e).DateEnd);

                     for (int j = first_week; j < last_week; j++)
                     {
                         data[(_row - 21), j - 1] = ((AEvent)_e).Title.Remove(1);
                     }
                 }
                 worksheet.get_Range("A" + _row.ToString(), "A" + _row.ToString()).Value2 = _a.Title;
                 _row++;
             }
             worksheet.get_Range("C21", "BB"+ (_row-1).ToString()).Value2 = data;

             aXL.Visible = true;
            //aXL.Save( wb);
            //FinishExcel(aXL);
            return _fileName;
        }

        private static void FinishExcel(Microsoft.Office.Interop.Excel.Application XL)
        {
            if (XL != null)
            {
                XL.ScreenUpdating = true;
                if (!XL.Interactive) XL.Interactive = true;
                XL.UserControl = true;
                if (XL.Workbooks.Count == 0)
                {
                    XL.Quit();
                }
                else
                {
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
            DateTime in_date = Convert.ToDateTime (_date);
            DateTime first_sept = new DateTime(DateTime.Now.Year, 9, 1);
            int _d = 8 - Convert.ToInt32(first_sept.DayOfWeek);
            DateTime first_dayofweek = new DateTime(DateTime.Now.Year, 9, 1 + _d);
            if (first_dayofweek > in_date) return 1;
            TimeSpan _weeks = in_date - first_dayofweek;
            double _days = _weeks.Days / 7;
            int num_week = Convert.ToInt32(Math.Ceiling(_days+1));
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
            else
            {
                _ret += first_dayofweek.AddDays((7 * (n - 2)) + 1).ToShortDateString();
                _ret += " - " + first_dayofweek.AddDays(7 * (n - 1)).ToShortDateString();
            }
            return _ret + ")";

        }



    }
}
