$(function() {
	$(window).bind('resize', function() {
		$('#wrap').height($(window).height());

		var _uc = $('.wzm div.uc');
		_uc.height(_uc.parent().height());

		_uc = $('.wztr div.uc');
		_uc.height(_uc.parent().height());
	}).resize();
	
});
function Split() {
	var _oldWizard = $('.wzm');
	var _leftPane = $('<div id=""leftPane"" class=""sb""/>');
	var _rightPane = $('<div id=""rightPane""/>');
	$('td.sb', _oldWizard).contents().appendTo(_leftPane);
	$('td.sb', _oldWizard).next().contents().appendTo(_rightPane);
	var _newWizard = $('<div class=""wzm"" id=""splitter"">');
	_leftPane.appendTo(_newWizard);
	_rightPane.appendTo(_newWizard);
	_oldWizard.replaceWith(_newWizard);
	_newWizard.splitter({ type: 'v', initA: true });
}