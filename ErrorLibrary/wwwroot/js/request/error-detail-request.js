function getErrorDetails() {
    return ajaxRequest({
        url: '/ErrorDetailLibrary/GetErrorDetails',
        method: 'GET',
    })
}

function getErrorDetailById(lineId, productId, errorId, userId) {
    return ajaxRequest({
        url: '/ErrorDetailLibrary/GetErrorDetailById',
        method: 'GET',
        data: { lineId, productId, errorId, userId }
    })
}

function addErrorDetail(errorDetailDto) {
    return ajaxRequest({
        url: '/ErrorDetailLibrary/AddErrorDetail',
        method: 'POST',
        data: errorDetailDto,
        showLoading: true,
        useToken: true,
    })
}

function updateErrorDetail(errorDetailDto) {
    return ajaxRequest({
        url: '/ErrorDetailLibrary/UpdateErrorDetail',
        method: 'POST',
        data: errorDetailDto,
        showLoading: true,
        useToken: true,
    })
}

function deleteErrorDetail(deleteErrorDetailDto) {
    return ajaxRequest({
        url: '/ErrorDetailLibrary/DeleteErrorDetail',
        method: 'POST',
        data: deleteErrorDetailDto,
        showLoading: true
    })
}