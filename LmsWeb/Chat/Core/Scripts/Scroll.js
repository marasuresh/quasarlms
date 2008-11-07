    // *********************************** //
    // MANEJO DEL SCROLL  
    var chatscroll = new Object();
    chatscroll.Pane = 
        function(scrollContainerId)
        {
            this.bottomThreshold = 250;
            this.scrollContainerId = scrollContainerId;
        }

    chatscroll.Pane.prototype.activeScroll = 
        function(siempre)
        {
            var scrollDiv = document.getElementById(this.scrollContainerId);
            var currentHeight = 0;
            
            if (scrollDiv.scrollHeight > 0)
                currentHeight = scrollDiv.scrollHeight;
            else 
                if (objDiv.offsetHeight > 0)
                    currentHeight = scrollDiv.offsetHeight;

            if (siempre || (currentHeight - scrollDiv.scrollTop - 
                ((  scrollDiv.style.pixelHeight) ? 
                    scrollDiv.style.pixelHeight : 
                    scrollDiv.offsetHeight) < this.bottomThreshold))
                scrollDiv.scrollTop = currentHeight;

            scrollDiv = null;
        }
    
    function ajustarScroll(siempre)
    {
        var divScroll = new chatscroll.Pane(div_chat_id);
        divScroll.activeScroll(siempre);
    }  
    // *********************************** //
