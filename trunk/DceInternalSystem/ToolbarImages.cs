using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Xml;

namespace DCEInternalSystem
{
	/// <summary>
	/// Ресурсы для Toolbar
	/// </summary>
	public class ToolbarImages : System.Windows.Forms.UserControl
	{
      private System.Windows.Forms.ImageList imageList1;
      private System.ComponentModel.IContainer components;

      public class Country
      {
         public string Name;
         public string Value;
         public override string ToString()
         {
            return Name;
         }
         public Country()
         {
         }
         public Country(string name, string val)
         {
            Name = name;
            Value = val;
         }
      }

      public Country[] countries;

		public ToolbarImages()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

         // loading countries XML
         string s = DCEAccessLib.XmlReports.LoadXmlFromResource("DCEInternalSystem.Res.Countries.xml");
         System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
         doc.LoadXml(s);
         XmlNodeList colist = doc.DocumentElement.ChildNodes;
           
         countries = new Country[colist.Count]; 
         int i=0;
         foreach (XmlNode node in colist)
         {
            countries[i] = new Country(node.InnerText,node.Attributes["value"].InnerText);
            i++;
         }
		}

      public static ToolbarImages Images = new ToolbarImages();

      public ImageList images
      {
         get
         {
            return this.imageList1;
         }
      }
		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
         this.components = new System.ComponentModel.Container();
         System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ToolbarImages));
         this.imageList1 = new System.Windows.Forms.ImageList(this.components);
         // 
         // imageList1
         // 
         this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
         this.imageList1.ImageSize = new System.Drawing.Size(18, 18);
         this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
         this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
         // 
         // ToolbarImages
         // 
         this.Name = "ToolbarImages";
         this.Size = new System.Drawing.Size(484, 68);

      }
		#endregion
	}
}
