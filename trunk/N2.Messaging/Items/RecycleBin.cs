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
    //[ItemAuthorizedRoles(Roles = new string[0])]
    [NotThrowable, NotVersionable]
    [AllowedChildren(typeof(Message))]
    [RestrictParents(typeof(IStructuralPage))]
    public class RecycleBin : ContentItem
    {
        public override string IconUrl { get { return "~/Lms/UI/Img/04/20.png"; } }

        public override bool IsPage { get { return false; } }
    }
}