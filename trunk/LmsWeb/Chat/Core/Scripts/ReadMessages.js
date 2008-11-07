// *********************************** //  
// Lectura de mensajes //

sc_addEvent(window,'load',sc_init,false);

function leerMensajes(channel)
{
    CreateXmlHttp();
    var ajaxRequest = 'subgurim_chat/procesador.aspx';
    var accion = '?accion=leerMensajes';
    
    //string channel, long autoN, long tick, string userName
    channel = (channel && (channel != 'undefined')) ? channel : 'SubgurimChat';
    var argumentos = '&channel=' + escape(channel) +'&ticks=' + ticks + '&autoN=' + autoN + '&userName=' + escape(userName)+'&i=' + sc_random();
    
    xmlHttp.onreadystatechange = leerMensajes_Callback;
    
    xmlHttp.open("GET", ajaxRequest + accion + argumentos, true);

    xmlHttp.send(null); 
}

function leerMensajes_Callback()
{
    if (xmlHttp_preparado(xmlHttp))
    {
        var xmlDoc = recoge_xmlHttp_asXML(xmlHttp);
       
        var lista = new ListadoMensajes(xmlDoc);

        // ¿Hay mensajes nuevos?
        if (lista.hasItems())
        {
            var mensaje;
            do
            {
                mensaje = lista.read();
                escribeMensaje(mensaje)
            } 
            while (lista.next());            
            
            // Como estamos al final de la lista, leemos el ticks y el autoN
            autoN = mensaje.autonumeric;
            ticks = mensaje.ticks;
            
            delay = 2000;
            ajustarScroll(true);
        }
        else
        {
            if (delay < 10000)
            {
                delay += 1000;
            }
        }
        setTimeout("leerMensajes()",delay);
    }
}

alternator = 0;
function escribeMensaje(mensaje)
{
    var chat = document.getElementById(div_chat_id);
    
    var _msg= document.createElement('div');	
    _msg.id = mensaje.autonumeric + '-' + mensaje.ticks;
    _msg.className  = (alternator++ % 2) == 0 ? "sc_msg" :  "sc_msg sc_alt";
    _msg.title = mensaje.datetime;
    
    
    var autor = document.createElement('div');
    autor.innerHTML = mensaje.autor;
    autor.className = 'sc_Author';
    
    var text = document.createElement('div');
    text.innerHTML = mensaje.texto;
    text.className = 'sc_Text';
    
    _msg.appendChild(autor);
    _msg.appendChild(text);

    chat.appendChild(_msg);    
}


function sc_init()
{
    document.getElementById(sc_loading).style.display='none';
    leerMensajes();
    document.getElementById(tb_message_id).focus();
}



function sc_addEvent(elm, evType, fn, useCapture) 
{
    if (elm.addEventListener) 
    {
        elm.addEventListener(evType, fn, useCapture);
        return true;
    }
	else if (elm.attachEvent) 
	{   
	    var r = elm.attachEvent('on' + evType, fn);
	    return r;
	}
	else 
	{
	    elm['on' + evType] = fn;
	}
}


function sc_random()
{
    var dt = new Date();
    var ran=Math.random()*400;
    return dt.getTime() + '-' + ran;
}