function reportInLine(reportInLineParams) {
    return ajaxRequest({
        url: '/ReportLibrary/ReportInLine',
        method: 'POST',
        data: reportInLineParams
    })
}

function reportEndLine(reportEndLineParams) {
    return ajaxRequest({
        url: '/ReportLibrary/ReportEndLine',
        method: 'POST',
        data: reportEndLineParams
    })
}

function inLineErrorChart(reportInLineParams) {
    return ajaxRequest({
        url: '/ReportLibrary/InLineErrorChart',
        method: 'POST',
        data: reportInLineParams
    })
}

function endLineErrorChart(reportEndLineParams) {
    return ajaxRequest({
        url: '/ReportLibrary/EndLineErrorChart',
        method: 'POST',
        data: reportEndLineParams
    })
}
