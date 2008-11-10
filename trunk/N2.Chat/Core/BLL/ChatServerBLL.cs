using System.Collections.Generic;

namespace Subgurim.Chat.Server
{
    /// <summary>
    /// Summary description for ChatServerBLL
    /// </summary>
    internal class ChatServerBLL
    {
        internal ChatServerBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Messages

        /// <summary>
        /// Añade un mensaje a la fuente de Dados
        /// Adds a message to the datasource
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        internal static int message_Add(Message msg)
        {
            return 1;
        }

        /// <summary>
        /// Devuelve un MensajesCollection en base al canal, el autoN y quizás del tick
        /// </summary>
        /// <param name="canal"></param>
        /// <param name="autoN"></param>
        /// <param name="tick"></param>
        /// <returns></returns>
        internal static MessagesCollection messages_Read(string channel, long autoN, long tick)
        {
            return null;
        }

        /// <summary>
        /// Devuelve todos los mensajes de un canal.
        /// </summary>
        /// <param name="canal"></param>
        /// <returns></returns>
        internal static MessagesCollection messages_Read(string channel)
        {
            return null;
        }

        internal static int messages_Delete(long autoN)
        {
            return 1;
        }

        #endregion

        #region Channels

        /// <summary>
        /// Adds a channel to DB
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        internal static int channel_Add(Channel channel)
        {
            return 1;
        }

        /// <summary>
        /// Returns the list of channels on the ChatSystem
        /// </summary>
        /// <returns></returns>
        internal static Dictionary<string, Channel> channel_List()
        {
            // Leeremos el XML o la BBDD con los canales y sus categorías. 
            // nos vale con que las categorías estén en la propiedad correspondiente del canal.

            return null;
        }

        #region 'Borrar Canales'

        internal static int channel_Delete(Channel c)
        {
            return channel_Delete(c.canal);
        }

        /// <summary>
        /// Deletes a channel from the DB
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        internal static int channel_Delete(string channel)
        {
            return 1;
        }

        #endregion

        #endregion
    }
}