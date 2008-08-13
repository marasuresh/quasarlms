using System;
using System.Data;

namespace DceAccessLib.DAL
{
	public static class PhotoController
	{
		public static DataTable Load(Guid id)
		{
			string _sql = @"
SELECT *
FROM	dbo.Content
WHERE	eid='{0}'
";
			return DCE.dbData.Instance.getDataSet(
					string.Format(_sql, id),
					"dataSet",
					"Photos").Tables["Photos"];
		}
		
		public static void Insert(Guid id)
		{
			string _sql = @"
INSERT INTO
	dbo.Content(eid, Type)
	VALUES('{0}', 4)
";
			DCE.dbData.Instance.ExecSQL(
					string.Format(_sql, id));
		}

		public static void Update(Guid id, byte[] content, string contentType)
		{
			DataSet _dsPhoto = DCE.dbData.Instance.getDataSet(
				string.Format(@"
SELECT *
FROM	dbo.Content
WHERE	eid='{0}'", id), "dataSet", "Photos");
			DataTable _tblPhoto = _dsPhoto.Tables["Photos"];
			DataRow _rowPhoto;
			
			if (_tblPhoto.Rows.Count > 0) {
				_rowPhoto = _tblPhoto.Rows[0];
			} else {
				_rowPhoto = _tblPhoto.NewRow();
				_rowPhoto["eid"] = id;
				_rowPhoto["Type"] = 4;
				_tblPhoto.Rows.Add(_rowPhoto);
			}
			
			_rowPhoto["Data"] = content;
			_rowPhoto["DataStr"] = contentType;

			DCE.dbData.Instance.UpdateDataSet("select*from content", "Photos", ref _dsPhoto);
			//_dsPhoto.AcceptChanges();
		}
	}
}
