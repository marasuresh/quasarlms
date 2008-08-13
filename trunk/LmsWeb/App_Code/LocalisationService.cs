using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Threading;
using System.Globalization;

/// <summary>
/// Summary description for LocalisationService
/// </summary>
public static class LocalisationService {
	public static string Language {
		get {
			return Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName.ToLower().Replace("uk", "ua");
		}
	}

	/// <summary>
	/// ѕолучение default €зыка, используемого в системе
	/// </summary>
	public static  string DefaultLanguage {
		get {
			string _lang = DCE.Settings.getValue("dceLanguage");
				
			try {
				CultureInfo _culture = CultureInfo.GetCultureInfo(_lang.ToLower().Replace("ua", "uk"));
				return _culture.TwoLetterISOLanguageName.ToLower().Replace("uk", "ua");
			} catch(ArgumentException) {
				return "ru";
			}
		}
	}
}
