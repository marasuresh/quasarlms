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
        #region Propiedades

        private static string prefijo_canal = prefijo + "Channel_";
        private static string prefijo_sistema = prefijo + "Sistema";

        private static string prefijo_canal_maintenance = prefijo + "Mantenimiento_";
        private static string prefijo_sistema_maintenance = prefijo + "Mantenimiento";

        /// <summary>
        /// Tiempo de sesión de un usuario. Pasado este tiempo, se le desconectará
        /// </summary>
        private static long user_SessionTime
        {
            get
            {
                // En principio le damos 5 minutos
                return TimeSpan.TicksPerMinute*10;
            }
        }

        /// <summary>
        /// Tiempo que dejaremos pasar entre mantenimientos de un mismo canal
        /// </summary>
        private static long user_ChannelMaintenanceWindow
        {
            get
            {
                // Le damos 5 minutos
                return TimeSpan.TicksPerMinute*15;
            }
        }

        /// <summary>
        /// Tiempo que dejaremos pasar entre mantenimientos del sistema
        /// </summary>
        private static long user_SystemMaintenanceWindow
        {
            get
            {
                // Le damos 15 minutos
                return TimeSpan.TicksPerMinute*15;
            }
        }

        #endregion

        #region Usuarios

        #region Ingresos

        /// <summary>
        /// Ingresa a un usuario en el sistema
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="mailWeb"></param>
        /// <returns>
        /// 1: Ingresado correctamente
        /// 0: Ya estaba ingresado
        /// -1: Error
        /// </returns>
        public static int user_Login(User _user)
        {
            user_Maintenance();

            // Si el usuario ya ha ingresado en el sistema, no permitir que ingrese de nuevo
            if (user_AlreadyLoggedIn(_user.nombre))
                return 0;
            else
            {
                // Ingresarlo en el sistema
                if (myCache.Get(prefijo_sistema) != null)
                    ((Dictionary<string, User>) myCache.Get(prefijo_sistema)).Add(_user.nombre, _user);
                else
                {
                    Dictionary<string, User> usuarios = new Dictionary<string, User>();
                    usuarios.Add(_user.nombre, _user);

                    myCache.Add(prefijo_sistema, usuarios, null, DateTime.MaxValue, TimeSpan.Zero,
                                CacheItemPriority.High, null);
                }
            }

            return 1;
        }

        /// <summary>
        /// Ingresa a un usuario en cierto canal
        /// </summary>
        /// <param name="canal"></param>
        /// <param name="nombre"></param>
        /// <returns>
        /// 1: Ingresado correctamente
        /// 0: Ya estaba ingresado
        /// -1: Error
        /// </returns>
        public static int user_LoginChannel(User _user, string channel)
        {
            user_Maintenance(channel);

            // Si el usuario ya ha ingresado en el canal, no lo hará otra vez
            if (user_AlreadyLoggedInChannel(channel, _user.nombre))
                return 0;
                // Si NO ha ingresado en el sistema, dará error
            else if (!user_AlreadyLoggedIn(_user.nombre))
                return -1;
            else
            {
                // Lo ingresamos en el canal

                if (myCache.Get(prefijo_canal + channel) != null)
                {
                    ((Dictionary<string, User>)
                     myCache.Get(prefijo_canal + channel)).Add(_user.nombre, _user);
                }
                else
                {
                    Dictionary<string, User> usuarios = new Dictionary<string, User>();
                    usuarios.Add(_user.nombre, _user);

                    myCache.Add(prefijo_canal + channel, usuarios, null, DateTime.MaxValue, TimeSpan.Zero,
                                CacheItemPriority.High, null);
                }
            }

            return 1;
        }

        private static bool user_AlreadyLoggedIn(string userName, string key)
        {
            bool retorno = false;

            Dictionary<string, User> usuarios = user_ListAux(key);
            if (usuarios != null)
            {
                retorno = usuarios.ContainsKey(userName);
            }

            return retorno;
        }

        /// <summary>
        /// Indica si el usuario ya ha ingresado en el sistema
        /// </summary>
        /// <param name="canal"></param>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public static bool user_AlreadyLoggedIn(string userName)
        {
            return user_AlreadyLoggedIn(userName, prefijo_sistema);
        }

        /// <summary>
        /// Indica si el usuario ya ha ingresado en cierto canal
        /// </summary>
        /// <param name="canal"></param>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public static bool user_AlreadyLoggedInChannel(string channel, string userName)
        {
            return user_AlreadyLoggedIn(userName, prefijo_canal + channel);
        }

        #endregion

        #region 'Maintenance'

        /// <summary>
        /// Borrar la relación canal - usuarios
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        public static int user_DeleteChannel(string channel)
        {
            myCache.Remove(prefijo_canal + channel);

            return 1;
        }

        /// <summary>
        /// Realiza el mantenimiento del sistema
        /// </summary>
        /// <returns></returns>
        public static int user_Maintenance()
        {
            long ahora = DateTime.UtcNow.Ticks;

            // Si el último mantenimiento lo hicimos antes de lo estipulado, volver a realizar el mantenimiento
            if ((ahora - user_SystemMaintenanceWindow) > user_LastMaintenance_Access())
            {
                Dictionary<string, User> usuarios = user_List();

                if (null != usuarios)
                {
                    foreach (KeyValuePair<string, User> kvp in usuarios)
                    {
                        // Si el usuario ha expirado la sesión, le borramos del sistema
                        if ((ahora - user_SessionTime) > kvp.Value.ultimoAcceso)
                        {
                            if (myCache.Get(prefijo_sistema) != null)
                                ((Dictionary<string, User>) myCache.Get(prefijo_sistema)).Remove(kvp.Value.nombre);
                        }
                    }

                    user_LastMaintenance_Modify();

                    channel_maintenance();
                }
            }

            return 1;
        }

        /// <summary>
        /// Realiza el mantenimiento en un canal
        /// </summary>
        /// <param name="canal"></param>
        /// <returns></returns>
        public static int user_Maintenance(string channel)
        {
            long ahora = DateTime.UtcNow.Ticks;

            // Si el último mantenimiento lo hicimos antes de lo estipulado, volver a realizar el mantenimiento
            if ((ahora - user_ChannelMaintenanceWindow) > user_LastMaintenance_Access(channel))
            {
                Dictionary<string, User> usuarios = user_List(channel);
                foreach (KeyValuePair<string, User> kvp in usuarios)
                {
                    // Si el usuario ha expirado la sesión, le borramos del sistema
                    if ((ahora - user_SessionTime) > kvp.Value.ultimoAcceso)
                    {
                        if (myCache.Get(prefijo_canal + channel) != null)
                            ((Dictionary<string, User>) myCache.Get(prefijo_canal + channel)).Remove(kvp.Value.nombre);
                    }
                }

                user_LastMaintenance_Modify();

                channel_maintenance(channel);
            }

            return 1;
        }

        /// <summary>
        /// Modifica el valor de la última vez que se ha hecho mantenimiento en el canal
        /// </summary>
        private static void user_LastMaintenance_Modify(string channel)
        {
            myCache.Add(prefijo_canal_maintenance + channel, DateTime.UtcNow.Ticks, null, DateTime.MaxValue,
                        TimeSpan.Zero, CacheItemPriority.High, null);
        }

        /// <summary>
        /// Devuelve el valor de la última vez que se ha hecho mantenimiento del canal
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        private static long user_LastMaintenance_Access(string channel)
        {
            if (myCache.Get(prefijo_canal_maintenance + channel) != null)
                return (long) myCache.Get(prefijo_canal_maintenance + channel);
            else
                return long.MaxValue;
        }


        /// <summary>
        /// Modifica el valor de la última vez que se ha hecho mantenimiento en el sistema
        /// </summary>
        private static void user_LastMaintenance_Modify()
        {
            myCache.Add(prefijo_sistema_maintenance, DateTime.UtcNow.Ticks, null, DateTime.MaxValue,
                        TimeSpan.Zero, CacheItemPriority.High, null);
        }

        /// <summary>
        /// Devuelve el valor de la última vez que se ha hecho mantenimiento del sistema
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        private static long user_LastMaintenance_Access()
        {
            if (myCache.Get(prefijo_sistema_maintenance) != null)
                return (long) myCache.Get(prefijo_sistema_maintenance);
            else
                return long.MaxValue;
        }

        #endregion

        #region 'LastMessage and LastAccess tickets'

        /// <summary>
        /// Actualizamos la última vez que escribió un mensaje en el sistema
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="canal"></param>
        /// <param name="tickUltimoMensaje"></param>
        /// <returns></returns>
        private static int user_ChangeLastMessageTicket(string userName, long lastMessageTick)
        {
            if ((myCache.Get(prefijo_sistema) != null) &&
                (((Dictionary<string, User>) myCache.Get(prefijo_sistema)).ContainsKey(userName)))
            {
                ((Dictionary<string, User>) myCache.Get(prefijo_sistema))[userName].ultimoMensaje
                    = lastMessageTick;
            }

            return 1;
        }

        /// <summary>
        /// Actualizamos la última vez que escribió un mensaje en el canal
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="canal"></param>
        /// <param name="tickUltimoMensaje"></param>
        /// <returns></returns>
        internal static int user_ChangeLastMessageTicket(string userName, string channel, long lastMessageTick)
        {
            if ((myCache.Get(prefijo_canal + channel) != null) &&
                (((Dictionary<string, User>) myCache.Get(prefijo_canal + channel)).ContainsKey(userName)))
            {
                ((Dictionary<string, User>) myCache.Get(prefijo_canal + channel))[userName].ultimoMensaje
                    = lastMessageTick;
            }

            user_ChangeLastMessageTicket(userName, lastMessageTick);
            return 1;
        }

        /// <summary>
        /// Actualizamos la última vez que accedió al sistema
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        internal static int user_ChangeLastAccessTicket(string userName)
        {
            if ((myCache.Get(prefijo_sistema) != null) &&
                (((Dictionary<string, User>) myCache.Get(prefijo_sistema)).ContainsKey(userName)))
            {
                ((Dictionary<string, User>) myCache.Get(prefijo_sistema))[userName].ultimoAcceso
                    = DateTime.UtcNow.Ticks;
            }

            return 1;
        }

        #endregion

        #region Listados

        /// <summary>
        /// Lista todos los usuarios en el sistema
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, User> user_List()
        {
            return user_ListAux(prefijo_sistema);
        }

        /// <summary>
        /// Lista todos los usuarios en cierto canal
        /// </summary>
        /// <param name="canal"></param>
        /// <returns></returns>
        public static Dictionary<string, User> user_List(string channel)
        {
            return user_ListAux(prefijo_canal + channel);
        }


        private static Dictionary<string, User> user_ListAux(string key)
        {
            object _oUsuarios = myCache.Get(key);

            if (_oUsuarios != null)
                return (Dictionary<string, User>) myCache.Get(key);
            else
                return null;
        }

        #endregion

        #endregion
    }
}