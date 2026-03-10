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

function deleteReportFinalFactoryDetail (id) {
    return ajaxRequest({
        url: '/ReportFinalFactoryDetailLibrary/DeleteReportFinalFactoryDetail',
        method: 'POST',
        data: id,
        showLoading: true
    })
}
