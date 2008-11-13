using System.Web.UI.WebControls;

namespace N2.Lms.Items
{
	using System.Linq;
	using N2.Definitions;
	using N2.Details;
	using N2.Installation;
	using N2.Integrity;
	using N2.Templates.Items;

	[Definition("Topic", "Topic", Installer = InstallerHint.NeverRootOrStartPage)]
	[RestrictParents(typeof(TopicContainer), typeof(Topic))]
	[WithEditableName("Name (Guid)", 03)]
	[WithEditableTitle]
	[WithEditablePublishedRange("Published between", 07)]
	[ReplaceDefinitions(typeof(AbstractContentPage), typeof(ContentItem))]
	public partial class Topic : ContentItem
	{
		#region System properties

		public override string IconUrl { get { return "~/Lms/UI/Img/04/19.png"; } }
		public override string TemplateUrl { get {
			return this.UseExternalLink
				? "~/Lms/UI/TopicInFrame.ascx"
				: "~/Lms/UI/Topic.ascx"; } }
		public override string ZoneName { get { return "Topics"; } }
		public override bool IsPage { get { return false; } }

		#endregion System properties

		#region Lms Properties
		[EditableTextBox("Файлы содержания", 17, TextMode = TextBoxMode.MultiLine)]
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

		[EditableCheckBox("Использовать внешние файлы содержания", 15,
			HelpTitle = "Указание на то, откуда брать содержимое урока.",
			HelpText = "Содержание урока может браться из внешнего файла в формате <strong>.html</strong> (чаще всего это происходит при импорте существующих курсов), или содержаться прямо в системе. В последнем случае для редактирования можно пользоваться встроенным редактором.")]
		public bool UseExternalLink {
			get { return this.GetDetail<bool>("UseExternalLink", true); }
			set { this.SetDetail<bool>("UseExternalLink", value); }
		}

		[EditableFreeTextArea("Содержание", 13)]
		public string Text {
			get { return this.GetDetail<string>("Text", string.Empty); }
			set { this.SetDetail<string>("Text", value); }
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
