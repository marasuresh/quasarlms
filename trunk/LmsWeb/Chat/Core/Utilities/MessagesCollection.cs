using System;
using System.Collections.Generic;
using System.Text;
using Subgurim.Chat.Server;

namespace Subgurim.Chat
{
    /// <summary>
    ///  Hereda de List y le añade funcionalidades para los mensajes
    /// </summary>
    public class MessagesCollection : List<Message>
    {
        #region Propiedades

        // El orden de la Lista
        public long autonumeric = 0;

        private int _step = 1;

        /// <summary>
        /// Define el incremento del autonumérico.
        /// Como mínimo valdrá 1
        /// </summary> 
        public int step
        {
            get { return _step; }
            set { _step = value > 0 ? value : 1; }
        }

        // Valor del primer elemento
        private long seed = 1;

        /// <summary>
        /// Número máximo de elementos del listado. 
        /// Obligamos a que por lo menos haya uno.
        /// Va a ser un número aproximado, pq para mayor performance utilizaremos random.
        /// Variará como máximo cierto porcentaje especificado 
        /// </summary>
        private int _maxItems = 100;

        private int maxItems
        {
            get { return _maxItems; }
            set { _maxItems = value > 1 ? value : 1; }
        }

        private int _maxSeconds = -1;

        /// <summary>
        /// Límite de segundos entre la última vez que se escribe y ahora.
        /// Si es <= 0, significa que no hay límite
        /// </summary>
        public int maxSeconds
        {
            get { return _maxSeconds; }
            set { _maxSeconds = value; }
        }

        #endregion

        #region Inicializacion

        public MessagesCollection(int maxItems, int maxSeconds, long seed, int step)
            : base()
        {
            this.step = step;
            this.seed = seed;
            this.maxItems = maxItems;
            this.maxSeconds = maxSeconds;
        }

        public MessagesCollection(int maxItems, int maxSeconds, long seed)
            : this(maxItems, maxSeconds, seed, 1)
        {
        }

        public MessagesCollection(int maxItems, long seed)
            : this(maxItems, -1, seed, 1)
        {
        }

        #endregion

        #region 'Funciones Extra'

        /// <summary>
        /// Cambiamos la clase Add, de forma que se le dé el valor adecuado al autonumérico
        /// </summary>
        /// <param name="item"></param> 
        public new string Add(Message item)
        {
            if (base.Count == 0)
                item.autonumeric = seed;
            else
            {
                // Por si acaso tras el mantenimiento nos quedamos a cero
                if (mantenimiento() && (base.Count == 0))
                    item.autonumeric = seed;
                else
                    item.autonumeric = base[base.Count - 1].autonumeric + step;
            }

            item.ticks = DateTime.UtcNow.Ticks;

            return (anyadeItem(item));
        }


        /// <summary>
        /// Recoge el rango a partir de cierto autonumérico
        /// </summary>
        /// <param name="autoN"></param>
        /// <returns></returns>
        public List<Message> getRangeFrom(long autoN)
        {
            if (base.Count <= 0)
                return null;

            // recoge el valor del autonumérico del primer elemento de la lista
            long autoN_i = base[0].autonumeric;

            // Nos aseguramos que autoN es mayor o igual que el de primer elemento - step
            autoN = autoN < autoN_i ? (autoN_i - step) : autoN;

            // Indice que ya está en base 0. 
            // Recordamos que step será forzosamente positivo, por lo que no hay peligro de división por cero.
            int indice = (int) Math.Ceiling((decimal) (autoN - autoN_i)/(decimal) step) + 1;

            // Nos aseguramos que autoN es menor o igual que el del último elemento
            if (indice > base.Count) return null;

            // Borrar por abajo
            //this.RemoveRange(0, indice);

            //return this;

            return base.GetRange(indice, base.Count - indice);
        }

        public override string ToString()
        {
            return ToString(string.Empty);
        }

        public string ToString(string userName)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Message item in this)
            {
                sb.AppendLine("<br />");

                //if (!string.IsNullOrEmpty(userName) && (nombre == item.autor))
                //{ }
                //else

                sb.AppendFormat("<b>{0}</b>", item.autor);

                sb.AppendLine("<br />");
                sb.AppendLine(item.texto);
            }

            return sb.ToString();
        }

        #region Mantenimiento

        /// <summary>
        /// Realizaremos operaciones de mantenimiento en base a un random (para no hacerlas a todas horas)
        /// </summary>
        private bool mantenimiento()
        {
            // 1 de cada 10 veces comprobaremos si se supera el límite máximo de mensajes
            if ((DateTime.Now.Millisecond%10) == 0)
            {
                if (mantenimientoNMax())
                    return true;

                // 1 de cada 40 veces comprobaremos si se excede el limite de tiempo.
                if ((DateTime.Now.Millisecond%4) == 0)
                {
                    return mantenimientoTMax();
                }
            }

            return false;
        }

        /// <summary>
        /// Comprobamos si se supera el límite máximo de mensajes
        /// </summary>
        /// <returns></returns>
        private bool mantenimientoNMax()
        {
            int total = base.Count;
            if (total > maxItems)
            {
                // Borraremos aproximadamente el 75% de los mensajes, obviamente los primeros en ser insertados
                int nuevoInicio = 3*total/4;

                borraItems(nuevoInicio);

                return true;
            }
            return false;
        }

        /// <summary>
        /// Comprobamos si se excede el límite de tiempo
        /// </summary>
        /// <returns></returns>
        private bool mantenimientoTMax()
        {
            // Si maxSeconds <= 0 es que NO queremos hacer el mantenimiento temporal
            if (maxSeconds > 0)
            {
                long ahora = DateTime.UtcNow.Ticks;
                long maxTicks = new DateTime(0, 0, 0, 0, 0, maxSeconds).Ticks;
                long limite = ahora - maxTicks;

                // if (el tiempo del mensaje inicial lo excede) actuamos, sino no hace falta hacer nada
                if (base[0].ticks < limite)
                {
                    int indice;

                    // if (el tiempo del mensaje final lo excede) borramos todo
                    if (base[base.Count - 1].ticks < limite)
                    {
                        indice = base.Count;
                    }
                    else
                    {
                        int min = 0;
                        int max = base.Count - 1;

                        indice = mantenimientoTMax_Aux(min, max, limite);
                    }

                    borraItems(indice);
                }
            }
            return false;
        }

        private int mantenimientoTMax_Aux(int min, int max, long limite)
        {
            int medio = (max + min)/2;

            if (base[medio].ticks < limite)
                max = medio;
            else
                min = medio;

            // Ya hemos encontrado el punto que buscábamos
            if ((max - min) <= 1)
                return max;

            return mantenimientoTMax_Aux(min, max, limite);
        }

        #endregion

        #endregion

        #region 'Actualizaciones de la fuente de datos'

        /// <summary>
        /// Se encarga de borrar los Items desde el 0 hasta el total, tanto en la Cache
        /// como en cualquier fuente de datos (XML, BBDD, etc.)
        /// </summary>
        /// <param name="indice"></param>
        /// <returns></returns>
        private void borraItems(int total)
        {
            if (ChatServerBLL.messages_Delete(base[total - 1].autonumeric) > 0)
            {
                base.RemoveRange(0, total);
            }
        }


        /// <summary>
        /// Se encarga de añadir el Item, tanto en la Cache
        /// como en cualquier fuente de datos (XML, BBDD, etc.)
        /// </summary>
        /// <param name="indice"></param>
        /// <returns></returns>
        private string anyadeItem(Message item)
        {
            // Añadimos a la fuente de datos
            if (ChatServerBLL.message_Add(item) > 0)
            {
                // Actualizamos la colección de la cache
                base.Add(item);

                // Actualizamos el token que indique el último cambio
                ChatServer.channel_ChangeWriteTicket(item.canal, DateTime.UtcNow.Ticks);

                // Actualizamos el tick del ultimoMensaje del usuario
                ChatServer.user_ChangeLastMessageTicket(item.autor, item.canal, item.ticks);

                return ChatServer.messages_formatAddReturn(item.autonumeric, item.ticks);
            }

            else return ChatServer.messages_formatAddReturn(-1, -1);
        }

        #endregion
    }
}