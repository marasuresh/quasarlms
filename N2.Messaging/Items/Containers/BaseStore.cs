using N2.Templates.Items;

namespace N2.Messaging
{
    using N2.Definitions;
    using N2.Details;
    using N2.Edit.Trash;
    using N2.Integrity;
    using N2.Persistence;

    [Definition]
    [NotThrowable, NotVersionable]
    [AllowedChildren(typeof(Message))]
    public abstract class BaseStore : AbstractItem
    {
        public override string TemplateUrl { get { return "~/Templates/Secured/Go.aspx"; } }

		public override string IconUrl { get { return "~/Lms/UI/Img/04/20.png"; } }
    }
}
