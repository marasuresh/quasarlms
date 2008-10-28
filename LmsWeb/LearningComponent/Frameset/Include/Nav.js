<!-- Copyright (c) Microsoft Corporation. All rights reserved. -->

// path for images
var VIEWER_ART_PATH = "Theme/";

// navbar images
var IMG_PREVIOUS_BUTTON_NORMAL = VIEWER_ART_PATH + "Prev.gif";
var IMG_PREVIOUS_BUTTON_HOVER = VIEWER_ART_PATH + "PrevHover.gif";
var IMG_PREVIOUS_BUTTON_PRESSED = VIEWER_ART_PATH + "PrevHover.gif";
var IMG_NEXT_BUTTON_NORMAL = VIEWER_ART_PATH + "Next.gif";
var IMG_NEXT_BUTTON_HOVER = VIEWER_ART_PATH + "NextHover.gif";
var IMG_NEXT_BUTTON_PRESSED = VIEWER_ART_PATH + "NextHover.gif";
var IMG_SAVE_BUTTON_NORMAL = VIEWER_ART_PATH + "Save.gif";
var IMG_SAVE_BUTTON_HOVER = VIEWER_ART_PATH + "SaveHover.gif";
var IMG_SAVE_BUTTON_PRESSED = VIEWER_ART_PATH + "SaveHover.gif";
var IMG_TOC_OPEN_BUTTON_NORMAL = VIEWER_ART_PATH + "TocOpen.gif";
var IMG_TOC_OPEN_BUTTON_HOVER = VIEWER_ART_PATH + "TocOpenHover.gif";
var IMG_TOC_OPEN_BUTTON_PRESSED = VIEWER_ART_PATH + "TocOpenHover.gif";
var IMG_TOC_MINIMIZE_BUTTON_NORMAL = VIEWER_ART_PATH + "TocClose.gif";
var IMG_TOC_MINIMIZE_BUTTON_HOVER = VIEWER_ART_PATH + "TocCloseHover.gif";
var IMG_TOC_MINIMIZE_BUTTON_PRESSED = VIEWER_ART_PATH + "TocCloseHover.gif";

// other images
var IMG_ONE_PIXEL = VIEWER_ART_PATH + "1px.gif";

var frameMgr;

function HandleEvent(f)
{
	return function(){
		var _e = document.all ? event : arguments[0];
		var _result = f(document.all ? _e.srcElement : _e.target, _e);
		if(!_result) {
			_e.cancelBubble = true;
			_e.returnValue = false;
		}
		return _result;
	};
}

document.onkeypress = HandleEvent(function (o, e){
	switch (o.id){
		case "imgPrevious":
		case "imgNext":
		case "imgSave":
		case "imgCloseToc":
		case "imgOpenToc":
		{
			if ((e.keyCode == 13) || (e.keyCode == 32))
				document.onclick();
			break;
		}
		default:
			return false;
	}
	return true;
});

document.onclick = HandleEvent(function(o, e) {
	switch (o.id) {
		case "imgPrevious":
			frameMgr.DoPrevious();
			break;
		case "imgNext":
			frameMgr.DoNext();
			break;
		case "imgSave":
			frameMgr.Save();
			break;
		case "imgCloseToc":
			CloseTOC();
			break;
		case "imgOpenToc":
			OpenTOC();
			break;
		default:
			return false;
	}
	return true;
});

var $pd = function(id) { return parent.document.getElementById(id); }
var $pfdc = function(id) { return parent.frames['frameNavClosed'].document.getElementById(id); }

function CloseTOC()
{
	var _pfs = $pd("framesetParentUI");
	// save the current frameset width
	parent.strFrameCols = _pfs.cols;

	// collapse the frameset
	_pfs.cols = "0px,*";

	// increase the height of our frameset to accommodate larger graphics
	$pd("framesetParentContent").rows = "21px,*";
	
	// swap NavClosed.htm images so we can allow the end-user to restore the TOC frameset
	$pfdc("TOCFrameVisibleDiv").style.display = "none";
	$pfdc("TOCFrameHiddenDiv").style.display = "inline";
}

function OpenTOC()
{
	// restore frameset to its original width
	$pd('framesetParentUI').cols = window.parent.strFrameCols;

	// Reset frameset height to its original size
	$pd('framesetParentContent').rows = "12px,*";
				
	// swap NavClosed.htm images, restoring them to their defaults
	$pfdc("TOCFrameVisibleDiv").style.display = "inline";
	$pfdc("TOCFrameHiddenDiv").style.display = "none";
}
var _setHoverIcons = function(i) {
	return function(o){
		var _map = {
			"imgPrevious":[
				IMG_PREVIOUS_BUTTON_HOVER,//"0"
				IMG_PREVIOUS_BUTTON_NORMAL,//"1"
				IMG_PREVIOUS_BUTTON_PRESSED,//"2"
				IMG_PREVIOUS_BUTTON_NORMAL],//"3"
			"imgNext":[
				IMG_NEXT_BUTTON_HOVER,
				IMG_NEXT_BUTTON_NORMAL,
				IMG_NEXT_BUTTON_PRESSED,
				IMG_NEXT_BUTTON_NORMAL],
			"imgSave":[
				IMG_SAVE_BUTTON_HOVER,
				IMG_SAVE_BUTTON_NORMAL,
				IMG_SAVE_BUTTON_PRESSED,
				IMG_SAVE_BUTTON_NORMAL],
			"imgCloseToc":[
				IMG_TOC_MINIMIZE_BUTTON_HOVER,
				IMG_TOC_MINIMIZE_BUTTON_NORMAL,
				IMG_TOC_MINIMIZE_BUTTON_PRESSED,
				IMG_TOC_MINIMIZE_BUTTON_NORMAL],
			"imgOpenToc":[
				IMG_TOC_OPEN_BUTTON_HOVER,
				IMG_TOC_OPEN_BUTTON_NORMAL,
				IMG_TOC_OPEN_BUTTON_PRESSED,
				IMG_TOC_OPEN_BUTTON_NORMAL]
		};
		if(o.id in _map) {
			document.getElementById(o.id).src = _map[o.id][i];
			return true;
		}
		return false;
	}
}
document.onmouseover = HandleEvent(_setHoverIcons(0));
document.onmouseout = HandleEvent(_setHoverIcons(1));
document.onmousedown = HandleEvent(_setHoverIcons(2));
document.onmouseup = HandleEvent(_setHoverIcons(3));

function OnLoad( frameName )
{
    frameMgr = API_GetFramesetManager()
    // Register with framemanager that loading is complete
    frameMgr.RegisterFrameLoad(frameName); 
}
