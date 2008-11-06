using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;

using N2;
using N2.Integrity;
using N2.Templates;
using N2.Templates.Items;
using N2.Web.UI;

namespace N2.Templates.Forum.Items
{
    /// <summary>
    /// A page containing user information.
    /// </summary>
    [Definition("Forum", "Forum", "A forum that is implemented inline", "", 20)]
    [TabPanel(Forum.ForumSettings, "Forum Settings", 500)]
    public class Forum : AbstractContentPage
    {
        #region Constants
        public const string ForumSettings = "forumSettings";

        /// <summary>
        /// Yet Another Forum configuration file
        /// </summary>
        private readonly string ConfigurationFile = System.Web.HttpContext.Current.Server.MapPath("~/yafnet.config");

        private readonly string DefaultConnectionString = "";
        private readonly string DefaultForumLocation = "/Forum/YAF/";
        private readonly string DefaultUploadFolder = "~/Upload/Forum/";
        #endregion

        #region Variables
        private string _connectionString = "";
        private string _customUserAssembly = "";
        private string _customUserClass = "";
        private string _forumLocation = "";
        private string _uploadFolder = "";
        #endregion

        #region Constructor & destructor
        public Forum()
        {
            // Subscribe to events
            N2.Context.Current.Persister.ItemSaved += new EventHandler<ItemEventArgs>(Persister_ItemSaved);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the connection string that is used for the forum database
        /// </summary>
        [N2.Details.EditableTextBox("Connection String", 80, ContainerName = ForumSettings,
            HelpText = "Set the connectionstring to a database. If you are using a database for N2CMS " + 
            "and you want to use the same database for the forum, simply leave this entry empty")]
        public virtual string ConnectionString
        {
            get
            {
                 // Return value
                return (string)GetDetail("ConnectionString") ?? string.Empty;
            }
            set
            {
                // Check for differences
                if (ConnectionString == value) return;

                // Store value
                SetDetail("ConnectionString", value);
            }
        }

        /// <summary>
        /// Gets or sets the user provider that handles the types in the forum
        /// </summary>
        [Details.UserProviderSelector("User Provider", 85, ContainerName = ForumSettings, 
            HelpText = "Here you can define whether the forum should have its own user registration or use " +
            "a custom one (which is derived from AbstractN2ForumUser or yaf.IForumUser)")]
        public virtual string UserProvider
        {
            get
            {
                // Declare variables
                string result = "";

                // Check the class
                if (string.IsNullOrEmpty(CustomUserClass))
                {
                    return result;
                }

                // Append the class
                result += CustomUserClass;

                // Check the assembly
                if (!string.IsNullOrEmpty(CustomUserAssembly))
                {
                    result += string.Format("|{0}", CustomUserAssembly);
                }

                // Return result
                return result;
            }
            set
            {
                // Split values
                if (string.IsNullOrEmpty(value))
                {
                    // Clear values
                    CustomUserAssembly = "";
                    CustomUserClass = "";

                    // Exit
                    return;
                }

                // Split value
                string[] splitted = value.Split(new char[] { '|' });

                // Validate
                if (splitted.Length != 2)
                {
                    // Exit
                    return;
                }

                // Store values internally
                CustomUserAssembly = splitted[1];
                CustomUserClass = splitted[0];
            }
        }

        /// <summary>
        /// Gets or sets the assembly that contains the custom user class
        /// </summary>
        //[N2.Details.EditableTextBox("Custom User Assembly", 90, ContainerName = ForumSettings)]
        public virtual string CustomUserAssembly
        {
            get
            {
                // Return value
                return (string)GetDetail("CustomUserAssembly") ?? string.Empty;
            }
            set
            {
                // Check for differences
                if (CustomUserAssembly == value) return;

                // Store value
                SetDetail("CustomUserAssembly", value);
            }
        }

        /// <summary>
        /// Gets or sets the type of the custom user class
        /// </summary>
        //[N2.Details.EditableTextBox("Custom User Class", 100, ContainerName = ForumSettings)]
        public virtual string CustomUserClass
        {
            get
            {
                // Return value
                return (string)GetDetail("CustomUserClass") ?? string.Empty;
            }
            set
            {
                // Check for differences
                if (CustomUserClass == value) return;

                // Store value
                SetDetail("CustomUserClass", value);
            }
        }

        /// <summary>
        /// Gets or sets the forum location (where the actual forum is located)
        /// </summary>
        [N2.Details.EditableTextBox("Forum Location", 110, DefaultValue = "/Forum/YAF/", ContainerName = ForumSettings)]
        public virtual string ForumLocation
        {
            get 
            {
                // Return value
                return GetDetail<string>("ForumLocation", "/Forum/YAF/");
            }
            set 
            {
                // Check for differences
                if (ForumLocation == value) return;

                // Store value
                SetDetail("ForumLocation", value);
            }
        }

        /// <summary>
        /// Gets or sets the upload folder for the forum
        /// </summary>
        [N2.Details.EditableTextBox("Upload Folder", 120, DefaultValue = "~/Upload/Forum/", ContainerName = ForumSettings)]
        public virtual string UploadFolder
        {
            get
            {
                // Return value
                return GetDetail<string>("UploadFolder", "~/Upload/Forum/");
            }
            set
            {
                // Check for differences
                if (UploadFolder == value) return;

                // Store value
                SetDetail("UploadFolder", value);
            }
        }

        /// <summary>
        /// Gets whether the configuration is up to date with the database settings
        /// </summary>
        protected bool IsConfigurationUpToDate
        {
            get
            {
                // Load
                Load();

                try
                {
                    // Check for differences
                    bool different = false;
                    if (ConnectionString != _connectionString) different = true;
                    if (CustomUserAssembly != _customUserAssembly) different = true;
                    if (CustomUserClass != _customUserClass) different = true;
                    if (ForumLocation != _forumLocation) different = true;
                    if (UploadFolder != _uploadFolder) different = true;

                    // Return result
                    return !different;
                }
                catch (Exception)
                { }

                // Not different
                return true;
            }
        }

        public override string TemplateUrl
        {
            get
            {
                return "~/Forum/UI/Views/Forum.aspx";
            }
        }

        public override string IconUrl
        {
            get
            {
                return "~/Forum/UI/Images/forum.png";
            }
        }
        #endregion

        #region Methods
        void Persister_ItemSaved(object sender, ItemEventArgs e)
        {
            // Check if the type is the current object
            if ((e.AffectedItem.GetType() == this.GetType()) && (e.AffectedItem.ID == this.ID))
            {
                // Save any differences
                if (!IsConfigurationUpToDate)
                {
                    // Save
                    Save();
                }
            }
        }

        /// <summary>
        /// Saves the configuration data
        /// </summary>
        /// <remarks>
        /// Unfortunately, we cannot use the properties here because we lost the session (or context). 
        /// Therefore, all the settings are stored inside local variables and these are used to save the values
        /// to the configuration file.
        /// </remarks>
        public void Save()
        {
            try
            {
                // Load the configuration
                XmlDocument doc = new XmlDocument();
                doc.Load(ConfigurationFile);

                // Create the root
                XmlNode rootNode = doc.SelectSingleNode("/yafnet");
                if (rootNode == null) return;

                // Connection string
                XmlNode connectionStringNode = doc.CreateElement("connstr");
                connectionStringNode.InnerText = (string.IsNullOrEmpty(ConnectionString)) ?
                    ((ConfigurationManager.ConnectionStrings.Count > 0) ? ConfigurationManager.ConnectionStrings[0].ConnectionString : DefaultConnectionString) :
                    ConnectionString;
                AddOrReplaceXmlNode(rootNode, connectionStringNode);

                // Custom user assembly
                XmlNode customUserAssemblyNode = doc.CreateElement("CustomUserAssembly");
                customUserAssemblyNode.InnerText = CustomUserAssembly;
                AddOrReplaceXmlNode(rootNode, customUserAssemblyNode);

                // Custom user class
                XmlNode customUserClassNode = doc.CreateElement("CustomUserClass");
                customUserClassNode.InnerText = CustomUserClass;
                AddOrReplaceXmlNode(rootNode, customUserClassNode);

                // Forum location
                XmlNode forumLocationNode = doc.CreateElement("root");
                forumLocationNode.InnerText = ForumLocation;
                AddOrReplaceXmlNode(rootNode, forumLocationNode);

                // Upload directory
                XmlNode uploadFolderNode = doc.CreateElement("uploaddir");
                uploadFolderNode.InnerText = UploadFolder;
                AddOrReplaceXmlNode(rootNode, uploadFolderNode);

                // Save
                doc.Save(ConfigurationFile);
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// Loads the configuration data
        /// </summary>
        public void Load()
        {
            try
            {
                // Load the configuration
                XmlDocument doc = new XmlDocument();
                doc.Load(ConfigurationFile);

                // Connection string
                XmlNode connectionStringNode = doc.SelectSingleNode("/yafnet/connstr");
                _connectionString = (connectionStringNode == null) ? ((ConfigurationManager.ConnectionStrings.Count > 0) ? ConfigurationManager.ConnectionStrings[0].ConnectionString : DefaultConnectionString) :
                    connectionStringNode.InnerText;

                // Custom user assembly
                XmlNode customUserAssemblyNode = doc.SelectSingleNode("/yafnet/CustomUserAssembly");
                _customUserAssembly = (customUserAssemblyNode == null) ? "" : customUserAssemblyNode.InnerText;

                // Custom user class
                XmlNode customUserClassNode = doc.SelectSingleNode("/yafnet/CustomUserClass");
                _customUserClass = (customUserClassNode == null) ? "" : customUserClassNode.InnerText;

                // Forum location
                XmlNode forumLocationNode = doc.SelectSingleNode("/yafnet/root");
                _forumLocation = (forumLocationNode == null) ? DefaultForumLocation : forumLocationNode.InnerText;

                // Upload directory
                XmlNode uploadFolderNode = doc.SelectSingleNode("/yafnet/uploaddir");
                _uploadFolder = (uploadFolderNode == null) ? DefaultUploadFolder : uploadFolderNode.InnerText;
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// Adds or replaces an xml node
        /// </summary>
        /// <param name="parent">Parent node that contains the old node that should be replaced or where the new node should be added to</param>
        /// <param name="newNode">New node to add</param>
        protected void AddOrReplaceXmlNode(XmlNode parent, XmlNode newNode)
        {
            // Check if the node exists
            XmlNodeList existingNodes = parent.SelectNodes(newNode.Name);
            
            // Remove existing nodes
            for (int i = 0; i < existingNodes.Count; i++)
            {
                parent.RemoveChild(existingNodes[i]);
            }

            // Add new node
            parent.AppendChild(newNode);
        }
        #endregion
    }
}
