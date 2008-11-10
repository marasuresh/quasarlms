

function sc_init()
{
    document.getElementById(sc_loading).style.display='none';
    leerMensajes();
    document.getElementById(tb_message_id).focus();
}



/*function sc_addEvent(elm, evType, fn, useCapture) 
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
}*/
