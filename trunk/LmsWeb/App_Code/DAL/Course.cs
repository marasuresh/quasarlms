using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.ComponentModel;
using System.Data.SqlClient;

namespace DceAccessLib.DAL
{
	[DataObject]
	public static class CourseController
	{
		public static bool Exists(Guid courseId)
		{
			string _sql = @"
SELECT	COUNT(id)
FROM	dbo.Courses
WHERE	id = @courseId";

			bool _result;

			DCE.dbData db = DCE.dbData.Instance;
			SqlCommand _cmd = db.Connection.CreateCommand();
			_cmd.CommandText = _sql;

			_cmd.Parameters.AddWithValue("@courseId", courseId);

			_cmd.Transaction = db.Transaction;
			_cmd.Connection = db.Connection;

			try {
				_cmd.Connection.Open();
				_result = (int)_cmd.ExecuteScalar() == 1;
			} finally {
				_cmd.Connection.Close();
			}

			return _result;
		}
		
		public static DataRow Select(Guid courseId)
		{
			string _sql = @"
SELECT	c.id,
		l.Abbr as CourseLanguage,
		dbo.GetStrContentAlt(c.Name, @lang, l.Abbr) as Name,
		dbo.GetContentAlt(c.DescriptionShort, @lang, l.Abbr) as Description
from	dbo.Courses c,
		dbo.Languages l 
where	l.id = c.CourseLanguage
		and c.id=@courseId
";

			DataRow _result = null;

			DCE.dbData db = DCE.dbData.Instance;
			SqlCommand _cmd = DCE.dbData.Instance.Connection.CreateCommand();
			_cmd.CommandText = _sql;

			_cmd.Parameters.AddWithValue("@lang", LocalisationService.Language);
			_cmd.Parameters.AddWithValue("@courseId", courseId);

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
		
		public static DataSet SelectAreas(Guid? parentId)
		{
			string select = @"
select	cd.id,
		'CoursesCommon' as control,
		dbo.GetStrContentAlt(cd.Name,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as text
from	dbo.CourseDomain cd
where	cd.Parent " + (!parentId.HasValue ? "is NULL" : "='" + parentId.Value + "'") + @"
		and dbo.isAreaHasCourses(cd.id)=1";
			
			return DCE.dbData.Instance.getDataSet(select, "dataSet", "item");
		}

		public static bool RecordsExist()
		{
			string _sql = @"select COUNT(id) from dbo.Courses where isReady=1";

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

		public static bool TrackRecordsExist()
		{
			string _sql = @"
select	COUNT(c.id)
from	dbo.Courses c,
		CTracks ctr 
where	c.id in (
			select	id
			from	GroupMembers
			where mGroup = ctr.Courses)
		and c.IsReady=1";

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
	
		public static DataSet SelectByTraining(Guid? trainingId)
		{
			string select = string.Format(@"
select	c.id,
		dbo.GetStrContentAlt(c.Name, '{0}','{1}') as Name,
		dbo.GetContentAlt(c.DescriptionShort, '{0}','{1}') as Description,
		t.StartDate,
		t.EndDate,
		t.Code,
		t.Instructors,
		t.Curators
from	dbo.Courses c,
		dbo.Trainings t
where	t.Course=c.id 
		and t.id='{2}'",
						LocalisationService.Language,
						LocalisationService.DefaultLanguage,
						trainingId);

			return DCE.dbData.Instance.getDataSet(select, "dataSet", "Courses");
		}

		[DataObjectMethod(DataObjectMethodType.Select)]
		public static DataSet SelectByCodeOrId(string reqCourseCode, Guid? reqCourseId, string CoursesRoot)
		{
			string select0 = null;
			DataSet dsCourses = null;
			if (!string.IsNullOrEmpty(reqCourseCode)) {
				select0 = @"
               select c.id, 
                  dbo.GetStrContentAlt(c.Name,'" + LocalisationService.Language + @"', l.Abbr) as Name,
                  '" + CoursesRoot + @"' as cRoot, c.DiskFolder, l.Abbr as CourseLanguage,
                  dbo.GetStrContentAlt(c.DescriptionLong,'" + LocalisationService.Language + @"', l.Abbr) as FullDescription, 
                  dbo.GetContentAlt(c.DescriptionShort,'" + LocalisationService.Language + @"', l.Abbr) as Description,
                  dbo.GetStrContentAlt(c.Author,'" + LocalisationService.Language + @"', l.Abbr) as Author,
                  dbo.GetStrContentAlt(c.Requirements,'" + LocalisationService.Language + @"', l.Abbr) as Requirements, 
                  dbo.GetContentAlt(c.Keywords,'" + LocalisationService.Language + @"', l.Abbr) as Keywords
               from dbo.Courses c, dbo.Languages l
               where l.id=c.CourseLanguage and c.Code='" + reqCourseCode + "'";
			} else if (reqCourseId.HasValue) {
				select0 = @"
select	c.id, 
		dbo.GetStrContentAlt(c.Name,'" + LocalisationService.Language + @"',l.Abbr) as Name,
		'" + CoursesRoot + @"' as cRoot, c.DiskFolder, l.Abbr as CourseLanguage,
                  dbo.GetStrContentAlt(c.DescriptionLong,'" + LocalisationService.Language + @"', l.Abbr) as FullDescription, 
                  dbo.GetContentAlt(c.DescriptionShort,'" + LocalisationService.Language + @"', l.Abbr) as Description,
                  dbo.GetStrContentAlt(c.Author,'" + LocalisationService.Language + @"', l.Abbr) as Author,
                  dbo.GetStrContentAlt(c.Requirements,'" + LocalisationService.Language + @"', l.Abbr) as Requirements, 
                  dbo.GetContentAlt(c.Keywords,'" + LocalisationService.Language + @"', l.Abbr) as Keywords
               from dbo.Courses c, dbo.Languages l
               where l.id=c.CourseLanguage and c.id='" + reqCourseId + "'";
			}

			if (!string.IsNullOrEmpty(select0)) {
				dsCourses = DCE.dbData.Instance.getDataSet(select0, "dataSet", "Courses");
			}
			return dsCourses;
		}
	}
}