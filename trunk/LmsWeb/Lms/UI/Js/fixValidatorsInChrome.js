//http://weblogs.asp.net/stefansedich/archive/2008/11/04/bug-with-latest-google-chrome-and-asp-net-validation.aspx
ValidatorHookupEvent = function(control, eventType, functionPrefix) {
	var ev;

	eval('ev = control.' + eventType + ';');

	if (typeof (ev) == 'function') {
		ev = ev.toString();
		ev = ev.substring(ev.indexOf('{') + 1, ev.lastIndexOf('}'));
	} else {
		ev = "";
	}
	var func;
	if (navigator.appName.toLowerCase().indexOf('explorer') > -1) {
		func = new Function(functionPrefix + ' ' + ev);
	} else {
		func = new Function('event', ' var evt = event; ' + functionPrefix + ' ' + ev);
	}
	eval('control.' + eventType + ' = func;');
}
