function getErrorCategorysPagination(errorCategoryParams) {
    return ajaxRequest({
        url: '/ErrorCategoryLibrary/GetErrorCategorysPagination',
        method: 'GET',
        data: $.param(errorCategoryParams, true)
    })
}

function getErrorCategories() {
    return ajaxRequest({
        url: '/ErrorCategoryLibrary/GetErrorCategories',
        method: 'GET',
    })
}

function getErrorCategoryById(id) {
    return ajaxRequest({
        url: '/ErrorCategoryLibrary/GetErrorCategoryById',
        method: 'GET',
        data: { id: id }
    })
}

function addErrorCategory(errorCategoryDto) {
    return ajaxRequest({
        url: '/ErrorCategoryLibrary/AddErrorCategory',
        method: 'POST',
        data: errorCategoryDto,
        showLoading: true
    })
}

function updateErrorCategory(errorCategoryDto) {
    return ajaxRequest({
        url: '/ErrorCategoryLibrary/UpdateErrorCategory',
        method: 'POST',
        data: errorCategoryDto,
        showLoading: true
    })
}

function deleteErrorCategory(id) {
    return ajaxRequest({
        url: '/ErrorCategoryLibrary/DeleteErrorCategory',
        method: 'POST',
        data: id,
        showLoading: true
    })
}

function addErrorCategoryByNames(names) {
    return ajaxRequest({
        url: '/ErrorCategoryLibrary/AddErrorCategoryByNames',
        method: 'POST',
        data: names,
        showLoading: true
    })
}