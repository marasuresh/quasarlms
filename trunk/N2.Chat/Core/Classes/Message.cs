using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;

namespace Subgurim.Chat
{
    /// <summary>
    /// Item mensaje
    /// </summary>
    public class Message
    {
        #region Propiedades

        private long _autonumeric;

        /// <summary>
        /// Autonumérico
        /// </summary>
        public long autonumeric
        {
            get { return _autonumeric; }
            internal set { _autonumeric = value; }
        }

        private string _canal;

        /// <summary>
        /// Channel al que pertenece el mensaje
        /// </summary>
        public string canal
        {
            get { return _canal; }
            set { _canal = HttpUtility.HtmlEncode(value); }
        }

        private string _texto;

        /// <summary>
        /// Texto del mensaje
        /// </summary>
        public string texto
        {
            get { return _texto; }
            set { _texto = HttpUtility.HtmlEncode(value); }
        }

        private string _autor;

        /// <summary>
        /// Autor del mensaje
        /// </summary>
        public string autor
        {
            get { return _autor; }
            set { _autor = HttpUtility.HtmlEncode(value); }
        }

        private long _ticks;

        /// <summary>
        /// Representa la unidad de tiempo en que el mensaje fue escrito
        /// </summary>
        public long ticks
        {
            get { return _ticks; }
            set { _ticks = value; }
        }

        #endregion

        #region Inicialización

        public Message()
        {
        }

        public Message(string canal, string autor, string texto)
        {
            this.canal = canal;
            this.autor = autor;
            this.texto = texto;
        }

        #endregion

        #region 'XML de un sólo mensaje'

        /* XML
         * 
         * <mensaje>
         *  <autonumeric />
         *  <canal />
         *  <texto />
         *  <autor />
         *  <ticks />
         * </mensaje>
         * */

        /// <summary>
        /// Coge el XML entrante y lo transforma en clase
        /// </summary>
        /// <param name="xml"></param>
        public Message(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                return;

            using (XmlTextReader reader = new XmlTextReader(xml, XmlNodeType.Document, null))
            {
                reader.MoveToContent();
                while (reader.Read() && (reader.NodeType == XmlNodeType.Element))
                {
                    switch (reader.Name)
                    {
                        case "canal":
                            canal = reader.ReadString();
                            break;
                        case "texto":
                            texto = reader.ReadString();
                            break;
                        case "autor":
                            autor = reader.ReadString();
                            break;
                        case "autonumeric":
                            string _autonumeric = reader.ReadString();
                            if (!string.IsNullOrEmpty(_autonumeric))
                                autonumeric = Convert.ToInt64(_autonumeric);
                            break;
                        case "ticks":
                            string _ticks = reader.ReadString();
                            if (!string.IsNullOrEmpty(_ticks))
                                ticks = Convert.ToInt64(_ticks);
                            break;
                    }
                }

                reader.Close();
            }
        }

        /// <summary>
        /// Convierte mi clase usuario en un XML
        /// </summary>
        /// <returns></returns>
        public string toXML()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            XmlTextWriter writer = new XmlTextWriter(sw);
            try
            {
                writer.WriteStartDocument();
                toXML_Aux(ref writer);
                writer.WriteEndDocument();
            }
            finally
            {
                writer.Close();
            }


            return sb.ToString();
        }

        private void toXML_Aux(ref XmlTextWriter writer)
        {
            writer.WriteStartElement("mensaje");

            writer.WriteElementString("autonumeric", autonumeric.ToString());
            writer.WriteElementString("canal", canal);
            writer.WriteElementString("texto", texto);
            writer.WriteElementString("autor", autor);
            writer.WriteElementString("ticks", ticks.ToString());

            TimeSpan ts = new DateTime(ticks, DateTimeKind.Local) - new DateTime(1970, 1, 1);
            writer.WriteElementString("datetime", ((long) ts.TotalMilliseconds).ToString());

            writer.WriteEndElement();
        }

        #endregion

        #region 'XML de un listado de Mensajes'

        /* XML
         * <leer_mensajes>
         *    <mensajes>
         *      <mensaje>
         *          <autonumeric />
         *          <canal />
         *          <texto />
         *          <autor />
         *          <ticks />
         *      </mensaje>
         *   </mensajes>
         * </leer_mensaje>
         * */

        internal static string listadoXML(List<Message> mensajes)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            XmlTextWriter writer = new XmlTextWriter(sw);
            try
            {
                writer.WriteStartDocument();

                writer.WriteStartElement("leer_mensajes");
                writer.WriteStartElement("mensajes");

                foreach (Message mensaje in mensajes)
                {
                    mensaje.toXML_Aux(ref writer);
                }

                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.WriteEndDocument();
                writer.Close();
            }
            finally
            {
                writer.Close();
            }


            return sb.ToString();
        }

        #endregion
    }
}