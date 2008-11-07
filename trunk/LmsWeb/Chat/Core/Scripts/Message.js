// *********************************** //  
// Mensaje //
// Define las propiedades y métodos de un mensaje

// Constructor de mensaje. 
// Inicializa las propiedades a vacío si no se le pasan parámetros,
// o los coge del xml si se le pasa un xml válido.
function Mensaje(xml)
{
    if (!xml || !xml.hasChildNodes())
    {
        this.autonumeric = '';
        this.canal = '';
        this.texto = '';
        this.autor = '';
        this.ticks = '';
        this.datetime = '';
    }
    else
    {
        this.autonumeric = xml.getElementsByTagName('autonumeric')[0].firstChild.nodeValue;
        this.canal = xml.getElementsByTagName('canal')[0].firstChild.nodeValue;
        this.texto = xml.getElementsByTagName('texto')[0].firstChild.nodeValue;
        this.autor = xml.getElementsByTagName('autor')[0].firstChild.nodeValue;
        this.ticks = xml.getElementsByTagName('ticks')[0].firstChild.nodeValue;
        
        this.datetime = new Date(parseInt(xml.getElementsByTagName('datetime')[0].firstChild.nodeValue));
    }
}

// Devuelve como XML el valor de las propiedades
Mensaje.prototype.toXML = function()
{
    var miXML = new XmlTextWriter();
    miXML.WriteStartDocument();
    miXML.WriteStartElement('Mensaje');
        miXML.WriteElementString('autonumeric', this.autonumeric);
        miXML.WriteElementStringCDATA('canal', this.canal);
        miXML.WriteElementStringCDATA('texto', this.texto);
        miXML.WriteElementStringCDATA('autor', this.autor);
        miXML.WriteElementString('ticks', this.ticks);
    miXML.WriteEndElement();
    miXML.WriteEndDocument();
    
    return miXML.escapeXML();
}
