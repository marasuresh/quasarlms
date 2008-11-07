
function listarCanales()
{
    CreateXmlHttp();
    var ajaxRequest = 'subgurim_chat/procesador.aspx';
    var accion = '?accion=listarCanales';
    
    xmlHttp.onreadystatechange = listarCanales_Callback;
    
    xmlHttp.open("GET", ajaxRequest + accion, true);

    xmlHttp.send(null); 
}

function listarCanales_Callback()
{
    if (xmlHttp_preparado(xmlHttp))
    {
        var xmlDoc = recoge_xmlHttp_asXML(xmlHttp);
    }
}
