using System.Web.UI.WebControls;

namespace N2.Lms.Items
{
	using System.Collections.Generic;
	using System.Linq;
	using N2.Definitions;
	using N2.Details;
	using N2.Installation;
	using N2.Integrity;
	using N2.Templates.Items;

	[Definition("Topic", "Topic", Installer = InstallerHint.NeverRootOrStartPage)]
	[RestrictParents(typeof(TopicList), typeof(Topic))]
	[WithEditableName("Name (Guid)", 03)]
	[WithEditableTitle("Title", 05)]
	[WithEditablePublishedRange("Published between", 07)]
	[ReplaceDefinitions(typeof(AbstractContentPage), typeof(ContentItem))]
	public partial class Topic : AbstractItem
	{
		#region System properties

		public override string IconUrl { get { return "~/Lms/UI/Img/04/19.png"; } }
		
		public override string TemplateUrl { get { return "~/Lms/UI/Topic.ascx"; } }
		
		public override string ZoneName {
			get { return "Topics"; }
			set { base.ZoneName = value; }
		}
		public override bool IsPage { get { return false; } }

		#endregion System properties

		#region Lms Properties
		[EditableTextBox(
			"Content URLs",
			13,
			TextMode = TextBoxMode.MultiLine)]
		public string ContentUrls {
			get {
				return string.Join("\n", this.Content.Cast<string>().ToArray());
			}
			set {
				this.Content.Clear();
				this.Content.AddRange(
					from _line in  value.Split('\n', '\r')
					select new N2.Details.StringDetail(this, string.Empty, _line)
					);
			}
		}

		internal DetailCollection Content {
			get {
				return
					this.GetDetailCollection("Content", true);
			}
		}

		/*		[EditableUrl("Content Url", 30)]
		public string ContentUrl {
			get { return (string)this.GetDetail("ContentUrl"); }
			set { this.SetDetail<string>("ContentUrl", value); }
		}
*/
		[EditableCheckBox("Mandatory", 70)]
		public bool Mandatory {
			get { return (bool?)this.GetDetail("Mandatory") ?? true; }
			set { this.SetDetail<bool>("Mandatory", value); }
		}
		
		#endregion Lms Properties
	}
}
