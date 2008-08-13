document.attachEvent("onreadystatechange",document_readyStateChange);
var chatInitialized = false;

var lastIndex = 0;

function document_readyStateChange()
{
    if( chatInitialized )
        return;
         
    if( document.readyState=="complete" )
    {
        chatInitialized = true; 
        initChat();
    }
}

function initChat()
{
    chatStatus.innerText = "Chat initializing: binding service...";
    ws.useService("chat.asmx?wsdl","chat");
   
    chatStatus.innerText = "Chat initializing: binding events...";
    chatSayBUTTON.onclick = chatSayBUTTON_click;
    chatText.onkeydown = chatText_onkeydown;

    chatStatus.innerText = "Chat is running.";
    beginGetMessagesFrom();
}

function beginGetMessagesFrom()
{
    ws.chat.callService(endGetMessagesFrom,"ScriptGetMessagesFrom",lastIndex);
}

var lastErrorString = null;
function endGetMessagesFrom(result)
{
    if( result.error )
    {
        chatStatus.innerText = "ScriptGetMessagesFrom: "+result.errorDetail.string; 
        setTimeout("beginGetMessagesFrom()",300);
    }
    else
    {
        if( chatStatus.innerText==lastErrorString )
            chatStatus.innerText="";
            
        for( var i=0; i<result.value.length; i+=2 )
        {
            var user = result.value[i];
            var message = result.value[i+1];
            
            var messageDIV = document.createElement("div");            
            messageDIV.className = "chat-message";
            chatContent.insertAdjacentElement("beforeEnd",messageDIV);
            
            var userSPAN = document.createElement("span");
            userSPAN.className = "chat-user";
            userSPAN.innerText = user;
            
            var messageSPAN = document.createElement("span");
            messageSPAN.className = "chat-message";
            messageSPAN.innerText = message;
            
            messageDIV.insertAdjacentElement("beforeEnd",userSPAN);
            messageDIV.insertAdjacentElement("beforeEnd",messageSPAN);
            
            lastIndex++;
            
            messageDIV.scrollIntoView(false);
        }       
        
        
        setTimeout("beginGetMessagesFrom()",1000);
    }
}

function chatSayBUTTON_click()
{
    sayCommand();
}

function lockSay()
{
    chatText.readOnly = true;
    chatSayBUTTON.disabled = true;    
}

function unlockSay()
{
    chatText.readOnly = false;
    chatSayBUTTON.disabled = false;
}

function beginSay()
{
    chatStatus.innerText = "Sending message...";
    ws.chat.callService(endSay,"SendMessage",userNameDIV.innerText,chatText.value);
}

function endSay(result)
{
    if( result.error )
    {
        chatStatus.innerText = "SendMessage: "+ result.errorDetail.string; 
        setTimeout("beginSay()",300);
    }
    else
    {
        chatStatus.innerText = "Your message was sent.";     
        chatText.value = "";
        unlockSay();
        chatText.focus(); 
    } 
}

function chatText_onkeydown()
{
    if( event.keyCode==13 )
    {
        if( chatText.readOnly )
            return;    
        sayCommand();          
    }  
}

function sayCommand()
{
    if( chatText.value==null || chatText.value==""
        || chatText.value.replace(/ /g,"")=="" )
        return;
        
    lockSay();
   
    beginSay();
}