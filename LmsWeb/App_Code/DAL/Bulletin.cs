using System;
using System.Data;
using System.Configuration;
using System.ComponentModel;

[DataObject]
public static class Bulletin {
	
	[DataObjectMethod(DataObjectMethodType.Select)]
	public static DataSet GetByTraining(Guid? trainingId)
	{
		string _lang = LocalisationService.Language;
		string _defLang = LocalisationService.DefaultLanguage;
		
		string _sql = @"
SELECT	b.id,
		dbo.UserName(b.Author, {0}) as Author,
		u.Email,
		dbo.GetStrContentAlt(b.Message, '{1}','{2}') as Text,
		PostDate as Date,
		dbo.GetUserRole(b.Author, t.id) as UserRole
FROM	dbo.BulletinBoard b,
		dbo.Users u,
		dbo.Trainings t
WHERE	b.Training=t.id
		AND t.id='{3}'
		AND u.id=b.Author
ORDER BY
	b.PostDate
";
		System.Data.DataSet dsBulletins = DCE.dbData.Instance.getDataSet(
				string.Format(
						_sql,
						(_lang=="EN" ? 1 :0).ToString(),
						_lang,
						_defLang,
						trainingId),
				"DataSet",
				"Bulletins");
		return dsBulletins;
	}
}
