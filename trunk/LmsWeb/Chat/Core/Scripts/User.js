// *********************************** //  
// Usuario //
// Define las propiedades y métodos de un usuario

// Constructor de Usuario. 
// Inicializa las propiedades a vacío si no se le pasan parámetros,
// o los coge del xml si se le pasa un xml válido.
function Usuario(xml)
{
    if (!xml || !xml.hasChildNodes())
    {
        this.nombre = '';
        this.mail = '';
        this.web = '';
        this.ultimoAcceso = '';
        this.ultimoMensaje = '';
    }
    else
    {
        this.nombre = xml.getElementsByTagName('nombre')[0].firstChild.nodeValue;
        this.mail = xml.getElementsByTagName('mail')[0].firstChild.nodeValue;
        this.web = xml.getElementsByTagName('web')[0].firstChild.nodeValue;
        this.ultimoAcceso = xml.getElementsByTagName('ultimoacceso')[0].firstChild.nodeValue;
        this.ultimoMensaje = xml.getElementsByTagName('ultimomensaje')[0].firstChild.nodeValue;
    }
}

// Devuelve como XML el valor de las propiedades
Usuario.prototype.toXML = function()
{
    var miXML = new XmlTextWriter();
    miXML.WriteStartDocument();
    miXML.WriteStartElement('Usuario');
        miXML.WriteElementString('nombre', this.nombre);
        miXML.WriteElementString('mail', this.mail);
        miXML.WriteElementString('web', this.web);
        miXML.WriteElementString('ultimoacceso', this.ultimoAcceso);
        miXML.WriteElementString('ultimomensaje', this.ultimoMensaje);
    miXML.WriteEndElement();
    miXML.WriteEndDocument();
    
    return miXML.escapeXML();
}