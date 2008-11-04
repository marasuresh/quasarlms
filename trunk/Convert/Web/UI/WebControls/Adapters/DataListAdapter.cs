using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace N2.Lms.Web.UI.WebControls.Adapters
{
	using CSSFriendly;

	public class DataListAdapter : System.Web.UI.WebControls.Adapters.WebControlAdapter
	{
		private WebControlAdapterExtender _extender = null;
		private WebControlAdapterExtender Extender
		{
			get
			{
				if (((_extender == null) && (Control != null)) ||
					((_extender != null) && (Control != _extender.AdaptedControl))) {
					_extender = new WebControlAdapterExtender(Control);
				}

				System.Diagnostics.Debug.Assert(_extender != null, "CSS Friendly adapters internal error", "Null extender instance");
				return _extender;
			}
		}

		protected override void RenderBeginTag(HtmlTextWriter writer)
		{
			if (Extender.AdapterEnabled) {
				Extender.RenderBeginTag(writer, "AspNet-DataList");
			} else {
				base.RenderBeginTag(writer);
			}
		}

		protected override void RenderEndTag(HtmlTextWriter writer)
		{
			if (Extender.AdapterEnabled) {
				Extender.RenderEndTag(writer);
			} else {
				base.RenderEndTag(writer);
			}
		}

		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (Extender.AdapterEnabled) {
				DataList dataList = Control as DataList;

				if (dataList != null) {
					if (dataList.HeaderTemplate != null && dataList.ShowHeader) {
						if (!String.IsNullOrEmpty(dataList.Caption)) {
							writer.WriteLine();
							writer.WriteBeginTag("div");
							writer.Write(HtmlTextWriter.TagRightChar);
							writer.Write(dataList.Caption);
							writer.WriteEndTag("div");
						}

						PlaceHolder container = new PlaceHolder();
						dataList.HeaderTemplate.InstantiateIn(container);
						container.DataBind();
						container.RenderControl(writer);
					}

					if (dataList.ItemTemplate != null) {
						writer.WriteLine();

						for (int iItem = 0; iItem < dataList.Items.Count; iItem++) {
							foreach (Control itemCtrl in dataList.Items[iItem].Controls) {
								itemCtrl.RenderControl(writer);
							}
						}
					}

					if (dataList.FooterTemplate != null && dataList.ShowFooter) {
						writer.WriteLine();

						PlaceHolder container = new PlaceHolder();
						dataList.FooterTemplate.InstantiateIn(container);
						container.DataBind();
						container.RenderControl(writer);
					}
				}
			} else {
				base.RenderContents(writer);
			}
		}
	}
}
