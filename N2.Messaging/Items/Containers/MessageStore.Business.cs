using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;

namespace N2.Messaging
{
	using N2.Templates.Items;
	using N2.Definitions;
	using N2.Details;
	using N2.Edit.Trash;
	using N2.Integrity;
	using N2.Persistence;
	using N2;
	
	partial class MessageStore
	{
		
		#region Sub-containers
		
		public RecycleBin RecycleBin { get { return this.GetOrFindOrCreateChild<RecycleBin>("RecycleBin", item => item); } }

		public DraughtStore DraftsFolder { get { return this.GetOrFindOrCreateChild<DraughtStore>("Drafts", item => item); } }
		
		#endregion Sub-containers

		#region Item collections

		//Все сообщения текущего пользователя.
		public IEnumerable<Message> MyInbox { get {
			return this.Children.OfType<Message>().Where(_msg => string.Equals(_msg.To, HttpContext.Current.User.Identity.Name, StringComparison.OrdinalIgnoreCase));
		} }

		//В черновиках.
		public IEnumerable<Message> MyDrafts { get { return this.DraftsFolder.GetMyMessages(); } }

		//В корзине.
		public IEnumerable<Message> MyRecycled { get { return this.RecycleBin.GetMyMessages(); } }

		public IEnumerable<Message> MyOutbox { get {
			return this.Children.OfType<Message>().Where(_msg => string.Equals(_msg.From, HttpContext.Current.User.Identity.Name, StringComparison.OrdinalIgnoreCase));
		} }

		#endregion Item collections
	}
}
