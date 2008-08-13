using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using DCEDbData;
using System.Xml;

using System.Configuration;
using System.Net.Sockets;
using System.Net;
using System.Web.Services.Protocols;
using System.Security.Authentication;

using sec = System.Web.Security;

namespace DCEService.Aval {
	[WebService(Namespace="http://edu.kvazar-micro.com/dce/2005")]
	public sealed class DCEAccess: DceAccessService
	{
		LoginToken m_login;
		
		protected override DCEDbData.ADbData dbData {
			get {
				return LoginGetDbData(m_login);
			}
		}

		[WebMethod]
		public DateTime PingTime() { return DateTime.Now; }

		[WebMethod]
		public void Login(string username, string password)
		{
            if( !sec.Membership.ValidateUser(username, password) )
                throw new AuthenticationException("Ім'я та пароль вказано невірно.");
            
			if(!sec.Roles.IsUserInRole(username, "Administrator"))
                throw new AuthenticationException("Доступ суворо заборонено.");
        }

        DCEDbData.ADbData LoginGetDbData(LoginToken login)
        {
            Login(login.Username, login.Password);

			return base.dbData;
		}

		[WebMethod]
		public DCETransactionResult GetDataSet(LoginToken login, string sqlString, string tableName)
		{
			this.m_login = login;
			return base.GetDataSet(sqlString, tableName);
		}

		[WebMethod]
		public DCETransactionResult SaveDataSet(LoginToken login, string sqlString, string tableName, DataSet ds)
		{
			this.m_login = login;
			return base.SaveDataSet(sqlString, tableName, ds);
		}

		[WebMethod]
		public DCETransactionResult UpdateDataSet(LoginToken login, string sqlString, string tableName, DataSet dataSet)
		{
			this.m_login = login;
			return base.UpdateDataSet(sqlString, tableName, dataSet);
		}
		
		[WebMethod]
		public DCETransactionResult ExecSQL(LoginToken login, string sqlString)
		{
			this.m_login = login;
			return base.ExecSQL(sqlString);
		}
		
		[WebMethod]
		public DCETransactionResult UpdateDataSets(LoginToken login, string[] sql, string[] tableName, System.Data.DataSet[] dataSets)
		{
			this.m_login = login;
			return base.UpdateDataSets(sql, tableName, dataSets);
		}
	}
}