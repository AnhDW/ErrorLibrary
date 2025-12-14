function getEndLineDetails() {
    return ajaxRequest({
        url: '/EndLineDetailLibrary/GetEndLineDetails',
        method: 'GET',
    })
}

function getEndLineDetailsByEndLine(endLineId) {
    return ajaxRequest({
        url: '/EndLineDetailLibrary/GetEndLineDetailsByEndLine',
        method: 'GET',
        data: { endLineId: endLineId }
    })
}

function getEndLineDetailById(id) {
    return ajaxRequest({
        url: '/EndLineDetailLibrary/GetEndLineDetailById',
        method: 'GET',
        data: { id: id }
    })
}

function addEndLineDetail(endLineDetailDto) {
    return ajaxRequest({
        url: '/EndLineDetailLibrary/AddEndLineDetail',
        method: 'POST',
        data: endLineDetailDto,
        showLoading: true
    })
}

function updateEndLineDetail(endLineDetailDto) {
    return ajaxRequest({
        url: '/EndLineDetailLibrary/UpdateEndLineDetail',
        method: 'POST',
        data: endLineDetailDto,
        showLoading: true
    })
}

function deleteEndLineDetail(id) {
    return ajaxRequest({
        url: '/EndLineDetailLibrary/DeleteEndLineDetail',
        method: 'POST',
        data: id,
        showLoading: true
    })
}