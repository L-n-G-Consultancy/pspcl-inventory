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
        cols += '<td><span class="required text-danger">*</span><input name="row_' + rowCounter + '_from" type="number" min="1" class="from-input" placeholder="From" required></td>';
        cols += '<td><span class="required text-danger">*</span><input name="row_' + rowCounter + '_to" type="number" min="1" class="to-input" placeholder="To" required></td>';
        cols += '<td><input name="row_' + rowCounter + '_qty" type="number" class="qty-input" placeholder="Quantity" readonly></td>';
        cols += '<td><button type="button" class="btn btn-danger remove-row"><i class="fas fa-minus"></i></button></td>';
        newRow.append(cols);
        newRow.attr('id', 'row_' + (rowCounter));
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
        $("#materialId").empty().append($('<option>').text("--Select Material Code--").val(""));;
        $("#makeId").empty();
        $("#ratingId").empty().append($('<option>').text("--Select Rating--").val(""));
        if (materialGroupId) {
            $.ajax({
                url: "/StockView/getMaterialTypes",
                type: "GET",
                data: { materialGroupId: materialGroupId },
                success: function (result) {
                    $("#materialTypeId").append($('<option> ').text("--Select Material Type--").val(""));
                    $.each(result, function (i, response) {
                        $("#materialTypeId").append($('<option>').text(response.text).val(response.value));
                    });
                }
            });
        }
        else {
            $("#materialTypeId").append($('<option>').text("--Select Material Type--").val(""));
        }
    });
});
$(function () {
    $("#materialTypeId").on("change", function () {
        var materialTypeId = $(this).val();
        $("#makeId").empty();
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
        $("#makeId").empty();
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
        $("#Division").empty();
        if (selectedSubDivId) {
            $.ajax({
                url: "/IssueStock/GetCircleAndDivisionAndLocationCode",
                type: "GET",
                data: { SelectedSubDivId: selectedSubDivId },
                success: function (result) {
                    $("#Division").val(result[0]);
                    $("#DivisionId").val(result[2]);
                    $("#Circle").val(result[1]);
                    $("#CircleId").val(result[3]);
                    $("#LocationCode").val(result[4]);
                }
            });
        }
    });
});



var alertMessage = '';

function validateInputs() {
    var isValidMsg = "";
    var listOfSerialNumber = [];
    $('.to-input').each(function () {
        var $this = $(this);
        var $row = $this.closest('tr');

        var fromVal = $row.find('.from-input').val();
        var toVal = $this.val();

        for (i = parseInt(fromVal); i <= toVal; i++) {
            listOfSerialNumber.push(i);
        }

        if (fromVal && toVal && parseInt(fromVal) > parseInt(toVal)) {
            isValidMsg = "qtynegative";
            $this.addClass('is-invalid');
        }
        else {
            $this.removeClass('is-invalid');
        }

    });

    if (listOfSerialNumber.length != Array.from(new Set(listOfSerialNumber)).length) {

        isValidMsg = "duplicatesrno";
    }
    return isValidMsg;
}


function validateSerialNumbers(listOfSerialNumber) {
    var isValid = true;

    var materialGroupId = parseInt($("#materialGroupId").val());
    var materialTypeId = parseInt($("#materialTypeId").val());
    var materialId = parseInt($("#materialId").val());
    var make = $("#Make").val();

    $.ajax({

        url: "/StockView/serverSideSerialNumberValidation",
        type: "POST",
        data: {
            listOfSerialNumber: listOfSerialNumber,
            materialGroupId: materialGroupId,
            materialTypeId: materialTypeId,
            materialId: materialId,
            make: make
        },
        success: function (result) {
            var isPresent = result;
            console.log(result);
            if (isPresent) {
                //show Modal
                isvalid = false;
            }
            else {
                //continue
                isvalid = true;
            }
        },
        error: function (xhr, status, error) {
            // Handle the error
        }
    });

    return isValid;
}
  

$(function () {
    $("#materialId").on("change", function () {
        var materialGroupId = $("#materialGroupId").val();
        var materialTypeId = $("#materialTypeId").val();
        var materialId = $(this).val();
        $("#AvailableStock").val('');
        

        if (materialId) {
            $.ajax({
                url: "/IssueStock/DisplayMakeWithQuantity",
                type: "GET",
                data: { materialGroupId: materialGroupId, materialTypeId: materialTypeId, materialId: materialId },
                success: function (response) {
                    $('#issueMaterialTableBody').empty();

                    var keys = Object.keys(response);
                    if (keys.length > 0) {
                        
                        $('#issueMaterial').show();

                        for (var i = 0; i < keys.length; i++) {
                            var key = keys[i];
                            var rowCounter = i+1;
                            var value = response[key];
                            var rowHtml = '<tr>';
                            rowHtml += '<td><input type="text" class="make-input" name="row_' + rowCounter + '_make" value="' + key + '" readonly/></td>';
                            rowHtml += '<td><input type="number" class="available-quantity-input" name="row_' + rowCounter + '_availQty" value="' + value + '" readonly/></td>';
                            rowHtml += '<td><input type="text" class="required-quantity-input" name="row_' + rowCounter + '_reqAty" id="row_' + key + '_ReqQty" /></td>';
                            rowHtml += '</tr>';
                            $('#issueMaterialTableBody').append(rowHtml);
                        }
                    } else {
                        // Hide the table if the response is empty
                        $('#issueMaterial').hide();
                    }
                }


            });
        }
    });
});   

$(document).ready(function () {
    showModal('', '');

});

function showModal(alertMessage, status) {
    var successMessage = $("#successMessage").val();
    console.log(successMessage);

    if (alertMessage) {
        $("#successMessagePlaceholder").text(alertMessage);
        $("#staticBackdropLiveLabel").text(status);
        $("#staticBackdropLive").modal("show");
    }
    if (successMessage) {
        $("#staticBackdropLiveLabel").text('Successful');
        $("#successMessagePlaceholder").text(successMessage);
        $("#staticBackdropLive").modal("show");
    }



}

$(document).on('submit', '#StockForm', function (event) {
    event.preventDefault();
    var userEnteredRate = $("#Rate").val();

    var response = validateInputs();
    if (response == "qtynegative") {
        alertMessage = 'Quantity cannot be zero or negative';
        showModal(alertMessage, 'Error..!');
    } else if (response == "duplicatesrno") {
        alertMessage = 'Duplicate serial numbers entered. Please enter unique serial numbers.';
        showModal(alertMessage, 'Error..!');
    }
    else if (userEnteredRate > 1000000) {
        alertMessage = 'Rate cannot exceed Rs 10,00,000';
        showModal(alertMessage, 'Error..!');
    }
    else if (userEnteredRate < 0) {
        $('.invalidEnteredRate').text('Please enter valid rate..!')
    }

    else {

        this.submit();
    }
});



document.getElementById("exportButton").addEventListener("click", function () {
    // Fetch the table element
    var table = document.querySelector(".table");

    // Create a new workbook
    var wb = XLSX.utils.table_to_book(table, { sheet: "Sheet 1" });

    // Convert the workbook to an Excel file (blob)
    var wbout = XLSX.write(wb, { bookType: "xlsx", type: "array" });
    var blob = new Blob([wbout], { type: "application/octet-stream" });

    // Generate a temporary download link and trigger the download
    var downloadLink = document.createElement("a");
    var url = URL.createObjectURL(blob);
    downloadLink.href = url;
    downloadLink.download = "report.xlsx";
    downloadLink.click();

    // Cleanup
    setTimeout(function () {
        URL.revokeObjectURL(url);
    }, 0);
});

function getCorrespondingMakeValue(invoiceNumber) {
    $.ajax({

        url: "/StockView/GetCorrespondingMakeValue",
        type: "GET",
        data: { invoiceNumber: invoiceNumber },
        success: function (result) {


            if (result != "Enter Make") {
                $('#Make').val(result);
                $('#Make').prop('readonly', true);
            }

            else {
                $('#Make').val('');
                $('#Make').prop('readonly', false);
            }
        },
        error: function (xhr, status, error) {
            // Handle the error
        }
    });
}

function GrnValidation(GrnNumber) {
    $.ajax({

        url: "/StockView/isGrnNumberExist",
        type: "GET",
        data: { GrnNumber: GrnNumber },
        success: function (result) {
            console.log(result);
            if (result) {
                $('#GrnNumber').text('Entered GRN already exists..!')
                $('#GRNfield').val(null);
            }
            else {
                $('#GrnNumber').text('')
            }
        },
        error: function (xhr, status, error) {
            // Handle the error
        }
    });
}


function validateDates() {
    var invoiceDate = document.getElementById("invoiceDate").value;
    var grnDate = document.getElementById("grnDate").value;

    if (invoiceDate === "" && grnDate !== "") {
        displayModal("Please enter the Invoice Date first.");
        document.getElementById("grnDate").value = "";
        return;
    }

    if (invoiceDate !== "" && grnDate !== "" && new Date(grnDate) < new Date(invoiceDate)) {
        displayModal("GRN Date must be greater than or equal to Invoice Date.");
        document.getElementById("grnDate").value = "";
    }
}

function displayModal(message) {
    var modalErrorMessage = document.getElementById("modalErrorMessage");
    modalErrorMessage.innerText = message;

    var validationModal = new bootstrap.Modal(document.getElementById("validationModal"));
    validationModal.show();
}

function ClearGrnDate() {
    document.getElementById("grnDate").value = "";
}







