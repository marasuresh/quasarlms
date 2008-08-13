var __url = "" + document.location;
var __location ="";
var __strParams = "" + __url.replace(/^[^\?]+\?/, "");
var __arrParams = [];

if(__url != __strParams){
	__arrParams = __strParams.split("&");
	__url = __url.replace(/\?[\w\W]*$/, "");
}		

function AddParameter(name, value){
	var arrPair;
	var flag = false;
			
	for(var i=0; i<__arrParams.length; i++){
		arrPair = __arrParams[i].split("=");
		
		if(arrPair[0] == name){
			__arrParams[i] = name + "=" + value;
			flag = true;
		}
	}
	
	if(!flag){
		__arrParams[__arrParams.length] = name + "=" + value;
	}
}

function applyParameters()
		{
			var strParams = "";
			for(var i=0; i<__arrParams.length; i++)
				strParams += "&" + __arrParams[i];
			strParams = strParams.replace(/&/,"?");	
			__location = __url + strParams;
			document.location = __url + strParams;
		}

		function sortI(field, order, index)
		{
			AddParameter('field'+index, field);
			AddParameter('order'+index, order);
			AddParameter('page'+index, 1);
			applyParameters();
		}
		function setFilterI(strFilter, index)
		{
			AddParameter('flt', strFilter);
			AddParameter('page'+index, 1);
			applyParameters();
		}
		function showPageI(currentPage, index)
		{
			if(currentPage<1)return;
			AddParameter('page'+index, currentPage);
			applyParameters();
		}

		function sort(field, order)
		{
		   sortI(field,order,'');
		}
		function setFilter(strFilter)
		{
		   setFilterI(strFilter,'');
		}
		function showPage(currentPage)
		{
		   showPageI(currentPage,'');
		}
		
		function showById(Id, tableNumber, url)
		{
			url = url.replace(/%Id%/, Id);
			url = url.replace(/%tableNumber%/, tableNumber);
			window.open(url, "_self");			
		}
		
		function ShowDate(date)
		{
			AddParameter('date', date);
			applyParameters();
		}