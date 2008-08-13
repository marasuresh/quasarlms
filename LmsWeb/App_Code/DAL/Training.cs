using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Data.SqlClient;

namespace DceAccessLib.DAL
{
	[DataObject]
	public static class TrainingController
	{
		public static DataRow Select(Guid trainingId, Guid studentId)
		{
			string _sql = @"
select	t.Course,
		t.TestOnly
from	dbo.Trainings t,
		dbo.AllStudentTrainings(@studentId) tr
where	tr.id=t.id
		and t.id = @trainingId
		and tr.id not in (
			select Training
			from dbo.TrainingBlocking
			where Student = @studentId
		)
";

			DataRow _result = null;

			DCE.dbData db = DCE.dbData.Instance;
			SqlCommand _cmd = db.Connection.CreateCommand();
			_cmd.CommandText = _sql;

			_cmd.Parameters.AddWithValue("@trainingId", trainingId);
			_cmd.Parameters.AddWithValue("@studentId", studentId);

			_cmd.Transaction = db.Transaction;
			_cmd.Connection = db.Connection;

			try {
				SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
				_cmd.Connection.Open();
				DataSet _ds = new DataSet();
				_adapter.Fill(_ds, "item");
				if (1 == _ds.Tables["item"].Rows.Count) {
					_result = _ds.Tables["item"].Rows[0];
				}

			} finally {
				_cmd.Connection.Close();
			}

			return _result;
		}

		public static DataSet Select(Guid studentId)
		{
			string _sql = @"
SELECT DISTINCT
		ISNULL(dbo.GetStrContentAlt(t.Name, @lang, l.Abbr), '') as text,
		'Welcome' as control,
		t.id,
		tr.id as trId
FROM	dbo.Trainings t,
		dbo.AllStudentTrainings(@studentId) tr,
		dbo.Courses c,
		dbo.Languages l
WHERE	tr.id = t.id
		AND c.id=t.Course
		AND t.isActive=1
		AND l.id=c.CourseLanguage
";
			DataSet _result = new DataSet("Items");

			DCE.dbData db = DCE.dbData.Instance;
			SqlCommand _cmd = db.Connection.CreateCommand();
			_cmd.CommandText = _sql;

			_cmd.Parameters.AddWithValue("@lang", LocalisationService.Language);
			_cmd.Parameters.AddWithValue("@studentId", studentId);

			_cmd.Transaction = db.Transaction;
			_cmd.Connection = db.Connection;

			try {
				SqlDataAdapter _adapter = new SqlDataAdapter(_cmd);
				_cmd.Connection.Open();
				_adapter.Fill(_result, "item");

			} finally {
				_cmd.Connection.Close();
			}

			return _result;
		}

		public static bool RecordsExist()
		{
			string _sql = @"
select	COUNT(t.id)
from	dbo.Trainings t
where	t.isActive=1
		and (	
			select	MIN(StartDate)
			from	Schedule
			where	Training=t.id) > {fn NOW()}";
			
			int _count = 0;
			
			DCE.dbData db = DCE.dbData.Instance;
			SqlCommand _cmd = db.Connection.CreateCommand();
			_cmd.CommandText = _sql;
			_cmd.Transaction = db.Transaction;
			_cmd.Connection = db.Connection;
			
			try {
				_cmd.Connection.Open();
				_count = (int)_cmd.ExecuteScalar();
			} finally {
				_cmd.Connection.Close();
			}
			
			return _count > 0;
		}
	}
}