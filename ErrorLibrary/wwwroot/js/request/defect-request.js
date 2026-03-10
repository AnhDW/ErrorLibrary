function getDefects() {
    return ajaxRequest({
        url: '/DefectLibrary/GetDefects',
        method: 'GET',
    })
}

function getDefectById(id) {
    return ajaxRequest({
        url: '/DefectLibrary/GetDefectById',
        method: 'GET',
        data: { id: id }
    })
}

function addDefect(defectDto) {
    return ajaxRequest({
        url: '/DefectLibrary/AddDefect',
        method: 'POST',
        data: defectDto,
        showLoading: true
    })
}

function updateDefect(defectDto) {
    return ajaxRequest({
        url: '/DefectLibrary/UpdateDefect',
        method: 'POST',
        data: defectDto,
        showLoading: true
    })
}

function deleteDefect(id) {
    return ajaxRequest({
        url: '/DefectLibrary/DeleteDefect',
        method: 'POST',
        data: id,
        showLoading: true
    })
}