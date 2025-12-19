function initialInLinePage() {
    var date = getVietnamDateString();
    console.log(addDays(date, -15));
    $('#fromDate').val(addDays(date, -15));
    $('#toDate').val(date);
}

async function renderReportInLineTable() {
    var fromDate = $('#fromDate').val();
    var toDate = $('#toDate').val();
    var reportInLineParams = {
        startDate: fromDate, endDate: toDate
    }
    var reportInLines = (await reportInLine(reportInLineParams)).result;
    var html = '';
    reportInLines.forEach((item) => {
        html += `
            <tr>
                <td>${item.line.name}</td>
                <td>${item.product.code}</td>
                <td>${item.user.fullName}</td>
                <td>${item.quantity}</td>
                <td>${item.totalErrors}</td>
                <td>${item.quantity == 0 ? "" : (item.totalErrors / item.quantity * 100).toFixed(2) + "%"}</td>
            </tr>
        `
    })

    $('#reportInLineTable').html(html);
}

async function renderReportEndLineTable() {
    var fromDate = $('#fromDate').val();
    var toDate = $('#toDate').val();
    var reportEndLineParams = {
        startDate: fromDate, endDate: toDate
    }
    var reportEndLines = (await reportEndLine(reportEndLineParams)).result;
    var html = '';
    reportEndLines.forEach((item) => {
        html += `
            <tr>
                <td>${item.line.name}</td>
                <td>${item.product.code}</td>
                <td>${item.orderQuantity}</td>
                <td>${item.checkQuantity}</td>
                <td>${item.totalErrors}</td>
                <td>${item.checkQuantity == 0 ? "" : (item.totalErrors / item.checkQuantity * 100).toFixed(2) + "%"}</td>
            </tr>
        `
    })

    $('#reportEndLineTable').html(html);
}

async function topInLineErrorChart() {
    var fromDate = $('#fromDate').val();
    var toDate = $('#toDate').val();
    var rowTake = $('#quantityTopErrors').val();
    var reportInLineParams = {
        startDate: fromDate, endDate: toDate, rowTake: rowTake
    }
    var topInLineErrors = (await inLineErrorChart(reportInLineParams)).result;
    console.log(topInLineErrors);
    renderTopErrorChart(topInLineErrors.errorQuantities, topInLineErrors.errorNames);
    renderErrorParetoChart(topInLineErrors.errorQuantities, topInLineErrors.errorNames);
}

async function topEndLineErrorChart() {
    var fromDate = $('#fromDate').val();
    var toDate = $('#toDate').val();
    var rowTake = $('#quantityTopErrors').val();
    var reportEndLineParams = {
        startDate: fromDate, endDate: toDate, rowTake: rowTake
    }
    var topEndLineErrors = (await endLineErrorChart(reportEndLineParams)).result;
    console.log(topEndLineErrors);
    renderTopErrorChart(topEndLineErrors.errorQuantities, topEndLineErrors.errorNames);
    renderErrorParetoChart(topEndLineErrors.errorQuantities, topEndLineErrors.errorNames);
}

$('#fromDate, #toDate, #selectTypeCheck, #quantityTopErrors').on('change keyup', function () {
    var type = $('#selectTypeCheck').val();
    if (type === 'Inline') {
        $('#inLineType').removeClass('d-none');
        $('#endLineType').addClass('d-none');
        renderReportInLineTable();
        topInLineErrorChart();
    } else if (type === 'Endline') {
        $('#endLineType').removeClass('d-none');
        $('#inLineType').addClass('d-none');
        renderReportEndLineTable();
        topEndLineErrorChart();
    }
});

$("#toggleFormBtn").on("click", function () {
    const wrapper = document.getElementById("formWrapper");
    const icon = document.getElementById("toggleIcon");

    wrapper.classList.toggle("d-none");

    if (wrapper.classList.contains("d-none")) {
        this.innerHTML = '<i class="fas fa-angle-double-down"></i>';
    } else {
        this.innerHTML = '<i class="fas fa-angle-double-up"></i>';
    }
});