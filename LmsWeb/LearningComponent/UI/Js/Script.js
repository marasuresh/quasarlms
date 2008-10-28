function UploadPackage() {
	// display the dialog to upload a package; if a package is added
	// successfully, AddRowToTrainingGrid() will be called from the
	// the dialog to update TrainingGrid
	var args = new Object;
	args.AddRowToTrainingGrid = AddRowToTrainingGrid;
	ShowDialog("UploadPackage.aspx", args, 740, 500, false);
}

function AddRowToTrainingGrid(rowId, aCells, aClassNames) {
	// add a row to TrainingGrid; <rowId> is the HTML ID to use; <aCells> is an array
	// containing the HTML for each cell in the row; <aClassNames> is corresponding HTML
	// class names
	var row = document.createElement("tr");
	row.id = rowId;
	row.style.backgroundColor = "#FFFFE0"; // highlight new row
	for (var iCell = 0; iCell < aCells.length; iCell++) {
		var cell = document.createElement("td");
		cell.className = aClassNames[iCell];
		$(cell).html(aCells[iCell]);
		row.appendChild(cell);
	}
	var TrainingGrid = document.getElementById("TrainingGrid");
	TrainingGrid.tBodies[0].appendChild(row);

	// if TrainingGrid previously was hidden (i.e. because there's no
	// training to show), display it now, and hide NoTrainingMessage
	TrainingGrid.style.display = "inline";
	if (typeof (NoTrainingMessage) != "undefined")
		NoTrainingMessage.style.display = "none";

	// update the selection state
	OnSelectionChanged();
}

function DeletePackages() {
	// display the dialog to delete packages; if packages are deleted
	// successfully, DeleteRowsFromTrainingGrid() will be called from
	// the dialog once per deleted package, then AfterDeletingRows()
	// will be called
	var args = new Object;
	args.PackagesToDelete = ForEachSelectionCheckbox(false, false);

	// if there is nothing selected, then do nothing
	if (args.PackagesToDelete.length == 0)
		return;

	args.DeleteRowsFromTrainingGrid = DeleteRowsFromTrainingGrid;
	args.AfterDeletingRows = AfterDeletingRows;
	ShowDialog("DeletePackages.aspx", args, 450, 250, false);
}

function DeleteRowsFromTrainingGrid(rowId) {
	// delete all rows from TrainingGrid that have <rowId> is their
	// HTML ID
	while (true) {
		var arow = document.getElementsByName(rowId);
		if ((arow == null) || (arow.length == 0))
			break;
		arow[0].removeNode(true);
	}
}

function AfterDeletingRows() {
	// perform UI cleanup that should happen after deleting rows from
	// TrainingGrid...

	// update UI based on the fact that the selection changed
	OnSelectionChanged();

	// if all packages were deleted, hide TrainingGrid and show
	// NoTrainingMessage
	var TrainingGrid = document.getElementById('TrainingGrid');
	if (TrainingGrid.rows.length == 1) {
		TrainingPanel.style.display = "none";
		NoTrainingMessage.style.display = "inline";
	}
}

function OpenTraining(strOrgOrAtt) {
	// open training content; <strOrgOrAtt> is either of the form "Org:<organizationId>"
	// (for content that has not been launched yet) or "Att:<attemptId>" for content that's
	// previously been launched -- in the former case we need to create an attempt for the
	// content...
	var a;
	if ((a = strOrgOrAtt.match(/^Org:([0-9]+)$/)) != null) {
		// display the dialog to create an attempt on this organization; if the attempt is
		// successfully created, OnAttemptCreated() will be called from the the dialog to
		// update TrainingGrid and display the training
		var args = new Object;
		args.OrganizationId = a[1];
		args.OnAttemptCreated = OnAttemptCreated;
		ShowDialog("CreateAttempt.aspx", args, 450, 250, false);
	}
	else
		if ((a = strOrgOrAtt.match(/^Att:([0-9]+)$/)) != null) {
		// open training in a new window
		OpenFrameset(a[1]);
	}
}

function OnAttemptCreated(strOrganizationId, strAttemptId) {
	// called after CreateAttempt.aspx has successfully created an attempt; update the
	// anchor tag to include the attempt number, then open the frameset
	var anchor = document.getElementById("Org_" + strOrganizationId);
	anchor.href = "javascript:OpenTraining('Att:" + strAttemptId + "')";
	anchor.title = "Continue training";
	var _a = anchor.parentElement || anchor.parentNode;
	_a = _a.parentElement || _a.parentNode;
	_a.cells[3].innerHTML =
			    "<A href=\"javascript:ShowLog(" + strAttemptId + ")\" title=\"Show Log\">Active</A>";
	OpenFrameset(strAttemptId);
}

function OpenFrameset(strAttemptId) {
	// open the frameset for viewing training content; <strAttemptId> is the attempt ID
	window.open("Frameset/Frameset.aspx?View=0&AttemptId=" + strAttemptId, "_blank");
}

function ShowLog(strAttemptId) {
	// displays the sequencing log for this attempt
	ShowDialog("SequencingLog.aspx?AttemptId=" + strAttemptId, null, 900, 650, true);
}

function OnSelectionChanged() {
	// called when the list of selected checkboxes has changed
	var cSelected = ForEachSelectionCheckbox(false, false).length;
	var fAnySelected = (cSelected > 0);
	var fAllSelected = (cSelected == g_cSelectionCheckboxes);
	$('#DeletePackagesLink').attr('disabled', !fAnySelected ? 'disabled' : '');
	$('#pageForm #SelectAll').get().checked = fAllSelected;
}

function OnSelectAllClicked() {
	// called when the "Select All" checkbox is clicked
	if ($('#pageForm #SelectAll').attr('checked'))
		ForEachSelectionCheckbox(true, false);
	else
		ForEachSelectionCheckbox(false, true);
	OnSelectionChanged();
}

function ForEachSelectionCheckbox(fSelect, fDeselect) {
	// for each selection checkbox (excluding the "Select All"
	// checkbox: select it if <fSelect> is true; deselect it if
	// <fDeselect> is true; return an array of IDs of selected
	// checkboxes; side effect: set global variable
	// <g_cSelectionCheckboxes> to the number of checkboxes
	
	// IDs of selected training
	var aSelected = $.makeArray($(':checkbox[id^="Select"][id!="SelectAll"]')).map(function(input) {
		if (fSelect) {
			input.checked = true;
		}
		if (fDeselect) {
			input.checked = false;
		}
		var a = $(input).attr('id').match(/^Select([0-9]+)$/);
		return input.checked ? a[1] : null;
	});
	g_cSelectionCheckboxes = aSelected.length;
	
	return aSelected;
}

function ShowDialog(strUrl, args, cx, cy, fScroll) {
	// display a dialog box with URL <strUrl>, arguments <args>, width <cx>, height <cy>,
	// scrollbars if <fScroll>; this can be done using either showModalDialog() or
	// window.open(): the former has better modal behavior; the latter allows selection
	// within the window
	var useShowModalDialog = false;
	var strScroll = fScroll ? "yes" : "no";
	if (useShowModalDialog) {
		showModalDialog(strUrl, args,
				    "dialogWidth: " + cx + "px; dialogHeight: " + cy +
					"px; center: yes; resizable: yes; scroll: " + strScroll + ";");
	}
	else {
		dialogArguments = args; // global variable accessed by dialog
		var x = Math.max(0, (screen.width - cx) / 2);
		var y = Math.max(0, (screen.height - cy) / 2);
		window.open(strUrl, "_blank", "left=" + x + ",top=" + y +
					",width=" + cx + ",height=" + cy +
					",location=no,menubar=no,scrollbars=" + strScroll +
					",status=no,toolbar=no,resizable=yes");
	}
}