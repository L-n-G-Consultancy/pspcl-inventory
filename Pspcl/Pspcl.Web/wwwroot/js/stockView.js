var addStock = {
	calculateQty: function () {
		var fromVal = $(this).closest('tr').find('.from-input').val();
		var toVal = $(this).closest('tr').find('.to-input').val();

		if (fromVal && toVal) {
			var qtyVal = parseInt(toVal) - parseInt(fromVal) + 1;
			$(this).closest('tr').find('.qty-input').val(qtyVal);
		}
		else {
			$(this).closest('tr').find('.qty-input').val('');
		}
	},

	addRow: function () {
		var rowCount = $("#myTableBody tr").length;
		var newRow = $('<tr>');
		var cols = '';

		cols += '<td><input type="text" class="from-input" placeholder="From"></td>';
		cols += '<td><input type="text" class="to-input" placeholder="To"></td>';
		cols += '<td><input type="text" class="qty-input" placeholder="Quantity" readonly></td>';

		if (rowCount > 1) {
			cols += '<td><button type="button" class="btn btn-primary add-row"><i class="fas fa-plus"></i></button></td>';
			cols += '<td><button type="button" class="btn btn-danger remove-row"><i class="fas fa-minus"></i></button></td>';
		}
		else {
			cols += '<td><button type="button" class="btn btn-primary add-row"><i class="fas fa-plus"></i></button></td>';
			cols += '<td><button style="display:none" type="button" class="btn btn-danger remove-row"><i class="fas fa-minus"></i></button></td>';
		}

		newRow.append(cols);
		$('tbody').append(newRow);

		if (rowCount > 0) {
			$("#myTableBody tr:last-child .remove-row").show();
		}
	},

	removeRow: function () {
		var rowCount = $("#myTableBody tr").length;
		if (rowCount > 1) {
			$(this).closest('tr').remove();
		}
	}
};

$(document).ready(() => {
	$("#addMaterialButton").click(function (event) {
		event.preventDefault();
		$("#materialTable").show();
		$(document).on('input', '.from-input, .to-input', addStock.calculateQty);
		$(document).on('click', '.add-row', addStock.addRow);
		$(document).on('click', '.remove-row', addStock.removeRow);
		$('tbody tr:first-child .remove-row').hide();
	});
});

$(function () {
    $("#materialGroupId").on("change", function () {
        var materialGroupId = $(this).val();
        $("#materialTypeId").empty();
        if (materialGroupId) {
            $.ajax({
                url: "/StockView/getMaterialTypes",
                type: "GET",
                data: { materialGroupId: materialGroupId },
                success: function (result) {
                    $("#materialTypeId").append($('<option> ').text("--Select material Type--").val(""));
                    $.each(result, function (i, response) {
                        $("#materialTypeId").append($('<option>').text(response.text).val(response.value));
                    });
                }
            });
        }
        else {
            $("#materialTypeId").append($('<option>').text("--Select material Type--").val(""));
        }
    });
});

$(function () {
    $("#materialTypeId").on("change", function () {
        var materialTypeId = $(this).val();
        $("#ratingId").empty();
        if (materialTypeId) {
            $.ajax({
                url: "/StockView/GetRating",
                type: "GET",
                data: { materialTypeId: materialTypeId },
                success: function (result) {
                    $("#ratingId").append($('<option>').text("--Select Rating--").val(""));
                    $.each(result, function (i, response) {
                        if (response.text == null) {
                            $("#ratingId").append($('<option>').text("None").val(response.Value));
                        } else {
                            $("#ratingId").append($('<option>').text(response.text).val(response.Value));
                        }
                    });
                }
            });
        }
        else {
            $("#ratingId").append($('<option>').text("--Select Rating--").val(""));
        }
    });
});

$(function () {
    $("#materialTypeId").on("change", function () {
        var materialTypeId = $(this).val();
        $("#materialId").empty();
        if (materialTypeId) {
            $.ajax({
                url: "/StockView/getMaterialCodes",
                type: "GET",
                data: { materialTypeId: materialTypeId },
                success: function (result) {
                    $("#materialId").append($('<option>').text("--Select Material Code--").val(""));
                    $.each(result, function (i, response) {
                        if (response.text == null) {
                            $("#materialId").append($('<option>').text("None").val(response.Value));
                        } else {
                            $("#materialId").append($('<option>').text(response.text).val(response.Value));
                        }
                    });
                }
            });
        }
        else {
            $("#materialId").append($('<option>').text("--Select Material Code--").val(""));
        }
    });
});
