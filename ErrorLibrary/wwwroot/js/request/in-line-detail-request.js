function getInLineDetails() {
    return ajaxRequest({
        url: '/InLineDetailLibrary/GetInLineDetails',
        method: 'GET',
    })
}

function getInLineDetailsByInLine(inLineId) {
    return ajaxRequest({
        url: '/InLineDetailLibrary/GetInLineDetailsByInLine',
        method: 'GET',
        data: { inLineId: inLineId }
    })
}

function getQuantityByInLineAndTimeFrame(inLineId, timeFrameId) {
    return ajaxRequest({
        url: '/InLineDetailLibrary/GetQuantityByInLineAndTimeFrame',
        method: 'GET',
        data: {
            inLineId: inLineId,
            timeFrameId: timeFrameId
        }
    })
}

function getInLineDetailById(id) {
    return ajaxRequest({
        url: '/InLineDetailLibrary/GetInLineDetailById',
        method: 'GET',
        data: { id: id }
    })
}

function addInLineDetail(inLineDetailDto) {
    return ajaxRequest({
        url: '/InLineDetailLibrary/AddInLineDetail',
        method: 'POST',
        data: inLineDetailDto,
        showLoading: true
    })
}

function updateInLineDetail(inLineDetailDto) {
    return ajaxRequest({
        url: '/InLineDetailLibrary/UpdateInLineDetail',
        method: 'POST',
        data: inLineDetailDto,
        showLoading: true
    })
}

function deleteInLineDetail(id) {
    return ajaxRequest({
        url: '/InLineDetailLibrary/DeleteInLineDetail',
        method: 'POST',
        data: id,
        showLoading: true
    })
}