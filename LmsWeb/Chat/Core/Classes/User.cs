using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Subgurim.Chat
{
    /// <summary>
    /// Usuarios
    /// </summary>
    public class User
    {
        #region Propiedades

        private string _nombre = string.Empty;

        /// <summary>
        /// Nombre del usuario. Debe ser único
        /// </summary>
        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _mail = string.Empty;

        /// <summary>
        /// Mail del usuario. Totalmente optativo
        /// </summary>
        public string mail
        {
            get { return _mail; }
            set { _mail = value; }
        }

        private string _web = string.Empty;

        /// <summary>
        /// Web del usuario. Totalmente optativo
        /// </summary>
        public string web
        {
            get { return _web; }
            set { _web = value; }
        }

        private long _ultimoAcceso;

        /// <summary>
        /// Tick del último acceso
        /// </summary>
        public long ultimoAcceso
        {
            get { return _ultimoAcceso; }
            set { _ultimoAcceso = value; }
        }

        private long _ultimoMensaje;

        /// <summary>
        /// Tick del último mensaje que ha escrito
        /// </summary>
        public long ultimoMensaje
        {
            get { return _ultimoMensaje; }
            set { _ultimoMensaje = value; }
        }

        #endregion

        #region Inicialización

        public User()
        {
            ultimoAcceso = DateTime.UtcNow.Ticks;
        }

        public User(string nombre, string mail, string web)
            : this()
        {
            this.nombre = nombre;
            this.mail = mail;
            this.web = web;
        }

        public User(string nombre, string mail, string web, long ultimoAcceso, long ultimoMensaje)
            : this(nombre, mail, web)
        {
            this.ultimoAcceso = ultimoAcceso;
            this.ultimoMensaje = ultimoMensaje;
        }

        #endregion

        #region 'XML de un sólo usuario'

        /* XML
         * 
         * <usuario>
         *  <nombre />
         *  <web />
         *  <mail />
         *  <ultimoacceso />
         *  <ultimomensaje />
         * </usuario>
         * */

        /// <summary>
        /// Coge el XML entrante y lo transforma en clase
        /// </summary>
        /// <param name="xml"></param>
        public User(string xml)
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
                        case "nombre":
                            nombre = reader.ReadString();
                            break;
                        case "web":
                            web = reader.ReadString();
                            break;
                        case "mail":
                            mail = reader.ReadString();
                            break;
                        case "ultimoacceso":
                            string _ua = reader.ReadString();
                            if (!string.IsNullOrEmpty(_ua))
                                ultimoAcceso = Convert.ToInt64(_ua);
                            break;
                        case "ultimomensaje":
                            string _um = reader.ReadString();
                            if (!string.IsNullOrEmpty(_um))
                                ultimoMensaje = Convert.ToInt64(_um);
                            break;
                    }
                }

                if (ultimoAcceso == 0)
                    ultimoAcceso = DateTime.UtcNow.Ticks;

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
            writer.WriteStartElement("usuario");

            writer.WriteElementString("nombre", nombre);
            writer.WriteElementString("web", web);
            writer.WriteElementString("mail", mail);
            writer.WriteElementString("ultimoacceso", ultimoAcceso.ToString());
            writer.WriteElementString("ultimomensaje", ultimoMensaje.ToString());

            writer.WriteEndElement();
        }

        #endregion

        #region 'XML de un listado de usuarios'

        /* XML
         * <leer_usuarios>
         *  <canal />
         *  <usuarios>
         *      <usuario>
         *          <nombre />
         *          <web />
         *          <mail />
         *          <ultimoacceso />
         *          <ultimomensaje />
         *      </usuario>
         * </usuarios>
         * </leer_usuarios>
         * */

        internal static string listadoXML(Dictionary<string, User> usuarios, string canal)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            XmlTextWriter writer = new XmlTextWriter(sw);
            try
            {
                writer.WriteStartDocument();

                writer.WriteStartElement("leer_usuarios");
                writer.WriteElementString("canal", canal);
                writer.WriteStartElement("usuarios");
                User user;
                foreach (KeyValuePair<string, User> kvp in usuarios)
                {
                    user = kvp.Value;
                    user.toXML_Aux(ref writer);
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