function getInLines() {
    return ajaxRequest({
        url: '/InLineLibrary/GetInLines',
        method: 'GET',
    })
}

function getInLineById(id) {
    return ajaxRequest({
        url: '/InLineLibrary/GetInLineById',
        method: 'GET',
        data: { id: id }
    })
}

function checkInitAndUpdateInLine(initAndUpdateInLineDto) {
    return ajaxRequest({
        url: '/InLineLibrary/CheckInitAndUpdateInLine',
        method: 'POST',
        data: initAndUpdateInLineDto,
        showLoading: true
    })
}

function addInLine(inLineDto) {
    return ajaxRequest({
        url: '/InLineLibrary/AddInLine',
        method: 'POST',
        data: inLineDto,
        showLoading: true
    })
}

function updateInLine(inLineDto) {
    return ajaxRequest({
        url: '/InLineLibrary/UpdateInLine',
        method: 'POST',
        data: inLineDto,
        showLoading: true
    })
}

function deleteInLine(id) {
    return ajaxRequest({
        url: '/InLineLibrary/DeleteInLine',
        method: 'POST',
        data: id,
        showLoading: true
    })
}