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

function generateErrorGroupCode() {
    return ajaxRequest({
        url: '/ErrorGroupLibrary/GenerateErrorGroupCode',
        method: 'GET',
    })
}

function generateErrorGroupCodeWhenUpdate(currentCode) {
    return ajaxRequest({
        url: '/ErrorGroupLibrary/GenerateErrorGroupCodeWhenUpdate',
        method: 'GET',
        data: { currentCode: currentCode }
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

function addErrorGroupByNames(names) {
    return ajaxRequest({
        url: '/ErrorGroupLibrary/AddErrorGroupByNames',
        method: 'POST',
        data: names,
        showLoading: true
    })
}