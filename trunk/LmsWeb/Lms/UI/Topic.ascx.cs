
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using N2.Lms.Items;
	using N2.Details;
	using N2.Resources;
	using N2.Web.UI;
	/// <summary>
	/// Просмотр учебных материалов по теме
	/// </summary>
public partial class Topic : ContentUserControl<N2.Lms.Items.Topic>
{
	protected override void OnLoad(EventArgs e)
	{
		Register.JQuery(this.Page);
		Register.StyleSheet(this.Page, "~/Lms/UI/Js/jQuery.tabs.css");
		Register.JavaScript(this.Page, "~/Lms/UI/Js/jQuery.tabs.js");

		base.OnLoad(e);
	}

	//TODO Move as property of Topic entity
	Training m_training;
	protected Training Training
	{
		get
		{
			return this.m_training
				?? (this.m_training = this.GetTraining());
		}
	}

	Training GetTraining()
	{
		N2.ContentItem _tr = this.CurrentItem.Parent;
		while (!(_tr is Training) && (_tr != null)) {
			_tr = _tr.Parent;
		}

		return _tr as Training;
	}
}