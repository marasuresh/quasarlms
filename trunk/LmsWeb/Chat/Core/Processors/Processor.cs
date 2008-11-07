using System;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using Subgurim.Chat.Server;

namespace Subgurim.Chat
{
    /// <summary>
    /// Procesador AJAX de todas las operaciones
    /// </summary>
    public class Processor : IHttpHandler
    {
        #region Propiedades

        private HttpContext context { get; set; }

        #endregion

        public void ProcessRequest(HttpContext currentContext)
        {
            context = currentContext;
            string accion = currentContext.Request.QueryString[0];

            string devuelvo = string.Empty;

            switch (accion.ToLower())
            {
                case "ingresarusuariosistema":
                    if (hayValorPOST)
                    {
                        devuelvo = ingresoUsuarioSistema().ToString();
                        currentContext.Response.ContentType = "text/plain";
                    }
                    break;
                case "ingresarusuariocanal":
                    if (hayValorPOST)
                    {
                        devuelvo = ingresoUsuarioCanal().ToString();
                        currentContext.Response.ContentType = "text/plain";
                    }
                    break;
                case "insertarmensaje":
                    if (hayValorPOST)
                    {
                        devuelvo = insertarMensaje();
                        currentContext.Response.ContentType = "text/xml";
                    }
                    break;
                case "leerusuarios":
                    devuelvo = leerUsuarios();
                    currentContext.Response.ContentType = "text/xml";
                    break;
                case "leermensajes":
                    devuelvo = leerMensajes();
                    currentContext.Response.ContentType = "text/xml";
                    break;
                case "listarcanales":
                    devuelvo = listarCanales();
                    currentContext.Response.ContentType = "text/xml";
                    break;
            }

            currentContext.Response.Write(devuelvo);
        }


        public bool IsReusable
        {
            get { return false; }
        }

        #region 'Canales y categorías'

        /// <summary>
        /// Devuelve un XML con la organización de canales y categorías
        /// </summary>
        /// <returns></returns>
        private string listarCanales()
        {
            return string.Empty;
        }

        #endregion

        #region Mensajes

        #region Insertar Mensajes

        private string insertarMensaje()
        {
            Message message = new Message(recogeInfoPOST());

            return ChatServer.messages_Add(message);
        }

        #endregion

        #region Leer Mensajes

        private string leerMensajes()
        {
            if (context.Request.QueryString.Count >= 5)
            {
                string channel = context.Request.QueryString[1];
                long tick = Convert.ToInt64(context.Request.QueryString[2]);
                long autoN = Convert.ToInt64(context.Request.QueryString[3]);
                string userName = context.Request.QueryString[4];
                return ChatServer.messages_Read(channel, autoN, tick, userName);
            }
            else
                return string.Empty;
        }

        #endregion

        #endregion

        #region Usuarios

        #region 'Leer usuarios'

        /// <summary>
        /// Lista los usuarios que hay en un determinado canal
        /// </summary>
        /// <returns></returns>
        private string leerUsuarios()
        {
            if (context.Request.QueryString.Count >= 2)
            {
                string canal = context.Request.QueryString[1];

                return User.listadoXML(ChatServer.user_List(canal), canal);
            }
            else
                return string.Empty;
        }

        #endregion

        #region 'Ingreso de usuarios'

        /// <summary>
        /// Ingremos un usuario en un canal
        /// </summary>
        /// <returns></returns>
        private int ingresoUsuarioCanal()
        {
            User user = new User(recogeInfoPOST());
            string canal = recogeInfoPOST(1);

            return ChatServer.user_LoginChannel(user, canal);
        }

        /// <summary>
        /// Ingresamos un usuario en el sistema
        /// </summary>
        /// <returns></returns>
        private int ingresoUsuarioSistema()
        {
            User user = new User(recogeInfoPOST());

            return ChatServer.user_Login(user);
        }

        #endregion

        #endregion

        #region 'Varios sobre POST'

        /// <summary>
        /// Indica si hay valor en el POST para recoger
        /// </summary>        
        private bool hayValorPOST
        {
            get { return (context.Request.Form.Count > 0); }
        }

        /// <summary>
        /// Recoge y decodifica la información pasada por POST según el índice
        /// </summary>
        /// <returns></returns>
        private string recogeInfoPOST(int indice)
        {
            return HttpUtility.UrlDecode(context.Request.Form[indice]);
        }

        /// <summary>
        /// Recoge y decodifica la información pasada por POST de índice 0
        /// </summary>
        /// <returns></returns>
        private string recogeInfoPOST()
        {
            return recogeInfoPOST(0);
        }

        #endregion

        #region Pruebas

        public string prueba(string xml)
        {
            string nombre = string.Empty;
            // Primero recojo el valor que se quiere transmitir
            using (XmlTextReader reader = new XmlTextReader(xml, XmlNodeType.Document, null))
            {
                reader.MoveToContent();
                reader.ReadStartElement();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "nombre")
                    {
                        nombre = reader.ReadString();
                        break;
                    }
                }
                reader.Close();
            }

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            using (XmlTextWriter writer = new XmlTextWriter(sw))
            {
                // writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();
                writer.WriteStartElement("futbolistas");

                writer.WriteStartElement("jugador");
                writer.WriteAttributeString("pais", "España");
                writer.WriteElementString("nombre", nombre);
                writer.WriteStartElement("equipo");
                writer.WriteString("Valencia C.F.");
                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();

                writer.Close();
            }

            /*XmlTextWriter miXML = new XmlTextWriter(sw);
            //var miXML = new XmlTextWriter();
            miXML.Formatting = true;

            miXML.WriteStartDocument();
            miXML.WriteStartElement("myXML");
            miXML.WriteStartElement("persona");
            miXML.WriteAttributeString("Id", "1");
            miXML.WriteStartElement("nombre");
            miXML.WriteValue("David");
            miXML.WriteEndElement();
            miXML.WriteElementString("Apellidos", "Villa");
            miXML.WriteEndElement();
            miXML.WriteStartElement("persona");
            miXML.WriteStartElement("nombre");
            miXML.WriteAttributeString("Id", "2");
            miXML.WriteCDATA("Samuel");
            miXML.WriteEndElement();
            miXML.WriteElementStringCDATA("Apellidos", "Etoo");
            miXML.WriteEndElement();
            miXML.WriteEndElement();
            miXML.WriteElementString("Vacío");
            miXML.WriteEndElement();
            miXML.WriteEndDocument();*/

            return sb.ToString();
        }

        public string futbolistas()
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            using (XmlTextWriter writer = new XmlTextWriter(sw))
            {
                // writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();
                writer.WriteStartElement("futbolistas");

                writer.WriteStartElement("jugador");
                writer.WriteAttributeString("pais", "España");
                writer.WriteElementString("nombre", "David Villa");
                writer.WriteStartElement("equipo");
                writer.WriteString("Valencia C.F.");
                writer.WriteEndElement();
                writer.WriteEndElement();


                writer.WriteStartElement("jugador");
                writer.WriteAttributeString("pais", "Argentina");
                writer.WriteElementString("nombre", "Leo Messi");
                writer.WriteStartElement("equipo");
                writer.WriteString("Barcelona F.C.");
                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();

                writer.Close();
            }

            return sb.ToString();
        }

        #endregion
    }
}