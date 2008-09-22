using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using N2.Templates.Items;

/// <summary>
/// Summary description for ProxyItem
/// </summary>
[N2.Definition]
public class ProxyItem : AbstractContentPage, IStructuralPage
{
	public override string IconUrl { get { return "~/Lms/UI/Img/03/15.png"; } }
	
	[N2.Details.EditableTextBox("Url", 100)]
	public string Url {
		get { return (string)this.GetDetail("TemplateUrl"); }
		set { this.SetDetail<string>("TemplateUrl", value); }
	}

	public override string TemplateUrl { get { return this.Url; } }
}