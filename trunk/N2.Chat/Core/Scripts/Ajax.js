    // *********************************** //    
    // AJAX
    var xmlHttp;
    function CreateXmlHttp()
    {       
        
        // Probamos con IE
        try
        {
            // Funcionará para JavaScript 5.0
            xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch(e)
        {
            try
            {
                xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            catch(oc)
            {  
                xmlHttp = null;
            }
        }

        // Si no se trataba de un IE, probamos con esto
        if(!xmlHttp && typeof XMLHttpRequest != "undefined")
        {
            xmlHttp = new XMLHttpRequest();
        }

        return xmlHttp;
    }
    
    // Recoge el resultado como un XML
    function recoge_xmlHttp_asXML(_xmlHttp)
    {
        var xmlDoc;
        
        // Para el Firefox
        if (document.implementation && document.implementation.createDocument)
        {
            xmlDoc = _xmlHttp.responseXML;
        } 
        // Para IE
        else if (window.ActiveXObject)
        {
            var testandoAppend = document.createElement('xml');
            testandoAppend.setAttribute('innerHTML',_xmlHttp.responseText);
            testandoAppend.setAttribute('id','_formjAjaxRetornoXML');
            document.body.appendChild(testandoAppend);
            xmlDoc = document.getElementById('_formjAjaxRetornoXML');
            document.body.removeChild(document.getElementById('_formjAjaxRetornoXML'));
        }   
            
        return xmlDoc;
    }    
    
    function xmlHttp_preparado(_xmlHttp)
    {
        return (_xmlHttp.readyState == 4 && _xmlHttp.status == 200);
    }
    
    
