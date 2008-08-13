using System;
using System.Collections.Generic;
using System.Data;

namespace DCEAccessLib.DAL
{
	public static class Reply
	{
		public static DataSet GetReplies(Guid topicId)
		{
			string _sql = @"
SELECT	e.Type,
		dbo.UserName(u.id,0) AS UserName,
		dbo.StudentName(s.id,0) AS StudentName,
		(IsNull(dbo.UserName(u.id,0),'')+IsNull(dbo.StudentName(s.id,0),'')) as ResName,
		r.PostDate,
		r.Message,
		r.id
FROM	dbo.ForumReplies r
	INNER JOIN	dbo.Entities e
		ON r.Author = e.id
	LEFT JOIN	dbo.Users u
		ON r.Author = u.id 
	LEFT JOIN	dbo.Students s
		ON r.Author= s.id
WHERE
	r.Topic = '{0}'
ORDER BY
	r.PostDate
";
			return DCEWebAccess.GetdataSet(
					string.Format(_sql, topicId),
					"replies");
		}
		
		public static DataRow LoadTopic(Guid id)
		{
			string _sql = @"
SELECT	t.Topic,
		t.Message,
		t.PostDate,
		t.Blocked,
		e.Type,
		dbo.UserName(u.id,0) AS UserName,
		dbo.StudentName(s.id,0) AS StudentName
FROM	dbo.ForumTopics t
	INNER JOIN	dbo.Entities e
		ON t.Author = e.id
	LEFT OUTER JOIN	dbo.Users u
		ON t.Author = u.id
	LEFT OUTER JOIN	dbo.Students s
		ON t.Author = s.id
WHERE
	(t.id = '{0}')
";
			DataTable _tblTopics = DCEWebAccess.GetdataSet(
					string.Format(_sql, id),
					"topic").Tables["topic"];

			return _tblTopics.Rows.Count > 0 ? _tblTopics.Rows[0] : null;
		}
	}
}
