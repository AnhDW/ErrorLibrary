function createReportFinalFactory(createReportFinalFactoryDto) {
    return ajaxRequest({
        url: '/ReportFinalFactoryLibrary/CreateReportFinalFactory',
        method: 'POST',
        data: createReportFinalFactoryDto,
        showLoading: true
    })
}

function checkInitReportFinalFactory(createReportFinalFactoryDto) {
    return ajaxRequest({
        url: '/ReportFinalFactoryLibrary/CheckInitReportFinalFactory',
        method: 'POST',
        data: createReportFinalFactoryDto,
        showLoading: true
    })
}
