function getInLineDetails() {
    return ajaxRequest({
        url: '/InLineDetailLibrary/GetInLineDetails',
        method: 'GET',
    })
}

function getInLineDetailById(id) {
    return ajaxRequest({
        url: '/InLineDetailLibrary/GetInLineDetailById',
        method: 'GET',
        data: { id: id }
    })
}

function addInLineDetail(inLineDetailDetailDto) {
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