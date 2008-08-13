document.attachEvent("onreadystatechange",document_readyStateChange);
var voteInitialized = false;

var lastQuestion = "";
var voteTextChanged = false;
var voteQuestion;

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
    status = "Vote administration initializing: binding service...";
    ws.useService("vote.asmx?wsdl","vote");
   
    status = "Vote administration initializing: binding events...";
    voteQuestion = document.all["voteQuestion"]; 
    voteQuestion.onkeydown = checkNewVoteText; 
    voteQuestion.onkeyup = checkNewVoteText; 
    voteQuestion.onpropertychange = checkNewVoteText;  
   
    newVoteBUTTON.onclick = newVoteBUTTON_click;
    rejectVoteBUTTON.onclick = rejectVoteBUTTON_click;
   
    status = "Vote administration initializing: requesting current statistics...";
    beginGetStatistics();
}


function lockNewQuestion()
{
    voteQuestion.readOnly = true;
    newVoteBUTTON.disabled = true;
    rejectVoteBUTTON.disabled = true;
   
    voteTextChanged = false;
    updateVoteTextChanged();    
}

function unlockNewQuestion()
{
    voteQuestion.readOnly = false;
    newVoteBUTTON.disabled = false;
    rejectVoteBUTTON.disabled = false;
   
    voteTextChanged = false;
    updateVoteTextChanged();    
}


var requestNumber = 1;
function beginGetStatistics()
{
    var statusText = " server request";
    if( status.indexOf(statusText)>=0 )
        status = status.substring(0, status.indexOf(statusText));
    status += statusText + "("+requestNumber + ")...";
    requestNumber ++; 
    
    ws.vote.callService(endGetStatistics,"ScriptGetStatistics");    
}

function endGetStatistics(result)
{
    if( result.error )
    {
        status = result.errorDetail.string; 
        setTimeout("beginGetStatistics()",300);
    }
    else
    {
        status += " processing...";
        
        var questionText;
        
        var yesUsers = new Array();
        var noUsers = new Array();
        
        questionText = result.value[0];
        
        for( var i = 1; i<result.value.length; i+=2 )
        {
            var userName = result.value[i];
            var isAnswerYesStr = result.value[i+1];
            
            if( isAnswerYesStr.toLowerCase()=="true" )
                yesUsers[yesUsers.length] = userName;
            else             
                noUsers[noUsers.length] = userName;
        }
        
        status += " updating...";
 
        if( !voteTextChanged )
        {
            if( lastQuestion!=questionText )
            {
                lastQuestion = questionText;        
                voteQuestion.value = questionText;
            }
        }
        
        var yesListText = "";
        for( var i = 0; i<yesUsers.length; i++ )
            yesListText+=yesUsers[i]+"\r\n";

        var noListText = "";
        for( var i = 0; i<noUsers.length; i++ )
            noListText+=noUsers[i]+"\r\n";
            
        yesList.innerText = yesListText;
        noList.innerText = noListText;
        
        status = "Updates loaded.";

//        lastQuestionIndex = questionIndex;
//        voteQuestion.innerText = questionText;

        setTimeout("beginGetStatistics()",1000);
    }
}

var lockCheckNewVoteText = false;
function checkNewVoteText()
{
    if( lockCheckNewVoteText )
        return; 

    var isTextChanged = ( voteQuestion.value+"" != lastQuestion+"" );
    if( isTextChanged!=voteTextChanged )
    {
        voteTextChanged = isTextChanged;
        updateVoteTextChanged(); 
    }   
}

function updateVoteTextChanged()
{
    if( voteTextChanged )
    {
        newVoteBUTTON.style.display = "block"; 
        rejectVoteBUTTON.style.display = "block"; 
    } 
    else
    {
        newVoteBUTTON.style.display = "none"; 
        rejectVoteBUTTON.style.display = "none";
        status = lastQuestion+"|"+voteQuestion.value;
    }   
}

function newVoteBUTTON_click()
{
    beginNewVote();
}

function rejectVoteBUTTON_click()
{
    lockCheckNewVoteText = true;
    
    voteQuestion.value = lastQuestion;
    voteTextChanged = false;     
   
    lockCheckNewVoteText = false;
    updateVoteTextChanged();  
}

function beginNewVote()
{
    lockNewQuestion();
    ws.vote.callService(endNewVote,"NewVote",voteQuestion.value);    
}

function endNewVote(result)
{
    if( result.error )
    {
        status = result.errorDetail.string; 
        setTimeout("beginNewVote()",300);
    }
    else
    {
        lastQuestion = voteQuestion.value; 
        unlockNewQuestion();
        setTimeout("beginGetStatistics()",300);         
    } 
}