using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using System.Data.SqlClient;

namespace DceAccessLib.DAL
{
	[DataObject]
	public class StudentController
	{
		[DataObjectMethod(DataObjectMethodType.Select)]
		public static DceUser GetByLogin(string usrLogin)
		{
			DceUser _result = null;
			const string _sql = @"
SELECT	id,
		Password,
		FirstName,
		LastName,
		Patronymic,
		Email
FROM	dbo.Students
WHERE	Login='{0}' OR Email='{0}'
";
			DataTable _tblUser = DCE.dbData.Instance.getDataSet(
					string.Format(_sql, usrLogin),
					"dataSet",
					"Students").Tables["Students"];
			if(1 == _tblUser.Rows.Count) {
				DataRow _row = _tblUser.Rows[0];
				_result = new DceUser();
				_result.Login = usrLogin;
				_result.ID = (Guid)_row["id"];
				_result.FirstName = _row["FirstName"] as string;
				_result.LastName = _row["LastName"] as string;
				_result.Patronymic = _row["Patronymic"] as string;
				_result.EMail = _row["Email"] as string;
			}
			return _result;
		}

		public static Guid? GetIdByLogin(string usrLogin)
		{
			string _sql = @"
SELECT Id FROM dbo.Students WHERE Login=@Login
";
			Guid? _result = default(Guid?);
			
			DCE.dbData db = DCE.dbData.Instance;
			SqlCommand _cmd = db.Connection.CreateCommand();
			_cmd.CommandText = _sql;
			_cmd.Parameters.AddWithValue("@Login", usrLogin);
			_cmd.Transaction = db.Transaction;
			_cmd.Connection = db.Connection;
			
			try {
				_cmd.Connection.Open();
				_result = (Guid?)_cmd.ExecuteScalar();
			} finally {
				_cmd.Connection.Close();
			}
			
			return _result;
		}

		public static string GetLoginById(Guid id)
		{
			string _result;
			string _sql = @"SELECT Login FROM [dbo].[Students] WHERE Id = @Id";
			
			DCE.dbData db = new DCE.dbData();
			SqlCommand _cmd = db.Connection.CreateCommand();
			_cmd.CommandText = _sql;

			_cmd.Parameters.AddWithValue("@Id", id);

			_cmd.Transaction = db.Transaction;
			_cmd.Connection = db.Connection;

			try {
				_cmd.Connection.Open();
				_result = (string)_cmd.ExecuteScalar();
			} finally {
				_cmd.Connection.Close();
			}
			return _result;
		}

		public static bool CheckCredentials(string login, string password)
		{
			string _sql = @"
SELECT	id
FROM	dbo.Students
WHERE	Login='{0}' AND Password='{1}'
";
			DataTable _dt = DCE.dbData.Instance.getDataSet(
					string.Format(_sql, login, password),
					"dataSet",
					"Students").Tables["Students"];
			return 1 == _dt.Rows.Count;
		}
		
		public static DataTable SelectByLogin(string usrLogin)
		{
			string _sql = @"
SELECT *
FROM	dbo.Students
WHERE	Login='{0}' OR Email='{0}'
";
			return DCE.dbData.Instance.getDataSet(
					string.Format(_sql, usrLogin),
					"dataSet",
					"Students").Tables["Students"];
		}
		
		public static DataTable Load(Guid id)
		{
			string _sql = @"
SELECT *
FROM	dbo.Students
WHERE	id='{0}'
";
			return DCE.dbData.Instance.getDataSet(
					string.Format(_sql, id),
					"dataSet",
					"Students").Tables["Students"];
		}
		
		public static DataTable GetName(Guid id, string lang)
		{
			string _sql = @"
SELECT	id,
		Email,
		dbo.StudentName(id, {0}) as studentName
FROM	dbo.Students
WHERE	id='{1}'
";
			return DCE.dbData.Instance.getDataSet(
					string.Format(_sql, lang=="EN" ? 1 :0, id),
					"dataSet",
					"Students").Tables["Students"];
		}
		
		[DataObjectMethod(DataObjectMethodType.Select)]
		public static DataSet SelectByTraining(Guid trainingId)
		{
			string _sql = @"
SELECT DISTINCT
		s.id,
		s.Photo,
		dbo.StudentName(s.id, {0}) as Name,
		s.Email
FROM	dbo.Students s
INNER JOIN
		dbo.AllTrainingStudents('{1}') a
	ON s.id=a.id
";
			return DCE.dbData.Instance.getDataSet(
					string.Format(_sql, LocalisationService.Language.Equals("EN", StringComparison.InvariantCultureIgnoreCase) ? 1 : 0, trainingId),
					"dataSet",
					"Students");
		}
		
		[DataObjectMethod(DataObjectMethodType.Update)]
		public static void Update(
				Guid id,
				string password,
				Guid photoId)
		{
			string _sql = @"
UPDATE dbo.Students
SET	LastLogin=GETDATE(),
	TotalLogins=TotalLogins + 1,
	Password='{1}',
	Photo='{2}'
WHERE Id='{0}'
";
			DCE.dbData.Instance.ExecSQL(
					string.Format(_sql, id, password, photoId));
		}
		
		[DataObjectMethod(DataObjectMethodType.Update)]
		public static void Update(
				Guid id,
				string login,
				string email)
		{
			string _sql = @"
UPDATE dbo.Students
SET	LastLogin=GETDATE(),
	TotalLogins=TotalLogins + 1,
	Login='{1}',
	Email='{2}'
WHERE Id='{0}'
";
			DCE.dbData.Instance.ExecSQL(
					string.Format(_sql, id, login, email));
		}
		
		[DataObjectMethod(DataObjectMethodType.Insert)]
		public static void Insert(
				string login,
				string email)
		{
			string _sql = @"
INSERT INTO
	dbo.Students(LastLogin, TotalLogins, Login, Email)
VALUES(GETDATE(), 0, '{0}', '{1}')
";
			DCE.dbData.Instance.ExecSQL(
					string.Format(_sql, login, email));
		}
		
		public static void Insert(
				string login,
				string email,
				string firstName,
				string lastName,
				string midName,
				string frsName,
				string lstName,
				int gender,
				DateTime birthDay)
		{
			string _sql = @"
INSERT INTO Students(
	Login,
	Email,
	FirstName,
	LastName,
	Patronymic,
	FirstNameEng,
	LastNameEng,
	Birthday,
	Sex)
VALUES(
	@Login,
	@Email,
	@FirstName,
	@LastName,
	@MidName,
	@FirstNameEng,
	@LastNameEng,
	@BirthDay,
	@Gender)
";
			DCE.dbData db = new DCE.dbData();
			SqlCommand _cmdInsert = db.Connection.CreateCommand();
			_cmdInsert.CommandText = _sql;
			
			_cmdInsert.Parameters.AddWithValue("@Login", login);
			_cmdInsert.Parameters.AddWithValue("@Email", email);
			_cmdInsert.Parameters.AddWithValue("@FirstName", firstName);
			_cmdInsert.Parameters.AddWithValue("@LastName", lastName);
			_cmdInsert.Parameters.AddWithValue("@MidName", midName);
			_cmdInsert.Parameters.AddWithValue("@FirstNameEng", frsName);
			_cmdInsert.Parameters.AddWithValue("@LastNameEng", lstName);
			_cmdInsert.Parameters.AddWithValue("@BirthDay", birthDay);
			_cmdInsert.Parameters.AddWithValue("@Gender", gender);

			db.ExecSQL(_cmdInsert);
		}
		
	}
}
