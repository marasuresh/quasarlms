using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Helper routines for a core BCL Guid class
/// </summary>
public static class GuidService
{
	public static Guid? Parse(string value)
	{
		if (string.IsNullOrEmpty(value)) {
			return default(Guid?);
		}
		try {
			return new Guid(value);
		} catch (FormatException) {
			return default(Guid?);
		}
	}
	
	public static bool TryParse(string value, out Guid guid)
	{
		bool _result = false;
		Guid? _guid = GuidService.Parse(value);
		if (!_guid.HasValue) {
			guid = Guid.Empty;
		} else {
			guid = _guid.Value;
			_result = true;
		}
		return _result;
	}
}
