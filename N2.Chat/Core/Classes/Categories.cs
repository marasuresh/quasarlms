namespace Subgurim.Chat
{
    /// <summary>
    /// Define la Categoria a la que pertenece un canal
    /// </summary>
    public class Categories
    {
        private string _categoria = string.Empty;

        /// <summary>
        /// Nombre de la categoría. El nombre debe ser único
        /// </summary>
        public string categoria
        {
            get { return _categoria; }
            set { _categoria = value; }
        }

        private string _categoriaMadre = string.Empty;

        /// <summary>
        /// Nombre de la categoría madre a la que pertenece
        /// </summary>
        public string categoríaMadre
        {
            get { return _categoriaMadre; }
            set { _categoriaMadre = value; }
        }


        public Categories()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}