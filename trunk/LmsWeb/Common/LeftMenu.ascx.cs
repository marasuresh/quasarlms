//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\common\Stub_leftmenu_ascx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'common\leftmenu.ascx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
namespace DCE.Common
{
	using System;
	using System.Data;
	using System.Web;
	using System.Xml;
	using System.Web.UI;
	using System.Collections.Generic;
	
	/// <summary>
	/// Левое меню
	/// </summary>
	public partial  class Migrated_LeftMenu : LeftMenu
	{
		private int genId = 0;
		
		/// <summary>
		/// Перебор пунктов дерева меню и удаление неотображаемых пунктов
		/// рекурсивно вызываемый метод
		/// </summary>
		/// <param name="item">пункт меню для проверки вглубь дерева</param>
		/// <param name="id">id пункта меню</param>
		/// <returns>является ли данная ветка выбраной пользователем</returns>
		private bool itemsEnum(XmlNode item, Guid? id)
		{
			XmlNodeList list = item.SelectNodes("item");
			
			if (list.Count > 0) {
				XmlAttribute a = doc.CreateAttribute("expand");
				a.Value = "true";
				item.Attributes.Append(a);
			}
			bool ret = false;
			
			XmlNode idNode = item.SelectSingleNode("id");
			
			if(idNode == null) {
				this.genId++;
				string newNodeId = genId.ToString();
				idNode = doc.CreateNode(XmlNodeType.Element, "id", "");
				idNode.InnerText = newNodeId;
				item.AppendChild(idNode);
			}
			
			if (idNode != null && GuidService.Parse(idNode.InnerText) == id) {
				XmlAttribute a = doc.CreateAttribute("selected");
				a.Value = "true";
				item.Attributes.Append(a);
				ret = true;
			}
			
			foreach (XmlNode child in list) {
				if (child.Name == "item" && this.itemsEnum(child, id)) {
					ret = true;
				}
			}
			/*
			if (!ret) {
				foreach (XmlNode child in list) {
					if (child.Name == "item") {
						item.RemoveChild(child);
					}
				}
			}*/
			
			return ret;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack) {
				Guid? id = GuidService.Parse(this.Request[/*.Form[*/"id"]);
				XmlNode root = doc.SelectSingleNode("/xml/Items");

				if (null != root) {

					foreach (XmlNode item in root.ChildNodes) {
						this.itemsEnum(item, id);
					}

					this.XmlDataSource1.Data = "<Items>" + root.InnerXml + "</Items>";
					this.XmlDataSource1.DataBind();
				}
			}
			
			this.Page.ClientScript.RegisterClientScriptInclude(
					this.GetType(),
					"UrlParamScript",
					this.ResolveUrl("~/js/urlparam.js"));
			
			this.Page.ClientScript.RegisterClientScriptInclude(
					this.GetType(),
					"LeftMenuScript",
					this.ResolveUrl("~/xsl/LeftMenu.js"));
		}
	}
}
