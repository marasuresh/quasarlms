using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.ComponentModel;
using System.Data.SqlClient;
using System.ComponentModel;

namespace DceAccessLib.DAL
{
	[DataObject]
	public static class TestController
	{
		public static bool RecordsExist()
		{
			string _sql = string.Format(@"
SELECT	COUNT(ts.id)
FROM	Tests ts,
		Entities e
WHERE	ts.Type = {0}
		and e.id=ts.id
		and e.Type={1}",
					(int)DCEAccessLib.TestType.globalquestionnaire,
					(int)DCEAccessLib.EntityType.globalquestionnaire);

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

		[DataObjectMethod(DataObjectMethodType.Select)]
		public static DataSet GetStatistics(Guid? courseId, Guid? studentId)
		{
			string select = @"
SELECT	dbo.TestResultPoints(tr.id) as Points,
		ts.id as testId,
		tr.*
FROM	dbo.TestResults tr,
		Tests ts
where	tr.Test=ts.id
		and ts.Parent = '" + courseId + @"'
		and tr.Student='" + studentId + @"'
		and ts.Type=1";
			DataSet _dsTestResults = DCE.dbData.Instance.getDataSet(select, "DataSet", "TestResults");
			
			string select0 = @"
SELECT	dbo.TestResultPoints(tr.id) as Points,
		ts.id as testId,
		tr.*,
		dbo.GetStrContentAlt(t.Name, '" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as Theme 
FROM	dbo.TestResults tr,
		Tests ts,
		Themes t 
where	tr.Test=ts.id
		and ts.Parent in(
			select id
			from dbo.Themes
			where Parent='"
				  + courseId + @"'
		)
		and tr.Student='" + studentId + @"'
		and t.id=ts.Parent
		and ts.Type=1
order by t.TOrder";
			DataSet _dsTestResults0 = DCE.dbData.Instance.getDataSet(select0, "DataSet", "TestResults");
			_dsTestResults0.Merge(_dsTestResults);
			return _dsTestResults0;
		}
	}
}
