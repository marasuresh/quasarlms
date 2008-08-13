using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;

public static class PageParameters
{
    static HttpSessionState Session
    {
        get { return HttpContext.Current.Session; }
    }

    static HttpRequest Request
    {
        get { return HttpContext.Current.Request; }
    }

    public static Guid? ID {
        get {
			return GuidService.Parse(Request["id"]);
        }
    }

	public static int? Id {
		get {
			int _result;
			if (Int32.TryParse(Request.Params["id"], out _result)) {
				return _result;
			} else {
				return default(int?);
			};
		}
	}
	
    public static Guid? Msg {
        get {
			return GuidService.Parse(Request["msg"]);
        }
    }

    public static Guid? trId {
        get {
			Guid? _result = GuidService.Parse(Request["trId"]);
			if (!_result.HasValue) {
				_result = GuidService.Parse(Request.Params["trainingId"]);
			}
			return _result;
		}
    }
	
	public static Guid? courseId {
		get {
			Guid? _result = GuidService.Parse(Request.Params["courseId"]);
			if (!_result.HasValue) {
				_result = DCE.Service.courseId;
				if (!_result.HasValue) {
					_result = GuidService.Parse(Request.Params["rcId"]);
				}
			}
			return _result;
		}
	}
}
