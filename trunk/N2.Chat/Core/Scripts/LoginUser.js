// *********************************** //  
// Ingresar usuario en el sistema o en el canal //

function ingresarUsuario_Sistema()
{
    CreateXmlHttp();
    var ajaxRequest = 'subgurim_chat/procesador.aspx';
    var accion = '?ingresarUsuarioSistema';
    
    xmlHttp.onreadystatechange = ingresarUsuario_Sistema_Callback;
    
    xmlHttp.open("POST", ajaxRequest + accion, true);
    xmlHttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded;');

    xmlHttp.send(recogeUsuario()); 
}

function ingresarUsuario_Sistema_Callback()
{
    if (xmlHttp_preparado(xmlHttp))
    {
        /*if (xmlHttp.responseText != 1)
            alert('Usuario no ingresado');
        else
            alert('Usuario ingresado con éxito');*/
    }
}

function ingresarUsuario_Canal(canal)
{
    CreateXmlHttp();
    var ajaxRequest = 'subgurim_chat/procesador.aspx';
    var accion = '?ingresarUsuarioCanal';
    
    xmlHttp.onreadystatechange = ingresarUsuario_Canal_Callback;
    
    xmlHttp.open("POST", ajaxRequest + accion, true);
    xmlHttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded;');

    xmlHttp.send(recogeUsuario()+'&canal='+canal); 
}

function ingresarUsuario_Canal_Callback()
{
    if (xmlHttp_preparado(xmlHttp))
    {
        if (xmlHttp.responseText < 0)
            alert('Usuario no ingresado en el canal');
        else if (xmlHttp.responseText == 0)
            alert('El usuario ya estaba ingresado');
        else
            alert('Usuario ingresado con éxito en el canal');
    }
}

function recogeUsuario()
{
    var _user = new Usuario();
    
    _user.nombre = userName;
    //_user.web = 'discotequeros.com';
    //_user.mail = 'webmaster@discotequeros.com';
    
    return _user.toXML();
}


