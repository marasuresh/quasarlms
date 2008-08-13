using System;
using System.Collections.Generic;
using System.Data;

namespace DCEAccessLib.DAL
{
	public static class Student
	{
		public static DataSet GetPracticeResults(Guid testId, Guid trainingId)
		{
			string _sql = @"
SELECT	s.FirstName,
		s.LastName,
		s.Patronymic,
		ISNULL(tr.Complete,0) Complete,
		tr.CompletionDate
FROM	dbo.Students s
	LEFT OUTER JOIN	dbo.TestResults tr
		ON s.id = tr.Student
		AND tr.Test ='{0}'
WHERE	s.id
	IN (
		SELECT DISTINCT
			id
		FROM	dbo.AllTrainingStudents('{1}')
	)
";

			return DCEWebAccess.GetdataSet(
				string.Format(_sql, testId, trainingId),
				"PracticeResults"
			);
		}
		
		public static DataSet GetTestResults(Guid testId, Guid trainingId)
		{
			string _sql = @"
SELECT	s.FirstName,
		s.LastName,
		s.Patronymic,
		tr.*,
		dbo.TestResultPoints(tr.id) as Points
FROM	dbo.Students s
	LEFT OUTER JOIN dbo.TestResults tr
		ON s.id = tr.Student
		AND tr.Test ='{0}'
WHERE	s.id
	IN (
		SELECT DISTINCT
				id
		FROM	dbo.AllTrainingStudents('{1}')
	)
";
			return DCEWebAccess.GetdataSet(
				string.Format(_sql, testId, trainingId),
				"TestResults"
			);
		}
		
		public static string GetName(Guid id)
		{
			string _sql = @"
SELECT	dbo.StudentName(s.id,0) AS StudentName 
FROM	dbo.Students s
WHERE	s.id = '{0}'
";
			DataTable _tblStudent = DCEWebAccess.WebAccess.GetDataSet(
					string.Format(_sql, id),
					"Students").Tables["Students"];
			
			return _tblStudent.Rows.Count > 0
				? _tblStudent.Rows[0][0].ToString()
				: string.Empty;
		}
		
		public static void Delete(Guid id)
		{
			string _sql = @"
DELETE FROM
		dbo.Students
WHERE	id='{0}'
";
			DCEAccessLib.DCEWebAccess.WebAccess.ExecSQL(
				string.Format(_sql, id));
		}
		
		public static DataSet GetDataSet()
		{
			string _sql = @"
SELECT *
FROM dbo.Students
";
			return DCEWebAccess.GetdataSet(_sql, "Students");
		}
	}
}
