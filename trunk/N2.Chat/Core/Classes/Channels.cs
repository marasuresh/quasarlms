using System.Collections.Specialized;

namespace Subgurim.Chat
{
    /// <summary>
    /// Channel de chat
    /// </summary>
    public class Channel
    {
        private string _canal = string.Empty;

        /// <summary>
        /// Nombre del canal
        /// </summary>
        public string canal
        {
            get { return _canal; }
            set { _canal = value; }
        }

        private string _categoria = string.Empty;

        /// <summary>
        /// Categoría a la que pertenece el canal
        /// </summary>
        public string categoria
        {
            get { return _categoria; }
            set { _categoria = value; }
        }


        private string _descripcion = string.Empty;

        /// <summary>
        ///  Pequeña descripción del canal
        /// </summary>
        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        private string _creador = string.Empty;

        /// <summary>
        /// Creador del canal. El único que puede borrarlo
        /// </summary>
        public string creador
        {
            get { return _creador; }
            set { _creador = value; }
        }

        private StringCollection _administradores = new StringCollection();

        /// <summary>
        /// Usuarios configurados como administradores del canal
        /// </summary>
        public StringCollection administradores
        {
            get { return _administradores; }
            set { _administradores = value; }
        }


        private string _contrasenya = string.Empty;

        /// <summary>
        /// Si tiene contraseña es un canal privado, sino es publico
        /// </summary>
        public string contrasenya
        {
            get { return _contrasenya; }
            set { _contrasenya = value; }
        }

        /// <summary>
        /// Indica si es público o no (privado)
        /// </summary>
        public bool publico
        {
            get { return string.IsNullOrEmpty(contrasenya); }
        }


        private bool _permanente = false;

        /// <summary>
        /// Indica si es permanente o sólo estará activo mientras haya usuarios
        /// </summary>
        public bool permanente
        {
            get { return _permanente; }
            set { _permanente = value; }
        }

        public Channel()
        {
        }

        public Channel(string canal, string categoria)
        {
            this.canal = canal;
            this.categoria = categoria;
        }
    }
}