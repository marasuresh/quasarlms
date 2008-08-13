namespace DCE.Common
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	/// Заявка на трек курсов
	/// </summary>
	public partial  class TrackRquest : DCE.BaseWebControl
	{
      private System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

      protected void Page_Load(object sender, System.EventArgs e)
		{
         Guid? studentId = CurrentUser.UserID;
         DCE.Service.LoadXmlDoc(this.Page, doc, "CoursesCommon.xml");
         
         string trackId = this.Request.Form["TrackId"];
         if (trackId != null && trackId != "" && studentId.HasValue) {
            try
            {
				dbData.Instance.ExecSQL("insert into CTrackRequest (Student, CTrack) values ('" + studentId + "', '" + trackId + "')");
            }
            catch (System.Exception)
            {
            }
         }

         string requestId = this.Request.Form["TrackRemoveId"];
         if (requestId != null && requestId != "" && studentId.HasValue)
         {
            try
            {
				dbData.Instance.ExecSQL("delete from CTrackRequest where id = '" + requestId + "'");
            }
            catch (System.Exception)
            {
            }
         }

         if (studentId.HasValue) {
            // Размещенные заявки
			 System.Data.DataSet dsTracks = Student_GetRequests(studentId);
            System.Data.DataTable tableTracks = dsTracks.Tables["Tracks"];
            doc.DocumentElement.InnerXml += dsTracks.GetXml();

            // Список треков
			DataSet dsTracks0 = Student_GetTracks(studentId);
            tableTracks = dsTracks0.Tables["Tracks"];
            doc.DocumentElement.InnerXml += dsTracks0.GetXml();

			DataSet dsCourses = Course_Select();
			DataTable tableCourses = dsCourses.Tables["Courses"];

            System.Xml.XmlNode cNode = doc.SelectSingleNode("xml/CoursesList");
            if (cNode != null)
            {
               cNode.InnerXml += dsCourses.GetXml();
            }
         }

         System.Xml.Xsl.XslTransform trans = new System.Xml.Xsl.XslTransform();
         trans.Load(this.Page.MapPath(@"~/xsl/TrackRequest.xslt"));
         
         this.Xml1.Document = doc;
         this.Xml1.TransformArgumentList = new System.Xml.Xsl.XsltArgumentList();
         this.Xml1.Transform = trans;
      }

		private DataSet Course_Select()
		{
			string select1 = "select dbo.GetStrContentAlt(c.Name, '" +
			   LocalisationService.Language + @"', l.Abbr) as Name, 
               dbo.GetContentAlt(c.DescriptionShort,'" + LocalisationService.Language + @"', l.Abbr) as Description,
               dbo.GetStrContentAlt(ct.Name,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as Type,
               dbo.GetStrContentAlt(ctr.Name,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as Track,
               dbo.GetStrContentAlt(ctr.Description,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as TDescription,
               dbo.CourseDuration(c.id) as Duration, c.id as cId, c.isReady, ctr.id as TrackId,
               c.CPublic, c.Cost1, c.Cost2
            from dbo.CourseType ct right join
               dbo.Courses c inner join dbo.Languages l on l.id=c.CourseLanguage
               on c.Type=ct.id, CTracks ctr 
            where c.id in (select id from GroupMembers where mGroup=ctr.Courses) and c.IsReady=1
            order by dbo.GetStrContentAlt(ctr.Name, '" +
			   LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"')";

			DataSet dsCourses = dbData.Instance.getDataSet(select1, "DataSet", "Courses");
			return dsCourses;
		}

		private DataSet Student_GetTracks(Guid? studentId)
		{
			string select0 = @"select distinct ctr.id as TrackId,
               dbo.GetStrContentAlt(ctr.Name,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as Track,
               dbo.GetStrContentAlt(ctr.Description,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as TDescription
            from CTracks ctr, Courses c
            where c.id in (select id from GroupMembers where mGroup=ctr.Courses) 
               and ctr.id not in (select CTrack from CTrackRequest where Student = '" + studentId + @"')
               and c.IsReady=1";

			DataSet dsTracks0 = dbData.Instance.getDataSet(select0, "DataSet", "Tracks");
			return dsTracks0;
		}

		private System.Data.DataSet Student_GetRequests(Guid? studentId)
		{
			string select = @"select distinct cr.id as CTRequestId, ctr.id as TrackId,
               dbo.GetStrContentAlt(ctr.Name,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as Track,
               dbo.GetStrContentAlt(ctr.Description,'" + LocalisationService.Language + "','" + LocalisationService.DefaultLanguage + @"') as TDescription
            from CTracks ctr, CTrackRequest cr
            where cr.CTrack = ctr.id and cr.Student = '" + studentId + "'";

			DataSet dsTracks = dbData.Instance.getDataSet(select, "DataSetExist", "Tracks");
			return dsTracks;
		}
	}
}
