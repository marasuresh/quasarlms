// *********************************** //  
// Listado de Mensajes //
// Define un listado de mensajes

ListadoMensajes.DeriveFrom(Listado);
function ListadoMensajes(xml)
{
    this.Listado();

    if (xml && xml.hasChildNodes())
    {
        this.fromXML(xml);
    }
}

ListadoMensajes.prototype.fromXML = function(xml)
{
    if (xml.hasChildNodes())
    {
        var hasta = xml.getElementsByTagName('mensaje').length;           
        for (var i = 0; i<hasta; i++)
        {
            var _msg = xml.getElementsByTagName('mensaje')[i];

            this.add(new Mensaje(_msg));
        }     
    }
}