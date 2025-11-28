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

function generateErrorCode(errorGroupId) {
    return ajaxRequest({
        url: '/ErrorLibrary/GenerateErrorCode',
        method: 'GET',
        data: { errorGroupId: errorGroupId }
    })
}

function generateErrorCodeWhenUpdate(errorGroupId, currentCode) {
    return ajaxRequest({
        url: '/ErrorLibrary/GenerateErrorCodeWhenUpdate',
        method: 'GET',
        data: {
            errorGroupId: errorGroupId,
            currentCode: currentCode
        }
    })
}

function addError(errorDto) {
    return ajaxRequest({
        url: '/ErrorLibrary/AddError',
        method: 'POST',
        data: errorDto,
        showLoading: true,
        useToken: true
    })
}

function updateError(errorDto) {
    return ajaxRequest({
        url: '/ErrorLibrary/UpdateError',
        method: 'POST',
        data: errorDto,
        showLoading: true,
        useToken:true
    })
}

function deleteError(id) {
    return ajaxRequest({
        url: '/ErrorLibrary/DeleteError',
        method: 'POST',
        data: id,
        showLoading: true,
        useToken: true

    })
}