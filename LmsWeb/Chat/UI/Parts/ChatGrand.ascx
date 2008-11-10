<%@ Control Language="C#" 
    AutoEventWireup="true" 
    CodeBehind="ChatGrand.ascx.cs" 
    Inherits="UserControls_Chat_ChatGrand" %>

<%@ Register Assembly="Chat" Namespace="Subgurim.Chat" TagPrefix="cc1" %>
    
    
    <cc1:SubgurimChatManager ID="SubgurimChatManager1" runat="server" />

    <style>
      
    .sc_chat_container
    {
        /*height: 160px;*/
        width: 100%;
        font-family: arial;
        padding: 1px;
        font-size: 11px;
        background-color: #ffffff;
        
        text-align: left;
    }
    
    .sc_chat_container input
    {
        margin: 0px;
        border: 1px groove #bbbbbb;    
    }
    
    .sc_chat_box
    {
        height: 200px;
    }  
    
    .sc_options_box
    {
        padding-left: 15px;
        height: 35px;
    }
        
    .sc_msg
    {
        padding-left: 2px;
        padding-right: 2px;
        padding-top: 0px;
        padding-bottom: 0px;
    }    
    
    .sc_alt
    {
        padding-top: 0px;
        padding-bottom: 0px;
        background-color: #dedede;
    }    
    
    .sc_Author
    {
        font-weight: bold;
    }  
       
    .sc_chat_message
    {
        width: 150px;
        margin:0px;
    }    
    
    .sc_chat_container input
    {
        font-family: verdana;
        font-size: 10px;
        border: groove 1px black;
    }
       
    .sc_chat_send
    {
        /*width: 45px;
        text-align:center;*/
    }
    
    /*inline grid fixes*/
    .grid { width:100%; }
    .grid .top { float:inherit!important; padding:0px!important; }
    .grid TABLE.gridview { border-collapse: separate!important; }   

    </style>

    <div align="center">
        <div class="sc_chat_container">
            <div class="sc_chat_box" style="overflow: scroll;" id="chat">
                <span id="sc_loading" style="text-decoration:blink; font-weight:bold;">Loading...</span>
            </div>
            <div class="sc_options_box">
                <div>
                    <input type="text" size="15" id="username" class="sc_chat_username" value="<%= nombreUsuario() %>" onfocus="this.focus();this.select();" />
                </div>
                <div>
                    <input type="text" size="15" id="message" class="sc_chat_message" onkeypress="return sc_KeyPress(this, event);" />
                    <input type="button" value="Send" onclick="insertarMensaje()" class="botonSudoku" />
                </div>
            </div>
        </div>    
    </div>
