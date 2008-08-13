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
	public abstract class DceAccessService
	{
		static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Dce2005ConnectionString"].ConnectionString;

		protected virtual DCEDbData.ADbData dbData {
			get { return new ADbData(ConnectionString); }
		}

		public virtual DCETransactionResult GetDataSet(string sqlString, string tableName)
		{
			try {
				return new DCETransactionResult(dbData.getDataSet(sqlString, "DataSet", tableName));
			} catch (System.Data.SqlClient.SqlException e) {
				return new DCETransactionResult(e);
			} catch (Exception ez) {
				return new DCETransactionResult(ez);
			}
		}

		public virtual DCETransactionResult SaveDataSet(string sqlString, string tableName, DataSet ds)
		{
			try {
				dbData.SaveDataSet(sqlString, tableName, ds);
				return new DCETransactionResult();
			} catch (System.Data.SqlClient.SqlException e) {
				return new DCETransactionResult(e);
			} catch (Exception ez) {
				return new DCETransactionResult(ez);
			}
		}

		public virtual DCETransactionResult UpdateDataSet(string sqlString, string tableName, DataSet dataSet)
		{
			try {
				return new DCETransactionResult(dbData.UpdateDataSet(sqlString, tableName, dataSet));
			} catch (System.Data.SqlClient.SqlException e) {
				return new DCETransactionResult(e);
			} catch (Exception ez) {
				return new DCETransactionResult(ez);
			}
		}

		public virtual DCETransactionResult ExecSQL(string sqlString)
		{
			int i = -1;
			try {
				i = dbData.ExecSQL(sqlString);
				return new DCETransactionResult();
			} catch (System.Data.SqlClient.SqlException e) {
				return new DCETransactionResult(e);
			} catch (Exception ez) {
				return new DCETransactionResult(ez);
			}
		}

		public virtual DCETransactionResult UpdateDataSets(string[] sql, string[] tableName, System.Data.DataSet[] dataSets)
		{
			try {
				System.Collections.ArrayList list = new System.Collections.ArrayList();

				for (int i = 0; i < dataSets.Length; i++) {
					DCEDbData.ADbData.DataSetUpdateBatch batch = new DCEDbData.ADbData.DataSetUpdateBatch();

					batch.sql = sql[i];
					batch.tableName = tableName[i];
					batch.dataSet = dataSets[i];
					list.Add(batch);
				}
				dbData.UpdateDataSets(list);
				return new DCETransactionResult();
			} catch (System.Data.SqlClient.SqlException e) {
				return new DCETransactionResult(e);
			} catch (Exception ez) {
				return new DCETransactionResult(ez);
			}
		}
	}
}