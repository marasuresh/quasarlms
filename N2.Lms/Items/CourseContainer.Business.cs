using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Web.Security;

namespace N2.Lms.Items
{
	using N2.Details;
	using N2.Collections;

	[DataObject]
	partial class CourseContainer
	{
		public IEnumerable<Course> Courses {
			get {
				return
					this.GetChildren(new TypeFilter(typeof(Course))).Cast<Course>();
			}
		}

		IEnumerable<string> AdminRoles {
			get {
				var _sm = Context.SecurityManager as N2.Security.SecurityManager;
				return
					null != _sm
						? _sm.AdminRoles.Concat(_sm.EditorRoles).Distinct()
						: new[] { "Administrators" };
			}
		}

		[DataObjectMethod(DataObjectMethodType.Select)]
		public IEnumerable<string> GetCurriculumNames()
		{
			return this.DetailCollections
				.Select(_dc => _dc.Key)
				.Concat(Roles.GetAllRoles())
				.Except(this.AdminRoles.Concat(new[] { string.Empty }))
				.Distinct();
		}

		/// <summary>
		/// Get Courses status from a given curriculum
		/// </summary>
		/// <param name="curriculumName"></param>
		/// <returns></returns>
		public IEnumerable<CurriculumCourseInfo> GetCurriculum(string curriculumName)
		{
			return
				from _cci in this.GetDetailCollection(curriculumName, true).AsDictionary<int>()
				select new CurriculumCourseInfo {
					Id = int.Parse(_cci.Key),
					ExistInCurriculum = true,
					Status = _cci.Value,
				};
		}

		/// <summary>
		/// Get combined info about Curriculum Course:
		///		1) From curriculum collection with appropriate Status
		///		2) From a global Course list without a Status
		/// </summary>
		/// <param name="curriculumName"></param>
		/// <returns></returns>
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public IEnumerable<CurriculumCourseInfo> GetCurriculumCourseInfo(string curriculumName)
		{
			//if curriculum doesn't yet exist, then take a complete course list
			var _currentCurriculum = string.IsNullOrEmpty(curriculumName)
				? (IDictionary<string, int>)new Dictionary<string, int>()
				: this.GetDetailCollection(curriculumName, true).AsDictionary<int>();

			var _curriculumCourses =
				_currentCurriculum.Select(_cd => int.Parse(_cd.Key));

			return
				from _combinedId in _curriculumCourses
					.Concat(this.Courses.Select(_course => _course.ID))
					.Distinct()

				join _course in this.Courses
					on _combinedId equals _course.ID
					into _existingCourses

				join _curriculumData in _currentCurriculum
					on _combinedId.ToString() equals _curriculumData.Key
					into _curriculum

				from _course in _existingCourses.DefaultIfEmpty()
				from _curricullumData in _curriculum.DefaultIfEmpty()
				orderby _combinedId
				select new CurriculumCourseInfo {
					Id = _combinedId,
					Title = _course != null ? _course.Title : "Deleted course " + _combinedId.ToString(),
					Status = _curricullumData.Value,

					Exists = _course != null,
					ExistInCurriculum = !string.IsNullOrEmpty(_curricullumData.Key)
				};
		}

		internal IEnumerable<CurriculumCourseInfo> MyCurriculumCourses {
			get {
				var _myRoles = Roles.GetRolesForUser();

				return
					_myRoles.Aggregate(
						Enumerable.Empty<CurriculumCourseInfo>(),
						(list, role) =>
							this.GetCurriculum(role)
								.Concat(list)
								.Distinct(new CurriculumCourseInfoEqualityComparer()));
			}
		}

		#region Types

		public class CurriculumCourseInfo
		{
			public int Id { get; set; }
			public string Title { get; set; }
			public int Status { get; set; }

			public bool ExistInCurriculum { get; set; }
			public bool Exists { get; set; }
		}

		#endregion Types

		class CurriculumCourseInfoEqualityComparer
			: IEqualityComparer<CurriculumCourseInfo>
		{
			#region IEqualityComparer<CurriculumCourseInfo> Members

			public bool Equals(CurriculumCourseInfo x, CurriculumCourseInfo y)
			{
				return x.Id == y.Id;
			}

			public int GetHashCode(CurriculumCourseInfo obj)
			{
				return obj.Id.GetHashCode();
			}

			#endregion
		}
	}
}
