function resizeFrame(frame) {
	var _frame = $(frame);
	try {
		theHeight = frame.contentWindow.document.body.scrollHeight;
		if (theHeight != 0); 
		{
			document.getElementById(frame).style.height = theHeight + 50;
		}
	} catch (e) {
		frame.scrolling = 'auto';
		frame.style.height = 500;
	}
}