// jDrop 0.3.7 - Alistair MacDonald - director@liminal-vj.com

$.fn.jdrop = function() {                  
    
    // Invisibly create a jDrop div for the purposes of checking wheather a CSS width has been set      
    $('body').append('<div class="jDrop" style="display:none"></div>')
    // Store the .jDrop width into a variable
    var setWidth=$('.jDrop').css('width');
    // IF the width of the invisible .jDrop div is not 'auto' then a CSS width has been defined, in which case, active the correct widthMode
    if(setWidth=='auto'){var widthMode="auto"} else{var widthMode="css";setWidth=setWidth.replace('px','');}

    // Loop through each <SELECT> instance and create the drop-down jDrop select DIVs. ( The drop-down menu is not created at this stage, which dramatically reduces load-speed for pages with lots of jDrop replacements. ) 
    this.each(function(intIndex){               
        // Bring the drop down menu into existence
        $(this).after('<div class="jDrop" title="'+$(this).attr('name')+'"><div class="jSel"><span class="jSelOp">'+$(this).children('option:selected').includeHTML()+'</span></div><div class="jDropBut"></div><div class="jOpDrop"></div></div>').hide();
        // Limit the width of the new select based on the width of the html select, I found +25px good for this, but you may find another value better if you customised the skin
        if(widthMode=="auto"){ var staticWidth = $(this).width()+25}
        else { var staticWidth = setWidth - $(".jDropBut:eq("+intIndex+")").width()}        
        // Set the width of the drop-down menu to the width of the jDrop to increase usability when clicking the select arrow
        $(".jDrop:eq("+intIndex+")").children('.jSel').css({width: staticWidth+'px'})                                                                                                                                        
    })
    
    // When the jDrop jSel (<SELECT>) or jDropBut (down-arrow) are clicked do the following...    
    $('.jSel, .jDropBut').click(function(){                            
        // Store the INDEX value of the current drop-down for repeated use ( this saves SELECTING the index everytime you want to opperate which would take up more clicks or force you to add un-needed classes like... jDrop-001 etc. )  
        if( $('.jSel').index(this) != -1){var jDropIndex = $('.jSel').index(this)} else {var jDropIndex = $('.jDropBut').index(this)}        
        // Create the jDropOptions variable to be filled with the <SELECT>'s options ( Doing this "onClick" of it's parent is much tidier than processing all the select's options at script initialisation time. Originaly jDrop did everything at initilasation, which caused 10+ second load-times with older machines when using 40+ jDrop instances. It's a Tug'o'War between Initialisation and onClick but ultimately you want the page to load faster, and not worry too much about onClick as the amount of data being processed is small. However, IE6 with throw up the timer cursor briefly as the <OPTION>'s HTML is included as the images are being loaded on-the-fly. Unfortunately the custom HTML attribute of the <OPTION> is not visible to the browser's cache engine. )
        var jDropOptions='';                      
        // Populate the drop-down menu before creating the select
        $('select:eq('+jDropIndex+')').children('option').each(function(intIndex){
            jDropOptions=jDropOptions+'<div class="jOp">'+$(this).includeHTML()+'</div>';                              
        })
        
        // Insert the recently populated drop-down menu ".jOpDrop" into the ".jDrop" <DIV>
        $('.jDrop:eq('+jDropIndex+')').children('.jOpDrop').html(jDropOptions)
        // Make the drop-down's width equal to the width of it's parent. This stops the drop-down select-list falling short of the drop-down arrow; which would be bas usability.  
        $(".jDrop:eq("+jDropIndex+")").children('.jOpDrop').css({minWidth:$(".jDrop:eq("+jDropIndex+")").outerWidth()})

        // Calculate the VERTICAL dimensions of the window, mouse and drop-down                                                 
        var jDropTop = $(this).parent().offset().top-$(this).parent().offsetParent().offset().top; // I'm offsetParent in case your form is within an absolutely positioned element. 
        var dropDownTop = jDropTop + $(this).parent().height()-1;        
        var dropDownHeight=$('.jDrop:eq('+jDropIndex+') .jOpDrop').height();  
        var winHeight=$(window).height();                                      
        var docScrollTop=$(document).scrollTop();
        var dropDownYSpace=dropDownTop+dropDownHeight + $(this).parent().offsetParent().offset().top;
        
        // Calculate the HORIZONTAL dimensions of the window, mouse and drop-down
        var jDropLeft = $(this).parent().offset().left - $(this).parent().offsetParent().offset().left; // Again, offsetParent in case your form is within an absolutely positioned element, careful!
        var dropDownLeft=jDropLeft;
        var dropDownWidth=$('.jDrop:eq('+jDropIndex+') .jOpDrop').width();         
        var jDropWidth = $(this).parent().outerWidth();
        var winWidth=$(window).width();
        var docScrollLeft=$(document).scrollLeft();
        var dropDownXSpace=dropDownLeft+dropDownWidth+ $(this).parent().offsetParent().offset().left;                      
        
        // IF the drop-down will appear outside of the visible area... calculate the new VERTICAL position
        if (dropDownYSpace>winHeight+docScrollTop){dropDownTop=jDropTop-dropDownHeight-1}                        
        // IF the drop-down will appear outside of the visible area... calculate the new HORIZONTAL position
        if (dropDownXSpace>winWidth+docScrollLeft){dropDownLeft=docScrollLeft+winWidth-dropDownWidth-$(this).parent().offsetParent().offset().left-3;}                                    

        // Apply the width/height calculations...
        $('.jDrop:eq('+jDropIndex+') .jOpDrop').css({top: dropDownTop, left: dropDownLeft })   

        // Fade in the newly created drop-down in it's correct window position
        $(this).parent().children('.jOpDrop').fadeIn(50,function(){            
            // Set the fadeout of the drop-down based on the .jDrop parent
            $(this).parent().hover(function(){},function(){$(this).children('.jOpDrop').fadeOut()}),
            // IE6-Friendly hover for the <option> ( <DIV class=".jOp" > )
            $(this).children('.jOp').hover(function(){$(this).addClass('hover')}, function(){$(this).removeClass('hover')})
            // When the user clicks on one of the options...
            $(this).children('.jOp').click(function(){
                // Change the value of the original <SELECT> form element. This makes the data POST'able on submit, integrating seamlessly with your existing forms. 
                $("select[name='"+ $(this).parent().parent().attr('title')+"']").val($(this).text())
                // Update the jSelOp DIV with the HTML from the option you just clicked so the user knows the form value has been changed
                $(this).parent().parent().children('.jSel').children('.jSelOp').html($(this).html()).parent().parent().children('.jOpDrop').fadeOut()})
        })
          
    })
    // IE6-Friendly hover for the select box
    $('.jSel').hover(function(){$(this).addClass('hover')},function(){$(this).removeClass('hover')})
    // IE6-Friendly hover for the drop-down arrow
    $('.jDropBut').hover(function(){$(this).addClass('hover')},function(){$(this).removeClass('hover')})        
                  
};
$.fn.includeHTML = function () {      
      // Store the HTML atrribute's value of this element in a variable 
      var thisHTML = $(this).attr('html');
      // IF the HTML attribute for the <OPTION> has been set... 
      if (thisHTML){
          // Return the HTML and the TEXT of the <OPTION>
          return thisHTML + $(this).text() }
      else{
          // ELSE just return the TEXT as HTML will pump out "undefined"
          return $(this).html()
      }
};