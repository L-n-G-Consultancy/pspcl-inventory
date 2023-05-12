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
        var rowCounter = rowCount + 1;
        var newRow = $('<tr>');
        var cols = '';
        cols += '<td><input name="row_' + rowCounter + '_from" type="text" class="from-input" placeholder="From"></td>';
        cols += '<td><input name="row_' + rowCounter + '_to" type="text" class="to-input" placeholder="To"></td>';
        cols += '<td><input name="row_' + rowCounter + '_qty" type="text" class="qty-input" placeholder="Quantity" readonly></td>';
        cols += '<td><button type="button" class="btn btn-danger remove-row"><i class="fas fa-minus"></i></button></td>';
        //if (rowCount > 1) {
        //	cols += '<td><button type="button" class="btn btn-danger remove-row"><i class="fas fa-minus"></i></button></td>';
        //}
        //else {
        //	cols += '<td><button style="display:none" type="button" class="btn btn-danger remove-row"><i class="fas fa-minus"></i></button></td>';
        //}
        newRow.append(cols);
        newRow.attr('id', 'row_' + (rowCounter)); // add id attribute with an incrementing number
        $('tbody').append(newRow);
        if (rowCount > 0) {
            $("#myTableBody tr:last-child .remove-row").show();
        }
    }
    ,
    removeRow: function () {
        var rowCount = $("#myTableBody tr").length;
        if (rowCount > 1) {
            $(this).closest('tr').remove();
        }
    }
};
$(document).ready(() => {
    $(document).on('input', '.from-input, .to-input', addStock.calculateQty);
    $(document).on('click', '#addMaterialButton', addStock.addRow);
    $(document).on('click', '.remove-row', addStock.removeRow);
    $('tbody tr:first-child .remove-row').hide();
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
                            $("#ratingId").append($('<option>').text("None").val(response.value));
                        } else {
                            $("#ratingId").append($('<option>').text(response.text).val(response.value));
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
                            $("#materialId").append($('<option>').text("None").val(response.value));
                        } else {
                            $("#materialId").append($('<option>').text(response.text).val(response.value));
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

$(function () {
    $("#SelectedSubDivId").on("change", function () {
        var selectedSubDivId = $(this)[0].selectedIndex;
        if (selectedSubDivId) {
            $.ajax({
                url: "/IssueStock/GetCircleAndDivision",
                type: "GET",
                data: { SelectedSubDivId: selectedSubDivId },
                success: function (result) {
                    console.log(result);
                    $("#Division").val(result[0]); 
                    $("#Circle").val(result[1]);
                }
            });
        }
    });
});

$("#IssueStockBtn").click(function (event) {
    event.preventDefault();
    const form1data = $("#IssueStockForm1").serializeArray();
    const form2data = $("#IssueStockForm2").serializeArray();
    const combinedData = $.extend({}, form1data, form2data);
    $.ajax({
        url: "IssueStock/IssueStockView",
        type: "POST",
        data: JSON.stringify(combinedData),
        contentType: "application/json",
        success: function (response) {
            console.log(response);
        },
        error: function (xhr, status, error) {
            console.log(xhr.responseText);
        }
    });
});


$('#StockForm').on('submit', function (event) {
    event.preventDefault();

    if (validateInputs()) {
        this.submit();
    } else {
        alert('Please make sure that every "To" input is greater than its corresponding "From" input, because Quantity cannot be negative.');
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