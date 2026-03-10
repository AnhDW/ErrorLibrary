function createReportFinalFactory(createReportFinalFactoryDto) {
    return ajaxRequest({
        url: '/ReportFinalFactoryLibrary/CreateReportFinalFactory',
        method: 'POST',
        data: createReportFinalFactoryDto,
        showLoading: true
    })
}