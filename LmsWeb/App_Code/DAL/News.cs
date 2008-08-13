using System;
using System.Data;
using System.Configuration;
using System.Data;
using System.ComponentModel;

using System.Data.SqlClient;

namespace DceAccessLib.DAL
{
	[DataObject]
	public static class NewsController
	{
		[DataObjectMethod(DataObjectMethodType.Select)]
		public static DataTable Select()
		{
			string _sql = @"
SELECT	n.id,
		dbo.GetStrContentAlt(n.Head,		@lang, @defLang) as head,
		dbo.GetStrContentAlt(n.Short,		@lang, @defLang) as short,
		dbo.GetStrContentAlt(n.MoreText,	@lang, @defLang) as moreText, 
		n.NewsDate as date,
		n.MoreHref as moreHref, 
		n.CourseCode as courseCode,
		c.eid as Image,
		c1.TData as text
FROM	Content c1
	RIGHT JOIN News n 
	LEFT JOIN Content c
		ON c.eid = Image 
		ON c1.id = dbo.GetTContentId(n.Text, @lang, @defLang)
ORDER BY NewsDate DESC";

			DataTable _result;
			
			DCE.dbData db = new DCE.dbData();
			SqlCommand _cmd = db.Connection.CreateCommand();
			_cmd.CommandText = _sql;
			
			_cmd.Parameters.AddWithValue("@lang", LocalisationService.Language);
			_cmd.Parameters.AddWithValue("@defLang", LocalisationService.DefaultLanguage);
			
			_cmd.Transaction = db.Transaction;
			_cmd.Connection = db.Connection;

			try {
				SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
				_cmd.Connection.Open();
				DataSet _ds = new DataSet();
				_adapter.Fill(_ds, "item");
				_result = _ds.Tables["item"];
				
			} finally {
				_cmd.Connection.Close();
			}
			
			return _result;
		}
	}
}