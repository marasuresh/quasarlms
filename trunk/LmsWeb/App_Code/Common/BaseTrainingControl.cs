using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.SessionState;
using System.Collections;

namespace DCE
{

    /// <summary>
    /// Базовый класс Web Control для проекта
    /// </summary>
    public abstract partial class BaseTrainingControl : BaseWebControl
    {
        /// <summary>
        /// Получить корневой каталог материалов курса
        /// </summary>
		protected string CoursesRootUrl {
			get {
				string root = this.ResolveUrl(Settings.CoursesRoot);
				
				Guid? studentId = CurrentUser.UserID;
				Guid? trainingId = Service.TrainingID;
				
				if(Request.Browser.Browser.ToUpper().Contains("IE") && studentId.HasValue && trainingId.HasValue ) {
					DataSet ds = DCE.dbData.Instance.getDataSet(
						string.Format(@"
SELECT	cdPath,
		useCDLib
FROM	CDPath 
WHERE	studentId='{0}'
		and trainingId='{1}'", studentId, trainingId),
						"dataSet",
						"CDPath");
					
					DataTable tablePath = ds.Tables["CDPath"];
					
					if( tablePath != null
							&& tablePath.Rows.Count == 1
							&& (bool)tablePath.Rows[0]["useCDLib"]
							&& !Convert.IsDBNull(tablePath.Rows[0]["cdPath"])) {
						Uri url = new Uri(this.Request.Url, ".");
						string croot = tablePath.Rows[0]["cdPath"].ToString().Replace("\\", "/");
						
						if (croot.Length > 0 && croot[croot.Length - 1] != '/') {
							croot += "/";
						}
						
						return "file:///" + croot;
					}
				}
				return root;
			}
		}
	}


}