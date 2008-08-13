document.attachEvent("onreadystatechange",document_readyStateChange);
var voteInitialized = false;

var lastQuestionIndex = 0;

function document_readyStateChange()
{
    if( voteInitialized )
        return;
         
    if( document.readyState=="complete" )
    {
        voteInitialized = true; 
        initVote();
    }
}

function initVote()
{
    voteStatus.innerText = "Vote initializing: binding service...";
    ws.useService("vote.asmx?wsdl","vote");
   
    voteStatus.innerText = "Vote initializing: binding events...";
    voteYes.onclick = vote_click; 
    voteNo.onclick = vote_click;  
   
    voteStatus.innerText = "Vote initializing: requesting vote question...";
    beginGetQuestion();
}


function lockVote()
{
    voteYes.disabled = true;
    voteNo.disabled = true;   
}

function unlockVote()
{
    voteYes.disabled = false;
    voteNo.disabled = false;   
}


var requestNumber = 1;
function beginGetQuestion()
{
    lockVote();
   
    var statusText = " server request";
    if( voteStatus.innerText.indexOf(statusText)>=0 )
        voteStatus.innerText = voteStatus.innerText.substring(0, voteStatus.innerText.indexOf(statusText));
//    if( status.substring(status.length-"...".length)=="..." )
//        status = status.substring(0,status.length - "...".length);
    voteStatus.innerText += statusText + "("+requestNumber + ")...";
    requestNumber ++; 
    
    ws.vote.callService(endGetQuestion,"ScriptGetQuestion");
}

function endGetQuestion(result)
{
    if( result.error )
    {
        voteStatus.innerText = result.errorDetail.string; 
        setTimeout("beginGetQuestion()",300);
    }
    else
    {
        voteStatus.innerText += " processing answer...";
        var questionIndex = result.value[0];
        var questionText = result.value[1];
        
        if( questionIndex==lastQuestionIndex )
        {
            voteStatus.innerText += " no new questions...";
            setTimeout("beginGetQuestion()",3000);
            return;
        }
        
        voteYes.className = "";
        voteNo.className = "";
        
        voteStatus.innerText = "New question loaded. Please vote.";

        lastQuestionIndex = questionIndex;
        voteQuestion.innerText = questionText;
        unlockVote();
    }
}

function vote_click()
{
    var isAnswerYes;
    if( event.srcElement==voteYes )
    {
        isAnswerYes = true; 
        voteYes.className = "selected"; 
        voteNo.className = ""; 
    }
    else
    {
        isAnswerYes = false; 
        voteYes.className = ""; 
        voteNo.className = "selected"; 
    }
    
    lockVote();
 
    voteStatus.innerText = "Sending vote answer "+new String(isAnswerYes).toUpperCase()+"...";
    beginSendVote(isAnswerYes); 
}

function beginSendVote(isAnswerYes)
{
    ws.vote.callService(endSendVote,"SendVote",userNameDIV.innerText, lastQuestionIndex, isAnswerYes);
}

function endSendVote(result)
{
    if( result.error )
    {
        voteStatus.innerText = "SendVote: "+ result.errorDetail.string; 
        setTimeout("beginSendVote()",300);
    }
    else
    {
        voteStatus.innerText = "Your vote sent. Wait a minute for a new question...";
        setTimeout("beginGetQuestion();",1000);
    }
}