
function addMessage(channel)
{
    insertarMensaje(channel);
}

function insertarMensaje(channel)
{
    if (hasMessage())
    {
        getUserName();

        CreateXmlHttp();
        var ajaxRequest = 'subgurim_chat/procesador.aspx';
        var accion = '?accion=insertarMensaje';
        
        var msg = getMensaje(channel);
        //xmlHttp.onreadystatechange = function() {insertarMensaje_Callback(msg)};
        xmlHttp.onreadystatechange = leerMensajes_Callback;
        
        xmlHttp.open("POST", ajaxRequest + accion, true);
        xmlHttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded;');

        xmlHttp.send(msg.toXML()); 
        
        limpiarTextBox();
    }
    else
    {
        document.getElementById(tb_message_id).focus();
    }    
}

function insertarMensaje_Callback(msg)
{
    if (xmlHttp_preparado(xmlHttp))
    {
        var _aux = xmlHttp.responseText.split('-');
        if (_aux.length == 2)
        {
            autoN = _aux[0];
            ticks = _aux[1];

            msg.autonumeric = autoN;
            msg.ticks = ticks;
            
            //msg.autonumeric = _aux[0];
            //msg.ticks = _aux[1];
            
            escribeMensaje(msg);
        }    
    }
    else if (xmlHttp.readyState == 4 && xmlHttp.status != 200)
    {
        alert(xmlHttp.responseText);
    }
}

function getUserName()
{    
    if (userName == '')
    {
        var aux = document.getElementById(tb_user_id);
        userName = aux.value;
        
        while (userName == '')
        {
            userName = prompt('UserName?', 'anonymous');   
        }
        aux.value = userName;
        aux.readOnly = true;
        
        ingresarUsuario_Sistema();
    }
    
    return true;
}

function getMensaje(channel)
{
    var _mensaje = new Mensaje();
    
    _mensaje.autor = userName;
    _mensaje.texto = document.getElementById(tb_message_id).value;

    channel = channel || ChannelID || 'SubgurimChat';
    _mensaje.canal = channel;
    
    _mensaje.autonumeric = autoN
    _mensaje.ticks = ticks;
    
    //return _mensaje.toXML();
    return _mensaje;
}

function hasMessage()
{
    return document.getElementById(tb_message_id).value != '';
}

function sc_KeyPress(tb, evt)
{
	var keyCode;
	if (evt.which || evt.charCode)		
	{
	    keyCode = evt.which ? evt.which : evt.charCode;
	    //return (keyCode != 13);
	}
	else if (window.event)
	{
	    keyCode = event.keyCode;
	    if (keyCode == 13) 
	    {		
	        if (event.keyCode) 
	            event.keyCode = 9;
	            
		    //return false;
	    } 
	    //else
	        //return true;
	}
	
	if (keyCode == 13)
	{
        insertarMensaje();
        limpiarTextBox();
        return false;
    }
    return true;
}

function limpiarTextBox()
{
    var tb = document.getElementById(tb_message_id);
    tb.value = "";
    tb.focus();    
}