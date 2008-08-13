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


namespace DCEService
{
	[WebService(Namespace="http://edu.kvazar-micro.com/dce")]
	public sealed class DCEAccess: DceAccessService
	{
		[WebMethod]
		public override DCETransactionResult GetDataSet(string sqlString, string tableName)
		{
			return base.GetDataSet(sqlString, tableName);
		}

		[WebMethod]
		public override DCETransactionResult SaveDataSet(string sqlString, string tableName, DataSet ds)
		{
			return base.SaveDataSet(sqlString, tableName, ds);
		}

		[WebMethod]
		public override DCETransactionResult UpdateDataSet(string sqlString, string tableName, DataSet dataSet)
		{
			return base.UpdateDataSet(sqlString, tableName, dataSet);
		}

		[WebMethod]
		public override DCETransactionResult ExecSQL(string sqlString)
		{
			return base.ExecSQL(sqlString);
		}

		[WebMethod]
		public override DCETransactionResult UpdateDataSets(string[] sql, string[] tableName, System.Data.DataSet[] dataSets)
		{
			return base.UpdateDataSets(sql, tableName, dataSets);
		}
	}
}