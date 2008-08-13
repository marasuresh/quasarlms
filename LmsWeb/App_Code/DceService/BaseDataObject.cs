using System;
using System.Data;
using System.Data.SqlClient;

namespace DCEDbData
{
   public enum TransactionResult
   {
      Success,
      SqlException,
      UnhandledException
   }
   /// <summary>
   /// Результат выполнения SQL запроса
   /// </summary>
   public class DCETransactionResult
   {
      
      public byte Class; // severity level
      public byte State; // State
      public int Number; // error number
      public string Message = null;
      public string SqlString = null;

      public DataSet dataSet = null;
      public TransactionResult Result = TransactionResult.Success;
      public string ExceptionClass = "";

      public DCETransactionResult()
      {

      }

      public DCETransactionResult(DataSet ds)
      {
         this.dataSet = ds;
      }

      public DCETransactionResult(Exception e)
      {
         Result = TransactionResult.UnhandledException;
         ExceptionClass = e.GetType().ToString();
         this.Message = e.Message;
      }

      public DCETransactionResult(System.Data.SqlClient.SqlException e)
      {
         Result = TransactionResult.SqlException;
         this.Message = e.Message;
         this.Class = e.Class;
         this.State = e.State;
         this.Number = e.Number;
         
         foreach (System.Data.SqlClient.SqlError err in  e.Errors)
         {
            SqlString += err.ToString();
         }
         ExceptionClass = e.GetType().ToString();
      }
   }

   /// <summary>
   /// 
   /// </summary>
   public class ADbData
   {
      public SqlConnection fConnObj;
      private System.Collections.ArrayList Transactions = new System.Collections.ArrayList();

      /// <summary>
      /// Конструктор
      /// </summary>
//      public ADbData()
//      {
//         this.createConnection( ADbData.getSqlServerName() );
//      }

      ~ADbData()
      {
         Close();
      }

      public ADbData( string lSQLServerName )
      {
         this.createConnection( lSQLServerName );
      }

      /// <summary>
      /// Имя SQL сервера
      /// </summary>
      /// <returns></returns>
      public static string getSqlServerName()
      {
         string sqlServerName = "";
//         try
//         {
//            RegistryKey lConfigKey = Registry.LocalMachine.OpenSubKey("Software\\Lundlay\\IM2PhoneServer");
//            sqlServerName = lConfigKey.GetValue("SQLServerName").ToString();
//         }
//         catch
//         {
//            sqlServerName = "(local)";
//         }
         if(sqlServerName=="") sqlServerName = "(local)";
         return sqlServerName;
      }
      private void createConnection( string ConnectString)
      {
         this.fConnObj = 
            new SqlConnection(ConnectString);
      }
      /// <summary>
      /// Open database connection
      /// </summary>
      public void Open()
      {
         if( fConnObj.State != ConnectionState.Open )
         {
            fConnObj.Open();
         }
      }

      /// <summary>
      /// Close the connection to the database
      /// </summary>
      public void Close()
      {
         if( fConnObj.State == ConnectionState.Open )
         try
         {
            foreach(SqlTransaction tran in this.Transactions)
            {
               tran.Rollback();
               tran.Dispose();
            }
            this.Transactions.Clear();
            fConnObj.Close();
         }
         catch{}
      }
      /// <summary>
      /// Получить DataSet по переданному SQL запросу
      /// </summary>
      /// <param name="strSQL">SQL запрос</param>
      /// <param name="dsName">имя "верхнего" тэга для результирующего XML</param>
      /// <param name="rowTagName">имя тэга для каждой строки</param>
      /// <returns>Заполненный DataSet</returns>
      public DataSet getDataSet(string strSQL, string dsName, string rowTagName)
      {
         SqlDataAdapter oDAdapter=null;
         SqlCommand lCommand=null;
         DataSet oDS = new DataSet();
         try
         {
            //Monitor.Enter(fConnObj);
            lCommand = fConnObj.CreateCommand();
            lCommand.CommandText = strSQL;
//            lCommand.Transaction = this.fTransaction;
            oDAdapter = new SqlDataAdapter(lCommand);

            this.Open();
            oDAdapter.Fill(oDS, rowTagName);
         }
//         catch( System.Data.SqlClient.SqlException e )
//         {
//            throw;
//         }
         finally
         {
            if(lCommand!=null)lCommand.Dispose();
            if(oDAdapter!=null)oDAdapter.Dispose();
            this.Close();
            //Monitor.Exit(fConnObj);
         }

         oDS.DataSetName=dsName;			
         return oDS;
      }

      
      /// <summary>
      /// Обновить DataSet
      /// </summary>
      /// <param name="strSQL"></param>
      /// <param name="tableName"></param>
      /// <param name="dataSet"></param>
      /// <returns></returns>
      public DataSet UpdateDataSet(string strSQL, string tableName, DataSet dataSet)
      {
         System.Data.DataSet xds = dataSet.GetChanges();
         if (xds == null)
         {
            return dataSet = this.getDataSet(strSQL, dataSet.DataSetName, tableName);
         }

         SqlDataAdapter oDAdapter=null;
         SqlCommand lCommand=null;
         DataSet oDS = new DataSet();
         SqlCommandBuilder cb = null;
         SqlTransaction tran = null; 
       
         try
         {
            //Monitor.Enter(fConnObj);
            //            lCommand = fConnObj.CreateCommand();
            //            lCommand.CommandText = strSQL;
            //            lCommand.Transaction = this.fTransaction;
            this.Open();
            tran = this.fConnObj.BeginTransaction();
            oDAdapter = new SqlDataAdapter();//(lCommand);
            oDAdapter.SelectCommand = new SqlCommand(strSQL, fConnObj,tran);
            cb = new SqlCommandBuilder(oDAdapter);

            oDAdapter.Update(dataSet, tableName);
            oDAdapter.Fill(oDS, tableName);
            tran.Commit();
         }
//         catch( System.Data.SqlClient.SqlException e )
//         {
//            //if (tran !=null)
//            //   tran.Rollback();
//            throw;
//         }
         finally
         {
            if (tran !=null)
            {
               tran.Dispose();
               tran = null;
            }
            if(lCommand!=null)lCommand.Dispose();
            if(oDAdapter!=null)oDAdapter.Dispose();
            this.Close();
            //Monitor.Exit(fConnObj);
         }
         oDS.DataSetName=dataSet.DataSetName;
         return oDS;
      }

      /// <summary>
      /// Сохранить DataSet
      /// </summary>
      /// <param name="strSQL"></param>
      /// <param name="tableName"></param>
      /// <param name="dataSet"></param>
      public void SaveDataSet(string strSQL, string tableName, DataSet dataSet)
      {
         System.Data.DataSet xds = dataSet.GetChanges();

         SqlDataAdapter oDAdapter = null;
         SqlCommand lCommand = null;
         SqlCommandBuilder cb = null;
         SqlTransaction tran = null; 
       
         try
         {
            this.Open();
            tran = this.fConnObj.BeginTransaction();
            oDAdapter = new SqlDataAdapter();
            oDAdapter.SelectCommand = new SqlCommand(strSQL, fConnObj,tran);
            cb = new SqlCommandBuilder(oDAdapter);

            oDAdapter.Update(dataSet, tableName);
            tran.Commit();
         }
         finally
         {
            if (tran !=null)
            {
               tran.Dispose();
               tran = null;
            }

            if(lCommand != null)
               lCommand.Dispose();

            if(oDAdapter != null)
               oDAdapter.Dispose();
            
            this.Close();
         }
      }

      /// <summary>
      /// Сервистный класс для пакетного обновления DataSets
      /// </summary>
      public class DataSetUpdateBatch
      {
         public string sql;
         public string tableName;
         public DataSet dataSet;
      };
    
      public void OnUpdatingTest(object obj, SqlRowUpdatingEventArgs args)
      {
//         if (args != null)
//         {
//            string message = "Event : OnRowUpdating\n" +
//               " event args: (" +
//               " command=" + args.Command + 
//               " commandType=" + args.StatementType + 
//               " status=" + args.Status + ")";
//         
//            throw new Exception( message );
//         }
      }
      public void OnUpdatedTest(object obj, SqlRowUpdatedEventArgs args)
      {
//         if (args != null)
//         {
//            string message =  "Event : OnRowUpdated\n" +
//               " event args: (" +
//               " command=" + args.Command +
//               " commandType=" + args.StatementType + 
//               " recordsAffected=" + args.RecordsAffected + 
//               " status=" + args.Status + ")";
//         
//            throw new Exception( message );
//         }
      }

      /// <summary>
      /// Пакетное обновление массива DataSets
      /// </summary>
      /// <param name="dataSets"></param>
      public void UpdateDataSets(System.Collections.ArrayList dataSets)
      {
         SqlDataAdapter oDAdapter=null;
         SqlCommand lCommand=null;
         SqlCommandBuilder cb = null;
         SqlTransaction tran = null; 

//         oDAdapter.RowUpdating += new SqlRowUpdatingEventHandler(OnUpdatingTest);
//         oDAdapter.RowUpdated += new SqlRowUpdatedEventHandler(OnUpdatedTest);
       
         try
         {
            this.Open();
            tran = this.fConnObj.BeginTransaction();
            oDAdapter = new SqlDataAdapter();

            for (int i=0; i<dataSets.Count; i++)
            {
               DataSetUpdateBatch batch = (DataSetUpdateBatch)dataSets[i];

               oDAdapter.SelectCommand = new SqlCommand(batch.sql, fConnObj,tran);
               if (batch.tableName != null && batch.tableName != "")
               {
                  cb = new SqlCommandBuilder(oDAdapter);
                  oDAdapter.Update(batch.dataSet,batch.tableName);
               }
               else
               {
                  oDAdapter.SelectCommand.ExecuteNonQuery();
               }
            }
            tran.Commit();
         }
//         catch( System.Data.SqlClient.SqlException e )
//         {
//            //if (tran != null)
//            //   tran.Rollback();
//            
//            throw;
//         }
         finally
         {
            if(lCommand!=null)lCommand.Dispose();
            if(oDAdapter!=null)oDAdapter.Dispose();
            this.Close();
            //Monitor.Exit(fConnObj);
         }
      }
      /// <summary>
      /// Получить DataSet по переданному SQL запросу
      /// </summary>
      /// <param name="strSQL">SQL запрос</param>
      /// <param name="dsName">имя "верхнего" тэга для результирующего XML</param>
      /// <returns>Заполненный DataSet</returns>
      public DataSet getDataSet(string strSQL, string dsName)
      {
         return getDataSet(strSQL, dsName, "tr");
      }
      /// <summary>
      /// Получить DataSet по переданному SQL запросу (имя "верхнего" тэга для результирующего XML = "DataSet")
      /// </summary>
      /// <param name="strSQL">SQL запрос</param>
      /// <returns>Заполненный DataSet</returns>
      public DataSet getDataSet(string strSQL)
      {
         return getDataSet(strSQL, "DataSet", "tr");
      }
      public SqlTransaction beginTransaction()
      {
         this.Open();
         SqlTransaction tran = this.fConnObj.BeginTransaction();
         this.Transactions.Add(tran);
         return tran;
      }

      public void rollbackTransaction(SqlTransaction tran)
      {
         try
         {
            tran.Rollback();
         }
         finally
         {
            this.Transactions.Remove(tran);
            tran.Dispose();
         }
      }

      public void commitTransaction(SqlTransaction tran)
      {
         try
         {
            tran.Commit();
         }
         finally
         {
            this.Transactions.Remove(tran);
            tran.Dispose();
         }
      }

      /// <summary>
      /// Выполнить SQL запрос
      /// </summary>
      /// <param name="strSQL"></param>
      /// <returns></returns>
      public int ExecSQL( string strSQL )
      {
         SqlCommand lCommand = null;
         SqlTransaction tran = null;
         int i=-1;
         try
         { 
            this.Open();
            tran = this.fConnObj.BeginTransaction();
            lCommand = fConnObj.CreateCommand();
            lCommand.CommandText = strSQL;
            lCommand.Transaction = tran;
            i = lCommand.ExecuteNonQuery();
            tran.Commit();
         }
         catch( System.Data.SqlClient.SqlException e )
         {
            //if (tran!=null)
            //tran.Rollback();
           
            throw new ApplicationException( " execSQL Error: " + e.Message +"; SQL:"+strSQL, e);
         }
         finally
         {
            if (tran!=null)
               tran.Dispose();
            if(lCommand!=null)lCommand.Dispose();
            this.Close();
         }
         return i;
      }

   }
}
