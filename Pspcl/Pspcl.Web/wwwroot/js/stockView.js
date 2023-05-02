$(document).on('input', '.from-input, .to-input', calculateQty);

$(document).on('click', '.add-row', addRow);

$(document).on('click', '.remove-row', removeRow);

$("#addMaterialButton").click(function (event) {
	event.preventDefault();
	$("#materialTable").show();
});

$('tbody tr:first-child .remove-row').hide();
function calculateQty() {
	var fromVal = $(this).closest('tr').find('.from-input').val();
	var toVal = $(this).closest('tr').find('.to-input').val();

	if (fromVal && toVal) {
		var qtyVal = toVal - fromVal;
		$(this).closest('tr').find('.qty-input').val(qtyVal);
	}
	else {
		$(this).closest('tr').find('.qty-input').val('');
	}
}

let rowCounter = 1;

function addRow() {
	var rowCount = $("#myTableBody tr").length;
	var newRow = $('<tr>');
	var cols = '';
	rowCounter++;
	cols += '<td><input name="row_' + rowCounter + '_from" type="text" class="from-input" placeholder="From"></td>';
	cols += '<td><input name="row_' + rowCounter + '_to" type="text" class="to-input" placeholder="To"></td>';
	cols += '<td><input name="row_' + rowCounter + '_qty" type="text" class="qty-input" placeholder="Quantity" readonly></td>';

	if (rowCount > 1) {
		cols += '<td><button type="button" class="btn btn-primary add-row"><i class="fas fa-plus"></i></button></td>';
		cols += '<td><button type="button" class="btn btn-danger remove-row"><i class="fas fa-minus"></i></button></td>';
	} else {
		cols += '<td><button type="button" class="btn btn-primary add-row"><i class="fas fa-plus"></i></button></td>';
		cols += '<td><button style="display:none" type="button" class="btn btn-danger remove-row"><i class="fas fa-minus"></i></button></td>';
	}

	newRow.append(cols);
	newRow.attr('id', 'row_' + (rowCounter)); // add id attribute with an incrementing number
	$('tbody').append(newRow);

	if (rowCount > 0) {
		$("#myTableBody tr:last-child .remove-row").show();
	}
}
function removeRow() {
	var rowCount = $("#myTableBody tr").length;
	if (rowCount > 1) {
		$(this).closest('tr').remove();
		rowCounter--;
	}

}


	// existing code here

	$('#StockForm').on('submit', function (event) {
		event.preventDefault();

		if (validateInputs()) {
			// submit the form
			this.submit();
		} else {
			// show an error message or handle the invalid input
			alert('Please make sure that every "To" input is greater than its corresponding "From" input.');
		}
	});





function validateInputs() {
	var isValid = true;

	$('.to-input').each(function () {
		var $this = $(this);
		var $row = $this.closest('tr');
		var fromVal = $row.find('.from-input').val();
		var toVal = $this.val();

		if (fromVal && toVal && parseFloat(fromVal) >= parseFloat(toVal)) {
			isValid = false;
			$this.addClass('is-invalid');
		} else {
			$this.removeClass('is-invalid');
		}
	});

	return isValid;
}