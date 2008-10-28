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
		
		public RecycleBin RecycleBin { get { return this.GetChild<RecycleBin>("RecycleBin", item => item); } }

		public DraughtStore DraftsFolder { get { return this.GetChild<DraughtStore>("Drafts", item => item); } }
		
		#endregion Sub-containers

		#region Item collections

		//Все сообщения текущего пользователя.
		public IEnumerable<Message> MyMessages { get { return this.GetMyMessages(); } }

		//Письма.
		public IEnumerable<Letter> MyLetters { get { return this.MyMessages.OfType<Letter>(); } }

		//Задания.
		public IEnumerable<Task> MyTasks { get { return this.MyMessages.OfType<Task>(); } }

		//Объявления.
		public IEnumerable<Announcement> MyAnnouncements { get { return this.MyMessages.OfType<Announcement>(); } }

		//В черновиках.
		public IEnumerable<Message> MyDrafts { get { return this.DraftsFolder.GetMyMessages(); } }

		//В корзине.
		public IEnumerable<Message> MyRecycled { get { return this.RecycleBin.GetMyMessages(); } }

		#endregion Item collections
	}
}
