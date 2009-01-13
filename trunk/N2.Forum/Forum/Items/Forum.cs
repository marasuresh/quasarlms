using N2.Templates.Items;
using N2.Web.UI;

namespace N2.Templates.Forum.Items
{
    /// <summary>
    /// A page containing user information.
    /// </summary>
    [Definition("Forum", "Forum", "A forum that is implemented inline", "", 20)]
    [TabPanel(Forum.ForumSettings, "Forum Settings", 500)]
    public class Forum : AbstractContentPage
    {
        #region Constants
        public const string ForumSettings = "forumSettings";
        #endregion

        #region Properties
        public override string TemplateUrl { get { return "~/Forum/UI/Views/Forum.aspx"; } }

		public override string IconUrl { get { return "~/Forum/UI/Images/forum.png"; } }
        #endregion

    }
}
