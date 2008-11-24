using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace N2.Messaging
{

	[DataObject]
	partial class MailBox
	{
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public IEnumerable<Message> GetFilteredFolderMessages()
		{
			var _args = Context.Current.RequestContext.CurrentTemplate.Argument.Split('/');
			string _folder = _args[0] ?? C.Folders.Inbox;
			string _filter = _args.Length > 1 ? _args[1] : C.Filter.All;

			switch (_folder) {
				default:
					return GetFilteredMessages(
						this.MessageStore.MyInbox,
						_filter);
				case C.Folders.Drafts:
					return this.MessageStore.MyDrafts;
				case C.Folders.RecyleBin:
					return this.MessageStore.MyRecycled;
				case C.Folders.Outbox:
					return GetFilteredMessages(
						this.MessageStore.MyOutbox,
						_filter);
			}
		}

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public IEnumerable<Message> GetFilteredFolderMessages(int maximumRows, int startRowIndex)
        {
			return this.GetFilteredFolderMessages()
				.Skip(startRowIndex)
				.Take(maximumRows);
        }

        public Int32 TotalNumberOfMessage()
        {
			return this.GetFilteredFolderMessages()
				.Count();
        }

		Message m_editedItem;
		public Message GetEditedItem()
		{
			if (null != this.m_editedItem) {
				return this.m_editedItem;
			}

			ActionEnum _action = (ActionEnum)Enum.Parse(
				typeof(ActionEnum),
				Context.Current.RequestContext.CurrentTemplate.Action);

			int _id;
			int.TryParse(Context.Current.RequestContext.CurrentTemplate.Argument, out _id);

			switch (_action) {
				case ActionEnum.Forward: {
						var _original = Context.Persister.Get(_id);
						this.m_editedItem = (Message)_original.Clone(false);
						this.m_editedItem.ID = 0;
						this.m_editedItem.Name = null;
						this.m_editedItem.Subject = "Fw: " + this.m_editedItem.Subject;
						this.m_editedItem.To = string.Empty;
					}
					break;
				case ActionEnum.Reply: {
						var _original = Context.Persister.Get(_id);
						this.m_editedItem = (Message)_original.Clone(false);
						this.m_editedItem.ID = 0;
						this.m_editedItem.Name = null;
						this.m_editedItem.Subject = "Re: " + this.m_editedItem.Subject;
						this.m_editedItem.To = this.m_editedItem.From;
					}
					break;
				//case ActionEnum.Create:
				default:
					this.m_editedItem = Context.Definitions.CreateInstance<Message>(this.MessageStore);
					break;
			}

			return this.m_editedItem;
		}
	}
}
