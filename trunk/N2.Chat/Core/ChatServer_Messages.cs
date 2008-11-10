using System;
using System.Collections.Generic;
using System.Web.Caching;

namespace Subgurim.Chat.Server
{
    /// <summary>
    /// La clase Chat es la que maneja el cotarro
    /// </summary>
    public partial class ChatServer
    {
        #region 'Leer mensajes'

        /// <summary>
        /// Manda la orden de leer los mensajes de un canal, según el último autonumérico
        /// recibido y el último tick en que se recibieron mensajes nuevos.
        /// También se dice el nombre del usuario que hace la petición.
        /// </summary>
        /// <param name="canal"></param>
        /// <param name="autoN"></param>
        /// <param name="tick"></param>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public static string messages_Read(string channel, long autoN, long tick, string userName)
        {
            // Si la última vez que el cliente accedió con éxito es menor que 
            // la última vez que se añadió un mensaje, 
            // o el token no Existe (<0), se leen los datos
            if (channel_AccessWriteTicket(channel) > tick)
            {
                // Buscamos los messages en la cache
                MessagesCollection messages = (MessagesCollection) myCache.Get(channel_Key(channel));

                // Si la cache no tiene nada, busquemos en la fuente de datos
                if (null == messages)
                {
                    messages = ChatServerBLL.messages_Read(channel);

                    // Si en la fuente de datos había messages, lo metemos en la Cache.
                    if (null != messages)
                        myCache.Add(channel_Key(channel), messages, null, DateTime.MaxValue,
                                    TimeSpan.Zero, CacheItemPriority.High, null);
                }

                // Si o la cache o la fuente de datos tienen algo
                if (null != messages)
                {
                    List<Message> nuevosMensajes = messages.getRangeFrom(autoN);
                    if ((null != nuevosMensajes) && (nuevosMensajes.Count > 0))
                    {
                        //Mensaje _aux = nuevosMensajes[nuevosMensajes.Count - 1];
                        //tick = _aux.ticks;
                        //autoN = _aux.autonumeric;

                        return Message.listadoXML(nuevosMensajes);
                        //return messages_formatReadReturn(channel, autoN, tick, nuevosMensajes.ToString(userName));
                    }
                }
            }

            // El usuario ha accedido, por tanto actualizamos su tick de último acceso
            user_ChangeLastAccessTicket(userName);

            // Realizamos el mantenimiento de los usuarios en el canal y en el sistema
            user_Maintenance();
            user_Maintenance(channel);


            // Si llega hasta aquí es que o no ha cambiado nada desde el anterior acceso o no existían mensajes
            //return messages_formatReadReturn(channel, autoN, tick, string.Empty);
            return string.Empty;
        }

        private static string messages_formatReadReturn(string canal, long autoN, long tick, string mensajes)
        {
            return canal + "-" + autoN.ToString() + "-" + tick.ToString() + "-" + mensajes;
        }

        #endregion

        #region 'Añadir mensajes'

        public static string messages_Add(Message msg)
        {
            string retorno;
            long _autonumeric = msg.autonumeric;
            long _ticks = msg.ticks;

            if (null != myCache.Get(channel_Key(msg.canal)))
            {
                // Añadimos a nuestra colección de mensajes. Dentro de ella se actualizará la fuente de datos.
                retorno = ((MessagesCollection) myCache.Get(channel_Key(msg.canal))).Add(msg);
            }
            else
            {
                // Es posible que estemos iniciando un canal, o que se haya perdido la cache
                // Por tanto leeremos de la fuente de datos, si no había nada comenzamos el canal,
                // y si sí había algo seguimos por donde lo habíamos dejado

                MessagesCollection messages = ChatServerBLL.messages_Read(msg.canal);

                if (messages == null)
                {
                    // Iniciamos un canal
                    channel_Add(msg.canal, string.Empty);
                    messages = new MessagesCollection(maxItems, DateTime.UtcNow.Ticks);
                }

                retorno = messages.Add(msg);

                myCache.Add(channel_Key(msg.canal), messages, null, DateTime.MaxValue, TimeSpan.Zero,
                            CacheItemPriority.High, null);
            }

            return messages_Read(msg.canal, _autonumeric, _ticks, msg.autor);
            // Devolvemos al usuario el nuevo autoN y el nuevo tick
            // return retorno;
        }


        internal static string messages_formatAddReturn(long autoN, long tick)
        {
            return autoN.ToString() + "-" + tick.ToString();
        }

        #endregion
    }
}