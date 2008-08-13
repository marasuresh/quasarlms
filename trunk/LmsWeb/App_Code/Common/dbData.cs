using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

using System.IO;
using System.Reflection;

namespace DCE
{
    /// <summary>
    /// Доступ к БД
    /// </summary>
    //[Obsolete("Rewrite to ADO.NET typed DataSets.")]
    public class dbData
    {
		static dbData s_instance;
		public static dbData Instance {
			get {
				if (null == s_instance) {
					s_instance = new dbData(DCE.Settings.ConnectionString);
				}
				return s_instance;
			}
		}

        public dbData()
        {
        }
        
		dbData(string conn)
        {
            this.Connection.ConnectionString = conn;
        }

        /// <summary>
        /// результирующий select-запрос !!! readonly !!!!!!!!!!!!
        /// </summary>
        public string ResultStrSqlSelect;
        //public string ResultStrSqlCountSelect;

        ///////////////////////////////////////////////////// !!!!!!!!!!!!!!!!!!!!!!!!!!!

        /// <summary>
        /// флаг - добавлено ли уже WHERE условие в SELECT-запрос
        /// </summary>
        protected bool _WhereConditionFlag = false;

        /// <summary>
        /// Соединение с SQL-Сервером
        /// </summary>
        protected SqlConnection _SqlConnection;


        /// <summary>
        /// SQL-Транзакция, которая используется для манипуляции данными
        /// </summary>
        public SqlTransaction Transaction = null;

        /// <summary>
        /// SQL-выражение для создания временной таблицы из вложенного SELECT-запроса 
        /// с добавленным increment-полем.
        /// Временной таблица используется для извлечения в дальнеёшем порции данных из неё
        /// для постраничного отображения данных.
        /// </summary>		
        public const string StrSqlCreateTemporaryTable =
           "select * " +
           "into #TemporaryTable2 " +
           "from (%strSelect%) TT ";
        /*public const string StrSqlCreateTemporaryTable = 
           "select *,  IDENTITY(int, 1,1) AS ID_Num " +
           "into #TemporaryTable " +
           "from (%strSelect%) TT " +
           "order by %sortField%  %order%";
        */

        public const string StrSqlCreateTemporaryTable2 =
           "select *,  IDENTITY(int, 1,1) AS ID_Num " +
           "into #TemporaryTable " +
           "from #TemporaryTable2 TT " +
           "order by %sortField%  %order%";

        /// <summary>
        /// SQL-SELECT-выражение для выборки данных из временной таблицы 
        /// для постраничного отображения данных,
        /// где данные выбираются с firstIndex по lastIndex increment-поля временной таблицы.
        /// </summary>
        public const string StrSqlSelectPartRows =
           "select * from #TemporaryTable " +
           "where ID_Num>=%firstIndex% and ID_Num<=%lastIndex%";
        /*public const string StrSqlSelectPartRows = 
           "select * from #TemporaryTable " +
           "where ID_Num>=%firstIndex% and ID_Num<=%lastIndex%";*/

        /// <summary>
        /// Свойство, инкапсулирующее доступ к члену класса _SqlConnection
        /// </summary>
        public SqlConnection Connection
        {
            set { _SqlConnection = value; }
            get
            {
                if (_SqlConnection == null)
                    _SqlConnection = new SqlConnection(Settings.ConnectionString);
                return _SqlConnection;
            }
        }

        /// <summary>
        /// Кол-во записей на одной странице данных
        /// </summary>
        protected int _PageSize = 2;
        public int PageSize
        {
            get { return _PageSize; }
            set { if (value >= 1)_PageSize = value; }
        }

        private int _CountData = -1;
        /// <summary>
        /// Количество общее записей, которые бы вернул ResultStrSqlSelect
        /// без разбивки на порции
        /// </summary>
        public int CountData
        {
            get
            {
                if (this._CountData > -1) return this._CountData;

                string countSelect = "select count(0) from (%SelectExpr%) #TTT";
                countSelect
                   = Regex.Replace(countSelect, "%SelectExpr%", this.ResultStrSqlSelect);

                //trace(countSelect);////

                DataSet dataSet = new DataSet();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand =
                   new SqlCommand(countSelect, Connection);
                sqlDataAdapter.SelectCommand.Transaction = Transaction;
                sqlDataAdapter.Fill(dataSet);
                this._CountData = (int)dataSet.Tables[0].Rows[0][0];

                return this._CountData;
            }
        }
        /// <summary>
        /// Открыть соединение с БД
        /// </summary>
        public void Open()
        {
            this.Connection.Open();
        }
        /// <summary>
        /// закрыть соединение с БД
        /// </summary>
        public void Close()
        {
            this.Connection.Close();
        }

        /// <summary>
        /// воборка порции данных с firstIndex по lastIndex,
        /// сортировка по sortField, порядок сортировки order(true-возрастание)
        /// </summary>		
        /// <returns>DataSet</returns>
        public virtual DataSet GetDataRows(int firstIndex, int lastIndex, string sortField, bool order)
        {
            //sql - команда для создания временной таблицы
            string strSqlCreateTemporaryTable = dbData.StrSqlCreateTemporaryTable;
            string strSqlCreateTemporaryTable2 = dbData.StrSqlCreateTemporaryTable2;
            strSqlCreateTemporaryTable =
               Regex.Replace(strSqlCreateTemporaryTable, "%strSelect%", this.ResultStrSqlSelect);
            strSqlCreateTemporaryTable2 =
               Regex.Replace(strSqlCreateTemporaryTable2, "%sortField%", sortField);
            strSqlCreateTemporaryTable2 =
               Regex.Replace(strSqlCreateTemporaryTable2, "%order%", (order) ? "asc" : "desc");

            // sql - команда для выборки порции строк из вр. таблицы,
            // с firstIndex по lastIndex
            string strSqlSelectPartRows = dbData.StrSqlSelectPartRows;
            strSqlSelectPartRows =
               Regex.Replace(strSqlSelectPartRows, "%firstIndex%", "" + firstIndex);
            strSqlSelectPartRows =
               Regex.Replace(strSqlSelectPartRows, "%lastIndex%", "" + lastIndex);


            ///////////////////////////// !!!!!!!!!!
            /**trace(strSqlCreateTemporaryTable + "\r\n" +
                  strSqlCreateTemporaryTable2 + "\r\n" +
                  strSqlSelectPartRows);
            /**/

            // создать временные таблицы
            ExecuteNonQuery(strSqlCreateTemporaryTable);
            ExecuteNonQuery(strSqlCreateTemporaryTable2);


            // заполнить DataSet порцией строк из вр. таблицы
            DataSet dataSet = new DataSet();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            //sqlDataAdapter.SelectCommand = new SqlCommand(strSqlSelectPartRows, _SqlConnection);
            sqlDataAdapter.SelectCommand = new SqlCommand(strSqlSelectPartRows, Connection);
            sqlDataAdapter.SelectCommand.Transaction = Transaction;
            sqlDataAdapter.Fill(dataSet, "row");
            dataSet.DataSetName = "xml";

            return dataSet;
        }


        public virtual DataSet GetDataAll()
        {
            string select = this.ResultStrSqlSelect;

            DataSet dataSet = new DataSet();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            sqlDataAdapter.SelectCommand = new SqlCommand(select, Connection);
            sqlDataAdapter.SelectCommand.Transaction = Transaction;
            sqlDataAdapter.Fill(dataSet, "row");
            dataSet.DataSetName = "xml";

            return dataSet;
        }

        /// <summary>
        /// выборка порции данных, где pageNumber - № страницы данных,
        /// количество данных задано в члене класса _PageSize
        /// сортировка по sortField, порядок сортировки order(true-возрастание)
        /// </summary>
        /// <returns>DataSet</returns>
        protected DataSet GetDataPage(int pageNumber, string sortField, bool order)
        {
            int firstIndex = (pageNumber - 1) * this.PageSize + 1;
            int lastIndex = pageNumber * this.PageSize;
            return GetDataRows(firstIndex, lastIndex, sortField, order);
        }

        /// <summary>
        /// выборка порции данных, где pageNumber - № страницы данных,
        /// количество данных задано в члене класса _PageSize
        /// сортировка по sortField, порядок сортировки order(true-возрастание)
        /// Предполагается что конроль типа sortField, будет возложен Facade использующий
        /// производный от AbstractData класс.
        /// </summary>		
        /// <returns>DataSet</returns>
        public DataSet GetDataPage(int pageNumber, object sortField, bool order)
        {
            return GetDataPage(pageNumber, sortField.ToString(), order);
        }
        /*public DataSet GetDataPage(int pageNumber, object sortField, bool order, out int count)	
        {
           count = 666;
           return GetDataPage(pageNumber, sortField.ToString(), order);
        }*/

        /// <summary>
        /// Добавить WHERE условие к SELECT-запросу на выборку данных.
        /// Используется для накладывания фильтра на данные.
        /// Метод переопределяется в производных классах, но метод базового класса
        /// должен быть вызван из переопределённого метода
        /// </summary>
        /// <param name="whereCondition"></param>
        protected virtual void AddWhereCondition(string whereCondition)
        {
            this.ResultStrSqlSelect += (_WhereConditionFlag ? " AND " : " WHERE ") + whereCondition;
            //this.ResultStrSqlCountSelect += (_WhereConditionFlag?" AND ":" WHERE ") + whereCondition;
            _WhereConditionFlag = true;
        }

        protected string GetStringExpression(object obj) //??????????????????????????????????????
        {
            string returnStr = obj.ToString();
            string typeName = obj.GetType().FullName;
            if (typeName == "System.String" |
               typeName == "System.Guid")
                returnStr = " '" + returnStr + "' ";
            return returnStr;
        }

        /// <summary>
        /// Выполнить (INSERT, UPDATE или DELETE) SQL-запрос
        /// </summary>		
        public void ExecuteNonQuery(string commandText)
        {
            //trace(commandText);

            SqlCommand sqlCommand = Connection.CreateCommand();
            sqlCommand.CommandText = commandText;
            sqlCommand.Transaction = Transaction;
            try
            {
                sqlCommand.ExecuteNonQuery();
            }
            catch( SqlException error )
            {
                throw new Exception(error.Message + "\r\n\r\n" + commandText);
            }
        }

        public void SetFilter(object filterField, object filterValue)
        {
            this.AddWhereCondition("" + filterField + " = "
               + this.GetStringExpression(filterValue));
        }

        public void trace(string str)
        {
            /////
            //MessageBox.Show(strSqlCreateTemporaryTable);////del
            StreamWriter fs = File.CreateText("C:/!/t.txt");
            fs.Write(str + "\r\n\r\n");
            fs.Close();
        }
        public DataSet getDataSet(string strSQL, string dsName, string rowTagName)
        {
            DataSet oDS = new DataSet();
            try
            {
                SqlCommand lCommand = this.Connection.CreateCommand();
                lCommand.CommandText = strSQL;
                lCommand.Transaction = this.Transaction;
                SqlDataAdapter oDAdapter = new SqlDataAdapter(lCommand);
                //            SqlDataAdapter oDAdapter = new SqlDataAdapter(strSQL,fConnObj);

                this.Open();
                oDAdapter.Fill(oDS, rowTagName);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new ApplicationException("getDataSet Error: " + e.Message + ", SQL:" + strSQL, e);
            }
            finally
            {
                this.Close();
            }

            oDS.DataSetName = dsName;
            return oDS;
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
        /// <summary>
        /// Выполнить SQL запрос
        /// </summary>
        /// <param name="strSQL"></param>
        public void ExecSQL(string strSQL)
        {
            SqlCommand lCommand = this.Connection.CreateCommand();
            lCommand.CommandText = strSQL;
            lCommand.Transaction = this.Transaction;
            try
            {
                this.Open();
                int i = lCommand.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new ApplicationException(" execSQL Error: " + e.Message + "; SQL:" + strSQL, e);
            }
            finally
            {
                this.Close();
                lCommand.Dispose();
            }
        }
        /// <summary>
        /// Выполнить SQL запрос
        /// </summary>
        /// <param name="lCommand"></param>
        public void ExecSQL(SqlCommand lCommand)
        {
            lCommand.Connection = this.Connection;
            lCommand.Transaction = this.Transaction;
            try
            {
                this.Open();
                int i = lCommand.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new ApplicationException(" execSQL Error: " + e.Message + "; SQL:" + lCommand.CommandText, e);
            }
            finally
            {
                this.Close();
            }
        }

        /// <summary>
        /// Обновить DataSet
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="tableName"></param>
        /// <param name="dataSet"></param>
        /// <returns></returns>
        public DataSet UpdateDataSet(string strSQL, string tableName, ref DataSet dataSet)
        {
            System.Data.DataSet xds = dataSet.GetChanges();
            if (xds == null)
            {
                return dataSet = this.getDataSet(strSQL, dataSet.DataSetName, tableName);
            }

            SqlDataAdapter oDAdapter = null;
            SqlCommand lCommand = null;
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
                tran = this.Connection.BeginTransaction();
                oDAdapter = new SqlDataAdapter();//(lCommand);
                oDAdapter.SelectCommand = new SqlCommand(strSQL, Connection, tran);
                cb = new SqlCommandBuilder(oDAdapter);

                oDAdapter.Update(dataSet, tableName);
                oDAdapter.Fill(oDS, tableName);
                tran.Commit();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                if (tran != null)
                    tran.Rollback();
                throw new ApplicationException("getDataSet Error: " + e.Message + ", SQL:" + strSQL, e);
            }
            catch (System.Data.DBConcurrencyException)
            {
            }
            finally
            {
                if (tran != null)
                {
                    tran.Dispose();
                    tran = null;
                }
                if (lCommand != null) lCommand.Dispose();
                if (oDAdapter != null) oDAdapter.Dispose();
                this.Close();
                //Monitor.Exit(fConnObj);
            }
            oDS.DataSetName = dataSet.DataSetName;
            return oDS;
        }

        /// <summary>
        /// получение контента по eid с указанинем требуемого и дефолтового языков
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="abbr">напр "RU"</param>
        /// <param name="defaultAbbr">напр "RU" или null</param>
        /// <returns></returns>
        public System.Data.DataRow GetContent(string eid, string abbr, string defaultAbbr)
        {
            System.Data.DataRow defaultRow = null;
            System.Data.DataRow current = null;

            string select = "select c.* , l.Abbr from dbo.Content c left join dbo.Languages l on (c.Lang=l.id) where c.eid='"
               + eid + "'";
            System.Data.DataSet ds = this.getDataSet(select, "dataSet", "Contents");
            System.Data.DataTable contents = ds.Tables["Contents"];
            if (contents != null)
            {
                for (int i = 0; i < contents.Rows.Count; i++)
                {
                    current = contents.Rows[i];
                    if (current["Abbr"].ToString() == abbr)
                        return current;
                    if (current["Abbr"].ToString() == defaultAbbr)
                        defaultRow = current;
                }
            }
            return defaultRow;// != null ? defaultRow : current;
        }

        public class DataSetUpdateBatch
        {
            public string sql;
            public string tableName;
            public DataSet dataSet;
        };

        /// <summary>
        /// Пакетное обновление массива DataSets
        /// </summary>
        /// <param name="sqls"></param>
        /// <param name="tables"></param>
        /// <param name="dataSets"></param>
        public void UpdateDataSets(string[] sqls, string[] tables, System.Data.DataSet[] dataSets)
        {
            SqlDataAdapter oDAdapter = null;
            SqlCommand lCommand = null;
            SqlCommandBuilder cb = null;
            SqlTransaction tran = null;

            //         oDAdapter.RowUpdating += new SqlRowUpdatingEventHandler(OnUpdatingTest);
            //         oDAdapter.RowUpdated += new SqlRowUpdatedEventHandler(OnUpdatedTest);

            try
            {
                this.Open();
                tran = this.Connection.BeginTransaction();
                oDAdapter = new SqlDataAdapter();

                for (int i = 0; i < sqls.Length; i++)
                {
                    oDAdapter.SelectCommand = new SqlCommand(sqls[i], this.Connection, tran);
                    if (tables[i] != "")
                    {
                        cb = new SqlCommandBuilder(oDAdapter);
                        oDAdapter.Update(dataSets[i], tables[i]);
                    }
                    else
                    {
                        oDAdapter.SelectCommand.ExecuteNonQuery();
                    }
                }
                tran.Commit();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                //if (tran != null)
                //   tran.Rollback();
                throw new ApplicationException("UpdateDataSets Error: "/* + e.Message + ","*/+ e);
            }
            finally
            {
                if (lCommand != null) lCommand.Dispose();
                if (oDAdapter != null) oDAdapter.Dispose();
                this.Close();
                //Monitor.Exit(fConnObj);
            }
        }

        /// <summary>
        /// Получить Фото из БД
        /// </summary>
        /// <param name="id">Photo id (поле eid в Content)</param>
        /// <param name="contentType"></param>
        /// <returns>byte[]</returns>
        static public byte[] GetPhoto(Guid id, out string contentType)
        {
            byte[] ret = null;
            contentType = null;
            dbData db = new dbData();
            db.Connection.ConnectionString = DCE.Settings.ConnectionString;
            System.Data.DataSet dsPhoto = db.getDataSet("select * from dbo.Content where eid='" + id + "'", "dataSet", "Photos");
            System.Data.DataTable tablePhoto = dsPhoto.Tables[0];
            if (tablePhoto.Rows.Count > 0)
            {
                System.Data.DataRow rowPhoto = tablePhoto.Rows[0];
                if (rowPhoto["Data"] != System.DBNull.Value)
                {
                    contentType = rowPhoto["DataStr"].ToString();
                    ret = (byte[])rowPhoto["Data"];
                }
            }
            return ret;
        }
    }
}
