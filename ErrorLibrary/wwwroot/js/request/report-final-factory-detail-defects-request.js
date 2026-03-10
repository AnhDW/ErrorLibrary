function getReportFinalFactoryDetailDefects() {
    return ajaxRequest({
        url: '/ReportFinalFactoryDetailDefectLibrary/GetReportFinalFactoryDetailDefects',
        method: 'GET',
    })
}

function getReportFinalFactoryDetailDefectById(reportFinalFactoryDetailId, defectId) {
    return ajaxRequest({
        url: '/ReportFinalFactoryDetailDefectLibrary/GetReportFinalFactoryDetailDefectById',
        method: 'GET',
        data: {
            reportFinalFactoryDetailId: reportFinalFactoryDetailId,
            defectId: defectId
        }
    })
}

function addReportFinalFactoryDetailDefect(reportFinalFactoryDetailDefectDto) {
    return ajaxRequest({
        url: '/ReportFinalFactoryDetailDefectLibrary/AddReportFinalFactoryDetailDefect',
        method: 'POST',
        data: reportFinalFactoryDetailDefectDto,
        showLoading: true
    })
}

function updateReportFinalFactoryDetailDefect(reportFinalFactoryDetailDefectDto) {
    return ajaxRequest({
        url: '/ReportFinalFactoryDetailDefectLibrary/UpdateReportFinalFactoryDetailDefect',
        method: 'POST',
        data: reportFinalFactoryDetailDefectDto,
        showLoading: true
    })
}

function deleteReportFinalFactoryDetailDefect(reportFinalFactoryDetailDefectDto) {
    return ajaxRequest({
        url: '/ReportFinalFactoryDetailDefectLibrary/DeleteReportFinalFactoryDetailDefect',
        method: 'POST',
        data: reportFinalFactoryDetailDefectDto,
        showLoading: true
    })
}