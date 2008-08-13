function menuClick(page, control, itemAction, id, trId, cId){
	if (page != ""){
		__url = page;
	}
	if (trId != ""){
		AddParameter("trId", trId);
	}
	if (control != ""){
		AddParameter("cset", control);
	}
	if (id != ""){
		AddParameter("id", id);
	}
	if (cId != ""){
		AddParameter("cId", cId);
	}
	applyParameters();
}