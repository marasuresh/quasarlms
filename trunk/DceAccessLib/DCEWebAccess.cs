using System;
using System.Data;
using Microsoft.Win32;
using System.Windows.Forms;
namespace DCEAccessLib
{

   public class DataSetBatchUpdate : System.Object
   {

      public DataSetBatchUpdate()
      {
      }
      public string sql=null;
      public string tableName=null;
      public DataSet dataSet=null;
   };

//   class DCEWebAccessService
//   {
//      public static DCEService.DCEAccess Service = new DCEService.DCEAccess();
//   }

	/// <summary>
	/// Доступ к БД через WEB Service
	/// </summary>
	/// 
	public class DCEWebAccess
	{
      public static DceService.DCEAccess Service = new DceService.DCEAccess();

      /// <summary>
      /// Статический объект для доступа к WEB Сервису
      /// </summary>
      public static DCEWebAccess WebAccess = new DCEWebAccess();

      public DCEWebAccess()
		{
		}

      /// <summary>
      /// Выполнить SQL запрос
      /// </summary>
      /// <param name="sqlString"></param>
      /// <returns></returns>
      public static int execSQL(string sqlString)
      {
         DceService.DCETransactionResult res = DCEWebAccess.Service.ExecSQL(sqlString);
         if (res.Result != DceService.TransactionResult.Success)
         {
            ShowSqlException(res);
            return -1;
         }
         return 0;
      }

      /// <summary>
      /// Выполнить SQL запрос
      /// </summary>
      /// <param name="sqlString"></param>
      /// <returns></returns>
      public int ExecSQL(string sqlString)
      {
         DceService.DCETransactionResult res = DCEWebAccess.Service.ExecSQL(sqlString);
         if (res.Result != DceService.TransactionResult.Success)
         {
            ShowSqlException(res);
            return -1;
         }
         return 0;
      }

      /// <summary>
      /// Обработка SQL исключений
      /// </summary>
      /// <param name="e"></param>
      public static void ShowSqlException(DceService.DCETransactionResult e)
      {
         if (e.Result == DceService.TransactionResult.SqlException)
            System.Windows.Forms.MessageBox.Show("Ошибка при обращении к базе данных:\n" 
               +e.Message + "\nКласс ошибки:"+e.Class.ToString()+"\nСостояние:"+e.State.ToString()
               +"\nНомер ошибки:"+e.Number.ToString()+"\n"+e.SqlString,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error
            );
         if (e.Result == DceService.TransactionResult.UnhandledException)
         {
            if (e.ExceptionClass != "System.Data.DBConcurrencyException")
            System.Windows.Forms.MessageBox.Show("При обращении к базе данных возникло исключение.\n" 
               + "Класс ошибки: "+e.ExceptionClass
               + "\nОписание ошибки: " +e.Message 
               ,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error
               );
         }
      }

      /// <summary>
      /// Получить DataSet
      /// </summary>
      /// <param name="sqlString"></param>
      /// <param name="tableName"></param>
      /// <returns></returns>
      public static System.Data.DataSet GetdataSet(string sqlString, string tableName)
      {
         DceService.DCETransactionResult res = DCEWebAccess.Service.GetDataSet(sqlString, tableName);
         if (res.Result == DceService.TransactionResult.Success)
         {
            return res.dataSet;
         }
         else
         {
            ShowSqlException(res);
            return null;
         }
      }

      /// <summary>
      /// Получить DataSet
      /// </summary>
      /// <param name="sqlString"></param>
      /// <param name="tableName"></param>
      /// <returns></returns>
      public System.Data.DataSet GetDataSet(string sqlString, string tableName)
      {
         
         DceService.DCETransactionResult res = DCEWebAccess.Service.GetDataSet(sqlString, tableName);
         if (res.Result == DceService.TransactionResult.Success)
         {
            return res.dataSet;
         }
         else
         {
            ShowSqlException(res);
            return null;
         }

      }

      /// <summary>
      /// Подучить строку по SQL запросу
      /// </summary>
      /// <param name="sql"></param>
      /// <returns></returns>
      public string GetString(string sql)
      {
         DataTable tbl=null;
         DceService.DCETransactionResult res = DCEWebAccess.Service.GetDataSet(sql, "t");
         if (res.Result == DceService.TransactionResult.Success)
         {
            tbl = res.dataSet.Tables["t"];
            if (tbl.Rows.Count>0)
            {
               return tbl.Rows[0][0].ToString();
            }
         }
         else
         {
            ShowSqlException(res);
            return "";
         }
         return "";
      }

      /// <summary>
      /// Обновить DataSet
      /// </summary>
      /// <param name="sqlString"></param>
      /// <param name="tableName"></param>
      /// <param name="dataSet"></param>
      /// <returns></returns>
      public System.Data.DataSet UpdateDataSet(string sqlString,
         string tableName, ref System.Data.DataSet dataSet)
      {
         if (dataSet != null)
         {
            System.Data.DataSet xds = dataSet.GetChanges();
            DceService.DCETransactionResult res;

            if (xds != null)
            {
               res = DCEWebAccess.Service.UpdateDataSet(sqlString, tableName, xds);
            }
            else
               res = DCEWebAccess.Service.GetDataSet(sqlString, tableName);

            if (res.Result == DceService.TransactionResult.Success)
            {
               dataSet = res.dataSet;
               return dataSet;
            }
            else
            {
               ShowSqlException(res);
               return null;
            }
         }
         return null;
      }

      /// <summary>
      /// Сохранить DataSet
      /// </summary>
      /// <param name="sqlString"></param>
      /// <param name="tableName"></param>
      /// <param name="dataSet"></param>
      static public void SaveDataSet(string sqlString,
         string tableName, System.Data.DataSet dataSet)
      {
         if (dataSet != null)
         {
            System.Data.DataSet xds = dataSet.GetChanges();

            if (xds != null)
            {
               DceService.DCETransactionResult res = DCEWebAccess.Service.SaveDataSet(sqlString, tableName, xds);
            
               if (res.Result != DceService.TransactionResult.Success)
                  ShowSqlException(res);
            }
         }
      }

      /// <summary>
      /// Пакетное обновление массива DataSet[]
      /// </summary>
      /// <param name="sql"></param>
      /// <param name="tables"></param>
      /// <param name="datasets"></param>
      public void UpdateDataSets(string[] sql, string[]tables, DataSet[] datasets )
      {
         DceService.DCETransactionResult res = DCEWebAccess.Service.UpdateDataSets(sql,tables,datasets);
         if (res.Result != DceService.TransactionResult.Success)
         {
            ShowSqlException(res);
         }
      }
   }
}
