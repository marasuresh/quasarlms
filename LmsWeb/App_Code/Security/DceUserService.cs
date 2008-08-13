using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.ComponentModel;

[DataObject]
public static class DceUserService
{
	[DataObjectMethod(DataObjectMethodType.Select, false)]
    public static DceUser GetUserByLogin(string login)
    {
        SqlQueriesTableAdapters.Users usersAdapter = new SqlQueriesTableAdapters.Users();

        SqlQueries.UsersDataTable resultTable = usersAdapter.GetDataByLogin(login);
        if( resultTable.Rows.Count < 1 )
            return null;
        else if( resultTable.Rows.Count > 1 )
            throw new InvalidOperationException("LOCALIZE! Multiple users with same login.");

        return CreateUserFromRow(resultTable[0]);
    }

	[DataObjectMethod(DataObjectMethodType.Select)]
    public static DceUser GetUserByID(Guid id)
    {
        SqlQueriesTableAdapters.Users usersAdapter = new SqlQueriesTableAdapters.Users();

        SqlQueries.UsersDataTable resultTable = usersAdapter.GetDataByID(id);
        if( resultTable.Rows.Count < 1 )
            return null;
        else if( resultTable.Rows.Count > 1 )
            throw new InvalidOperationException("LOCALIZE! Multiple users with same login.");

        return CreateUserFromRow(resultTable[0]);
    }

    public static DceUser CheckUser(string login, string password)
    {
        SqlQueriesTableAdapters.PasswordInfo passwordInfoAdapter = new SqlQueriesTableAdapters.PasswordInfo();

        SqlQueries.PasswordInfoDataTable passwordInfoTable = passwordInfoAdapter.GetDataByLogin(login);
        if( passwordInfoTable.Rows.Count < 1 )
            return null;
        else if( passwordInfoTable.Rows.Count > 1 )
            throw new InvalidOperationException("LOCALIZE! Multiple users with same login.");

        SqlQueries.PasswordInfoRow infoRow = passwordInfoTable[0];

        if( infoRow.IsPasswordHashNull() || infoRow.IsPasswordHashSaltNull() )
            return null;

        Guid hash = infoRow.PasswordHash;
        Guid salt = infoRow.PasswordHashSalt;

        if( hash == HashServices.CalcPasswordHash(password, salt) )
            return GetUserByLogin(login);
        else
            return null;
    }

    public static void SetUserPassword(string login, string password)
    {
        SqlQueriesTableAdapters.StoredProcedures spAdapter = new SqlQueriesTableAdapters.StoredProcedures();
        
        Guid hash;
        Guid salt;

        HashServices.GeneratePasswordHash(password,out hash, out salt);

        spAdapter.dceaccess_SetUserPasswordInfo(login, hash, salt);
    }

	[DataObjectMethod(DataObjectMethodType.Insert, false)]
    public static void CreateUser(string login)
    {
        SqlQueriesTableAdapters.StoredProcedures spAdapter = new SqlQueriesTableAdapters.StoredProcedures();
        spAdapter.dceaccess_CreateUser(login);
    }

    public static void DeleteUserByLogin(string login)
    {
        throw new NotSupportedException();
    }

	[DataObjectMethod(DataObjectMethodType.Update)]
    public static void UpdateUser(DceUser user)
    {
        SqlQueriesTableAdapters.StoredProcedures spAdapter = new SqlQueriesTableAdapters.StoredProcedures();
        spAdapter.dceaccess_UpdateUser(
            user.ID,
            user.Login,
            user.FirstName,
            user.Patronymic,
            user.LastName,
            user.EMail,
            user.JobPosition,
            user.Comments,
            user.RegionID,
            user.RoleID);
    }

	[DataObjectMethod(DataObjectMethodType.Select)]
    public static DceUser[] GetAllUsers()
    {
        SqlQueriesTableAdapters.Users usersAdapter = new SqlQueriesTableAdapters.Users();

        SqlQueries.UsersDataTable resultTable = usersAdapter.GetData();

        DceUser[] resultArray = new DceUser[resultTable.Rows.Count];
        for( int i = 0; i < resultArray.Length; i++ )
        {
            resultArray[i] = CreateUserFromRow(resultTable[i]);
        }

        return resultArray;
    }

    static DceUser CreateUserFromRow(SqlQueries.UsersRow row)
    {
        DceUser resultUser = new DceUser();

        resultUser.ID = row.ID;
        resultUser.Login = row.Login;
        resultUser.FirstName = row.FirstName;
        resultUser.Patronymic = row.Patronymic;
        resultUser.LastName = row.LastName;
        resultUser.EMail = row.EMail;
        resultUser.JobPosition = row.JobPosition;
        resultUser.Comments = row.Comments;

        resultUser.CreateDate = row.CreateDate;
        resultUser.LastModify = row.LastModify;

        resultUser.RegionID = row.IsRegionIDNull() ? (Guid?)null : row.RegionID;
        resultUser.RegionName = row.RegionName;

        resultUser.RoleID = row.IsRoleIDNull() ? (Guid?)null : row.RoleID;
        resultUser.RoleName = row.RoleName;

        return resultUser;
    }

	public static string GetOldPlainTextPassword(Guid id)
	{
		return getScalarById(@"SELECT Password FROM [dbo].[Students] WHERE Id = @Id", id);
	}

	public static string GetRoleCodeNameById(Guid id)
	{
		return getScalarById(@"SELECT CodeName FROM [dbo].[Roles] WHERE Id = @Id", id);
	}

	static string getScalarById(string sql, Guid id)
	{
		string _result;
		
		DCE.dbData db = new DCE.dbData();
		SqlCommand _cmd = db.Connection.CreateCommand();
		_cmd.CommandText = sql;

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
	
	/// <summary>
	/// Manualy simulate MambershipUser.LastLoginDate
	/// when using ActiveDirectoryMembershipProvider
	/// and this property is not supported
	/// </summary>
	public static DateTime? LastEntry {
		get {
			DateTime _result;
			return DateTime.TryParse(DCE.Service.GetCook("LastEntry"), out _result)
					? _result
					: default(DateTime?);
		}
	}
}