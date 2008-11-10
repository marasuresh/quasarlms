function leerUsuarios(canal)
{
    CreateXmlHttp();
    var ajaxRequest = 'subgurim_chat/procesador.aspx';
    var accion = '?accion=leerUsuarios';
    var argumentos = '&canal=' + escape(canal);
    
    xmlHttp.onreadystatechange = leerUsuarios_Callback;
    
    xmlHttp.open("GET", ajaxRequest + accion + argumentos, true);

    xmlHttp.send(null); 
}

function leerUsuarios_Callback()
{
    if (xmlHttp_preparado(xmlHttp))
    {
        var xmlDoc = recoge_xmlHttp_asXML(xmlHttp);
       
        var lista = new ListadoUsuarios(xmlDoc);

        if (lista.hasItems())
        {
            do
            {
                var user = lista.read();
                alert(user.nombre + '-' + user.web + '-' + user.mail + '-' + user.ultimoAcceso);
            } 
            while (lista.next());
        }
    }
}