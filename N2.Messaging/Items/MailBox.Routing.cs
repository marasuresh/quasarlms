using System;
using System.Linq;
using System.Collections.Generic;

namespace N2.Messaging
{
	using N2.Collections;

	partial class MailBox
	{
		#region MVC implementation

		public enum ActionEnum
		{
			List,
			Reply,
			Forward,
			Create
		}

		public ActionEnum Action { get; set; }
		public string Folder { get; set; }
		public string Filter { get; set; }


		public override ContentItem GetChild(string childName)
		{
			var _matches = Routes.Match(new Uri(BaseUri, childName));

			if (_matches.Any()) {

				var _match = _matches.First();
				this.Action = (ActionEnum)_match.Data;
				
				switch (this.Action) {
					case ActionEnum.List:
						this.Folder = _match.BoundVariables["folder"];
						this.Filter = _match.BoundVariables["filter"];
						break;
					case ActionEnum.Create:
						this.EditedItem = Context.Definitions.CreateInstance<Message>(this.MessageStore);
						break;
					case ActionEnum.Forward:
						{
							int _id = int.Parse(_match.BoundVariables["id"]);
							var _original = Context.Persister.Get(_id);
							this.EditedItem = (Message)_original.Clone(false);
							this.EditedItem.ID = 0;
							this.EditedItem.Name = null;
							this.EditedItem.Subject = "Fw: " + this.EditedItem.Subject;
							this.EditedItem.To = string.Empty;
						}
						break;
					case ActionEnum.Reply:
						{
							int _id = int.Parse(_match.BoundVariables["id"]);
							var _original = Context.Persister.Get(_id);
							this.EditedItem = (Message)_original.Clone(false);
							this.EditedItem.ID = 0;
							this.EditedItem.Name = null;
							this.EditedItem.Subject = "Re: " + this.EditedItem.Subject;
							this.EditedItem.To = this.EditedItem.From;
						}
						break;
					}
				
				return this;
			}

			return base.GetChild(childName);
		}
		
		#endregion MVC implementation

		#region Uri Routing

		readonly static Uri BaseUri = new Uri("http://localhost/");

		static UriTemplateTable s_routes;
		static UriTemplateTable Routes { get { return s_routes ?? (s_routes = GetRoutes()); } }

		static UriTemplateTable GetRoutes()
		{
			var _uris = new[] {
				new KeyValuePair<UriTemplate, object>(new UriTemplate("folder/{folder}/filter/{filter}"), ActionEnum.List),
				new KeyValuePair<UriTemplate, object>(new UriTemplate("folder/{folder}"), ActionEnum.List),
				new KeyValuePair<UriTemplate, object>(new UriTemplate("reply/{id}"), ActionEnum.Reply),
				new KeyValuePair<UriTemplate, object>(new UriTemplate("forward/{id}"), ActionEnum.Forward),
				new KeyValuePair<UriTemplate, object>(new UriTemplate("new"), ActionEnum.Create),
			};

			return new UriTemplateTable(BaseUri, _uris);
		}

		#endregion Uri routing

		#region N2 menu support

		class FakeMailBox : MailBox
		{
		}

		ItemList GetFakeChildren()
		{
			if (this is FakeMailBox) {
				return new ItemList();
			}

			var _result = new ItemList(new[] {
				new FakeMailBox { Title = "Входящие", Name = "folder/" + C.Folders.Inbox, Parent = this},
				new FakeMailBox { Title = "Черновики", Name = "folder/" + C.Folders.Drafts, Parent = this},
				new FakeMailBox { Title = "Удаленные", Name = "folder/" + C.Folders.RecyleBin, Parent = this},
				new FakeMailBox { Title = "Отправленные", Name = "folder/" + C.Folders.Outbox, Parent = this},
			});

			foreach (var _child in _result) {
				((Web.IUrlParserDependency)_child).SetUrlParser(N2.Context.UrlParser);
			}

			return _result;
		}

		public override ItemList GetChildren(params ItemFilter[] filters)
		{
			if (filters.Length == 1 && filters[0] is NavigationFilter) {
				return this.GetFakeChildren();
			}
			return base.GetChildren(filters);
		}

		public override ItemList GetChildren()
		{
			return this.GetFakeChildren();
		}
		
		#endregion N2 menu support
	}
}
