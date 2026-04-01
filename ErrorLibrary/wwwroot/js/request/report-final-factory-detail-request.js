function reportFinalFactoryDetailExcelPreview(previewReportFinalFactoryDetailExcelDto) {
    return ajaxRequest({
        url: '/ReportFinalFactoryDetailLibrary/ReportFinalFactoryDetailExcelPreview',
        method: 'POST',
        data: previewReportFinalFactoryDetailExcelDto
    })
}

function getByReportFinalFactory(reportFinalFactoryId) {
    return ajaxRequest({
        url: '/ReportFinalFactoryDetailLibrary/GetByReportFinalFactory',
        method: 'GET',
        data: { reportFinalFactoryId: reportFinalFactoryId }
    })
}

function getByDate(date) {
    return ajaxRequest({
        url: '/ReportFinalFactoryDetailLibrary/GetByDate',
        method: 'GET',
        data: { date: date }
    })
}

function createReportFinalFactoryDetail(reportFinalFactoryDetailDto) {
    return ajaxRequest({
        url: '/ReportFinalFactoryDetailLibrary/CreateReportFinalFactoryDetail',
        method: 'POST',
        data: reportFinalFactoryDetailDto,
        showLoading: true
    })
}

function createReportFinalFactoryDetail(reportFinalFactoryDetailDto) {
    return ajaxRequest({
        url: '/ReportFinalFactoryDetailLibrary/CreateReportFinalFactoryDetail',
        method: 'POST',
        data: reportFinalFactoryDetailDto,
        showLoading: true
    })
}

function updateReportFinalFactoryDetail(reportFinalFactoryDetailDto) {
    return ajaxRequest({
        url: '/ReportFinalFactoryDetailLibrary/UpdateReportFinalFactoryDetail',
        method: 'POST',
        data: reportFinalFactoryDetailDto,
        showLoading: true
    })
}

function deleteReportFinalFactoryDetail (id) {
    return ajaxRequest({
        url: '/ReportFinalFactoryDetailLibrary/DeleteReportFinalFactoryDetail',
        method: 'POST',
        data: id,
        showLoading: true
    })
}

function importReportFinalFactoryToExcel(importErrorDto) {
    const formData = new FormData();
    formData.append("file", $("#importReportFinalFactories")[0].files[0]);
    formData.append("worksheetIndex", importErrorDto.worksheetIndex);

    return ajaxRequest({
        url: '/ReportFinalFactoryDetailLibrary/ImportReportFinalFactoryToExcel',
        method: 'POST',
        data: formData,
        isFormData: true,
        showLoading: true,
    })
}

function addReportFinalFactoryDetailsFromExcel(reportFinalFactoryDetailGridDtos) {
    return ajaxRequest({
        url: '/ReportFinalFactoryDetailLibrary/AddReportFinalFactoryDetailsFromExcel',
        method: 'POST',
        data: reportFinalFactoryDetailGridDtos,
        showLoading: true
    })
}

function exportReportFinalFactoryToExcel(factoryId) {
    return $.ajax({
        url: '/ReportFinalFactoryDetailLibrary/ExportReportFinalFactoryToExcel',
        type: 'POST',
        data: JSON.stringify(factoryId),
        contentType: 'application/json',
        xhrFields: {
            responseType: 'blob'   // 👈 quan trọng
        },
        success: function (blob, status, xhr) {
            // Tạo link download từ blob
            const url = window.URL.createObjectURL(blob);
            const a = document.createElement('a');
            a.href = url;

            // Lấy tên file từ header nếu có
            const disposition = xhr.getResponseHeader('Content-Disposition');
            let fileName = "Report.xlsx";
            if (disposition && disposition.indexOf('filename=') !== -1) {
                fileName = disposition.split('filename=')[1].trim();
            }

            a.download = fileName;
            document.body.appendChild(a);
            a.click();
            a.remove();
        },
        error: function (xhr) {
            toastr.error("Xuất Excel thất bại");
        }
    });
}

function deleteReportFinalFactoryDetailsFromExcel(reportFinalFactoryId) {
    return ajaxRequest({
        url: '/ReportFinalFactoryDetailLibrary/DeleteReportFinalFactoryDetailsFromExcel',
        method: 'POST',
        data: reportFinalFactoryId,
        showLoading: true
    })
}
