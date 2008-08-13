var oSummaryTime=0;
var oInterval="";
startTimer();
var current=1;
var oStartTime=new Date();
function startTimer()
{
   //if (document.all.oTimer != null)
   {
   	oInterval=window.setInterval("onTimer()",1000);
   }
}
function onTimer()
{
   if (document.getElementById("oTimer") != null)
   {
      oSummaryTime=oTimer.getAttribute("timeLeft");
   	var oDate=new Date();
   	var delta = oDate - oStartTime;
   	var oTimeLeft = oSummaryTime - Math.floor(delta/1000);
   	if (oTimeLeft == 0)
   	{
   	   window.clearInterval(oInterval);
   	   oTimer.style.color="#CD2F62";
   	   ContinueForm.ContinueButton.click();
   	}

	   var iHours=Math.floor(oTimeLeft/3600);
	   if (iHours<0)
	      iHours=0;
	   var sHours = iHours + "";
	   if(sHours.length==1){
		   sHours="0" + sHours;
	   }
	   var iMinutes=Math.floor(oTimeLeft%3600/60);
	   if (iMinutes<0)
	      iMinutes=0;
	   var sMinutes = iMinutes + "";
	   if(sMinutes.length==1){
		   sMinutes="0" + sMinutes;
	   }
	   var iSeconds=oTimeLeft%60;
	   if (iSeconds<0)
	      iSeconds=0;
	   var sSeconds=iSeconds + "";
	   if(sSeconds.length==1){
		   sSeconds="0" + sSeconds;
	   }
   	oTimer.innerHTML = iHours + ":" + sMinutes + ":" + sSeconds;
   }
}
dceurl = "";
function resizeFrame(ctid)
{
   try
   {
      theHeight = document.getElementById(ctid).contentWindow.document.body.scrollHeight;
      
      if (theHeight != 0);
      {
         document.getElementById(ctid).style.height = theHeight+50;
      }
   }
   catch(e)
   {
      document.getElementById(ctid).scrolling="auto";
      document.getElementById(ctid).style.height = 400;
   }
}
function sHint(txtHint)
{
   //if (sHintButton.openState == "false")
   {
      //sHintButton.openState = "true";
      sHintButton.disabled="true";
      sHintButton.removeAttribute("href");
      sHintArea.className = "content";
      sHintArea.style.display = "";
   }
}
var lHintHTML = "";
function setLHint(lhint)
{
   lHintHTML = lhint;
}
function lHint(txtHint)
{
   //if (lHintButton.openState == "false")
   {
      //lHintButton.openState = "true";
      lHintButton.disabled="true";
      lHintButton.removeAttribute("href");
      lHintArea.className = "content";
      lHintArea.style.display = "";
   }
}
function qwHref(qId)
{
   if (qId == "")
      return;
/*   QwSwitchForm.qwId.value = qId;
   QwSwitchForm.submit();*/

   AddParameter("cset", "Questionnaire");
   AddParameter("qId", qId);
   applyParameters();
}
function setresult(res)
{
   ContinueForm.objRes.value=res;
   ContinueForm.ContinueButton.click();
}
var cRoot = "";
var cPath = "";
function setcRoot(path)
{
   cRoot = path;
}
