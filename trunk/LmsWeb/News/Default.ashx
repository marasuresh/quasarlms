<%@ WebHandler Language="C#" Class="NewsChannelHandler" %>

using System;
using System.Web;
using System.Data;

public class NewsChannelHandler : NewsChannelHttpHandlerBase {

	protected override void PopulateChannel(string channelName, string userName)
	{
		string _lang = LocalisationService.Language;
		if (string.IsNullOrEmpty(_lang)) {
			_lang = "ru";
		}
		
		DataTable _table = DceAccessLib.DAL.NewsController.Select();

		this.Channel.LastBuildDate = DateTime.Today.ToString();
		this.Channel.Language = _lang;
		this.Channel.Copyright = "Copyright 2007, Kvazar-Micro";
		this.Channel.Name = "DCE News Channel";
		
		foreach(DataRow _row in _table.Rows) {
			NewsChannelItem _item = new NewsChannelItem();
			_item.Guid = ((Guid)_row["id"]).ToString();
			_item.PubDate = ((DateTime)_row["date"]).ToString();
			_item.Title = (string)_row["head"];
			_item.Description = (string)_row["text"];
			
			_item.Link = Convert.IsDBNull(_row["moreHref"]) ? string.Empty :(string)_row["moreHref"];
			_item.Enclosure = Convert.IsDBNull(_row["Image"]) ? string.Empty : (string)_row["Image"];
			_item.MoreText = Convert.IsDBNull(_row["moreText"]) ? string.Empty : (string)_row["moreText"];
			
			this.Channel.Items.Add(_item);
			
		}
		
	}
}