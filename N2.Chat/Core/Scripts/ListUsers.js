// *********************************** //  
// Listado de usuarios //
// Define un listado de usuarios

ListadoUsuarios.DeriveFrom(Listado);
function ListadoUsuarios(xml)
{
    this.Listado();

    if (!xml || !xml.hasChildNodes())
        this.canal = '';
    else
    {
        this.canal = xml.getElementsByTagName('canal')[0].firstChild.nodeValue;
        this.fromXML(xml);
    }
}

ListadoUsuarios.prototype.fromXML = function(xml)
{
    if (xml.hasChildNodes())
    {
        var hasta = xml.getElementsByTagName('usuario').length;           
        
        for (var i = 0; i<hasta; i++)
        {
            var _user = xml.getElementsByTagName('usuario')[i];

            this.add(new Usuario(_user));
        }     
    }
}