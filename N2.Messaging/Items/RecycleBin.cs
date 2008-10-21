using N2.Templates.Items;

namespace N2.Messaging
{
    using N2.Definitions;
    using N2.Details;
    using N2.Edit.Trash;
    using N2.Integrity;
    using N2.Persistence;

    [Definition]
    [WithEditableTitle("Title", 10)]
    [NotThrowable, NotVersionable]
    [AllowedChildren(typeof(Message))]
    public class RecycleBin : BaseStore
    {
		public RecycleBin()
		{
			this.Title = "Корзина";
		}

		public override string IconUrl {
			get { return "~/Messaging/UI/Images/basket_edit.png"; }
		}
    }
}