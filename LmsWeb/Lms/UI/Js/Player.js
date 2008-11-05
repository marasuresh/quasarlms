$(function() {
	$(window).bind('resize', function() {
		$('#wrap').height($(window).height());

		var _uc = $('.player');
		_uc.height(_uc.parent().height());
	}).resize();

	$('#moduleTree').treeview({
		control: '#treecontrol',
		//collapsed:true,
		animated: 'fast',
		persist: 'location'
	});
/*
	$('.player .wizard').splitter({
		type:'v',
		initA: true,
		accesskey: '|'
	});

	// Firefox doesn't fire resize on page elements
	$(window).bind("resize", function() {
		$('.player .wizard').trigger("resize");
	}).trigger("resize");*/
});