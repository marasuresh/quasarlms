using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using System.Text;

namespace Dce
{
	public static class Roles
	{
		public sealed class RoleInfo
		{
			readonly Guid? m_ID;
			readonly string m_Name;
			readonly bool m_IsGlobal;

			public Guid? ID
			{
				get { return m_ID; }
			}

			public string Name
			{
				get { return m_Name; }
			}

			public bool IsGlobal
			{
				get { return m_IsGlobal; }
			}


			public RoleInfo(Guid? id, string name, bool isGlobal)
			{
				if (name == null)
					throw new ArgumentNullException("name");
				if (name.Length == 0)
					throw new ArgumentException("Empty string.", "name");

				this.m_ID = id;
				this.m_Name = name;
				this.m_IsGlobal = isGlobal;
			}
		}

		public static readonly RoleInfo Student;

		public static readonly RoleInfo Administrator;

		public static readonly RoleInfo Tutor;
		public static readonly RoleInfo TutorRegional;
		public static readonly RoleInfo BusinessTutor;
		public static readonly RoleInfo BusinessTutorRegional;

		static readonly Dictionary<Guid, RoleInfo> roleByID;

		public static RoleInfo FindByID(Guid? roleID)
		{
			if (roleID == null)
				return Student;
			else
				return roleByID[roleID.Value];
		}

		static Roles()
		{
			ConfigData.RolesDataTable rolesTable = new ConfigDataTableAdapters.RolesTableAdapter().GetData();
			roleByID = new Dictionary<Guid, RoleInfo>(rolesTable.Count);

			foreach (ConfigData.RolesRow roleRow in rolesTable) {
				RoleInfo role;

				if (roleRow.IsIDNull()) {
					role = new RoleInfo(null, roleRow.Name, false);
					Student = role;
				} else {
					role = new RoleInfo(roleRow.ID, roleRow.Name, roleRow.IsGlobal);

					switch (roleRow.CodeName) {
						case "Administrator":
							Administrator = role;
							break;

						case "Tutor":
							Tutor = role;
							break;

						case "TutorRegional":
							TutorRegional = role;
							break;

						case "BusinessTutor":
							BusinessTutor = role;
							break;

						case "BusinessTutorRegional":
							BusinessTutorRegional = role;
							break;

						default:
							role = null;
							break;
					}

					if (role != null)
						roleByID.Add(roleRow.ID, role);
				}
			}

			if (Student == null
				|| Administrator == null
				|| Tutor == null
				|| TutorRegional == null
				|| BusinessTutor == null
				|| BusinessTutorRegional == null) {
				Dictionary<string, RoleInfo> definedRoles = new Dictionary<string, RoleInfo>();
				definedRoles.Add("Student", Student);
				definedRoles.Add("Administrator", Administrator);
				definedRoles.Add("Tutor", Tutor);
				definedRoles.Add("TutorRegional", TutorRegional);
				definedRoles.Add("BusinessTutor", BusinessTutor);
				definedRoles.Add("BusinessTutorRegional", BusinessTutorRegional);

				foreach (string roleName in new List<string>(definedRoles.Keys)) {
					if (definedRoles[roleName] == null)
						definedRoles.Remove(roleName);
				}

				StringBuilder definedRolesDump;
				if (definedRoles.Count == 0) {
					definedRolesDump = new StringBuilder("No known roles found.");
				} else {
					definedRolesDump = new StringBuilder("Found known roles (" + definedRoles.Count + "):");
					definedRolesDump.AppendLine();
					foreach (string roleName in definedRoles.Keys) {
						definedRolesDump.AppendLine(
							"  " + roleName + ": \"" + definedRoles[roleName].Name + "\" " + definedRoles[roleName].ID);
					}
				}

				StringBuilder roleConfigRowsDump;
				if (rolesTable.Count == 0) {
					roleConfigRowsDump = new StringBuilder("No rows found int Roles table.");
				} else {
					roleConfigRowsDump = new StringBuilder("Rows of Roles table (" + rolesTable.Count + "):");
					roleConfigRowsDump.AppendLine();
					foreach (ConfigData.RolesRow roleRow in rolesTable) {
						roleConfigRowsDump.AppendLine(
							"  " + roleRow.CodeName + ": \"" + roleRow.Name + "\" " + (roleRow.IsIDNull() ? "(null)" : roleRow.ID.ToString()));
					}
				}

				throw new ConfigurationErrorsException("Not all roles defined.\r\n\r\n" + definedRolesDump + "\r\n" + roleConfigRowsDump);
			}
		}
	}
}