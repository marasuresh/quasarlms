/* Backwards compability to make JQuery 1.2.* to work with interface 1.2*/
(function($) {
    $.extend({ dequeue: function(elem, effect) {
        $(elem).dequeue(effect);
    }
    });
})(jQuery);