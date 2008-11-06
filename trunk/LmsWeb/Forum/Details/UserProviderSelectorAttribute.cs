using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using N2.Templates.Details;

namespace N2.Templates.Forum.Details
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UserProviderSelectorAttribute : DropDownAttribute
    {
        #region Variables
        #endregion

        #region Constructor & destructor
        public UserProviderSelectorAttribute(string title, int sortOrder)
            : base(title, null, sortOrder)
        {
        }
        #endregion

        #region Properties
        #endregion

        #region Methods
        protected override IEnumerable<ListItem> GetListItems(Control container)
        {
            // Declare variables
            List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();

            // First of all, add Forum 
            items.Add(new KeyValuePair<string, string>("Forum", ""));

            // Now search all the assemblies in the current AppDomain for user providers
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                try
                {
                    // Get the types
                    GetAllIForumUserTypesFromAssembly(assembly, ref items);
                }
                catch (Exception)
                { }
            }

            // Add all products
            foreach (KeyValuePair<string, string> item in items)
            {
                yield return new ListItem(item.Key, item.Value);
            }
        }

        /// <summary>
        /// Gets all the IForumUser types from a specific assembly
        /// </summary>
        /// <param name="assembly">Assembly object</param>
        /// <param name="items">Reference to an items list where the types should be added to</param>
        protected void GetAllIForumUserTypesFromAssembly(Assembly assembly, ref List<KeyValuePair<string, string>> items)
        {
            // Get all the types of the assembly
            Type[] types = assembly.GetTypes();

            // Loop the assembly
            for (int i = 0; i < types.Length; i++)
            {
                try
                {  
                    // Retrieve the type
                    Type type = types[i];

                    // Check if this type implements IForumUser or is a subclass of AbstractN2ForumUser
                    if (((type.IsSubclassOf(typeof(N2.Templates.Forum.Services.AbstractN2ForumUser))) ||
                         (type.GetInterface("yaf.IForumUser") != null)) &&
                        (!type.IsAbstract))
                    {
                        // Get filename (replacing of .DLL by .dll is necessary because assembly.Location strangely
                        // returns .DLL, and the forum works case sensitive)
                        string fileName = System.IO.Path.GetFileName(assembly.Location).Replace(".DLL", ".dll");

                        // Create listitem
                        KeyValuePair<string, string> item = new KeyValuePair<string, string>(type.FullName,
                            string.Format("{0}|{1}", type.FullName, fileName));

                        // Add type
                        if (!items.Contains(item))
                        {
                            items.Add(item);
                        }
                    }
                }
                catch (Exception)
                { }
            }
        }
        #endregion
    }
}
