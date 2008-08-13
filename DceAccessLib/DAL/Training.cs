using System;
using System.Collections.Generic;
using System.Data;

namespace DCEAccessLib.DAL
{
	public static class Training
	{
		public static DataSet GetBlocking(Guid id)
		{
			string _sql = @"
SELECT DISTINCT
		s.LastName +' '+ s.FirstName +' ' + s.Patronymic as FIO,
		s.LastLogin,
		ts.id as StudentId,
		b.id,
		dbo.IdIsNotNull(b.id) as isBlocked
FROM	dbo.Students s,
		dbo.AllTrainingStudents('{0}') as ts
	LEFT JOIN	TrainingBlocking b
		ON (ts.id = b.Student
			AND b.Training='{0}')
WHERE s.id = ts.id
ORDER BY FIO
";
			return DCEWebAccess.GetdataSet(
					string.Format(_sql, id),
					"blocking");
		}
		
		public static DataSet GetForumTopics(Guid id)
		{
			string _sql = @"
SELECT	t.id,
		ISNULL(dbo.UserName(u.id,0),'')+ISNULL(dbo.StudentName(s.id,0),'') as Author,
		t.Topic,
		t.PostDate,
		t.Blocked,
		e.Type,
		dbo.UserName(u.id,0) AS UserName, 
		dbo.StudentName(t.Student,0) AS StudentName,
		dbo.NumReplies(t.id) AS NumAnswers
FROM	dbo.ForumTopics t
	LEFT OUTER JOIN	dbo.Entities e
		ON t.Author = e.id
	LEFT OUTER JOIN	dbo.Users u
		ON t.Author = u.id
	LEFT OUTER JOIN	dbo.Students s
		ON t.Author = s.id
WHERE	
	t.Training = '{0}'
";
			return DCEWebAccess.GetdataSet(
					string.Format(_sql, id),
					"topics");
		}
		
		public static DataSet GetTests(Guid id)
		{
			string _sql = @"

SELECT	s.LastName,
		s.FirstName as StudentName,
		s.Patronymic,
		dbo.GetThemeName(t.Parent,1) as ThemeName,
		t.Parent,
		tr.Complete,
		dbo.TestResultPoints(tr.id) as Points,
		t.Points as reqPoints,
		tr.CompletionDate,
		tr.Tries,
		tr.AllowTries,
		tr.id,
		tr.Skipped
FROM	dbo.Students s,
		Tests t,
		TestResults tr 
WHERE	tr.Student = s.id
		AND tr.Test = t.id
		AND t.id 
			IN (
				SELECT	id
				FROM	dbo.TrainingTests('{0}')
			)
		AND s.id
			IN (
				SELECT	id
				FROM	dbo.AllTrainingStudents('{0}')
			)
";
			return DCEWebAccess.GetdataSet(
					string.Format(_sql, id),
					"Tests");
		}

		public static DataSet GetStudents(Guid id)
		{
			string _sql = @"
SELECT	s.*
FROM	dbo.Students s,
		dbo.AllDistinctTrainingStudents('{0}') t
WHERE	t.id = s.id
";
			return DCEWebAccess.GetdataSet(
					string.Format(_sql, id),
					"Students");
		}
	}
}
