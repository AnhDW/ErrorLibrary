function getErrorGroups() {
    return ajaxRequest({
        url: '/ErrorLibrary/GetErrorGroups',
        method: 'GET',
    })
}

function getErrors() {
    return ajaxRequest({
        url: '/ErrorLibrary/GetErrors',
        method: 'GET',
    })
}

function getErrorById(id) {
    return ajaxRequest({
        url: '/ErrorLibrary/GetErrorById',
        method: 'GET',
        data: { id: id }
    })
}

function addError(errorDto) {
    return ajaxRequest({
        url: '/ErrorLibrary/AddError',
        method: 'POST',
        data: errorDto,
        showLoading: true
    })
}

function updateError(errorDto) {
    return ajaxRequest({
        url: '/ErrorLibrary/UpdateError',
        method: 'POST',
        data: errorDto,
        showLoading: true
    })
}

function deleteError(id) {
    return ajaxRequest({
        url: '/ErrorLibrary/DeleteError',
        method: 'POST',
        data: id,
        showLoading: true
    })
}