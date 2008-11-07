using System.Web;
using System.Web.Caching;

namespace Subgurim.Chat.Server
{
    /// <summary>
    /// La clase Chat es la que maneja el cotarro
    /// </summary>
    public partial class ChatServer
    {
        private static string prefijo = "SubgurimChat_";
        private static int maxItems = 100;

        private static Cache myCache
        {
            get { return HttpContext.Current.Cache; }
        }

        public ChatServer()
        {
        }

        #region Keys

        /// <summary>
        /// Identifies the cache key for the channel messageCollection
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        internal static string channel_Key(string channel)
        {
            return prefijo + channel;
        }

        /// <summary>
        /// Identifies de cache key for the token that represents the last time a channel was writen
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        internal static string channel_Key_Token(string channel)
        {
            return prefijo + channel + "_Token";
        }

        /// <summary>
        /// Identifies the cache key for the list of channels
        /// </summary>
        /// <returns></returns>
        internal static string channel_Key_List()
        {
            return prefijo + "_Listado";
        }

        #endregion
    }
}