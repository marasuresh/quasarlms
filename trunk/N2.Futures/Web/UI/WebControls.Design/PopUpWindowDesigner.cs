using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI.Design;
using N2.Web.UI.WebControls;

namespace N2.Web.UI.WebControls
{
    public class PopUpWindowDesigner : ControlDesigner
    {
        protected override void PostFilterProperties(IDictionary properties)
        {
            properties.Remove("AccessKey");
            properties.Remove("BackColor");
            properties.Remove("BackImageUrl");
            properties.Remove("BorderColor");
            properties.Remove("BorderStyle");
            properties.Remove("BorderWidth");
            properties.Remove("CssClass");
            properties.Remove("DefaultButton");
            properties.Remove("Direction");
            properties.Remove("Enabled");
            properties.Remove("EnableTheming");
            properties.Remove("EnableViewState");
            properties.Remove("Font-Bold");
            properties.Remove("Font-Italic");
            properties.Remove("Font-Names");
            properties.Remove("Font-Override");
            properties.Remove("Font-Size");
            properties.Remove("Font-Strikeout");
            properties.Remove("Font-Underline");
            properties.Remove("ForeColor");
            properties.Remove("GroupingText");
            properties.Remove("ScrollBars");
            properties.Remove("ScinID");
            properties.Remove("TabIndex");
            properties.Remove("ToolTip");
            properties.Remove("Visible");
            properties.Remove("Wrap");

        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            base.SetViewFlags(ViewFlags.TemplateEditing, true);
        }

        protected override string GetEmptyDesignTimeHtml()
        {
            var text = "Switch to design view to add a tamplate to this control.";
            return CreatePlaceHolderDesignTimeHtml(text);

        }

        protected override string GetErrorDesignTimeHtml(Exception e)
        {
            string text = string.Format("{0} {1} {2} {3})",
                "Error was happen. The control can't by reflected.",
                "<br/>",
                "Exception: ",
                e.Message);
            return CreatePlaceHolderDesignTimeHtml(text);
        }

        public override string GetDesignTimeHtml()
        {
            try
            {
                var PUpWindow = (PopUpWindow)base.Component;
                return string.Concat(new object[] 
                    { "<div>\r\n","PopUpWindow",
                      "</div>\r\n"
                        
                    });
            }
            catch (Exception e)
            {
                return GetErrorDesignTimeHtml(e);
            }
        }

    }
}
