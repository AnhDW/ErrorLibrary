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

function deleteReportFinalFactoryDetailsFromExcel(reportFinalFactoryId) {
    return ajaxRequest({
        url: '/ReportFinalFactoryDetailLibrary/DeleteReportFinalFactoryDetailsFromExcel',
        method: 'POST',
        data: reportFinalFactoryId,
        showLoading: true
    })
}
