using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;

public static class StudentsReportsDataBuilder
{
    public static StudentsReportsData.FiltersRow GetFilters(DataSet ds)
    {
        StudentsReportsData.FiltersDataTable filtersTable = ((StudentsReportsData)ds).Filters;
        if( filtersTable.Rows.Count == 0 )
        {
            filtersTable.AddFiltersRow(filtersTable.NewFiltersRow());
        }
        return filtersTable[0];
    }

    public static StudentsReportsData.FiltersRow GetFilters(DataRow row)
    {
        return GetFilters(row.Table.DataSet);
    }

    public static class SortColumn
    {
        public const string Name = "Name";
        public const string Date = "Date";
        public const string TryCount = "TryCount";
        public const string QuestionCount = "QuestionCount";
        public const string RequiredPoints = "RequiredPoints";
        public const string CollectedPoints = "CollectedPoints";
        public const string AnswerPercent = "AnswerPercent";
    }

    public static StudentsReportsData GetReportsData(bool rebuildRequired, bool showAllStudents)
    {
		string cacheKey = typeof(StudentsReportsDataBuilder).FullName + "." + typeof(StudentsReportsData).FullName + "-Cached";
		
		if( !rebuildRequired ) {
			StudentsReportsData cachedResult = HttpContext.Current.Cache[cacheKey] as StudentsReportsData;
			if (cachedResult != null) {
				return cachedResult;
			}
		}
		
		using(SqlConnection conn = new SqlConnection(DCE.Settings.ConnectionString)) {
			using(SqlCommand sproc = new SqlCommand()) {
				sproc.Connection = conn;

                sproc.CommandText = "dbo.dcereports_Student_LoadAllRelatedTables";
                sproc.CommandType = CommandType.StoredProcedure;

                if( CurrentUser.Region.ID == null )
                    sproc.Parameters.Add("@homeRegion", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                else
                    sproc.Parameters.Add("@homeRegion", SqlDbType.UniqueIdentifier).Value = CurrentUser.Region.ID;


                if( showAllStudents )
                    sproc.Parameters.Add("@studentID", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
                else
                    sproc.Parameters.Add("@studentID", SqlDbType.UniqueIdentifier).Value = CurrentUser.UserID;

                conn.Open();

                StudentsReportsData resultData = new StudentsReportsData();
                resultData.EnforceConstraints = false;
                
				SqlDataReader _reader = sproc.ExecuteReader();
				
				resultData.Load(
					_reader,
                    LoadOption.OverwriteChanges,
                    resultData.CourseDomain,
                    resultData.Courses,
                    resultData.Themes,
                    resultData.Tests,
                    resultData.TestQuestions,
                    resultData.TestResults,
                    resultData.TestAnswers,
                    resultData.Students,
                    resultData.StudentGroup,
                    resultData.Groups);

                try
                {
                    resultData.EnforceConstraints = true;
                }
                catch( ConstraintException error )
                {
                    foreach( DataTable tab in resultData.Tables )
                    {
                        foreach( Constraint ct in new System.Collections.ArrayList(tab.Constraints) )
                        {
                            if( (bool)ct.GetType().InvokeMember(
                                "IsConstraintViolated",
                                System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance,
                                null,
                                ct,null) )
                                throw new ConstraintException(ct.GetType().Name + " " + ct.ConstraintName + " on " + tab.TableName + " violated.");
                        }
                    }

                    throw;
                }

                HttpContext.Current.Cache[cacheKey] = resultData;
                return resultData;
            }
        }
    }

    public static DataSet CompactData(StudentsReportsData data)
    {
        DataSet resultDataSet = new DataSet(data.DataSetName);

        Dictionary<Guid,int> guidList = new Dictionary<Guid,int>();

        foreach( DataTable sourceTable in data.Tables )
        {
            DataTable resultTable = resultDataSet.Tables.Add(sourceTable.TableName);

            bool[] isColumnGuid = new bool[sourceTable.Columns.Count];
            for( int i=0; i<sourceTable.Columns.Count; i++ )
            {
                DataColumn sourceColumn = sourceTable.Columns[i];

                if( sourceColumn.DataType == typeof(Guid) )
                {
                    resultTable.Columns.Add(sourceColumn.ColumnName, typeof(int));
                    isColumnGuid[i] = true;
                }
                else
                {
                    resultTable.Columns.Add(sourceColumn.ColumnName, sourceColumn.DataType);
                }
            }

            foreach( DataRow sourceRow in sourceTable.Rows )
            {
                DataRow resultRow = resultTable.NewRow();

                for( int i = 0; i < sourceTable.Columns.Count; i++ )
                {
                    DataColumn sourceColumn = sourceTable.Columns[i];

                    if( sourceRow.IsNull(i) )
                        continue;

                    if( isColumnGuid[i] )
                    {
                        Guid sourceGuid = (Guid)sourceRow[i];
                        int resultInt;
                        if( !guidList.TryGetValue(sourceGuid, out resultInt) )
                        {
                            resultInt = guidList.Count;
                            guidList[sourceGuid] = resultInt;
                        }

                        resultRow[i] = resultInt;
                    }
                    else
                    {
                        resultRow[i] = sourceRow[i];
                    }
                }

                resultTable.Rows.Add(resultRow);
            }
        }

        return resultDataSet;
    }
}
