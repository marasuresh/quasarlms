using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using N2;
using N2.Engine;
using N2.Persistence.Finder;
using N2.Plugin;
using N2.Web;

namespace N2.Templates.Forum.Services
{
    [AutoInitialize]
    public class ForumInitializer : IPluginInitializer
    {
        #region Variables
        private int _singletonForumInstanceID = 0;
        #endregion

        #region Constructor & destructor
        #endregion

        #region Properties
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the plugin
        /// </summary>
        /// <param name="engine"></param>
        public void Initialize(N2.Engine.IEngine engine)
        {
            // Subscribe to the event
            engine.UrlParser.PageNotFound += delegate(object sender, PageNotFoundEventArgs e)
            {
                if (_singletonForumInstanceID == 0)
                {
                    _singletonForumInstanceID = FindForumItem(engine);
                }

                if (IsRequestingForum(e.Url))
                {
                    e.AffectedItem = engine.Persister.Get(_singletonForumInstanceID);
                }
            };
        }

        /// <summary>
        /// Gets whether the url belongs to a forum
        /// </summary>
        /// <param name="url">Url to check</param>
        /// <returns>True if the requesting url belongs to a forum, otherwise false</returns>
        private bool IsRequestingForum(string url)
        {
            return url.StartsWith("Forum", StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Finds a specific forum item
        /// </summary>
        /// <param name="engine"></param>
        /// <returns></returns>
        private int FindForumItem(IEngine engine)
        {
            // Query items
            IList<ContentItem> items = engine.Resolve<IItemFinder>()
                .Where.Type.Eq(typeof(Items.Forum))
                .MaxResults(1)
                .Select();

            // Return resu;t
            return (items.Count > 0) ? items[0].ID : 0;
        }
        #endregion
    }
}
