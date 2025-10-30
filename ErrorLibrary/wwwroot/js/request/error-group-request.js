function getErrorGroups() {
    return ajaxRequest({
        url: '/ErrorGroupLibrary/GetErrorGroups',
        method: 'GET',
    })
}

function getErrorGroupById(id) {
    return ajaxRequest({
        url: '/ErrorGroupLibrary/GetErrorGroupById',
        method: 'GET',
        data: { id: id }
    })
}

function addErrorGroup(errorGroupDto) {
    return ajaxRequest({
        url: '/ErrorGroupLibrary/AddErrorGroup',
        method: 'POST',
        data: errorGroupDto,
        showLoading: true
    })
}

function updateErrorGroup(errorGroupDto) {
    return ajaxRequest({
        url: '/ErrorGroupLibrary/UpdateErrorGroup',
        method: 'POST',
        data: errorGroupDto,
        showLoading: true
    })
}

function deleteErrorGroup(id) {
    return ajaxRequest({
        url: '/ErrorGroupLibrary/DeleteErrorGroup',
        method: 'POST',
        data: id,
        showLoading: true
    })
}