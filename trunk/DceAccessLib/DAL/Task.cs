using System;
using System.Collections.Generic;
using System.Data;

namespace DCEAccessLib.DAL
{
	public static class Task
	{
		public static DataSet GetSolutions(Guid id)
		{
			string _sql = @"
SELECT	dbo.StudentName(s.id,0) as StudentName,
		sol.*
FROM	dbo.Students s,
		dbo.TaskSolutions sol
WHERE	s.id = sol.Student
		AND sol.Task = '{0}'
";
			return DCEWebAccess.GetdataSet(
					string.Format(_sql, id),
					"sol");
		}
	}
}
