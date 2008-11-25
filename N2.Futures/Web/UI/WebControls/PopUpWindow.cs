using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using N2.Resources;

namespace N2.Web.UI.WebControls
{
    [DefaultProperty("Title")]
    [ToolboxData("<{0}:PopUpWindow runat=server></{0}:PopUpWindow>")]
    //[Designer(typeof(PopUpWindowDesigner))]
    [ToolboxBitmap(typeof(PopUpWindow), "N2.Futures.Images.PopUpWindow.PUpWindow.png")]
    public class PopUpWindow : Panel
    {
        #region Enums
        public enum PUpWindowPositionEnum
        {
            AssociatedControl,
            DocumentCenter
        }

        #endregion

        #region Constructor
        public PopUpWindow()
        {
            Title = "";
            AssociatedControlID = "";
            CssClass = "PUpWindow";
            minWidth = 160;
            maxWidth = 800;
            minHeight = 420;
            maxHeight = 800;
            Height = 300;
            Width = 400;
        }
        #endregion

        #region Properties

        #region public string Title
        [Browsable(true)]
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(typeof(String), "")]
        [Description("The title of the PopUpWindow.")]
        public string Title
        {
            get
            {
                var str = (string)ViewState["Title"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }

            set
            {
                ViewState["Title"] = value;
            }
        }
        #endregion

        #region public string AssociatedControlID
        [Browsable(true)]
        [Bindable(true)]
        [Category("Accessibility")]
        [IDReferenceProperty]
        [TypeConverter(typeof(AssociatedControlConverter))]
        [DefaultValue(typeof(String), "")]
        [Description("The ID of associated control in current container.")]
        public string AssociatedControlID
        {
            get
            {
                var str = (string)ViewState["AssociatedControlID"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                ViewState["AssociatedControlID"] = value;
            }
        }
        #endregion

        #region private string AssociatedControlClientID
        private string AssociatedControlClientID
        {
            get
            {
                var str = (string)ViewState["AssociatedControlClientID"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                ViewState["AssociatedControlClientID"] = value;
            }
        }
        #endregion

        #region public Unit maxWidth
        [Browsable(true)]
        [Category("Layout")]
        [DefaultValue(typeof(Unit), "800")]
        [Description("Specifies the width of the movie in either pixels or percentage")]
        public Unit maxWidth
        {
            get
            {
                var unit = (Unit)ViewState["MaxWidth"];
                if (unit == 0)
                {
                    return unit;
                }
                return Unit.Empty;
            }
            set
            {
                if (value.Value >= 160 || value.Value <= 800)
                    ViewState["MaxWidth"] = value;
            }
        }
        #endregion

        #region public Unit minWidth
        [Browsable(true)]
        [Category("Layout")]
        [DefaultValue(typeof(Unit), "160")]
        [Description("Specifies the width of the movie in either pixels or percentage")]
        public Unit minWidth
        {
            get
            {
                var unit = (Unit)ViewState["MinWidth"];
                if (unit == 0)
                {
                    return unit;
                }
                return Unit.Empty;
            }
            set
            {
                if (value.Value >= 160 || value.Value <= 800)
                    ViewState["MinWidth"] = value;
            }
        }
        #endregion

        #region public Unit maxHeight
        [Browsable(true)]
        [Category("Layout")]
        [DefaultValue(typeof(Unit), "420")]
        [Description("Specifies the width of the movie in either pixels or percentage")]
        public Unit maxHeight
        {
            get
            {
                var unit = (Unit)ViewState["MaxHeight"];
                if (unit == 0)
                {
                    return unit;
                }
                return Unit.Empty;
            }
            set
            {
                if (value.Value >= 115 || value.Value <= 420)
                    ViewState["MaxHeight"] = value;
            }
        }
        #endregion

        #region public Unit minHeight
        [Browsable(true)]
        [Category("Layout")]
        [DefaultValue(typeof(Unit), "115")]
        [Description("Specifies the width of the movie in either pixels or percentage")]
        public Unit minHeight
        {
            get
            {
                var unit = (Unit)ViewState["MinHeight"];
                if (unit == 0)
                {
                    return unit;
                }
                return Unit.Empty;
            }
            set
            {
                if (value.Value >= 115 || value.Value <= 420)
                    ViewState["MinHeight"] = value;
            }
        }
        #endregion

        #region public override Unit Width
        [Browsable(true)]
        [Category("Layout")]
        [DefaultValue(typeof(Unit), "400")]
        [Description("Specifies the width of the PopUpWindow in either pixels.")]
        public override Unit Width
        {
            get
            {
                var unit = (Unit)ViewState["PUpWindowWidth"];
                return ((unit == 0) ? Unit.Empty : unit);
            }
            set
            {
                if (value.Value >= 160 || value.Value <= 800)
                    ViewState["PUpWindowWidth"] = value;
            }
        }
        #endregion

        #region public override Unit Height
        [Browsable(true)]
        [Category("Layout")]
        [DefaultValue(typeof(Unit), "300")]
        [Description("Specifies the height of the PopUpWindow in either pixels.")]
        public override Unit Height
        {
            get
            {
                var unit = (Unit)ViewState["PUpWindowHeight"];
                return ((unit == 0) ? Unit.Empty : unit);
            }
            set
            {
                if (value.Value >= 115 || value.Value <= 420)
                    ViewState["PUpWindowHeight"] = value;
            }
        }
        #endregion

        #region public PUpWindowPositionEnum AllignTo

        [Browsable(true)]
        [Category("Layout")]
        [DefaultValue(typeof(PUpWindowPositionEnum), "AssociatedControl")]
        [Description("Defines how the PopUpWindow allign to objects on a page.")]
        public PUpWindowPositionEnum AllignTo { get; set; }

        #endregion

        #endregion

        #region public/protected Methods

        protected override void OnInit(EventArgs e)
        {
            Register.StyleSheet(Page, Page.ClientScript.GetWebResourceUrl(
                                    typeof(PopUpWindow), "N2.Futures.Css.PopUpWindow.PopUpWindow.css"));

            Register.JQuery(Page);
            Register.JavaScript(Page, typeof(PopUpWindow), "N2.Futures.Scripts.PopUpWindow.interface.js");
            Register.JavaScript(Page, typeof(PopUpWindow), "N2.Futures.Scripts.PopUpWindow.interface.path.js");


            base.OnInit(e);
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            string associatedControlID = AssociatedControlID;
            if (associatedControlID.Length != 0)
            {
                Control control = FindControl(associatedControlID);
                if (control != null)
                {
                    var clientID = control.ClientID;
                    writer.AddAttribute(HtmlTextWriterAttribute.For, clientID);
                    AssociatedControlClientID = clientID;
                }
            }


            base.AddAttributesToRender(writer);
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            base.RenderBeginTag(writer);

            writer.WriteJScript(PUpWindowJScript());

            writer.Write(@"<div id='window'>");
            writer.Write(@"<div id='windowTop'>");
            writer.Write(@"<div id='windowTopContent'>" + Title + "</div>");
            writer.Write(@"<img src='" + Page.ClientScript.GetWebResourceUrl(typeof(PopUpWindow), "N2.Futures.Images.PopUpWindow.window_min.png") + "' id='windowMin' />");
            writer.Write(@"<img src='" + Page.ClientScript.GetWebResourceUrl(typeof(PopUpWindow), "N2.Futures.Images.PopUpWindow.window_max.png") + "' id='windowMax' />");
            writer.Write(@"<img src='" + Page.ClientScript.GetWebResourceUrl(typeof(PopUpWindow), "N2.Futures.Images.PopUpWindow.window_close.png") + "' id='windowClose' />");
            writer.Write(@"</div>");
            writer.Write(@"<div id='windowBottom'><div id='windowBottomContent'>&nbsp;</div></div>");
            writer.Write(@"<div id='windowContent'>");

        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.Write(@"</div>");
            writer.Write(@"<img src='" + Page.ClientScript.GetWebResourceUrl(typeof(PopUpWindow), "N2.Futures.Images.PopUpWindow.window_resize.gif") + "' id='windowResize' />");
            writer.Write(@"</div>");

            base.RenderEndTag(writer);
        }

        #endregion

        #region Private Methods

        private string PUpWindowJScript()
        {
            var OpenPUpWindowControlID = AssociatedControlClientID ?? "windowOpen";
            var duration = (AllignTo == PUpWindowPositionEnum.AssociatedControl) ? 250 : 400;

            var script = new StringBuilder();
            script.Append(@"
$(document).ready(
	function() {
	    $('#" + OpenPUpWindowControlID +
                          @"').bind(
			'click',
			function() {
			    if ($('#window').css('display') == 'none') {
			        $(this).TransferTo(
						{
						    to: 'window',
						    className: 'transferer2',
						    duration: " + duration + @",
						    complete: function() {
						        $('#window').show();
						    }
						}
					);
			    }
			    this.blur();
			    return false;
			}
		);
	    $('#windowClose').bind(
			'click',
			function() {
			    $('#window').TransferTo(
					{
					    to: '" +
                          OpenPUpWindowControlID +
                          @"',
					    className: 'transferer2',
					    duration: " + duration + @"
					}
				).hide();
			}
		);
	    $('#windowMin').bind(
			'click',
			function() {
			    $('#windowContent').SlideToggleUp(300);
			    $('#windowBottom, #windowBottomContent').animate({ height: 10 }, 300);
			    $('#window').animate({ height: 40 }, 300).get(0).isMinimized = true;
			    $(this).hide();
			    $('#windowResize').hide();
			    $('#windowMax').show();
			}
		);
	    $('#windowMax').bind(
			'click',
			function() {
			    var windowSize = $.iUtil.getSize(document.getElementById('windowContent'));
			    $('#windowContent').SlideToggleUp(300);
			    $('#windowBottom, #windowBottomContent').animate({ height: windowSize.hb + 13 }, 300);
			    $('#window').animate({ height: windowSize.hb + 43 }, 300).get(0).isMinimized = false;
			    $(this).hide();
			    $('#windowMin, #windowResize').show();
			}
		);
	    $('#window').Resizable(
			{
			    minWidth: " +
                          minWidth.Value + @",
			    minHeight: " + minHeight.Value + @",
			    maxWidth: " +
                          maxWidth.Value + @",
			    maxHeight: " + maxHeight.Value +
                          @",
			    dragHandle: '#windowTop',
			    handlers: {
			        se: '#windowResize'
			    },
			    onResize: function(size, position) {
			        $('#windowBottom, #windowBottomContent').css('height', size.height - 33 + 'px');
			        var windowContentEl = $('#windowContent').css('width', size.width - 25 + 'px');
			        if (!document.getElementById('window').isMinimized) {
			            windowContentEl.css('height', size.height - 48 + 'px');
			        }
			    }
			}
		);
        var el = $('#" +
                          OpenPUpWindowControlID + @"');
        el.addClass('openPUpWindowControl');");

            if (AllignTo == PUpWindowPositionEnum.AssociatedControl)
            {
                script.Append(@"
        var offset = el.offset();
        var PUpWindowTop = offset.top + $('#" +
                              OpenPUpWindowControlID + @"').height() + 6;
        var PUpWindowLeft = offset.left;");
            }
            else
            {
                script.Append(@"
        var mainWindow = $(window);
        var PUpWindowTop = mainWindow.height()/2 - " + Height.Value + @"/2;
        var PUpWindowLeft = mainWindow.width()/2 - " + Width.Value + @"/2;
            ");
            }


            script.Append(@"
        var PUpWindow = $('#window');
        PUpWindow
            .css('top', PUpWindowTop)
            .css('left', PUpWindowLeft);
        $('#windowBottom, #windowBottomContent').css('height', " + Height.Value + @" - 33);
        var windowContentEl = $('#windowContent').css('width', " + Width.Value + @" - 25);
        if (!document.getElementById('window').isMinimized) {
            windowContentEl.css('height', " + Height.Value + @" - 48);
        }
        PUpWindow
            .css('height', " + Height.Value + @")
            .css('width', " + Width.Value + @");
    }
);
            ");

            return script.ToString();
        }

        #endregion
    }
}
