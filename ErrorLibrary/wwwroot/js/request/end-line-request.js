function getEndLines() {
    return ajaxRequest({
        url: '/EndLineLibrary/GetEndLines',
        method: 'GET',
    })
}

function getEndLineById(id) {
    return ajaxRequest({
        url: '/EndLineLibrary/GetEndLineById',
        method: 'GET',
        data: { id: id }
    })
}

function checkInitAndUpdateEndLine(initAndUpdateEndLineDto) {
    return ajaxRequest({
        url: '/EndLineLibrary/CheckInitAndUpdateEndLine',
        method: 'POST',
        data: initAndUpdateEndLineDto,
        //showLoading: true
    })
}

function addEndLine(endLineDto) {
    return ajaxRequest({
        url: '/EndLineLibrary/AddEndLine',
        method: 'POST',
        data: endLineDto,
        showLoading: true
    })
}

function updateEndLine(endLineDto) {
    return ajaxRequest({
        url: '/EndLineLibrary/UpdateEndLine',
        method: 'POST',
        data: endLineDto,
        showLoading: true
    })
}

function deleteEndLine(id) {
    return ajaxRequest({
        url: '/EndLineLibrary/DeleteEndLine',
        method: 'POST',
        data: id,
        showLoading: true
    })
}